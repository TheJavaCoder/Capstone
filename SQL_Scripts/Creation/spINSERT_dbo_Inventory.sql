USE [DB_111206_clicker]
GO

/****** Object:  StoredProcedure [dbo].[spINSERT_dbo_Inventory]    Script Date: 3/4/2021 6:32:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spINSERT_dbo_Inventory] 
@inventory_id int, 
@player_id int,
@inventory_item int,
@amount int
AS
SET NOCOUNT ON
-- 1 - Declare variables
-- 2 - Initialize variables
-- 3 - Execute INSERT command

INSERT INTO [dbo].[Inventory]

([inventory_id],
[player_id], 
[inventory_item], 
[amount])

VALUES
(@inventory_id,
@player_id,
@inventory_item,
@amount)

GO


