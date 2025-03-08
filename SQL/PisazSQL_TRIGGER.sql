USE Pisaz;

GO
CREATE OR ALTER TRIGGER GenerateUniqueReferralCode
ON Client
AFTER INSERT
AS
BEGIN
    DECLARE @ReferralCode CHAR(10);

    WHILE 1 = 1
    BEGIN
        SET @ReferralCode = CAST(ABS(CHECKSUM(NEWID())) % 10000000000 AS CHAR(10));

        IF NOT EXISTS (SELECT 1 FROM Client WHERE ReferralCode = @ReferralCode)
            BREAK;
    END

    UPDATE Client
    SET ReferralCode = @ReferralCode
    FROM Client
    INNER JOIN inserted i ON Client.ID = i.ID;
END;



GO
CREATE OR ALTER TRIGGER RefersValidation
ON Refers
AFTER INSERT
AS
BEGIN
	DECLARE @ReferrerSignupDate	DATETIME;
	DECLARE @RefereSignupDate DATETIME;

	SELECT @RefereSignupDate = SignupDate
	FROM Client JOIN INSERTED ON ID = Referee;
	
	SELECT @ReferrerSignupDate = SignupDate
	FROM Client JOIN INSERTED ON ID = Referrer;

	IF(@ReferrerSignupDate > @RefereSignupDate)
	BEGIN
		RAISERROR('Referee''s signup time can''t be before the referrer''s signup time.', 15, 1);
		ROLLBACK TRANSACTION;
		RETURN;
	END
END;



GO
CREATE OR ALTER TRIGGER CreateCartForClient
ON Client
AFTER INSERT
AS
BEGIN
	INSERT INTO ShoppingCart(ID)
	SELECT ID FROM INSERTED;
END;



GO
CREATE OR ALTER TRIGGER CreateCartForVIPClient
ON VIPClient
AFTER INSERT
AS
BEGIN
	DECLARE @ID	INT;
	SELECT @ID = ID
	FROM INSERTED;

	INSERT INTO ShoppingCart(ID, CartNumber)
	VALUES (@ID, 2),
		   (@ID, 3),
		   (@ID, 4),
		   (@ID, 5);
END;



GO
CREATE OR ALTER TRIGGER ChargeWallet
ON DepositsIntoWallet 
AFTER INSERT
AS
BEGIN
    UPDATE Client
    SET WalletBalance = C.WalletBalance + I.Amount
    FROM INSERTED I JOIN Client C ON I.ID = C.ID
END;



GO
CREATE OR ALTER TRIGGER BuyVIPAccount
ON Subscribes
INSTEAD OF INSERT
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM INSERTED AS I JOIN WalletTransaction AS W ON I.TrackingCode = W.TrackingCode)
		RETURN;
    IF( (SELECT WalletBalance FROM Client AS c JOIN Inserted AS i ON c.ID = i.ID) < 300000)
    BEGIN
		RAISERROR('Wallet balance is not enough.', 15, 1);
		RETURN;
    END;

    UPDATE Client
    SET WalletBalance = C.WalletBalance - 300000
    FROM INSERTED AS I JOIN Client C ON I.ID = C.ID

    INSERT INTO VIPClient(ID, SubsctiptionExpirationTime)
    SELECT C.ID, DATEADD(DAY, 30, GETDATE())
    FROM INSERTED I JOIN Client C ON I.ID = C.ID
END;



