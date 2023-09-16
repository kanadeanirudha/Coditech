CREATE TABLE [dbo].[OrganisationMaster](
	[OrganisationMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[EstablishmentCode] [nvarchar](120) NULL,
	[OrganisationName] [nvarchar](120) NOT NULL,
	[FoundationDatetime] [datetime] NOT NULL,
	[FounderMember] [nvarchar](60) NOT NULL,
	[Address1] [nvarchar](200) NOT NULL,
	[GeneralCityMasterId] [int] NOT NULL,
	[Pincode] [nvarchar](15) NOT NULL,
	[EmailId] [varchar](60) NOT NULL,
	[Url] [varchar](60) NULL,
	[OfficeComment] [nvarchar](4000) NULL,
	[MissionStatement] [nvarchar](4000) NULL,
	[MobileNumber] [nvarchar](50) NOT NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[OfficePhone1] [nvarchar](50) NULL,
	[OfficePhone2] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[OrganisationCode] [nvarchar](35) NULL,
	[PFNumber] [nvarchar](35) NULL,
	[ESICNumber] [nvarchar](35) NULL,
 CONSTRAINT [PKOrganisationMasterId] PRIMARY KEY CLUSTERED 
(
	[OrganisationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


