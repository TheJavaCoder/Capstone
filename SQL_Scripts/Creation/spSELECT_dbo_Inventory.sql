USE [DB_111206_clicker]
GO

/****** Object:  StoredProcedure [dbo].[spSELECT_dbo_Inventory]    Script Date: 3/4/2021 6:35:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spSELECT_dbo_Inventory] 
AS
SELECT * FROM Inventory
GO


