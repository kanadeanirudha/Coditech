CREATE TABLE [dbo].[DBTMSubscriptionPlan](
	[DBTMSubscriptionPlanId] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionPlanTypeEnumId] [int] NOT NULL,
	[PlanName] [nvarchar](100) NOT NULL,
	[DurationInDays] [smallint] NOT NULL,
	[PlanCost] [money] NOT NULL,
	[PlanDiscount] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsTaxExclusive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMSubscriptionPlan] PRIMARY KEY CLUSTERED 
(
	[DBTMSubscriptionPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DBTMSubscriptionPlan] ADD  CONSTRAINT [DF_DBTMSubscriptionPlan_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO