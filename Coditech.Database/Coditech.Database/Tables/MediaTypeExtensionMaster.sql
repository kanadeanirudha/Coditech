CREATE TABLE [dbo].[MediaTypeExtensionMaster](
	[MediaTypeExtensionMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[MediaTypeMasterId] [tinyint] NOT NULL,
	[ExtensionName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MediaTypeExtensionMaster] PRIMARY KEY CLUSTERED 
(
	[MediaTypeExtensionMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
