CREATE TABLE [dbo].[MediaConfiguration](
	[MediaConfigurationId] [tinyint] IDENTITY(1,1) NOT NULL,
	[MediaServerMasterId] [tinyint] NULL,
	[Server] [varchar](100) NULL,
	[AccessKey] [nvarchar](50) NULL,
	[SecretKey] [nvarchar](100) NULL,
	[URL] [nvarchar](200) NULL,
	[CDNUrl] [nvarchar](200) NULL,
	[BucketName] [varchar](100) NULL,
	[Custom1] [varchar](100) NULL,
	[Custom2] [varchar](100) NULL,
	[Custom3] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MediaConfiguration] PRIMARY KEY CLUSTERED 
(
	[MediaConfigurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MediaConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_MediaConfigurationServerMaster] FOREIGN KEY([MediaServerMasterId])
REFERENCES [dbo].[MediaServerMaster] ([MediaServerMasterId])
GO

ALTER TABLE [dbo].[MediaConfiguration] CHECK CONSTRAINT [FK_MediaConfigurationServerMaster]
GO
