SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spINSERT_dbo_Inventory] 
@Invent [dbo].[InventoryItemType] READONLY
AS
SET NOCOUNT ON
-- 1 - Declare variables
-- 2 - Initialize variables
-- 3 - Execute INSERT command

BEGIN

MERGE INTO dbo.Inventory AS Target
        USING @Invent AS Source
        ON ( 1 = 0 )
        WHEN NOT MATCHED THEN
            INSERT
            (
               player_id,
               inventory_item,
               amount,
               resourceGatheringLevel
            )
            VALUES
            (
                Source.player_id,
                Source.inventory_item,
                Source.amount,
                Source.resourceGatheringLevel
            );
        

END;
GO
