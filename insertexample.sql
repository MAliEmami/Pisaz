-- INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance, ReferralCode)
-- VALUES
-- ('09123456789', 'Ali', 'Ahmadi', 50000, '123456789'),
-- ('09234567890', 'Sara', 'Hosseini', 30000, '223456789'),
-- ('09345678901', 'Reza', 'Mohammadi', 45000, '323456789'),
-- ('09456789012', 'Niloofar', 'Jafari', 60000, '423456789'),
-- ('09567890123', 'Mehdi', 'Kazemi', 70000, '523456789');

-- INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
-- VALUES
-- ('CPU', 3500000, 10, 'Intel', 'i9-12900K'),
-- ('GPU', 12000000, 5, 'NVIDIA', 'RTX 3080'),
-- ('RAM_Stick', 2000000, 20, 'Corsair', 'Vengeance 16GB DDR4'),
-- ('SSD', 2500000, 15, 'Samsung', '970 Evo 1TB'),
-- ('PowerSupply', 3000000, 8, 'CoolerMaster', '750W Gold');

-- INSERT INTO Address (ID, Province, Remainder)
-- VALUES
-- (1, 'Tehran', 'Street A, Building 12'),
-- (2, 'Shiraz', 'Street B, Building 20'),
-- (3, 'Esfahan', 'Street C, Building 5'),
-- (4, 'Mashhad', 'Street D, Building 9'),
-- (5, 'Tabriz', 'Street E, Building 15');


-- INSERT INTO ShoppingCart (ID, CartNumber, CartStatus)
-- VALUES
-- (1, 1, 'active'),
-- (2, 1, 'active'),
-- (3, 1, 'locked'),
-- (4, 2, 'active'),
-- (5, 1, 'blocked');

-- INSERT INTO LockedShoppingCart (ID, CartNumber, LockedNumber)
-- VALUES
-- (1, 1, 1001),
-- (2, 1, 1002),
-- (3, 1, 1003),
-- (4, 2, 1004),
-- (5, 1, 1005);


-- INSERT INTO AddedTo (ID, CartNumber, LockedNumber, ProductID, Quantity, CartPrice)
-- VALUES
-- (1, 1, 1001, 1, 2, 7000000),
-- (1, 1, 1001, 3, 4, 8000000),
-- (2, 1, 1002, 2, 1, 12000000),
-- (3, 1, 1003, 4, 2, 5000000),
-- (4, 2, 1004, 5, 1, 3000000);

-- INSERT INTO Transactions (TrackingCode, TransactionStatus, TransactionTime)
-- VALUES
-- ('TRX12345', 'Successful', GETDATE()),
-- ('TRX67890', 'Successful', GETDATE()),
-- ('TRX11223', 'SemiSuccessful', GETDATE()),
-- ('TRX44556', 'Unsuccessful', GETDATE()),
-- ('TRX77889', 'Successful', GETDATE());


-- INSERT INTO IssuedFor (TrackingCode, ID, CartNumber, LockedNumber)
-- VALUES
-- ('TRX12345', 1, 1, 1001),
-- ('TRX67890', 2, 1, 1002),
-- ('TRX11223', 3, 1, 1003),
-- ('TRX44556', 4, 2, 1004),
-- ('TRX77889', 5, 1, 1005);

-- INSERT INTO VIPClient (ID)
-- VALUES
-- (1), (3), (5);

-- INSERT INTO DiscountCode (Amount, DiscountLimit, UsageCount, ExpirationDate)
-- VALUES
-- (500000, NULL, 3, DATEADD(DAY, 30, GETDATE())),
-- (1000000, NULL, 1, DATEADD(DAY, 60, GETDATE())),
-- (10, 5000000, 5, DATEADD(DAY, 45, GETDATE()));


-- INSERT INTO AppliedTo (ID, CartNumber, LockedNumber, Code)
-- VALUES
-- (1, 1, 1001, 1),
-- (2, 1, 1002, 2),
-- (3, 1, 1003, 3);

-- INSERT INTO DepositsIntoWallet (TrackingCode, ID, Amount)
-- VALUES
-- ('TRX12345', 1, 5000000),
-- ('TRX67890', 2, 1000000),
-- ('TRX11223', 3, 3000000);

-- INSERT INTO Subscribes (TrackingCode, ID)
-- VALUES
-- ('TRX77889', 5);

-- INSERT INTO PrivateCode (Code, ID, UsageTimestamp)
-- VALUES
-- (1, 1, GETDATE()),
-- (2, 2, GETDATE());

-- INSERT INTO PublicCode (Code)
-- VALUES
-- (3);


-- INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
-- VALUES
-- ('HDD', 1500000, 12, 'Seagate', 'Barracuda 2TB'),
-- ('HDD', 1800000, 10, 'Western Digital', 'Blue 1TB'),
-- ('Case', 2500000, 6, 'NZXT', 'H510'),
-- ('Cooler', 1200000, 15, 'Noctua', 'NH-D15'),
-- ('Motherboard', 5000000, 7, 'ASUS', 'ROG Strix B550-F'),
-- ('PowerSupply', 3500000, 9, 'EVGA', 'SuperNOVA 750W'),
-- ('RAM_Stick', 2200000, 18, 'G.Skill', 'Trident Z 32GB DDR4'),
-- ('SSD', 3000000, 14, 'Crucial', 'P5 500GB'),
-- ('GPU', 15000000, 4, 'AMD', 'Radeon RX 6800 XT'),
-- ('CPU', 4000000, 11, 'AMD', 'Ryzen 7 5800X');


-- INSERT INTO HDD (ID, RotationalSpeed, Wattage, Capacity, Depth, Height, Width)
-- VALUES
-- (6, 7200, 5, 2000, 14.7, 10.2, 2.5),
-- (7, 5400, 4, 1000, 14.7, 10.2, 2.5);


-- INSERT INTO "Case" (ID, NumberOfFans, FanSize, Wattage, CaseType, Material, Color, Depth, Height, Width)
-- VALUES
-- (8, 2, 120, 50, 'Mid Tower', 'Steel', 'Black', 43.5, 46.0, 21.0);

-- INSERT INTO PowerSupply (ID, SupportedWattage, Depth, Height, Width)
-- VALUES
-- (12, 750, 16.0, 8.6, 15.0);


-- INSERT INTO PowerSupply (ID, SupportedWattage, Depth, Height, Width)
-- VALUES
-- (12, 750, 16.0, 8.6, 15.0);


-- INSERT INTO GPU (ID, ClockSpeed, RamSize, NumberOfFans, Wattage, Depth, Height, Width)
-- VALUES
-- (14, 2.25, 16, 3, 300, 28.0, 12.0, 4.0);


-- INSERT INTO SSD (ID, Wattage, Capacity)
-- VALUES
-- (10, 5, 500);


-- INSERT INTO RAM_Stick (ID, Frequency, Capacity, Generation, Wattage, Depth, Height, Width)
-- VALUES
-- (11, 3200, 32, 'DDR4', 10, 13.3, 3.8, 0.9);


-- INSERT INTO Motherboard (ID, Chipset, NumberOfMemorySlots, MemorySpeedRange, Wattage, Depth, Height, Width)
-- VALUES
-- (9, 'B550', 4, '3200-4400 MHz', 60, 30.5, 24.4, 2.5);


-- INSERT INTO CPU (ID, MaxAddressableMemLimit, BoostFrequency, BaseFrequency, NumOfCores, NumOfThreads, Microarchitecture, Generation, Wattage)
-- VALUES
-- (15, 128, 4.7, 3.8, 8, 16, 'Zen 3', 'Ryzen 5000', 105);


-- INSERT INTO Cooler (ID, MaxRotationalSpeed, FanSize, CoolingMethod, Wattage, Depth, Height, Width)
-- VALUES
-- (13, 1500, 120.0, 'Air Cooling', 5, 16.5, 15.0, 13.5);


-- SELECT TOP 5
--     T.TransactionTime,
--     STRING_AGG(P.Category + ' ' + P.Brand + ' ' + P.Model, ', ') AS ProductList,
--     SUM(A.CartPrice * A.Quantity) AS TotalPrice
-- FROM 
--     AddedTo A
-- JOIN 
--     Products P ON A.ProductID = P.ID
-- JOIN 
--     IssuedFor I  ON A.ID = I.ID 
--                  AND A.CartNumber = I.CartNumber 
--                  AND A.LockedNumber = I.LockedNumber
-- JOIN 
--     Transactions T ON I.TrackingCode = T.TrackingCode
-- JOIN 
--     LockedShoppingCart LSC  ON A.ID = LSC.ID 
--                             AND A.CartNumber = LSC.CartNumber 
--                             AND A.LockedNumber = LSC.LockedNumber
-- WHERE 
--     T.TransactionStatus = 'Successful'
--     AND
--     LSC.ID = 1
-- GROUP BY 
--     A.ID, A.CartNumber, A.LockedNumber, I.TrackingCode, T.TransactionStatus, T.TransactionTime
-- ORDER BY 
--     T.TransactionTime DESC;

-- INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model)
-- VALUES
-- ('HDD', 2000000, 8, 'Seagate', 'IronWolf 4TB'),
-- ('HDD', 1700000, 5, 'Western Digital', 'Black 2TB'),
-- ('Case', 2800000, 10, 'Cooler Master', 'MasterBox TD500'),
-- ('Cooler', 1500000, 12, 'Corsair', 'iCUE H150i'),
-- ('Motherboard', 6000000, 6, 'MSI', 'MEG X570 ACE'),
-- ('PowerSupply', 4000000, 8, 'Corsair', 'RM850x'),
-- ('RAM_Stick', 2500000, 15, 'Kingston', 'Fury Beast 16GB DDR5'),
-- ('SSD', 3500000, 10, 'Samsung', '970 EVO Plus 1TB'),
-- ('GPU', 18000000, 3, 'NVIDIA', 'RTX 3080 Ti'),
-- ('CPU', 4500000, 9, 'Intel', 'Core i9-11900K');

