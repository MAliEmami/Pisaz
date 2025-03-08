USE Pisaz;
BEGIN TRANSACTION; -- Start the transaction

-- Test Case 2: Purchase with Discount
-- Create a customer
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance)
VALUES ('09123456789', 'Jane', 'Doe', 1000);
SELECT * FROM Client;

DECLARE @ID INT;
SELECT @ID = ID FROM Client WHERE PhoneNumber = '09123456789' ;
-- Add products to the shopping cart
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('CPU', 100, 10, 'Intel', 'i7-12700K'),
       ('GPU', 100, 5, 'NVIDIA', 'RTX 3080');

INSERT INTO LockedShoppingCart(ID, CartNumber, LockedNumber)
VALUES (@ID,1, 1);

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity)
VALUES (@ID, 1, 1, (SELECT ID FROM Products WHERE Model = 'i7-12700K'), 1), -- Product 1 with a price of 100,000 Tomans
       (@ID, 1, 1, (SELECT ID FROM Products WHERE Model = 'RTX 3080'), 1); -- Product 2 with a price of 100,000 Tomans

	   
-- Create two discount code
INSERT INTO DiscountCode (Code, Amount, DiscountLimit, UsageCount, ExpirationDate)
VALUES (123, 25, 1000, 1, DATEADD(DAY, 7, GETDATE()));
INSERT INTO DiscountCode (Code, Amount, DiscountLimit, UsageCount, ExpirationDate)
VALUES (1234, 5, NULL, 1, DATEADD(DAY, 7, GETDATE()));

-- Apply the discount code to the shopping cart
INSERT INTO AppliedTo (ID, CartNumber, LockedNumber, Code, ApplyTimestamp)
VALUES (@ID, 1, 1, 123, GETDATE());
WAITFOR DELAY '00:00:00:01'; -- 
INSERT INTO AppliedTo (ID, CartNumber, LockedNumber, Code, ApplyTimestamp)
VALUES (@ID, 1, 1, 1234, GETDATE());


-- Create a successful transaction
INSERT INTO Transactions (TrackingCode, TransactionStatus)
VALUES ('TR12311111111111', 'Successful');

INSERT INTO WalletTransaction (TrackingCode)
VALUES ('TR12311111111111');

INSERT INTO IssuedFor (TrackingCode, ID, CartNumber, LockedNumber)
VALUES ('TR12311111111111', @ID, 1, 1);

SELECT * FROM Client;

ROLLBACK TRANSACTION; -- Undo all changes made during the test

