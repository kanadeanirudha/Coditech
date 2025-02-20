CREATE TABLE [dbo].[GymMemberDetails](
	[GymMemberDetailId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[PersonCode] [nvarchar](200) NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[PastInjuries] [nvarchar](500) NULL,
	[MedicalHistory] [nvarchar](500) NULL,
	[GymGroupEnumId] [int] NULL,
	[SourceEnumId] [int] NULL,
	[OtherInformation] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GymMemberDetails] PRIMARY KEY CLUSTERED 
(
	[GymMemberDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GymMemberDetails] ADD  CONSTRAINT [DF_GymMemberDetails_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[GymMemberDetails]  WITH CHECK ADD  CONSTRAINT [FK_GymMemberDetails_GeneralPerson] FOREIGN KEY([PersonId])
REFERENCES [dbo].[GeneralPerson] ([PersonId])
GO

ALTER TABLE [dbo].[GymMemberDetails] CHECK CONSTRAINT [FK_GymMemberDetails_GeneralPerson]
GO

ALTER TABLE [dbo].[GymMemberDetails]  WITH CHECK ADD  CONSTRAINT [FK_GymMemberDetails_OrganisationCentreMaster] FOREIGN KEY([CentreCode])
REFERENCES [dbo].[OrganisationCentreMaster] ([CentreCode])
GO

ALTER TABLE [dbo].[GymMemberDetails] CHECK CONSTRAINT [FK_GymMemberDetails_OrganisationCentreMaster]
GO
