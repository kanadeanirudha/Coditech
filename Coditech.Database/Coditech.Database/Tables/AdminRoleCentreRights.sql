CREATE TABLE [dbo].[AdminRoleCentreRights](
	[AdminRoleCentreRightId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT 1,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkAdmRlCntId] PRIMARY KEY CLUSTERED 
(
	[AdminRoleCentreRightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[AdminRoleCentreRights]  WITH CHECK ADD  CONSTRAINT [FKAdminRoleCentreRightsAdminRoleMasterId] FOREIGN KEY([AdminRoleMasterId])
REFERENCES [dbo].[AdminRoleMaster] ([AdminRoleMasterId])
GO

ALTER TABLE [dbo].[AdminRoleCentreRights] CHECK CONSTRAINT [FKAdminRoleCentreRightsAdminRoleMasterId]
GO

