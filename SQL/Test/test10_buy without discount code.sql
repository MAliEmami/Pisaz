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