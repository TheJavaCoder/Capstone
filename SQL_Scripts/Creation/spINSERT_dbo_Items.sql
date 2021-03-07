USE [DB_111206_clicker]
GO

/****** Object:  StoredProcedure [dbo].[spINSERT_dbo_Items]    Script Date: 3/4/2021 6:33:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spINSERT_dbo_Items] 
@items int, 
@item_name varchar(50),
@icon varchar(50),
@calc nvarchar(100)
AS
SET NOCOUNT ON
-- 1 - Declare variables
-- 2 - Initialize variables
-- 3 - Execute INSERT command

INSERT INTO [dbo].[Items]

([items_id],
[item_name],
[icon],
[calc])

VALUES (@items,@item_name,@icon, @calc)

GO


