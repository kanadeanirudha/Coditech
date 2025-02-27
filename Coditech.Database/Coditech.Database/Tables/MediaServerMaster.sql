CREATE TABLE [dbo].[MediaServerMaster](
	[MediaServerMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ServerName] [varchar](200) NULL,
	[PartialViewName] [varchar](200) NULL,
	[IsOtherServer] [bit] NULL,
	[ThumbnailFolderName] [varchar](200) NULL,
	[ClassName] [varchar](200) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MediaServerMaster] PRIMARY KEY CLUSTERED 
(
	[MediaServerMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
