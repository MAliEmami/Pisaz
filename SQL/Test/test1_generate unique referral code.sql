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
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09567890123', 'Hossein', 'Shahbazi');
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09678901234', 'Leila', 'Jafari'    );
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09789012345', 'Mehdi', 'Moradi'    );
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09890123456', 'Fatemeh', 'Sadat'   );
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance) 
VALUES    
('09901234567', 'Saeed', 'Bahrami', 100000   );
INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance) 
VALUES    
('09912345678', 'Narges', 'Fazeli', 20000   );

SELECT * FROM Client;

ROLLBACK TRANSACTION;