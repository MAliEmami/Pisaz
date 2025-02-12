CREATE DATABASE Pisaz;
USE Pisaz;

CREATE TABLE Client
(
    ID                          INT         PRIMARY KEY     IDENTITY(1,1)   NOT NULL, -- IDENTITY(1,1) is AUTO_INCREMENT in sql server
    PhoneNumber                 CHAR(11)    UNIQUE                          NOT NULL    CHECK (PhoneNumber LIKE '09%'),
    FirstName                   VARCHAR(40)                                 NOT NULL,
    LastName                    VARCHAR(40)                                 NOT NULL,
    WalletBalance               DECIMAL     DEFAULT 0                       NOT NULL    CHECK (WalletBalance >= 0),
    ReferralCode                CHAR(10)    UNIQUE,
    SignUpDate                  DATETIME    DEFAULT CURRENT_TIMESTAMP       NOT NULL, 
);

CREATE TABLE Addresses
(
    ID                          INT                                         NOT NULL,
    Province                    VARCHAR(20)                                 NOT NULL,
    Remainder                   VARCHAR(255)                                NOT NULL,
    PRIMARY KEY(ID,Province,Remainder),
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE TABLE VIPClient
(
    ID                          INT         PRIMARY KEY                     NOT NULL,
    SubsctiptionExpirationTime  DATETIME                                    NOT NULL,
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE TABLE Refers
(
    Referee                     INT         PRIMARY KEY                     NOT NULL,
    Referrer                    INT                                         NOT NULL,
	CHECK(Referee != Referrer),
    FOREIGN KEY(Referee) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY(Referrer) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE DiscountCode
(
    Code                        INT         PRIMARY KEY					    NOT NULL,
    Amount                      INT                                         NOT NULL     CHECK(Amount > 0),
    DiscounLimit                INT                                                      CHECK(DiscounLimit > 0),-- NOT Null = percentage, Null = Value
    UsageCount                  SMALLINT    DEFAULT 1                       NOT NULL     CHECK(UsageCount >= 1),
    ExpirationDate              DATE                                        NOT NULL,            
);

CREATE TABLE PrivateCode
(
    Code                        INT         PRIMARY KEY                     NOT NULL,
    ID                          INT                                         NOT NULL,
    UsageTimestamp              DATETIME                                    NOT NULL,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE PublicCode
(
    Code                        INT         PRIMARY KEY                     NOT NULL,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Transactions
(
    TrackingCode                VARCHAR(20)         PRIMARY KEY             NOT NULL,
    TransactionStatus           VARCHAR(20)                                 NOT NULL    CHECK(TransactionStatus IN ('Successful',
                                                                                                                     'SemiSuccessful',
                                                                                                                     'Unsuccessful')),-- sql server dose no have enum
    TransactionTime             DATETIME    DEFAULT CURRENT_TIMESTAMP       NOT NULL
);

CREATE TABLE BankTransaction
(
    TrackingCode                VARCHAR(20)         PRIMARY KEY             NOT NULL,
    CardNumber                  INT                                         NOT NULL    CHECK(LEN(CardNumber) = 16),
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE WalletTransaction
(
    TrackingCode                VARCHAR(20)         PRIMARY KEY             NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE Subscribes
(
    TrackingCode                VARCHAR(20)         PRIMARY KEY             NOT NULL,
    ID                          INT                                         NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE DepositsIntoWallet
(
    TrackingCode                VARCHAR(20)         PRIMARY KEY             NOT NULL,
    ID                          INT                                         NOT NULL,
    Amount                      INT                                         NOT NULL    CHECK (Amount > 0),
    FOREIGN KEY(TrackingCode) REFERENCES BankTransaction(TrackingCode) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE ShoppingCart
(
    ID                            INT                                       NOT NULL,
    CartNumber                    TINYINT                                   NOT NULL    CHECK (CartNumber IN (1, 2, 3, 4, 5)),
    CartStatus                    VARCHAR(7)                                NOT NULL    CHECK (CartStatus IN ('locked',
                                                                                                              'blocked',
                                                                                                              'active')),
    PRIMARY KEY(ID,CartNumber),
    FOREIGN KEY(ID) REFERENCES Client(ID) ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE LockedShoppingCart
(
    ID                             INT                                    NOT NULL,
    CartNumber                     TINYINT                                NOT NULL,
    LockedNumber                   INT                                    NOT NULL,

    LockTimestamp    DATETIME   DEFAULT CURRENT_TIMESTAMP,			-- NULL : active cart , NOT NULL : locked cart
    PRIMARY KEY(ID, CartNumber, LockedNumber),
    FOREIGN KEY(ID, CartNumber) REFERENCES ShoppingCart(ID, CartNumber) ON UPDATE CASCADE ON DELETE NO ACTION,
);

CREATE TABLE AppliedTo
(
    ID                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    Code                            INT                                     NOT NULL,
    ApplyTimestamp					DATETIME                                NOT NULL,
    PRIMARY KEY(ID, CartNumber, LockedNumber, Code),
    FOREIGN KEY(ID, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(ID, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(Code) REFERENCES DiscountCode(Code) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE IssuedFor
(
    TrackingCode                    VARCHAR(20)     PRIMARY KEY             NOT NULL,
    ID                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    FOREIGN KEY(TrackingCode) REFERENCES Transactions(TrackingCode) ON UPDATE CASCADE ON DELETE NO ACTION,
    FOREIGN KEY(ID, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(ID, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE NO ACTION
);

CREATE TABLE Products
(
    ID                              INT     PRIMARY KEY     IDENTITY(1,1)   NOT NULL,
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
    ID                              INT                                     NOT NULL,
    CartNumber                      TINYINT                                 NOT NULL,
    LockedNumber                    INT                                     NOT NULL,
    ProductID                       INT                                     NOT NULL,
    Quantity                        SMALLINT                                           CHECK (quantity > 0),
    CartPrice                       INT                                                CHECK (CartPrice >= 0),
    PRIMARY KEY (ID, CartNumber, LockedNumber, ProductID),
    FOREIGN KEY(ID, CartNumber, LockedNumber) REFERENCES LockedShoppingCart(ID, CartNumber, LockedNumber) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY(ProductID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE NO ACTION
);


CREATE TABLE HDD
(
    ID                              INT     PRIMARY KEY,
    RotationalSpeed                 INT                                                 CHECK (RotationalSpeed >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Capacity                        INT                                                 CHECK (Capacity > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE "Case"
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    NumberOfFans                    SMALLINT                                            CHECK (NumberOfFans >= 0),
    FanSize                         INT                                                 CHECK (FanSize >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    CaseType                        VARCHAR(50)                             NOT NULL,
    Material                        VARCHAR(50)                             NOT NULL,
    Color                           VARCHAR(30)                             NOT NULL,  -- No specific constraint but should not be NULL
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE PowerSupply
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    SupportedWattage                INT                                                 CHECK (SupportedWattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0), 
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY(ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE GPU
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    ClockSpeed                      DECIMAL(6,2)                                        CHECK (ClockSpeed > 0),
    RamSize                         INT                                                 CHECK (RamSize > 0),
    NumberOfFans                    INT                                                 CHECK (NumberOfFans >= 0),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0) 
    FOREIGN KEY(ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE SSD
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    Wattage                         INT                                     NOT NULL    CHECK (Wattage > 0),
    Capacity                        INT                                     NOT NULL    CHECK (Capacity > 0),
    FOREIGN KEY(ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE RAM_Stick
(
    ID                              INT    PRIMARY KEY                      NOT NULL,
    Frequency                       INT                                                 CHECK (Frequency > 0),
    Capacity                        INT                                                 CHECK (Capacity > 0),
    Generation                      CHAR(4)                                             CHECK (Generation IN ('DDR3', 'DDR4', 'DDR5')),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2) CHECK (Height > 0),
    Width                           DECIMAL(6,2) CHECK (Width > 0),
    FOREIGN KEY (ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Motherboard
(
    ID                              INT    PRIMARY KEY                      NOT NULL,
    Chipset                         VARCHAR(50)                             NOT NULL,
    NumberOfMemorySlots             INT                                                 CHECK (NumberOfMemorySlots BETWEEN 1 AND 8),
    MemorySpeedRange                VARCHAR(20)                             NOT NULL,
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY (ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE CPU
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    MaxAddressableMemLimit          INT                                                 CHECK (MaxAddressableMemLimit > 0),
    BoostFrequency                  DECIMAL(6,2)                                        CHECK (BoostFrequency > 0),
    BaseFrequency                   DECIMAL(6,2)                                        CHECK (BaseFrequency > 0),
    NumOfCores                      INT                                                 CHECK (NumOfCores > 0),
    NumOfThreads                    INT,
    Microarchitecture               VARCHAR(50)                             NOT NULL,
    Generation                      VARCHAR(10)                             NOT NULL,
    Wattage                         DECIMAL(5,2)                                        CHECK (Wattage > 0),
	CHECK (NumOfThreads >= NumOfCores),
    FOREIGN KEY (ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Cooler
(
    ID                              INT     PRIMARY KEY                     NOT NULL,
    MaxRotationalSpeed              INT                                                 CHECK (MaxRotationalSpeed > 0),
    FanSize                         DECIMAL(6,2)                                        CHECK (FanSize > 0),
    CoolingMethod                   VARCHAR(20),
    Wattage                         INT                                                 CHECK (Wattage > 0),
    Depth                           DECIMAL(6,2)                                        CHECK (Depth > 0),
    Height                          DECIMAL(6,2)                                        CHECK (Height > 0),
    Width                           DECIMAL(6,2)                                        CHECK (Width > 0),
    FOREIGN KEY (ID) REFERENCES Products(ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE ConnectorCompatibleWith
(
	GPUID	    				INT,
	PowerID						INT,
	PRIMARY KEY (GPUID, PowerID),
	FOREIGN KEY (GPUID) REFERENCES GPU(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (PowerID) REFERENCES PowerSupply(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE SmSlotCompatibleWith
(
	MotherboardID				INT,
	SSDID						INT,
	PRIMARY KEY (MotherboardID, SSDID),
	FOREIGN KEY (MotherboardID) REFERENCES Motherboard(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (SSDID) REFERENCES SSD(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE GmSlotCompatibleWith
(
	MotherboardID				INT,
	GPUID						INT,
	PRIMARY KEY (MotherboardID, GPUID),
	FOREIGN KEY (MotherboardID) REFERENCES Motherboard(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (GPUID) REFERENCES GPU(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE RmSlotCompatibleWith
(
	MotherboardID				INT,
	RAMID						INT,
	PRIMARY KEY (MotherboardID, RAMID),
	FOREIGN KEY (MotherboardID) REFERENCES Motherboard(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (RAMID) REFERENCES RAM_Stick(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE McSlotCompatibleWith
(
	MotherboardID				INT,
	CPUID						INT,
	PRIMARY KEY (MotherboardID, CPUID),
	FOREIGN KEY (MotherboardID) REFERENCES Motherboard(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (CPUID) REFERENCES CPU(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);

CREATE TABLE CcSlotCompatibleWith
(
	CoolerID				INT,
	CPUID					INT,
	PRIMARY KEY (CoolerID, CPUID),
	FOREIGN KEY (CoolerID) REFERENCES Cooler(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY (CPUID) REFERENCES CPU(ID) ON UPDATE NO ACTION ON DELETE NO ACTION,
);
