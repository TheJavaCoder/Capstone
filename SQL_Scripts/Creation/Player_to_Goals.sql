USE [DB_111206_clicker]
GO

/****** Object:  Table [dbo].[Player_to_Goals]    Script Date: 3/4/2021 6:31:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Player_to_Goals](
	[player_ID] [int] NOT NULL,
	[goals_ID] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Player_to_Goals]  WITH CHECK ADD  CONSTRAINT [FK_Goals_to_Player_to_Goals] FOREIGN KEY([goals_ID])
REFERENCES [dbo].[Goals] ([goal_id])
GO

ALTER TABLE [dbo].[Player_to_Goals] CHECK CONSTRAINT [FK_Goals_to_Player_to_Goals]
GO

ALTER TABLE [dbo].[Player_to_Goals]  WITH CHECK ADD  CONSTRAINT [FK_Player_to_Player_to_Goals] FOREIGN KEY([player_ID])
REFERENCES [dbo].[Player] ([player_ID])
GO

ALTER TABLE [dbo].[Player_to_Goals] CHECK CONSTRAINT [FK_Player_to_Player_to_Goals]
GO


