CREATE TRIGGER GenerateUniqueReferralCode
ON Client
AFTER INSERT
AS
BEGIN
    DECLARE @ReferralCode CHAR(10);
    DECLARE @Id INT;

    WHILE 1 = 1
    BEGIN
        SET @ReferralCode = CAST(ABS(CHECKSUM(NEWID())) % 10000000000 AS CHAR(10));

        IF NOT EXISTS (SELECT 1 FROM Client WHERE ReferralCode = @ReferralCode)
            BREAK;
    END

    UPDATE Client
    SET ReferralCode = @ReferralCode
    FROM Client
    INNER JOIN inserted i ON Client.Id = i.Id;
END;

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
    FROM INSERTED I, Client C, AddedTo A, Product P, VIPClients V
    WHERE
        -- VIP Client
        C.Id = V.Id
        -- JOIN IssuedFor with Client
        AND I.Id = C.Id
        -- for SUM of Product price JOIN LockedShoppingCart with AddedTo
        AND I.Id = A.Id
        AND I.CartNumber = A.CartNumber
        AND I.LockedNumber = A.LockedNumber
        -- for price of each product
        AND A.Id = P.Id
    GROUP BY A.LockedNumber
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

CREATE FUNCTION ApplyDiscount(
  @Price INT,
  @Amount INT,
  @Limit INT
)
RETURNS INT
AS
BEGIN
  IF(limit = 0)
    RETURN @Price - @Amount;
  ELSE
  BEGIN
    
    DECLARE @DiscountPrice INT;
    SET @DiscountPrice = @price * @amount;

    IF(@DiscountPrice > @Limit)
      RETURN @Price - @Limit;
    ELSE
      RETURN @Price - @DiscountPrice;
  END
END

CREATE TRIGGER BuyProduct --this trigger needs to be tested on sql server
ON IssuedFor
AFTER INSERT
AS
BEGIN
  IF NOT EXISTS(SELECT 1
                FROM Inserted AS i
                JOIN Transaction AS t
                ON i.TrackingCode = t.TrackingCode AND t.Status = "Successfull")
    BEGIN
    RAISERROR('transaction is''nt successfull', 2, 1);
    ROLLBACK TRANSACTION;
    END
  
  DECLARE TotalPrice INT;
  
  SELECT SUM(CartPrice) INTO TotalPrice
  FROM Inserted AS i
  JOIN AddedTo AS a
  ON i.Id = a.Id AND i.CartNumber = a.CartNumber AND i.LockedNumber = a.LockedNumber;
  
  --Apply Discount Codes on TotalPrice
  SELECT ApplyDiscount(TotalPrice, amount, limit) INTO TotalPrice --Im not sure about this line
  FROM Inserted AS i, AppliedTo AS a, DiscountCode AS d
  WHERE 
    i.Id = a.Id
    AND i.CartNumber = a.CartNumber
    AND i.LockedNumber = a.LockedNumber
    
    AND a.Code = d.Code;

  IF EXISTS (SELECT 1
            FROM WalletTransaction AS w
            JOIN Inserted AS i
            ON w.TrackingCode = i.TrackingCode)
  BEGIN
    UPDATE Client
    SET WalletBalance = WalletBalance - TotalPrice
    FROM Inserted AS i
    JOIN Client AS c
    ON i.Id = c.Id;
  END
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







-- CREATE TRIGGER AddToBAXITable
-- ON IssuedFor
-- AFTER INSERT
-- AS
-- BEGIN
--     SET NOCOUNT ON;
--     INSERT INTO BAXI (Id, FirstName, LastName, PhoneNumber, Province, Remainder)
--     SELECT c.Id, c.FirstName, c.LastName, c.PhoneNumber, a.Province, a.Remainder
--     FROM inserted i JOIN Client c ON i.Id = c.Id JOIN Address a ON i.Id = a.Id;
-- END; I dont know
