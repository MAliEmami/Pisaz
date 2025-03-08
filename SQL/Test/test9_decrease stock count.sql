

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09234567890', 'Sara', 'Ahmadi');

-- Insert a product with StockCount = 5
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
VALUES ('RAM_Stick', 50000, 5, 'BrandX', 'ModelY');

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

SELECT * FROM Products WHERE ID =  (SELECT ID FROM Products WHERE Brand = 'BrandX' AND Model = 'ModelY');

ROLLBACK TRANSACTION