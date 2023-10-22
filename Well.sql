USE [TestAEM]
GO

/****** Object:  Table [dbo].[PlatformWell]    Script Date: 22/10/2023 3:12:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlatformWell](
	[Id] [int] NOT NULL,
	[PlatformId] [int] NULL,
	[UniqueName] [varchar](255) NOT NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PlatformWell]  WITH CHECK ADD  CONSTRAINT [FK_PlatformWell_PlatformTable] FOREIGN KEY([PlatformId])
REFERENCES [dbo].[PlatformTable] ([Id])
GO

ALTER TABLE [dbo].[PlatformWell] CHECK CONSTRAINT [FK_PlatformWell_PlatformTable]
GO

