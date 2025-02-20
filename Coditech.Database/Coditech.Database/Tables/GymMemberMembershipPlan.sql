CREATE TABLE [dbo].[GymMemberMembershipPlan](
	[GymMemberMembershipPlanId] [bigint] IDENTITY(1,1) NOT NULL,
	[GymMembershipPlanId] [int] NOT NULL,
	[GymMemberDetailId] [int] NOT NULL,
	[PlanStartDate] [date] NULL,
	[PlanDurationExpirationDate] [date] NULL,
	[RemainingSessionCount] [smallint] NULL,
	[PlanAmount] [money] NOT NULL,
	[DiscountAmount] [money] NOT NULL,
	[PaymentTypeEnumId] [int] NOT NULL,
	[TransactionReference] [nvarchar](200) NULL,
	[Remark] [nvarchar](200) NULL,
	[IsExpired] [bit] NOT NULL,
	[IsTransfered] [bit] NOT NULL,
	[TransferedGymMemberDetailId] [int] NULL,
	[SalesInvoiceMasterId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMemberMembershipPlan] PRIMARY KEY CLUSTERED 
(
	[GymMemberMembershipPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] ADD  CONSTRAINT [DF_GymMemberMembershipPlan_RemainingSessionCount]  DEFAULT ((0)) FOR [RemainingSessionCount]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] ADD  CONSTRAINT [DF_GymMemberMembershipPlan_DiscountAmount]  DEFAULT ((0)) FOR [DiscountAmount]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] ADD  CONSTRAINT [DF_GymMemberMembershipPlan_IsExpired]  DEFAULT ((0)) FOR [IsExpired]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] ADD  CONSTRAINT [DF_GymMemberMembershipPlan_IsActive]  DEFAULT ((0)) FOR [IsTransfered]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] ADD  CONSTRAINT [DF_GymMemberMembershipPlan_SalesInvoiceMasterId]  DEFAULT ((0)) FOR [SalesInvoiceMasterId]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan]  WITH CHECK ADD  CONSTRAINT [FK_GymMemberMembershipPlan_GymMemberDetails] FOREIGN KEY([GymMemberDetailId])
REFERENCES [dbo].[GymMemberDetails] ([GymMemberDetailId])
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] CHECK CONSTRAINT [FK_GymMemberMembershipPlan_GymMemberDetails]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan]  WITH CHECK ADD  CONSTRAINT [FK_GymMemberMembershipPlan_GymMembershipPlan] FOREIGN KEY([GymMembershipPlanId])
REFERENCES [dbo].[GymMembershipPlan] ([GymMembershipPlanId])
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] CHECK CONSTRAINT [FK_GymMemberMembershipPlan_GymMembershipPlan]
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan]  WITH CHECK ADD  CONSTRAINT [FK_GymMemberMembershipPlan_SalesInvoiceMaster] FOREIGN KEY([SalesInvoiceMasterId])
REFERENCES [dbo].[SalesInvoiceMaster] ([SalesInvoiceMasterId])
GO

ALTER TABLE [dbo].[GymMemberMembershipPlan] CHECK CONSTRAINT [FK_GymMemberMembershipPlan_SalesInvoiceMaster]
GO
