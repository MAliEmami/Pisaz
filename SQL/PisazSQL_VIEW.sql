CREATE VIEW SimpleUserID AS
  SELECT Id
  From Client
  EXCEPT
  SELECT Id
  From VIPClient;

CREATE VIEW FilteredShoppingCart AS
  SELECT ShoppingCart.*
  FROM VIPClient AS v
  JOIN ShoppingCart AS c
  ON v.Id = c.Id
  WHERE c.Status != 'blocked'
  UNION
  SELECT ShoppingCart.*
  FROM SimpleUserID AS s
  JOIN ShoppingCart AS c
  ON s.Id = c.Id
  WHERE (c.Number = 1 AND c.Status != 'blocked') OR c.Status = 'locked';