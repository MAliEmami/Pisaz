-- Client
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
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09901234567', 'Saeed', 'Bahrami'   );
INSERT INTO Client (PhoneNumber, FirstName, LastName) 
VALUES    
('09912345678', 'Narges', 'Fazeli'   );

-- Discoutn
INSERT INTO DiscountCode (Code, Amount, DiscountLimit, UsageCount, ExpirationDate) 
VALUES 
(1001, 10, NULL, 1, '2025-03-01 23:59:59'),  -- Flat 10 currency units discount
(1002, 15, 100, 1, '2025-04-01 23:59:59'),  -- 15% discount, max limit 100
(1003, 20, NULL, 5, '2025-05-01 23:59:59'),  -- Flat 20 currency units, usable 5 times
(1004, 5, 50, 1, '2025-06-01 23:59:59'),  -- 5% discount, max limit 50
(1005, 25, NULL, 2, '2025-07-01 23:59:59'),  -- Flat 25 currency units, usable 2 times
(1006, 30, 200, 1, '2025-08-01 23:59:59'),  -- 30% discount, max limit 200
(1007, 50, NULL, 3, '2025-09-01 23:59:59'),  -- Flat 50 currency units, usable 3 times
(1008, 10, 75, 1, '2025-10-01 23:59:59'),  -- 10% discount, max limit 75
(1009, 40, NULL, 4, '2025-11-01 23:59:59'),  -- Flat 40 currency units, usable 4 times
(1010, 20, 150, 1, '2025-12-01 23:59:59');  -- 20% discount, max limit 150

-- Product
INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('HDD', 60, 50, 'Seagate', 'Barracuda 1TB'),
('HDD', 120, 30, 'Western Digital', 'WD Blue 2TB'),
('HDD', 180, 20, 'Toshiba', 'X300 4TB');

INSERT INTO HDD (ID, RotationalSpeed, Wattage, Capacity, Depth, Height, Width) 
VALUES 
(1, 7200, 5, 1000, 14.7, 2.6, 10.2),
(2, 7200, 6, 2000, 14.7, 2.6, 10.2),
(3, 7200, 7, 4000, 14.7, 2.6, 10.2);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('CPU', 250, 40, 'Intel', 'Core i5-13600K'),
('CPU', 450, 20, 'AMD', 'Ryzen 7 7800X'),
('CPU', 600, 15, 'Intel', 'Core i9-14900K');

INSERT INTO CPU (ID, MaxAddressableMemLimit, BoostFrequency, BaseFrequency, NumOfCores, NumOfThreads, Microarchitecture, Generation, Wattage) 
VALUES 
(4, 128, 5.1, 3.5, 6, 12, 'Raptor Lake', '13th Gen', 125.0),
(5, 128, 5.4, 3.8, 8, 16, 'Zen 4', '7000 Series', 170.0),
(6, 128, 5.8, 3.6, 16, 24, 'Raptor Lake', '14th Gen', 253.0);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('GPU', 350, 25, 'NVIDIA', 'RTX 4060 Ti'),
('GPU', 700, 10, 'AMD', 'RX 7900 XT'),
('GPU', 1200, 5, 'NVIDIA', 'RTX 4090');

INSERT INTO GPU (ID, ClockSpeed, RamSize, NumberOfFans, Wattage, Depth, Height, Width) 
VALUES 
(7, 2.61, 8, 2, 160, 30.0, 5.0, 12.0),
(8, 2.40, 20, 3, 300, 32.5, 6.5, 14.0),
(9, 2.52, 24, 3, 450, 35.0, 7.5, 15.0);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('RAM_Stick', 80, 50, 'Corsair', 'Vengeance LPX 16GB DDR4'),
('RAM_Stick', 120, 40, 'G.Skill', 'Trident Z 32GB DDR5'),
('RAM_Stick', 200, 20, 'Kingston', 'Fury Beast 64GB DDR5');

INSERT INTO RAM_Stick (ID, Frequency, Capacity, Generation, Wattage, Depth, Height, Width) 
VALUES 
(10, 3200, 16, 'DDR4', 5, 13.5, 3.5, 0.7),
(11, 6000, 32, 'DDR5', 7, 14.0, 4.0, 0.8),
(12, 6400, 64, 'DDR5', 9, 15.0, 4.5, 0.9);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('Motherboard', 150, 30, 'ASUS', 'ROG STRIX B550-F'),
('Motherboard', 200, 25, 'MSI', 'MAG Z690 TOMAHAWK'),
('Motherboard', 300, 10, 'Gigabyte', 'AORUS X670E MASTER');

INSERT INTO Motherboard (ID, Chipset, NumberOfMemorySlots, MemorySpeedRange, Wattage, Depth, Height, Width) 
VALUES 
(13, 'B550', 4, '3200-4400MHz', 65, 30.5, 24.4, 2.0),
(14, 'Z690', 4, '4800-6000MHz', 75, 30.5, 24.4, 2.2),
(15, 'X670E', 4, '5200-6400MHz', 80, 30.5, 24.4, 2.5);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('SSD', 100, 50, 'Samsung', '970 EVO Plus 1TB'),
('SSD', 180, 30, 'Western Digital', 'SN850X 2TB'),
('SSD', 250, 20, 'Crucial', 'P5 Plus 4TB');

INSERT INTO SSD (ID, Wattage, Capacity) 
VALUES 
(16, 5, 1000),
(17, 7, 2000),
(18, 9, 4000);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('PowerSupply', 80, 40, 'Corsair', 'RM750x 750W'),
('PowerSupply', 120, 25, 'EVGA', 'SuperNOVA 850 G7'),
('PowerSupply', 150, 15, 'Seasonic', 'Focus GX-1000 1000W');

INSERT INTO PowerSupply (ID, SupportedWattage, Depth, Height, Width) 
VALUES 
(19, 750, 16.0, 8.6, 15.0),
(20, 850, 17.0, 8.6, 16.0),
(21, 1000, 18.0, 8.6, 17.0);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('Case', 100, 30, 'NZXT', 'H510'),
('Case', 150, 20, 'Lian Li', 'O11 Dynamic'),
('Case', 200, 10, 'Fractal Design', 'Meshify 2');

INSERT INTO "Case" (ID, NumberOfFans, FanSize, Wattage, CaseType, Material, Color, Depth, Height, Width) 
VALUES 
(22, 2, 120, 0, 'Mid Tower', 'Steel', 'Black', 42.0, 46.0, 21.0),
(23, 3, 140, 0, 'Full Tower', 'Aluminum', 'White', 45.0, 49.0, 23.0),
(24, 4, 120, 0, 'Mid Tower', 'Tempered Glass', 'Gray', 44.0, 48.0, 22.0);


INSERT INTO Products (Category, CurrentPrice, StockCount, Brand, Model) 
VALUES 
('Cooler', 50, 40, 'Noctua', 'NH-D15'),
('Cooler', 100, 25, 'Corsair', 'iCUE H150i Elite'),
('Cooler', 120, 15, 'NZXT', 'Kraken X73');

INSERT INTO Cooler (ID, MaxRotationalSpeed, FanSize, CoolingMethod, Wattage, Depth, Height, Width) 
VALUES 
(25, 1500, 140.0, 'Air', 5, 16.5, 16.0, 14.0),
(26, 2400, 120.0, 'Liquid', 7, 12.0, 12.0, 3.5),
(27, 2800, 120.0, 'Liquid', 8, 13.0, 13.0, 4.0);
