USE [DB_111206_clicker]
GO

/****** Object:  Table [dbo].[Items]    Script Date: 3/4/2021 6:29:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Items](
	[items_id] [int] NOT NULL,
	[item_name] [nvarchar](50) NOT NULL,
	[icon] [nvarchar](50) NOT NULL,
	[calc] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[items_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


