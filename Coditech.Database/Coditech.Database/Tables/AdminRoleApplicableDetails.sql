CREATE TABLE [dbo].[AdminRoleApplicableDetails](
	[AdminRoleApplicableDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[WorkFromDate] [datetime] NULL,
	[WorkToDate] [datetime] NULL,
	[RoleType] [varchar](10) NOT NULL,
	[Reason] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminRoleApplicableDetails] PRIMARY KEY CLUSTERED 
(
	[AdminRoleApplicableDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] ADD  CONSTRAINT [DF__AdminRole__IsAct__50FB042B]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleApplicableDetails_AdminRoleMaster] FOREIGN KEY([AdminRoleMasterId])
REFERENCES [dbo].[AdminRoleMaster] ([AdminRoleMasterId])
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] CHECK CONSTRAINT [FK_AdminRoleApplicableDetails_AdminRoleMaster]
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleApplicableDetails_EmployeeMaster] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[EmployeeMaster] ([EmployeeId])
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] CHECK CONSTRAINT [FK_AdminRoleApplicableDetails_EmployeeMaster]
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails]  WITH NOCHECK ADD  CONSTRAINT [CkAdminRoleApplicableDetailsRoleType] CHECK  (([RoleType]='Additional' OR [RoleType]='Regular'))
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] NOCHECK CONSTRAINT [CkAdminRoleApplicableDetailsRoleType]
GO