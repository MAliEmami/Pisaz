CREATE TRIGGER IncreaseProduct
ON AddedTo
AFTER INSERT
AS
BEGIN
    UPDATE Product
    SET P.StockCount = P.StockCount - 1
    FROM INSERTED I JOIN Product P ON I.Id = P.Id
END;


CREATE TRIGGER VIPDepo --i think its dont work
ON IssuedFor
AFTER INSERT
AS
BEGIN
    UPDATE Client
    SET C.WalletBalance := C.WalletBalance + (SUM(CurrentPrice) * 0.15)
    FROM INSERTED I, LockedShoppingCart L, Client C, AddedTo A, Product P, VIPClients V
    WHERE
        C.Id = V.Id
        -- JOIN IssuedFor with LockedShoppingCart
        AND I.Id = L.Id 
        AND I.CartNumber = L.CartNumber 
        AND I.LockedNumber = L.LockedNumber
        -- JOIN LockedShoppingCart with Client
        AND L.Id = C.Id
        -- for SUM of Product price JOIN LockedShoppingCart with AddedTo
        AND L.Id = A.Id 
        AND L.CartNumber = A.CartNumber 
        AND L.LockedNumber = A.LockedNumber 
        -- for price of each product
        AND A.Id = p.Id
    GROUP BY L.LockedNumber
END;

CREATE TRIGGER ChargeWallet
ON DepositsIntoWallet 
AFTER INSERT
AS
BEGIN
    UPDATE Client
    SET C.WalletBalance := C.WalletBalance + I.Amount
    FROM INSERTED I JOIN Client C ON I.Id = C.Id
END;

CREATE TRIGGER BuyVIPAccount
ON Subscribes
BEFORE INSERT
AS
BEGIN
    UPDATE Client
    SET C.WalletBalance := C.WalletBalance - 300000
    FROM (INSERTED I JOIN DepositsIntoWallet D) JOIN Client C
END;

CREATE TRIGGER BuyProduct
ON IssuedFor
BEFORE INSERT
AS
BEGIN
END;


CREATE TRIGGER ToBeVIP
ON Subscribes
AFTER INSERT
AS
BEGIN
    INSERT INTO VIPClients(Id, SubsctiptionExpirationTime)
    SELECT C.Id, DATEADD(DAY, 30, GETDATE())
    FROM INSERTED I JOIN Client C ON I.Id = C.Id
END;

-- CREATE TRIGGER RemoveExpiredVIP -its dont work
-- ON Vip
-- AFTER INSERT, UPDATE -- its should be change to period time
-- AS
-- BEGIN
--     DELETE FROM VIPClients
--     WHERE DATEDIFF(DAY, SubscriptionExpirationTime, GETDATE()) > 30;
-- END;