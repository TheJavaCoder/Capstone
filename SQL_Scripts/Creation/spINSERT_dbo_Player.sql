USE [DB_111206_clicker]
GO

/****** Object:  StoredProcedure [dbo].[spINSERT_dbo_Player]    Script Date: 3/6/2021 9:05:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spINSERT_dbo_Player] 
@player_ID int, 
@username varchar(255),
@password varchar(500)
AS
SET NOCOUNT ON
-- 1 - Declare variables
-- 2 - Initialize variables
-- 3 - Execute INSERT command

INSERT INTO [dbo].[Player]

([player_ID]

,[username],
[password])

VALUES

(@player_ID

,@username,
@password)

GO


