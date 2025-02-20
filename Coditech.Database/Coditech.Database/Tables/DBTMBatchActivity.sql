CREATE TABLE [dbo].[DBTMBatchActivity](
	[DBTMBatchActivityId] [bigint] IDENTITY(1,1) NOT NULL,
	[GeneralBatchMasterId] [int] NOT NULL,
	[DBTMTestMasterId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMBatchActivity] PRIMARY KEY CLUSTERED 
(
	[DBTMBatchActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO