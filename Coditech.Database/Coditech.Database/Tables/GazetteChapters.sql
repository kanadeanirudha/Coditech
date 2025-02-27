CREATE TABLE [dbo].[GazetteChapters](
	[GazetteChapterId] [int] IDENTITY(1,1) NOT NULL,
	[GeneralDistrictMasterId] [smallint] NOT NULL,
	[ChapterName] [nvarchar](500) NOT NULL,
	[ChapterNumber] [nvarchar](100) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GazetteChapters] PRIMARY KEY CLUSTERED 
(
	[GazetteChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
