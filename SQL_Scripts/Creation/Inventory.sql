USE [DB_111206_clicker]
GO

/****** Object:  Table [dbo].[Inventory]    Script Date: 3/6/2021 9:00:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inventory](
	[inventory_id] [int] NOT NULL,
	[player_id] [int] NOT NULL,
	[inventory_item] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[resourceGatheringLevel] [int] NOT NULL,
	[enabled] [tinyint] NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[inventory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Items_Inventory] FOREIGN KEY([inventory_item])
REFERENCES [dbo].[Items] ([items_id])
GO

ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Items_Inventory]
GO

ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Player_Inventory] FOREIGN KEY([player_id])
REFERENCES [dbo].[Player] ([player_ID])
GO

ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Player_Inventory]
GO


