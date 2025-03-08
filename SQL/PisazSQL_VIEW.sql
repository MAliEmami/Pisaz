USE Pisaz;

GO
CREATE OR ALTER VIEW SimpleUserID AS
  SELECT ID
  From Client
  EXCEPT
  SELECT ID
  From VIPClient;
  
GO
CREATE OR ALTER VIEW FilteredShoppingCart AS
  SELECT C.*
  FROM VIPClient AS V
  JOIN ShoppingCart AS C
  ON V.ID = C.ID
  WHERE C.CartStatus != 'blocked'
  UNION
  SELECT C.*
  FROM SimpleUserID AS S
  JOIN ShoppingCart AS C
  ON S.ID = C.ID
  WHERE (C.CartNumber = 1 AND C.CartStatus != 'blocked') OR C.CartStatus = 'locked';