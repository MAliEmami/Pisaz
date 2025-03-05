
-- Done
                -- Refers Systems
                    -- int ReferralCode
                    -- int NumRefreee
                    -- int NumDiscountGifted
                -- WITH ReferralChain AS (
                --     SELECT 
                --         Referee, 
                --         Referrer, 
                --         1 AS Depth
                --     FROM 
                --         Refers
                --     WHERE 
                --         Referrer = 1
                --     UNION ALL
                --     SELECT 
                --         R.Referee, 
                --         R.Referrer, 
                --         RC.Depth + 1 AS Depth
                --     FROM 
                --         Refers R
                --     INNER JOIN 
                --         ReferralChain RC ON R.Referrer = RC.Referee
                -- )
                -- SELECT 
                --     (SELECT ReferralCode FROM Client WHERE ID = 1),
                --     (SELECT COUNT(*) FROM Refers WHERE Referrer = 1) AS DirectReferrals,
                --     MAX(Depth) AS MaxReferralDepth
                -- FROM 
                --     ReferralChain;


                -- Cart Status
                -- SELECT 
                --         CartNumber, 
                --         CartStatus,
                --         (SELECT COUNT(*) From ShoppingCart WHERE CartStatus = 'active') As Active_Cart
                -- FROM ShoppingCart 
                -- WHERE ID = 1;

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