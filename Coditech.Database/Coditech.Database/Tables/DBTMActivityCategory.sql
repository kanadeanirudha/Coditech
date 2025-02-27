CREATE TABLE [dbo].[DBTMActivityCategory](
	[DBTMActivityCategoryId] [smallint] IDENTITY(1,1) NOT NULL,
	[DBTMParentActivityCategoryId] [int] NOT NULL,
	[ActivityCategoryCode] [nvarchar](50) NOT NULL,
	[ActivityCategoryName] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMActivityCategory] PRIMARY KEY CLUSTERED 
(
	[DBTMActivityCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO