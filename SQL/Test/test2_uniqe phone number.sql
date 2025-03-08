USE Pisaz;

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES 
('09234567890', 'Sara', 'Ahmadi'     );
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES 
('09345678901', 'Reza', 'Hosseini'   );
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09456789012', 'Mina', 'Karimi'     );

-- this should result to error
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09234567890', 'Hossein', 'Shahbazi');


SELECT * FROM Client;

ROLLBACK TRANSACTION;