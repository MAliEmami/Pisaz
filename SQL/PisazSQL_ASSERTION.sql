CREATE TRIGGERSalaryConstraint
ON EMPLOYEE
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM INSERTED E
        JOIN DEPARTMENT D ON E.Dno = D.Dnumber
        JOIN EMPLOYEE M ON D.Mgr_ssn = M.Ssn
        WHERE E.Salary > M.Salary
    )
    BEGIN
        RAISERROR('Employee salary cannot be greater than the manager salary.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

CREATE TRIGGER BlockedCart
ON LockedShoppingCart
AFTER INSERT, UPDATE
AS
BEGIN
    -- Check if ShoppingCart is blocked
    IF EXISTS (
        SELECT *
        FROM INSERTED I, ShoppingCart S
        WHERE I.Id = S.Id AND I.CartNumber = S.CartNumber -- JOIN
        AND I.CartStatus := 'blocked' -- condition
    )
    BEGIN
        RAISERROR('blocked ShoppingCart cant submitted.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

CREATE TRIGGER NotExistsProduct
ON AddedTo
AFTER INSERT, UPDATE
AS
BEGIN 
    IF EXISTS (
        SELECT 1
        FROM INSERTED I JOIN Product P ON I.Id = P.Id
        WHERE StockCount = 0
    )
    BEGIN
        RAISERROR('Product is Not Exists in warehous.', 16, 2);
        ROLLBACK TRANSACTION;
    END
END;

CREATE TRIGGER MaxUseDiscount
ON AppliedTo
AFTER INSERT, UPDATE
AS
BEGIN 
    IF EXISTS (
        SELECT 1
        FROM INSERTED I JOIN DiscountCode D ON I.Code = D.Code
        GROUP BY Id
        HAVING COUNT(*) > UsageCount
    )
    BEGIN
        RAISERROR('You use this discount more than you could', 16, 3);
        ROLLBACK TRANSACTION;
    END
END;




-- syntax
-- CREATE TRIGGER 
-- ON 
-- AFTER INSERT, UPDATE
-- AS
-- BEGIN 
--     IF EXISTS (
--         SELECT 1
--         FROM INSERTED I
--     )
--     BEGIN
--         RAISERROR('', 16, );
--         ROLLBACK TRANSACTION;
--     END
-- END;