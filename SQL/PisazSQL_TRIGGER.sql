CREATE TRIGGER IncreaseProduct
ON AddedTo
AFTER INSERT
AS
BEGIN
    UPDATE Product
    SET P.StockCount = P.StockCount - 1
    FROM INSERTED I JOIN Product P ON I.Id = P.Id
END;