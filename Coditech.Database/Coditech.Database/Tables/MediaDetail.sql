CREATE TABLE [dbo].[MediaDetail](
	[MediaId] [bigint] IDENTITY(1,1) NOT NULL,
	[MediaConfigurationId] [tinyint] NOT NULL,
	[MediaFolderMasterId] [int] NOT NULL,
	[Path] [varchar](300) NOT NULL,
	[FileName] [varchar](300) NOT NULL,
	[Size] [varchar](30) NOT NULL,
	[Height] [varchar](30) NOT NULL,
	[Width] [varchar](30) NOT NULL,
	[Length] [varchar](30) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MediaDetail] PRIMARY KEY CLUSTERED 
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MediaDetail]  WITH CHECK ADD  CONSTRAINT [FK_MediaDetail_MediaConfiguration] FOREIGN KEY([MediaConfigurationId])
REFERENCES [dbo].[MediaConfiguration] ([MediaConfigurationId])
GO

ALTER TABLE [dbo].[MediaDetail] CHECK CONSTRAINT [FK_MediaDetail_MediaConfiguration]
GO

ALTER TABLE [dbo].[MediaDetail]  WITH CHECK ADD  CONSTRAINT [FK_MediaDetail_MediaFolderMaster] FOREIGN KEY([MediaFolderMasterId])
REFERENCES [dbo].[MediaFolderMaster] ([MediaFolderMasterId])
GO

ALTER TABLE [dbo].[MediaDetail] CHECK CONSTRAINT [FK_MediaDetail_MediaFolderMaster]
GO
