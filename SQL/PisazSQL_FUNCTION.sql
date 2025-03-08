USE PsazDBTset2;

GO
CREATE OR ALTER FUNCTION CalculateFinalPrice (@ID INT, @CartNumber TINYINT, @LockedNumber INT)
RETURNS INT
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @TotalPrice INT;
  
	SELECT @TotalPrice = SUM(CartPrice)
	FROM AddedTo
	WHERE ID = @ID AND CartNumber = @CartNumber AND LockedNumber = @LockedNumber;
	
	DECLARE @Discounts TABLE (Amount INT, Limit INT, Rank INT);

	INSERT INTO @Discounts
	SELECT
    D.Amount,
	D.DiscountLimit,
    ROW_NUMBER() OVER (ORDER BY ApplyTimestamp DESC)
	FROM (SELECT Code, ApplyTimestamp
		  FROM AppliedTo
		  WHERE ID = @ID AND CartNumber = @CartNumber AND LockedNumber = @LockedNumber) AS A
		  JOIN DiscountCode AS D ON A.Code = D.Code;

	DECLARE @Count INT;
	DECLARE @Index INT = 1;
	SELECT @Count = Count(*) FROM @Discounts;
	
	DECLARE @Amount INT, @Limit INT;
	WHILE @Index <= @Count
	BEGIN
		
		SELECT @Amount = D.Amount, @Limit = D.Limit
		FROM @Discounts AS D
		WHERE D.rank = @Index;

		IF( @Limit is NULL)
		BEGIN
			SET @TotalPrice = @TotalPrice - @Amount;
		END
		ELSE
		BEGIN
			IF (@TotalPrice - @TotalPrice * (@Amount/100.0) > @Limit)
				SET @TotalPrice = @TotalPrice - @Limit;
			ELSE
				SET @TotalPrice = @TotalPrice * (1.0 - @Amount/100.0);
		END

		SET @Index = @Index + 1;
	END
	

	
    RETURN (@TotalPrice);
END;
GO