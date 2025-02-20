CREATE TABLE [dbo].[DBTMTestMaster](
	[DBTMTestMasterId] [int] IDENTITY(1,1) NOT NULL,
	[DBTMActivityCategoryId] [smallint] NOT NULL,
	[TestCode] [nvarchar](50) NOT NULL,
	[TestName] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMTestMaster] PRIMARY KEY CLUSTERED 
(
	[DBTMTestMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO