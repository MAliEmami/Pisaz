USE Pisaz;

BEGIN TRANSACTION

INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES ('09234567890', 'Sara', 'Ahmadi'     );

DECLARE @ID INT;
SELECT @ID = ID FROM Client WHERE PhoneNumber = '09234567890';

INSERT INTO Transactions (TrackingCode, TransactionStatus, TransactionTime)
VALUES ('12123456789', 'Successful', '2020-12-30 13:08:54.193');

INSERT INTO BankTransaction (TrackingCode, CardNumber)
VALUES ('12123456789', '1234567890123456');

-- Change Amount value once higher than vip price and once lower than vip price
INSERT INTO DepositsIntoWallet (TrackingCode, ID, Amount)
VALUES ('12123456789', @ID, 100000);

SELECT ID, WalletBalance FROM Client WHERE ID = @ID;


INSERT INTO Transactions (TrackingCode, TransactionStatus, TransactionTime)
VALUES ('12123456799', 'Successful', '2020-12-30 13:08:54.193');

INSERT INTO WalletTransaction(TrackingCode)
VALUES ('12123456799');

INSERT INTO Subscribes (ID, TrackingCode)
VALUES (@ID, '12123456799');

SELECT C.ID, C.WalletBalance, V.SubsctiptionExpirationTime FROM Client AS C JOIN VIPClient AS V ON C.ID = V.ID;
SELECT * FROM ShoppingCart WHERE ID = @ID;

ROLLBACK TRANSACTION