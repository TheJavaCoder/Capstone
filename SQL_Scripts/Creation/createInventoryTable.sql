CREATE TABLE Inventory (
   InventoryID int IDENTITY(1,1) PRIMARY KEY,
   PlayerID int NOT NULL,
   ItemID int NOT NULL,
   resourceGatheringLevel int NOT NULL,
   itemAmount int NOT NULL,
   enabled tinyint NOT NULL,
 );
