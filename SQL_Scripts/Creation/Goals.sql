USE [DB_111206_clicker]
GO

/****** Object:  Table [dbo].[Goals]    Script Date: 3/4/2021 6:27:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Goals](
	[goal_id] [int] NOT NULL,
	[goal_name] [nvarchar](50) NOT NULL,
	[background_image] [image] NULL,
	[reward] [nvarchar](100) NULL,
 CONSTRAINT [PK_Goals] PRIMARY KEY CLUSTERED 
(
	[goal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


