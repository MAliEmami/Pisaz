CREATE DATABASE pisaz;
USE pisaz;

CREATE TABLE client
(
	id								INT				PRIMARY KEY					idENTITY(1,1),
	phone_number					CHAR(10)		UNIQUE						NOT NULL,
	first_name						VARCHAR(40)									NOT NULL,
	last_name						VARCHAR(40)									NOT NULL,
	wallet_balance					INT											NOT NULL,
	join_date						DATETIME		DEFAULT CURRENT_TIMESTAMP	NOT NULL,
	referral_code					INT				UNIQUE						NOT NULL		CHECK(LEN(referral_code) = 10),
);

CREATE TABLE addresses
(
	id								INT,
	province						VARCHAR(20),
	remainder						VARCHAR(255),
	PRIMARY KEY	(id,province, remainder),
	FOREIGN KEY (id) REFERENCES client(id) 	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE vip_client
(
	id								INT				PRIMARY KEY,
	subscription_expiration_time	DATETIME		NOT NULL,
	FOREIGN KEY (id)				REFERENCES client(id) 	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE refers
(
	referrer						INT				PRIMARY KEY
	FOREIGN KEY REFERENCES client(referral_code)	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE discount_code
(
	code							INT				PRIMARY KEY		idENTITY(1,1),
	amount							INT				NOT NULL		CHECK(amount > 0),
	limit							INT				NOT NULL		CHECK(limit >= 0),		
	usage_count						INT				NOT NULL		CHECK(usage_count > 0),
	expiration_date					DATE,
);

CREATE TABLE public_code
(
	code							INT				PRIMARY KEY		
	FOREIGN	KEY	REFERENCES discount_code(code)	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE private_code
(
	code							INT				PRIMARY KEY,
	id								INT				NOT	NULL,
	create_time						DATETIME		NOT NULL,
	FOREIGN	KEY (code) REFERENCES discount_code(code)	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE transactions
(
	tracking_code					VARCHAR(20)		PRIMARY KEY,
	tstatus							TINYINT			NOT NULL		CHECK(tstatus  < 3 AND tstatus > -1), -- (0:unsuccessful, 1:successful, 2:semi_successful)
	ttime							DATETIME		NOT NULL,
);

CREATE TABLE bank_transaction
(
	tracking_code					VARCHAR(20)		PRIMARY KEY,
	card_number						INT				NOT NULL		CHECK(LEN(card_number) = 16),
	FOREIGN KEY (tracking_code)	REFERENCES transactions(tracking_code)	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE wallet_transaction
(
	tracking_code					VARCHAR(20)		PRIMARY KEY
	FOREIGN KEY REFERENCES transactions(tracking_code)	ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE subscribers
(
	tracking_code					VARCHAR(20)		PRIMARY KEY,
	id								INT				NOT NULL,
	FOREIGN KEY (tracking_code) REFERENCES transactions(tracking_code)	ON UPDATE CASCADE	 ON DELETE CASCADE,
	FOREIGN KEY (id) REFERENCES client(id)	ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE deposit_into_wallet
(
    tracking_code					VARCHAR(20)		PRIMARY KEY,
    id								INT				NOT NULL,
    amount							INT             NOT NULL		CHECK (amount > 0),
    FOREIGN KEY(tracking_code) REFERENCES bank_transaction(tracking_code)	ON UPDATE CASCADE	ON DELETE CASCADE,
    FOREIGN KEY(id) REFERENCES client(id) ON UPDATE CASCADE		ON DELETE CASCADE
);

CREATE TABLE shopping_cart
(
	id								INT,
	number							TINYINT,
	cstatus							TINYINT			NOT NULL		CHECK(cstatus > -1 AND cstatus < 3), -- (0: locked, 1: open, 2: blocked)
	PRIMARY KEY (id, number),
	FOREIGN KEY (id) REFERENCES client(id) ON UPDATE CASCADE	ON DELETE CASCADE,
);

CREATE TABLE locked_shopping_cart
(
	id								INT,
	cart_number						TINYINT,
	"timestamp"						DATETIME		NOT NULL		DEFAULT CURRENT_TIMESTAMP,
	number							INT				NOT NULL,
	PRIMARY KEY (id, cart_number, number),
	FOREIGN KEY (id, cart_number) REFERENCES shopping_cart(id, number) ON UPDATE CASCADE	ON DELETE CASCADE,
);

CREATE TABLE applied_to
(
    id                              INT             NOT NULL,
    cart_number                     TINYINT         NOT NULL,
    locked_number                   INT             NOT NULL,
    code                            INT             NOT NULL,
    "timestamp"                     DATETIME        NOT NULL,
    PRIMARY KEY(id, cart_number, locked_number, code),
    FOREIGN KEY(id, cart_number, locked_number) REFERENCES locked_shopping_cart(id, cart_number, number) ON UPDATE CASCADE	ON DELETE CASCADE,
    FOREIGN KEY(code) REFERENCES discount_code(code) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE issued_for
(
    tracking_code                   VARCHAR(20)		PRIMARY KEY	            NOT NULL,
    id                              INT                                     NOT NULL,
    cart_number                     TINYINT                                 NOT NULL,
    locked_number                   INT                                     NOT NULL,
    FOREIGN KEY(tracking_code) REFERENCES transactions(tracking_code) ON UPDATE CASCADE		ON DELETE CASCADE,
    FOREIGN KEY(id, cart_number, locked_number) REFERENCES locked_shopping_cart(id, cart_number, number) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE products
(
    id                              INT				PRIMARY KEY				idENTITY(1,1),
    category                        VARCHAR(11)     CHECK(Category IN(
														'RAM_Stick',
														'HDD',
														'Case',
														'Cooler',
														'CPU',
														'PowerSupply',
														'GPU',
														'Motherboard',
														'SSD')
													)						NOT NULL,
    pimage                          VARBINARY(MAX), -- like BLOB in sql standard
    current_price                   INT                                     NOT NULL    CHECK(current_price >= 0),
    stock_count                     INT                                     NOT NULL    CHECK(stock_count >= 0),
    brand                           VARCHAR(100)                            NOT NULL,
    model                           VARCHAR(100)                            NOT NULL,
);

CREATE TABLE added_to 
(
    id                              INT                                     NOT NULL,
    cart_number                     TINYINT                                 NOT NULL,
    locked_number                   INT                                     NOT NULL,
    product_id                      INT                                     NOT NULL,
    quantity                        SMALLINT                                NOT NULL	CHECK (quantity > 0),
    cart_price                      INT                                     NOT NULL	CHECK (cart_price >= 0),
    PRIMARY KEY (id, cart_number, locked_number, product_id),
	FOREIGN KEY(id, cart_number, locked_number) REFERENCES locked_shopping_cart(id, cart_number, number) ON UPDATE CASCADE	 ON DELETE CASCADE,
    FOREIGN KEY(product_id) REFERENCES products(id) ON UPDATE CASCADE	ON DELETE CASCADE
);

CREATE TABLE hdd
(
    id                              INT     PRIMARY KEY,
    rotational_speed                INT                                                 CHECK (rotational_speed >= 0),
    wattage                         INT                                                 CHECK (wattage > 0),
    capacity                        INT                                                 CHECK (capacity > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)                                        CHECK (width > 0),
    FOREIGN KEY(id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE "case"
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    number_of_fans                  SMALLINT											CHECK (number_of_fans >= 0),
    fan_size                        INT                                                 CHECK (fan_size >= 0),
    wattage                         INT                                                 CHECK (wattage > 0),
    ctype                           VARCHAR(50)                             NOT NULL,
    material                        VARCHAR(50)                             NOT NULL,
    color                           VARCHAR(30)                             NOT NULL,  -- No specific constraint but should not be NULL
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)                                        CHECK (width > 0),
    FOREIGN KEY(id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE power_supply
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    supported_wattage               INT													CHECK (supported_wattage > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0), 
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)                                        CHECK (width > 0),
    FOREIGN KEY(id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE gpu
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    clock_speed                     DECIMAL(6,2)                                        CHECK (clock_speed > 0),
    ram_size                        INT                                                 CHECK (ram_size > 0),
    number_of_fans                  INT                                                 CHECK (number_of_fans >= 0),
    wattage                         INT                                                 CHECK (wattage > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)                                        CHECK (width > 0) 
    FOREIGN KEY(id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE ssd
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    wattage                         INT                                     NOT NULL    CHECK (wattage > 0),
    capacity                        INT                                     NOT NULL    CHECK (capacity > 0),
    FOREIGN KEY(id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE ram_stick
(
    id                              INT    PRIMARY KEY                      NOT NULL,
    frequency                       INT                                                 CHECK (frequency > 0),
    capacity                        INT                                                 CHECK (capacity > 0),
    generation                      CHAR(4)                                             CHECK (generation IN ('DDR3', 'DDR4', 'DDR5')),
    wattage                         INT                                                 CHECK (wattage > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2) CHECK (height > 0),
    width                           DECIMAL(6,2) CHECK (width > 0),
    FOREIGN KEY (id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE motherboard
(
    id                              INT    PRIMARY KEY                      NOT NULL,
    chipset                         VARCHAR(50)                             NOT NULL,
    number_of_memory_slots          INT                                                 CHECK (number_of_memory_slots BETWEEN 1 AND 8),
    memory_speed_range              VARCHAR(20)                             NOT NULL,
    wattage                         INT                                                 CHECK (wattage > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)                                        CHECK (width > 0),
    FOREIGN KEY (id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE cpu
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    max_addressable_mem_limit       INT                                                 CHECK (max_addressable_mem_limit > 0),
    boost_frequency                 DECIMAL(6,2)                                        CHECK (boost_frequency > 0),
    base_frequency                  DECIMAL(6,2)                                        CHECK (base_frequency > 0),
    num_of_cores                    INT                                                 CHECK (num_of_cores > 0),
    num_of_threads                  INT                                                 CHECK (num_of_threads >= num_of_cores),
    microarchitecture               VARCHAR(50)                             NOT NULL,
    generation                      VARCHAR(10)                             NOT NULL,
    wattage                         DECIMAL(5,2)                                        CHECK (wattage > 0),
    FOREIGN KEY (id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE cooler
(
    id                              INT     PRIMARY KEY                     NOT NULL,
    max_rotational_speed            INT                                                 CHECK (max_rotational_speed > 0),
    fan_size                        DECIMAL(6,2)                                        CHECK (fan_size > 0),
    cooling_method                  VARCHAR(20),
    wattage                         INT                                                 CHECK (wattage > 0),
    depth                           DECIMAL(6,2)                                        CHECK (depth > 0),
    height                          DECIMAL(6,2)                                        CHECK (height > 0),
    width                           DECIMAL(6,2)				                        CHECK (width > 0),
    FOREIGN KEY (id) REFERENCES products(id) ON UPDATE CASCADE	 ON DELETE CASCADE
);

CREATE TABLE connector_compatibale_with
(
	gpu_id							INT,
	power_id						INT,
	PRIMARY KEY (gpu_id, power_id),
	FOREIGN KEY (gpu_id) REFERENCES gpu(id),
	FOREIGN KEY (power_id) REFERENCES power_supply(id),
);

CREATE TABLE sm_slot_compatibale_with
(
	motherboard_id				INT,
	ssd_id						INT,
	PRIMARY KEY (motherboard_id, ssd_id),
	FOREIGN KEY (motherboard_id) REFERENCES motherboard(id),
	FOREIGN KEY (ssd_id) REFERENCES ssd(id),
);

CREATE TABLE gm_slot_compatibale_with
(
	motherboard_id				INT,
	gpu_id						INT,
	PRIMARY KEY (motherboard_id, gpu_id),
	FOREIGN KEY (motherboard_id) REFERENCES motherboard(id),
	FOREIGN KEY (gpu_id) REFERENCES gpu(id),
);

CREATE TABLE rm_slot_compatibale_with
(
	motherboard_id				INT,
	ram_id						INT,
	PRIMARY KEY (motherboard_id, ram_id),
	FOREIGN KEY (motherboard_id) REFERENCES motherboard(id),
	FOREIGN KEY (ram_id) REFERENCES ram_stick(id),
);

CREATE TABLE mc_slot_compatibale_with
(
	motherboard_id				INT,
	cpu_id						INT,
	PRIMARY KEY (motherboard_id, cpu_id),
	FOREIGN KEY (motherboard_id) REFERENCES motherboard(id),
	FOREIGN KEY (cpu_id) REFERENCES cpu(id),
);

CREATE TABLE cc_slot_compatibale_with
(
	cooler_id				INT,
	cpu_id					INT,
	PRIMARY KEY (cooler_id, cpu_id),
	FOREIGN KEY (cooler_id) REFERENCES cooler(id),
	FOREIGN KEY (cpu_id) REFERENCES cpu(id),
);

-- TRIGGER

-- VIEW

GO

CREATE VIEW available_shopping_cart AS
	SELECT *
	FROM shopping_cart
	WHERE cstatus != 2;