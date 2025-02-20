CREATE TABLE [dbo].[GazetteChapterPageDetails](
	[GazetteChapterPageDetailId] [int] IDENTITY(1,1) NOT NULL,
	[GazetteChapterId] [int] NOT NULL,
	[PageHeader] [nvarchar](200) NOT NULL,
	[PageFooter] [nvarchar](200) NOT NULL,
	[PageContent] [nvarchar](max) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GazetteChapterPageDetails] PRIMARY KEY CLUSTERED 
(
	[GazetteChapterPageDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO