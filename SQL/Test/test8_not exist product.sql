

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09234567890', 'Sara', 'Ahmadi');

-- Insert a product with StockCount = 0
-- Change the StockCount and execute code
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('RAM_Stick', 50000, 0, 'BrandX', 'ModelY');

INSERT INTO LockedShoppingCart (ID, CartNumber, LockedNumber)
VALUES (
	(SELECT ID FROM Client WHERE PhoneNumber = '09234567890'),
    1,
    1
);

INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
VALUES (
    (SELECT ID FROM Client WHERE PhoneNumber = '09234567890'),
    1,
    1,
    (SELECT ID FROM Products WHERE Brand = 'BrandX' AND Model = 'ModelY'),
    1,
    50000
);

SELECT *
FROM AddedTo
WHERE ID = (SELECT ID FROM Client WHERE PhoneNumber = '09234567890');

SELECT * FROM LockedShoppingCart WHERE ID = (SELECT ID FROM Client WHERE PhoneNumber = '09234567890');

ROLLBACK TRANSACTION