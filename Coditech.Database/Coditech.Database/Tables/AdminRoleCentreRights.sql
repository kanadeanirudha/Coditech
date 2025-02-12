CREATE TABLE [dbo].[AdminRoleCentreRights](
	[AdminRoleCentreRightId] [int] IDENTITY(1,1) NOT NULL,
	[AdminRoleMasterId] [int] NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdminRoleCentreRights] PRIMARY KEY CLUSTERED 
(
	[AdminRoleCentreRightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdminRoleCentreRights] ADD  CONSTRAINT [DF__AdminRole__IsAct__4959E263]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[AdminRoleCentreRights]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleCentreRights_AdminRoleMaster] FOREIGN KEY([AdminRoleMasterId])
REFERENCES [dbo].[AdminRoleMaster] ([AdminRoleMasterId])
GO

ALTER TABLE [dbo].[AdminRoleCentreRights] CHECK CONSTRAINT [FK_AdminRoleCentreRights_AdminRoleMaster]
GO

ALTER TABLE [dbo].[AdminRoleCentreRights]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleCentreRights_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[AdminRoleCentreRights] CHECK CONSTRAINT [FK_AdminRoleCentreRights_OrganisationCentreMaster]
GO
