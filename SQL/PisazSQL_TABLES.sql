CREATE DATABASE Pisaz
USE Pisaz;

CREATE TABLE Client
(
    Id                          INT         PRIMARY KEY     IDENTITY(1,1)   NOT NULL, -- IDENTITY(1,1) is AUTO_INCREMENT in sql server
    PhoneNumber                 CHAR(11)    UNIQUE                          NOT NULL    CHECK (PhoneNumber LIKE '09%'),
    FirstName                   VARCHAR(40)                                 NOT NULL,
    LastName                    VARCHAR(40)                                 NOT NULL,
    WalletBalance               DECIMAL     DEFAULT 0                       NOT NULL    CHECK (WalletBalance >= 0),
    ReferralCode                CHAR(10)    UNIQUE,
    SignUpDate                  DATETIME    DEFAULT CURRENT_TIMESTAMP       NOT NULL, 
);

CREATE TABLE Address
(
    Id                          INT                                         NOT NULL,
    Province                    VARCHAR(20) DEFAULT 'Theran'                NOT NULL,
    Remainder                   VARCHAR(255)                                NOT NULL,
    PRIMARY KEY(Id,Province,Remainder),
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE VIPClients
(
    Id                          INT         PRIMARY KEY                     NOT NULL,
    SubsctiptionExpirationTime  DATETIME                                    NOT NULL,
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Refers
(
    Referee                     INT         PRIMARY KEY                     NOT NULL,
    Referrer                    INT                                         NOT NULL,
    FOREIGN KEY(Referee) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Referrer) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE DiscountCode
(
    Code                        INT         PRIMARY KEY     IDENTITY(1,1)   NOT NULL,
    Amount                      INT                                         NOT NULL     CHECK(Amount > 0),
    DiscounLimit                INT                                                      CHECK(DiscounLimit > 0),-- NOT Null = percentage, Null = Value
    UsageCount                  SMALLINT    DEFAULT 1                       NOT NULL     CHECK(UsageCount >= 1),
    ExpirationDate              DATE                                        NOT NULL,            
);

CREATE TABLE PrivateCode
(
    Code                        INT         PRIMARY KEY                     NOT NULL,
    Id                          INT                                         NOT NULL,
    UsageTimestamp              DATETIME                                    NOT NULL,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE PublicCode
(
    Code                        INT         PRIMARY KEY                     NOT NULL,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Transactions
(
    TrackingCode                INT         PRIMARY KEY     IDENTITY(1,1)   NOT NULL,
    TransactionsStatus          VARCHAR(20)                                 NOT NULL    CHECK(TransactionsStatus IN ('Successful',
                                                                                                                     'SemiSuccessful',
                                                                                                                     'Unsuccessful')),-- sql server dose no have enum
    TransactionTime             DATETIME    DEFAULT CURRENT_TIMESTAMP       NOT NULL
);

CREATE TABLE BankTransaction
(
    TrackingCode                INT         PRIMARY KEY                     NOT NULL,
    CartNumber                  INT                                         NOT NULL    CHECK(LEN(CartNumber) = 16),
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE WalletTransaction
(
    TrackingCode                INT         PRIMARY KEY                     NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Subscribes
(
    TrackingCode                INT         PRIMARY KEY                     NOT NULL,
    Id                          INT                                         NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE DepositsIntoWallet
(
    TrackingCode                INT         PRIMARY KEY                     NOT NULL,
    Id                          INT                                         NOT NULL,
    Amount                      INT                                         NOT NULL    CHECK (Amount > 0),
    FOREIGN KEY(TrackingCode) REFERENCES BankTransaction(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE ShoppingCart
(
    Id                            INT                                       NOT NULL,
    CartNumber                    TINYINT                                   NOT NULL    CHECK (CartNumber IN (1, 2, 3, 4, 5)),
    CartStatus                    VARCHAR(20)                               NOT NULL    CHECK (CartStatus IN ('locked',
                                                                                                              'blocked',
                                                                                                              'active')),
    PRIMARY KEY(Id,CartNumber),
    FOREIGN KEY(Id) REFERENCES Client(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE LockedShoppingCart
(
    Id                             INT                                    NOT NULL,
    CartNumber                     TINYINT                                NOT NULL,
    LockedNumber                   INT                    IDENTITY(1,1)   NOT NULL,

    LockedShoppingCartTimestamp    DATETIME   DEFAULT CURRENT_TIMESTAMP   NOT NULL,
    PRIMARY KEY(Id, CartNumber, LockedNumber),
    FOREIGN KEY(Id, CartNumber) REFERENCES ShoppingCart(Id, CartNumber) ON UPDATE CASCADE ON DELETE CASCADE,
);

CREATE TABLE AppliedTo
(
    Id                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    Code                            INT                                     NOT NULL,
    AppliedToTimestamp              DATETIME                                NOT NULL,
    PRIMARY KEY(Id, CartNumber, LockedNumber, Code),
    FOREIGN KEY(Id, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(Id, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE IssuedFor
(
    TrackingCode                    INT     PRIMARY KEY                     NOT NULL,
    Id                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Id, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(Id, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Product
(
    Id                              INT     PRIMARY KEY     IDENTITY(1,1)   NOT NULL,
    Category                        VARCHAR(11)                             NOT NULL    CHECK(Category IN('RAM_Stick',
                                                                                                          'HDD',
                                                                                                          'Case',
                                                                                                          'Cooler',
                                                                                                          'CPU',
                                                                                                          'PowerSupply',
                                                                                                          'GPU',
                                                                                                          'Motherboard',
                                                                                                          'SSD')),
    ProductImage                    VARBINARY(MAX), -- like BLOB in sql standard
    CurrentPrice                    INT                                     NOT NULL    CHECK(CurrentPrice >= 0),
    StockCount                      INT                                     NOT NULL    CHECK(StockCount >= 0),
    Brand                           VARCHAR(100)                            NOT NULL,
    Model                           VARCHAR(100)                            NOT NULL,
);

CREATE TABLE AddedTo 
(
    Id                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    ProductId                       INT                                     NOT NULL,
    Quantity                        SMALLINT                                           CHECK (quantity > 0),
    CartPrice                       INT                                                CHECK (CartPrice >= 0),
    PRIMARY KEY (Id, CartNumber, LockedNumber, ProductId),
    FOREIGN KEY(Id, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(Id, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(ProductId) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE HDD
(
    Id                              INT     PRIMARY KEY,
    RotationalSpeed                 INT                                                 CHECK (RotationalSpeed >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Capacity                        INT                                                 CHECK (Capacity > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Case
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    NumberOfFans                    SMALLINT                                            CHECK (NumberOfFans >= 0),
    FanSize                         INT                                                 CHECK (FanSize >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    CaseType                        VARCHAR(50)                             NOT NULL,
    Material                        VARCHAR(50)                             NOT NULL,
    Color                           VARCHAR(30)                             NOT NULL,  -- No specific constraint but should not be NULL
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE PowerSupply
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    SupportedWattage                INT                                                 CHECK (SupportedWattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0), 
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE GPU
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    ClockSpeed                      DECIMAL(6,2)                                        CHECK (ClockSpeed > 0),
    RamSize                         INT                                                 CHECK (RamSize > 0),
    NumberOfFans                    INT                                                 CHECK (NumberOfFans >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0) 
    FOREIGN KEY(Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE SSD
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    Wattage                         INT                                     NOT NULL    CHECK (Wattage > 0),
    Capacity                        INT                                     NOT NULL    CHECK (Capacity > 0),
    FOREIGN KEY(Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE RAM_Stick
(
    Id                              INT    PRIMARY KEY                      NOT NULL,
    Frequency                       INT                                                 CHECK (Frequency > 0),
    Capacity                        INT                                                 CHECK (Capacity > 0),
    Generation                      CHAR(4)                                             CHECK (Generation IN ('DDR3', 'DDR4', 'DDR5')),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2) CHECK (Height > 0),
    Width                           DECIMAL(6,2) CHECK (Width > 0),
    FOREIGN KEY (Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Motherboard
(
    Id                              INT    PRIMARY KEY                      NOT NULL,
    Chipset                         VARCHAR(50)                             NOT NULL,
    NumberOfMemorySlots             INT                                                 CHECK (NumberOfMemorySlots BETWEEN 1 AND 8),
    MemorySpeedRange                VARCHAR(20)                             NOT NULL,
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY (Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE CPU
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    MaxAddressableMemLimit          INT                                                 CHECK (MaxAddressableMemLimit > 0),
    BoostFrequency                  DECIMAL(6,2)                                        CHECK (BoostFrequency > 0),
    BaseFrequency                   DECIMAL(6,2)                                        CHECK (BaseFrequency > 0),
    NumOfCores                      INT                                                 CHECK (NumOfCores > 0),
    NumOfThreads                    INT                                                 CHECK (NumOfThreads >= NumOfCores),
    Microarchitecture               VARCHAR(50)                             NOT NULL,
    Generation                      VARCHAR(10)                             NOT NULL,
    Wattage                         DECIMAL(5,2)                                        CHECK (Wattage > 0),
    FOREIGN KEY (Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Cooler
(
    Id                              INT     PRIMARY KEY                     NOT NULL,
    MaxRotationalSpeed              INT                                                 CHECK (MaxRotationalSpeed > 0),
    FanSize                         DECIMAL(6,2)                                        CHECK (FanSize > 0),
    CoolingMethod                   VARCHAR(20),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY (Id) REFERENCES Product(Id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE ConnectorCompatibaLeWith
(
	GPUId	    				INT,
	PowerId						INT,
	PRIMARY KEY (GPUId, PowerId),
	FOREIGN KEY (GPUId) REFERENCES GPU(Id),
	FOREIGN KEY (PowerId) REFERENCES PowerSupply(Id),
);

CREATE TABLE SmSlotCompatibaleWith
(
	MotherboardId				INT,
	SSDId						INT,
	PRIMARY KEY (MotherboardId, SSDId),
	FOREIGN KEY (MotherboardId) REFERENCES Motherboard(Id),
	FOREIGN KEY (SSDId) REFERENCES SSD(Id),
);

CREATE TABLE GmSlotCompatibaleWith
(
	MotherboardId				INT,
	GPUId						INT,
	PRIMARY KEY (MotherboardId, GPUId),
	FOREIGN KEY (MotherboardId) REFERENCES Motherboard(Id),
	FOREIGN KEY (GPUId) REFERENCES GPU(Id),
);

CREATE TABLE RmSlotCompatibaleWith
(
	MotherboardId				INT,
	RAMID						INT,
	PRIMARY KEY (MotherboardId, RAMID),
	FOREIGN KEY (MotherboardId) REFERENCES Motherboard(Id),
	FOREIGN KEY (RAMID) REFERENCES RAM_Stick(Id),
);

CREATE TABLE McSlotCompatibaleWith
(
	MotherboardId				INT,
	CPUID						INT,
	PRIMARY KEY (MotherboardId, CPUID),
	FOREIGN KEY (MotherboardId) REFERENCES Motherboard(Id),
	FOREIGN KEY (CPUID) REFERENCES CPU(Id),
);

CREATE TABLE CcSlotCompatibaleWith
(
	CoolerId				INT,
	CPUID					INT,
	PRIMARY KEY (CoolerId, CPUID),
	FOREIGN KEY (CoolerId) REFERENCES cooler(Id),
	FOREIGN KEY (CPUID) REFERENCES CPU(Id),
);


-- BAXI --> هزینه ارسال به سبد خرید قفل شده
    -- Province
    -- Remainder
    -- PhoneNumber
    -- FirstName
    -- LastName