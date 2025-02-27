CREATE TABLE [dbo].[GymMembershipPlan](
	[GymMembershipPlanId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[PlanTypeEnumId] [int] NOT NULL,
	[MembershipPlanName] [nvarchar](100) NOT NULL,
	[MaxCost] [money] NOT NULL,
	[MinCost] [money] NOT NULL,
	[PlanDurationTypeEnumId] [int] NULL,
	[PlanDurationInMonth] [tinyint] NULL,
	[PlanDurationInDays] [smallint] NULL,
	[PlanDurationInSession] [smallint] NULL,
	[IsRenewable] [bit] NOT NULL,
	[IsTimebaseBiometricAccess] [bit] NOT NULL,
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[IsActive] [bit] NOT NULL,
	[IsTaxExclusive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMembershipPlan] PRIMARY KEY CLUSTERED 
(
	[GymMembershipPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymMembershipPlan] ADD  CONSTRAINT [DF_GymMembershipPlan_IsRenewable]  DEFAULT ((0)) FOR [IsRenewable]
GO

ALTER TABLE [dbo].[GymMembershipPlan] ADD  CONSTRAINT [DF_GymMembershipPlan_IsTimebaseBiometricAccess]  DEFAULT ((0)) FOR [IsTimebaseBiometricAccess]
GO

ALTER TABLE [dbo].[GymMembershipPlan] ADD  CONSTRAINT [DF_GymMembershipPlan_IsTaxExclusive]  DEFAULT ((0)) FOR [IsTaxExclusive]
GO

ALTER TABLE [dbo].[GymMembershipPlan]  WITH CHECK ADD  CONSTRAINT [FK_GymMembershipPlan_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[GymMembershipPlan] CHECK CONSTRAINT [FK_GymMembershipPlan_OrganisationCentreMaster]
GO
