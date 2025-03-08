

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09234567890', 'Sara', 'Ahmadi'     );

DECLARE @ID INT;
SELECT @ID = ID FROM Client WHERE PhoneNumber = '09234567890';


INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09345678901', 'Reza', 'Hosseini'   );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09456789012', 'Mina', 'Karimi'     );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09567890123', 'Hossein', 'Shahbazi');

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09678901234', 'Leila', 'Jafari'    );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09789012345', 'Mehdi', 'Moradi'    );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09890123456', 'Fatemeh', 'Sadat'   );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance) 
VALUES    
('09901234567', 'Saeed', 'Bahrami', 100000   );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;

INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance) 
VALUES    
('09912345678', 'Narges', 'Fazeli', 20000   );

INSERT INTO Refers (Referee, Referrer)
VALUES (@ID + 1,@ID);
SET @ID = @ID + 1;



SELECT * FROM Refers;
SELECT * FROM DiscountCode AS D JOIN PrivateCode AS P ON D.Code = P.Code
ORDER BY P.ID;

ROLLBACK TRANSACTION