Use PsazDBTset2;

-- -- Insert More HDDs
-- INSERT INTO HDD (ID, RotationalSpeed, Wattage, Capacity, Depth, Height, Width)
-- VALUES
-- (1002, 7200, 6, 4000, 14.7, 10.2, 2.5),
-- (1003, 7200, 5, 2000, 14.7, 10.2, 2.5);

-- -- Insert More Cases
-- INSERT INTO "Case" (ID, NumberOfFans, FanSize, Wattage, CaseType, Material, Color, Depth, Height, Width)
-- VALUES
-- (1004, 3, 140, 55, 'Full Tower', 'Aluminum', 'White', 48.0, 50.5, 23.0);

-- -- Insert More Power Supplies
-- INSERT INTO PowerSupply (ID, SupportedWattage, Depth, Height, Width)
-- VALUES
-- (1007, 850, 16.5, 8.7, 15.5);

-- -- Insert More GPUs
-- INSERT INTO GPU (ID, ClockSpeed, RamSize, NumberOfFans, Wattage, Depth, Height, Width)
-- VALUES
-- (1010, 1.9, 12, 3, 320, 29.5, 13.0, 4.5);

-- -- Insert More SSDs
-- INSERT INTO SSD (ID, Wattage, Capacity)
-- VALUES
-- (1009, 4, 1000);

-- -- Insert More RAM Sticks
-- INSERT INTO RAM_Stick (ID, Frequency, Capacity, Generation, Wattage, Depth, Height, Width)
-- VALUES
-- (1008, 3600, 16, 'DDR5', 12, 13.3, 4.0, 1.0);

-- -- Insert More Motherboards
-- INSERT INTO Motherboard (ID, Chipset, NumberOfMemorySlots, MemorySpeedRange, Wattage, Depth, Height, Width)
-- VALUES
-- (1006, 'X570', 4, '3200-5000 MHz', 70, 30.5, 24.4, 2.6);

-- -- Insert More CPUs
-- INSERT INTO CPU (ID, MaxAddressableMemLimit, BoostFrequency, BaseFrequency, NumOfCores, NumOfThreads, Microarchitecture, Generation, Wattage)
-- VALUES
-- (1011, 128, 5.3, 3.5, 10, 20, 'Rocket Lake', '11th Gen', 125);

-- -- Insert More Coolers
-- INSERT INTO Cooler (ID, MaxRotationalSpeed, FanSize, CoolingMethod, Wattage, Depth, Height, Width)
-- VALUES
-- (1005, 1800, 140.0, 'Liquid Cooling', 6, 17.0, 15.5, 14.0);


-- INSERT INTO ConnectorCompatibleWith (GPUID, PowerID)
-- VALUES
-- (14, 12), -- AMD Radeon RX 6800 XT with EVGA SuperNOVA 750W
-- (1010, 1007); -- NVIDIA RTX 3080 Ti with Corsair RM850x

-- INSERT INTO SmSlotCompatibleWith (MotherboardID, SSDID)
-- VALUES
-- (9, 10),   -- ASUS ROG Strix B550-F with Crucial P5 500GB
-- (1006, 1009); -- MSI MEG X570 ACE with Samsung 970 EVO Plus 1TB


-- INSERT INTO GmSlotCompatibleWith (MotherboardID, GPUID)
-- VALUES
-- (9, 14),   -- ASUS ROG Strix B550-F with AMD Radeon RX 6800 XT
-- (1006, 1010); -- MSI MEG X570 ACE with NVIDIA RTX 3080 Ti


-- INSERT INTO RmSlotCompatibleWith (MotherboardID, RAMID)
-- VALUES
-- (9, 11),   -- ASUS ROG Strix B550-F with G.Skill Trident Z 32GB DDR4
-- (1006, 1008); -- MSI MEG X570 ACE with Kingston Fury Beast 16GB DDR5


-- INSERT INTO McSlotCompatibleWith (MotherboardID, CPUID)
-- VALUES
-- (9, 15),   -- ASUS ROG Strix B550-F with AMD Ryzen 7 5800X
-- (1006, 1011); -- MSI MEG X570 ACE with Intel Core i9-11900K


-- INSERT INTO CcSlotCompatibleWith (CoolerID, CPUID)
-- VALUES
-- (13, 15),   -- Noctua NH-D15 with AMD Ryzen 7 5800X
-- (1005, 1011); -- Corsair iCUE H150i with Intel Core i9-11900K
