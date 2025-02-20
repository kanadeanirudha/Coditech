CREATE TABLE [dbo].[DBTMSubscriptionPlanAssociatedToUser](
	[DBTMSubscriptionPlanAssociatedToUserId] [bigint] IDENTITY(1,1) NOT NULL,
	[DBTMSubscriptionPlanId] [int] NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[DBTMDeviceMasterId] [bigint] NOT NULL,
	[DurationInDays] [smallint] NOT NULL,
	[PlanCost] [money] NOT NULL,
	[PlanDiscount] [money] NOT NULL,
	[IsExpired] [bit] NOT NULL,
	[PlanDurationExpirationDate] [datetime] NOT NULL,
	[SalesInvoiceMasterId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMSubscriptionPlanAssociatedToUser] PRIMARY KEY CLUSTERED 
(
	[DBTMSubscriptionPlanAssociatedToUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO