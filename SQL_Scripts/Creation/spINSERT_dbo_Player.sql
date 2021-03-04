USE [DB_111206_clicker]
GO

/****** Object:  StoredProcedure [dbo].[spINSERT_dbo_Player]    Script Date: 3/4/2021 6:34:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spINSERT_dbo_Player] 
@player_ID int, 
@username varchar(50)
AS
SET NOCOUNT ON
-- 1 - Declare variables
-- 2 - Initialize variables
-- 3 - Execute INSERT command

INSERT INTO [dbo].[Player]

([player_ID]

,[username])

VALUES

(@player_ID

,@username)

GO


