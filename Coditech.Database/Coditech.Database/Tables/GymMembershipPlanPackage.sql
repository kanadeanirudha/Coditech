CREATE TABLE [dbo].[GymMembershipPlanPackage](
	[GymMembershipPlanPackageId] [int] IDENTITY(1,1) NOT NULL,
	[GymMembershipPlanId] [int] NOT NULL,
	[InventoryGeneralItemLineId] [bigint] NOT NULL,
	[ServiceCost] [money] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMembershipPlanPackage] PRIMARY KEY CLUSTERED 
(
	[GymMembershipPlanPackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymMembershipPlanPackage]  WITH CHECK ADD  CONSTRAINT [FK_GymMembershipPlanPackage_GymMembershipPlan] FOREIGN KEY([GymMembershipPlanId])
REFERENCES [dbo].[GymMembershipPlan] ([GymMembershipPlanId])
GO

ALTER TABLE [dbo].[GymMembershipPlanPackage] CHECK CONSTRAINT [FK_GymMembershipPlanPackage_GymMembershipPlan]
GO

ALTER TABLE [dbo].[GymMembershipPlanPackage]  WITH CHECK ADD  CONSTRAINT [FK_GymMembershipPlanPackage_InventoryGeneralItemLine] FOREIGN KEY([InventoryGeneralItemLineId])
REFERENCES [dbo].[InventoryGeneralItemLine] ([InventoryGeneralItemLineId])
GO

ALTER TABLE [dbo].[GymMembershipPlanPackage] CHECK CONSTRAINT [FK_GymMembershipPlanPackage_InventoryGeneralItemLine]
GO
