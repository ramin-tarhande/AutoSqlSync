/****** Object:  Table [dbo].[ADestination]    Script Date: 2019-12-24 03:24:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ADestination](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](10) NOT NULL,
	[Descrip] [nvarchar](50) NOT NULL,
	[Together] [nvarchar](100) NOT NULL,
	[Extra] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_ADesti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ASource](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ASource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


