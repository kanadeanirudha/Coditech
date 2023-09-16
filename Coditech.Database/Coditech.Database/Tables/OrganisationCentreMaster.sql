CREATE TABLE [dbo].[OrganisationCentreMaster](
	[OrganisationCentreMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[CentreName] [nvarchar](100) NOT NULL,
	[HoCoRoScFlag] [varchar](5) NULL,
	[HoId] [int] NULL,
	[CoId] [int] NULL,
	[RoId] [int] NULL,
	[CentreSpecialization] [nvarchar](100) NULL,
	[CentreAddress] [nvarchar](100) NOT NULL,
	[GeneralCityMasterId] [int] NOT NULL,
	[Pincode] [nvarchar](50) NOT NULL,
	[EmailId] [varchar](70) NOT NULL,
	[Url] [varchar](30) NULL,
	[CellPhone] [nvarchar](50) NOT NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[PhoneNumberOffice] [nvarchar](50) NULL,
	[CentreEstablishmentDatetime] [datetime] NULL,
	[OrganisationId] [tinyint] NOT NULL,
	[CentreLoginNumber] [int] NULL,
	[InstituteCode] [nvarchar](50) NULL,
	[TimeZone] [varchar](32) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[CampusArea] [float] NULL,
	[CINNumber] [nvarchar](25) NULL,
	[GSTINNumber] [nvarchar](25) NULL,
	[PanNumber] [nvarchar](25) NULL,
	[PFNumber] [nvarchar](35) NULL,
	[ESICNumber] [nvarchar](35) NULL,
	[WaterMark] [nvarchar](35) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKOrganisationCentreMasterId] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentreMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKOrganisationCentreMasterCentreCode] UNIQUE NONCLUSTERED 
(
	[CentreCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrganisationCentreMaster]  WITH CHECK ADD  CONSTRAINT [FKOrganisationCentreMasterCityId] FOREIGN KEY([GeneralCityMasterId])
REFERENCES [dbo].[GeneralCityMaster] ([GeneralCityMasterId])
GO

ALTER TABLE [dbo].[OrganisationCentreMaster] CHECK CONSTRAINT [FKOrganisationCentreMasterCityId]
GO

ALTER TABLE [dbo].[OrganisationCentreMaster]  WITH CHECK ADD  CONSTRAINT [FKOrganisationCentreMasterOrganisationId] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[OrganisationMaster] ([OrganisationMasterId])
GO

ALTER TABLE [dbo].[OrganisationCentreMaster] CHECK CONSTRAINT [FKOrganisationCentreMasterOrganisationId]
GO


