USE [DB_111206_clicker]
GO

/****** Object:  Table [dbo].[Goal_items]    Script Date: 3/4/2021 6:25:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Goal_items](
	[goal_item_id] [int] NOT NULL,
	[goal_id] [int] NOT NULL,
	[required_item_id] [int] NOT NULL,
	[required_amount] [int] NOT NULL,
 CONSTRAINT [PK_Goal_items] PRIMARY KEY CLUSTERED 
(
	[goal_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Goal_items]  WITH CHECK ADD  CONSTRAINT [FK_Goals_Goal_items] FOREIGN KEY([goal_id])
REFERENCES [dbo].[Goals] ([goal_id])
GO

ALTER TABLE [dbo].[Goal_items] CHECK CONSTRAINT [FK_Goals_Goal_items]
GO

ALTER TABLE [dbo].[Goal_items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Goal_items] FOREIGN KEY([required_item_id])
REFERENCES [dbo].[Items] ([items_id])
GO

ALTER TABLE [dbo].[Goal_items] CHECK CONSTRAINT [FK_Items_Goal_items]
GO


