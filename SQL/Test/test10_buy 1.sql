USE Pisaz;
BEGIN TRANSACTION; -- Start the transaction

-- Test Case 1: Successful Purchase Using Wallet
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456789', 'John', 'Doe', 500000);
SELECT * FROM Client;

DECLARE @ID INT;
SELECT @ID = ID FROM Client WHERE PhoneNumber = '09123456789' ;
-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('CPU', 100000, 10, 'Intel', 'i7-12700K'),
       ('GPU', 100000, 5, 'NVIDIA', 'RTX 3080');

INSERT INTO LockedShoppingCart(ID, CartNumber, LockedNumber)
VALUES (@ID,1, 1);

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity)
VALUES (@ID, 1, 1, (SELECT ID FROM Products WHERE Model = 'i7-12700K'), 1), -- Product 1 with a price of 100,000 Tomans
       (@ID, 1, 1, (SELECT ID FROM Products WHERE Model = 'RTX 3080'), 1); -- Product 2 with a price of 100,000 Tomans

-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR12311111111111', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR12311111111111');

INSERT INTO IssuedFor (TrackingCode, ID, CartNumber, LockedNumber)
VALUES ('TR12311111111111', @ID, 1, 1);

SELECT * FROM Client;

ROLLBACK TRANSACTION; -- Undo all changes made during the test





-- Test Case 2: Purchase with Discount
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456780', 'Jane', 'Doe', 1000000);

-- Create a shopping cart
INSERT INTO ShoppingCart (ID, CartNumber, CartStatus)
VALUES (2, 1, 'active');

-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('SSD', 250000, 10, 'Samsung', '970 EVO'),
       ('RAM_Stick', 250000, 5, 'Corsair', 'Vengeance');

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
VALUES (2, 1, 1, 3, 1, 250000), -- Product 3 with a price of 250,000 Tomans
       (2, 1, 1, 4, 1, 250000); -- Product 4 with a price of 250,000 Tomans

-- Create a discount code
INSERT INTO DiscountCode (Code, Amount, DiscountLimit, UsageCount, ExpirationDate)
VALUES (123, 50, 100000, 1, DATEADD(DAY, 7, GETDATE()));

-- Apply the discount code to the shopping cart
INSERT INTO AppliedTo (ID, CartNumber, LockedNumber, Code, ApplyTimestamp)
VALUES (2, 1, 1, 123, GETDATE());

-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR124', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR124');

-- Test Case 3: Purchase Without Discount
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456781', 'Alice', 'Smith', 300000);

-- Create a shopping cart
INSERT INTO ShoppingCart (ID, CartNumber, CartStatus)
VALUES (3, 1, 'active');

-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('Motherboard', 100000, 10, 'ASUS', 'ROG Strix'),
       ('Cooler', 100000, 5, 'Noctua', 'NH-D15');

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
VALUES (3, 1, 1, 5, 1, 100000), -- Product 5 with a price of 100,000 Tomans
       (3, 1, 1, 6, 1, 100000); -- Product 6 with a price of 100,000 Tomans

-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR125', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR125');

-- Test Case 4: Purchase with Insufficient Wallet Balance
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456782', 'Bob', 'Johnson', 100000);

-- Create a shopping cart
INSERT INTO ShoppingCart (ID, CartNumber, CartStatus)
VALUES (4, 1, 'active');

-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('PowerSupply', 100000, 10, 'Corsair', 'RM850x'),
       ('Case', 100000, 5, 'NZXT', 'H510');

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
VALUES (4, 1, 1, 7, 1, 100000), -- Product 7 with a price of 100,000 Tomans
       (4, 1, 1, 8, 1, 100000); -- Product 8 with a price of 100,000 Tomans

-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR126', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR126');

-- Test Case 5: Purchase with Locked or Blocked Cart
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456783', 'Charlie', 'Brown', 500000);

-- Create a shopping cart with blocked status
INSERT INTO ShoppingCart (ID, CartNumber, CartStatus)
VALUES (5, 1, 'blocked');

-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('HDD', 100000, 10, 'Seagate', 'Barracuda'),
       ('SSD', 100000, 5, 'Western Digital', 'Blue');

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
VALUES (5, 1, 1, 9, 1, 100000), -- Product 9 with a price of 100,000 Tomans
       (5, 1, 1, 10, 1, 100000); -- Product 10 with a price of 100,000 Tomans

-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR127', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR127');

-- Rollback the transaction to undo all changes
ROLLBACK TRANSACTION; -- Undo all changes made during the test