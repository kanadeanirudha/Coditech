CREATE TABLE [dbo].[DBTMSubscriptionPlanActivity](
	[DBTMSubscriptionPlanActivityId] [int] IDENTITY(1,1) NOT NULL,
	[DBTMSubscriptionPlanId] [int] NOT NULL,
	[DBTMTestMasterId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMSubscriptionPlanActivity] PRIMARY KEY CLUSTERED 
(
	[DBTMSubscriptionPlanActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO