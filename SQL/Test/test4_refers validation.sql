

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09234567890', 'Sara', 'Ahmadi'     );

DECLARE @ID INT;
SELECT @ID = ID FROM Client WHERE PhoneNumber = '09234567890';

WAITFOR DELAY '00:00:01';

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09345678901', 'Reza', 'Hosseini'   );

SELECT * FROM Client;

-- Expected Result: The Row can't be inserted
INSERT INTO Refers (Referee, Referrer)
VALUES (@ID,@ID + 1);

ROLLBACK TRANSACTION