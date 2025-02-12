CREATE TABLE [dbo].[MediaFolderMaster](
	[MediaFolderMasterId] [int] IDENTITY(1,1) NOT NULL,
	[MediaFolderParentId] [int] NULL,
	[FolderName] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediaFolderMaster] PRIMARY KEY CLUSTERED 
(
	[MediaFolderMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MediaFolderMaster] ADD  CONSTRAINT [DF_MediaFolderMaster_MediaFolderParentId]  DEFAULT ((0)) FOR [MediaFolderParentId]
GO

ALTER TABLE [dbo].[MediaFolderMaster] ADD  CONSTRAINT [DF_MediaFolderMaster_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
