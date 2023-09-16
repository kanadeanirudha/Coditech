CREATE TABLE [dbo].[AdminRoleApplicableDetails](
	[AdminRoleApplicableDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DesignationId] [smallint] NOT NULL,
	[WorkFromDate] [datetime] NULL,
	[WorkToDate] [datetime] NULL,
	[RoleType] [varchar](10) NOT NULL,
	[Reason] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkAdminRoleApplicableDetailsId] PRIMARY KEY CLUSTERED 
(
	[AdminRoleApplicableDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[AdminRoleApplicableDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleApplicableDetails_AdminRoleApplicableDetails] FOREIGN KEY([AdminRoleApplicableDetailId])
REFERENCES [dbo].[AdminRoleApplicableDetails] ([AdminRoleApplicableDetailId])
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] CHECK CONSTRAINT [FK_AdminRoleApplicableDetails_AdminRoleApplicableDetails]
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails]  WITH NOCHECK ADD  CONSTRAINT [CkAdminRoleApplicableDetailsRoleType] CHECK  (([RoleType]='Additional' OR [RoleType]='Regular'))
GO

ALTER TABLE [dbo].[AdminRoleApplicableDetails] NOCHECK CONSTRAINT [CkAdminRoleApplicableDetailsRoleType]
GO