GO
CREATE OR ALTER TRIGGER GiveDiscountsToReferrers
ON Refers
AFTER INSERT
AS
BEGIN
	
	DECLARE @ID INT;
	SELECT @ID = Referee
	FROM INSERTED;
	DECLARE @DiscountAmount INT;
	DECLARE @DiscountType VARCHAR(7);
	DECLARE @DiscountCode CHAR(10);
	DECLARE @DiscountLimit INT;
	SET @DiscountAmount = 50;
	SET @DiscountType = 'Percent';
	SET @DiscountLimit = 10000000;
	
	WHILE 1=1
	BEGIN
		--give discount code to @ID
		WHILE 1 = 1
		BEGIN
			SET @DiscountCode = CAST(ABS(CHECKSUM(NEWID())) % 10000000000 AS CHAR(10));

			IF NOT EXISTS (SELECT 1 FROM DiscountCode WHERE Code = @DiscountCode)
				BREAK;
		END

		INSERT INTO DiscountCode(Code, Amount, DiscountLimit, UsageCount, ExpirationDate)
		SELECT @DiscountCode, @DiscountAmount, @DiscountLimit, 1, DATEADD(DAY, 7, GETDATE());

		INSERT INTO PrivateCode(Code, ID)
		SELECT @DiscountCode, @ID;

		--find next @ID
		IF NOT EXISTS(SELECT 1
					  FROM Refers
					  WHERE Referee = @ID)
		BREAK;

		SELECT @ID = Referrer
		FROM Refers
		WHERE Referee = @ID;
		
		IF(@DiscountType = 'Percent')
		BEGIN
			SET @DiscountAmount = @DiscountAmount / 2;

			IF(@DiscountAmount < 1)
			BEGIN
				SET @DiscountType = 'Amount';
				SET @DiscountAmount = 500000;
				SET @DiscountLimit = NULL;
			END
		END
	END
END;


 
GO
CREATE OR ALTER TRIGGER NotExistsProduct
ON AddedTo
AFTER INSERT, UPDATE
AS
BEGIN 
    IF EXISTS (
        SELECT 1
        FROM INSERTED I JOIN Products P ON I.ProductID = P.Id
        WHERE P.StockCount < I.Quantity
    )
    BEGIN
        RAISERROR('Product not exists in warehouse.', 15, 2);
        ROLLBACK TRANSACTION;
    END
END;

 

GO
CREATE OR ALTER TRIGGER MaxUseDiscount
ON AppliedTo
AFTER INSERT, UPDATE
AS
BEGIN 
	DECLARE @UsageCount SMALLINT;
	DECLARE @MaxUsageCount SMALLINT;

	SELECT @UsageCount = COUNT(*), @MaxUsageCount = D.UsageCount
    FROM INSERTED I JOIN DiscountCode D ON I.Code = D.Code
	GROUP BY UsageCount;

    IF (@UsageCount > @MaxUsageCount)
    BEGIN
        RAISERROR('You can''t use this discount more than you could.', 15, 3);
        ROLLBACK TRANSACTION;
    END
END;



GO
CREATE OR ALTER TRIGGER ExpDiscount
ON AppliedTo
AFTER INSERT, UPDATE
AS
BEGIN 
    IF NOT EXISTS(SELECT 1
				  FROM INSERTED I JOIN DiscountCode D ON I.Code = D.Code
				  WHERE D.ExpirationDate > CURRENT_TIMESTAMP)
    BEGIN
        RAISERROR('This discount is expired.', 15, 3);
        ROLLBACK TRANSACTION;
    END
END;



