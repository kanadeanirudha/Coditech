CREATE TABLE [dbo].[HospitalPatientRegistration](
	[HospitalPatientRegistrationId] [bigint] IDENTITY(1,1) NOT NULL,
	[HospitalPatientTypeId] [tinyint] NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[UAHNumber] [varchar](100) NOT NULL,
	[UserType] [varchar](10) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[HospitalPatientFirstVisitId] [bigint] NULL,
	[HospitalPatientLastVisitId] [bigint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPatientRegistration] PRIMARY KEY CLUSTERED 
(
	[HospitalPatientRegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
