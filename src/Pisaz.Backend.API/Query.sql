-- Refers Systems
    int ReferralCode
    int NumRefreee
    int NumDiscountGifted
WITH ReferralChain AS (
    SELECT 
        Referee, 
        Referrer, 
        1 AS Depth
    FROM 
        Refers
    WHERE 
        Referrer = 1
    UNION ALL
    SELECT 
        R.Referee, 
        R.Referrer, 
        RC.Depth + 1 AS Depth
    FROM 
        Refers R
    INNER JOIN 
        ReferralChain RC ON R.Referrer = RC.Referee
)
SELECT 
    (SELECT ReferralCode FROM Client WHERE ID = 1),
    (SELECT COUNT(*) FROM Refers WHERE Referrer = 1) AS DirectReferrals,
    MAX(Depth) AS MaxReferralDepth
FROM 
    ReferralChain;


-- Cart Status
SELECT 
        CartNumber, 
        CartStatus,
        (SELECT COUNT(*) From ShoppingCart WHERE CartStatus = 'active') As Active_Cart
FROM ShoppingCart 
WHERE ID = 1;

-- Vip Club
SELECT 
    DATEDIFF(day, CURRENT_TIMESTAMP, SubsctiptionExpirationTime) AS DaysRemaining
    -- (SELECT ) Benefit
FROM VIPClient
WHERE ID = 1;


-- lest 5 PurchaseHistory
SELECT TOP 5
    STRING_AGG(P.Category + ' ' + P.Brand + ' ' + P.Model, ', ') AS ProductList,
    SUM(A.CartPrice * A.Quantity) AS TotalPrice
    T.TransactionTime,
FROM 
    AddedTo A
JOIN 
    Products P ON A.ProductID = P.ID
JOIN 
    IssuedFor I  ON A.ID = I.ID 
                 AND A.CartNumber = I.CartNumber 
                 AND A.LockedNumber = I.LockedNumber
JOIN 
    Transactions T ON I.TrackingCode = T.TrackingCode
JOIN 
    LockedShoppingCart LSC  ON A.ID = LSC.ID 
                            AND A.CartNumber = LSC.CartNumber 
                            AND A.LockedNumber = LSC.LockedNumber
WHERE 
    T.TransactionStatus = 'Successful'
GROUP BY 
    A.ID, A.CartNumber, A.LockedNumber, I.TrackingCode, T.TransactionStatus, T.TransactionTime
ORDER BY 
    T.TransactionTime DESC;


-- Done ^

-- VIP Profit
SELECT 
    COALESCE(SUM(ADT.CartPrice) * 0.15, 0),
    DATEDIFF(day, CURRENT_TIMESTAMP, SubsctiptionExpirationTime) AS DaysRemaining
FROM 
    VIPClient VC
JOIN 
    IssuedFor ISF ON VC.ID = ISF.ID
JOIN 
    Transactions T ON ISF.TrackingCode = T.TrackingCode
JOIN 
    AddedTo ADT ON ISF.ID = ADT.ID 
    AND ISF.CartNumber = ADT.CartNumber 
    AND ISF.LockedNumber = ADT.LockedNumber
WHERE VC.ID = 1
    AND T.TransactionStatus = 'Successful'
    AND T.TransactionTime >= DATEADD(DAY, -DAY(GETDATE()) + 1, CAST(GETDATE() AS DATE))
    AND T.TransactionTime <= GETDATE()
    AND VC.SubsctiptionExpirationTime >= T.TransactionTime
GROUP BY 
    VC.ID, VC.SubsctiptionExpirationTime;


-- Find Product ID
SELECT 
        ID
FROM
        Products
WHERE 
        model = 'NH-D15';


-- Compatible
SELECT 
       P.Brand, 
       P.Model, 
       P.CurrentPrice, 
       P.StockCount,
       P.Category
FROM (
    -- CPU-Cooler compatibility
    SELECT CoolerID AS CompatibleID FROM CcSlotCompatibleWith WHERE CPUID = 9
    UNION
    SELECT CPUID FROM CcSlotCompatibleWith WHERE CoolerID = 9
    
    UNION ALL
    
    -- CPU-Motherboard compatibility
    SELECT MotherboardID FROM McSlotCompatibleWith WHERE CPUID = 9
    UNION
    SELECT CPUID FROM McSlotCompatibleWith WHERE MotherboardID = 9
    
    UNION ALL
    
    -- RAM-Motherboard compatibility
    SELECT MotherboardID FROM RmSlotCompatibleWith WHERE RAMID = 9
    UNION
    SELECT RAMID FROM RmSlotCompatibleWith WHERE MotherboardID = 9
    
    UNION ALL
    
    -- GPU-PowerSupply compatibility
    SELECT PowerID FROM ConnectorCompatibleWith WHERE GPUID = 9
    UNION
    SELECT GPUID FROM ConnectorCompatibleWith WHERE PowerID = 9
    
    UNION ALL
    
    -- SSD-Motherboard compatibility
    SELECT MotherboardID FROM SmSlotCompatibleWith WHERE SSDID = 9
    UNION
    SELECT SSDID FROM SmSlotCompatibleWith WHERE MotherboardID = 9
    
    UNION ALL
    
    -- GPU-Motherboard compatibility
    SELECT MotherboardID FROM GmSlotCompatibleWith WHERE GPUID = 9
    UNION
    SELECT GPUID FROM GmSlotCompatibleWith WHERE MotherboardID = 9
) AS Compatible
JOIN Products P ON P.ID = Compatible.CompatibleID;