GO
CREATE OR ALTER TRIGGER BuyProcess --Works only for wallet
ON IssuedFor
AFTER INSERT, UPDATE
AS
BEGIN 
    IF NOT EXISTS (SELECT 1
				   FROM INSERTED AS I 
						JOIN WalletTransaction AS W ON I.TrackingCode = W.TrackingCode
						JOIN Transactions AS T ON I.TrackingCode = T.TrackingCode
				   WHERE T.TransactionStatus = 'Successful') 
	RETURN;

	DECLARE @TotalPrice INT;
  
	SELECT @TotalPrice = dbo.CalculateFinalPrice(ID, CartNumber, LockedNumber)
	FROM INSERTED;

	SELECT
    D.Amount,
	D.DiscountLimit,
    ROW_NUMBER() OVER (ORDER BY A.ApplyTimestamp DESC) AS RANK
	INTO #Discounts
	FROM INSERTED AS I 
		 JOIN AppliedTo AS A ON I.ID = A.ID AND I.CartNumber = A.CartNumber AND I.LockedNumber = A.LockedNumber
		 JOIN DiscountCode AS D ON A.Code = D.Code;

	DECLARE @Count INT;
	DECLARE @Index INT;

	SELECT @Count = Count(*)
	FROM #Discounts;
	SET @Index = 1;

	DECLARE @Amount INT, @Limit INT;
	WHILE @Index <= @Count
	BEGIN
		SELECT @Amount = D.Amount, @Limit = D.DiscountLimit
		FROM #Discounts AS D
		WHERE D.rank = @Index;

		IF( @Limit = NULL)
		BEGIN
			SET @TotalPrice = @TotalPrice - @Amount;
		END
		ELSE
		BEGIN
			IF (@TotalPrice - @TotalPrice * @Amount > @Limit)
				SET @TotalPrice = @TotalPrice - @Limit;
			ELSE
				SET @TotalPrice = @TotalPrice * @Amount;
		END
	END

	UPDATE Client
	SET WalletBalance = C.WalletBalance - @TotalPrice
	FROM Client AS C JOIN INSERTED AS I ON C.ID = I.ID;

	--unlock shopping cart
	UPDATE ShoppingCart
	SET CartStatus = 'active'
	FROM INSERTED AS I
	JOIN ShoppingCart AS C
	ON I.ID = C.ID AND I.CartNumber = C.CartNumber;
END;

GO
CREATE OR ALTER TRIGGER ApplyCart
ON LockedShoppingCart
AFTER INSERT
AS
BEGIN
	IF NOT EXISTS(SELECT 1
				  FROM INSERTED AS I JOIN ShoppingCart AS S ON I.ID = S.ID AND I.CartNumber = S.CartNumber
				  WHERE S.CartStatus = 'active')
	BEGIN
		RAISERROR('You can''t apply a locked or blocked cart.', 20, 2);
        ROLLBACK TRANSACTION;
	END

	IF EXISTS(SELECT 1 FROM INSERTED WHERE CartNumber  > 1) AND
	   NOT EXISTS(SELECT 1 FROM VIPClient AS V JOIN INSERTED AS I ON V.ID = I.ID)
	BEGIN
		RAISERROR('You can''t apply more than one cart without VIP subscribtion.', 21, 2);
        ROLLBACK TRANSACTION;
	END
END




GO
CREATE OR ALTER TRIGGER RestoreProductFromBlockedCart
ON ShoppingCart
AFTER UPDATE
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM INSERTED WHERE CartStatus = 'blocked')
	AND NOT EXISTS(SELECT 1 FROM DELETED WHERE CartStatus = 'locked')
	RETURN;

	UPDATE LockedShoppingCart
	SET LockTimestamp = GETDATE()
	FROM INSERTED AS I JOIN LockedShoppingCart AS L ON I.ID = L.ID AND I.CartNumber = L.CartNumber;

	SELECT 
		A.ProductID, 
		A.Quantity, 
		ROW_NUMBER() OVER (ORDER BY A.Quantity DESC) AS RANK
		INTO #NewQuantities
	FROM INSERTED AS I 
				JOIN LockedShoppingCart AS L ON I.ID = L.ID AND I.CartNumber = L.CartNumber
				JOIN AddedTo AS A ON I.ID = A.ID AND I.CartNumber = A.CartNumber AND L.LockedNumber = A.LockedNumber;

	
	DECLARE @Count INT;
	DECLARE @Index INT;

	SELECT @Count = Count(*)
	FROM #NewQuantities;
	SET @Index = 1;

	DECLARE @ID INT, @NewAmount INT;
	WHILE @Index <= @Count
	BEGIN
		SELECT @ID = ProductID, @NewAmount = Quantity
		FROM #NewQuantities AS D
		WHERE rank = @Index;

		UPDATE Products
		SET StockCount = StockCount + @NewAmount
		WHERE ID = @ID;
	END
END