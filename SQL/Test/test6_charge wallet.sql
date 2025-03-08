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

INSERT INTO DepositsIntoWallet (TrackingCode, ID, Amount)
VALUES ('12123456789', @ID, 10000);

SELECT ID, WalletBalance FROM Client WHERE ID = @ID;

ROLLBACK TRANSACTION