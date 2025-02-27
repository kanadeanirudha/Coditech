CREATE TABLE [dbo].[GeneralPerson](
	[PersonId] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonTitle] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[GenderEnumId] [int] NOT NULL,
	[EmailId] [varchar](250) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[CallingCode] [varchar](5) NULL,
	[MobileNumber] [nvarchar](15) NOT NULL,
	[EmergencyContact] [nvarchar](15) NULL,
	[GeneralNationalityMasterId] [smallint] NULL,
	[MaritalStatus] [varchar](50) NULL,
	[IndentificationNumber] [varchar](50) NULL,
	[IndentificationEnumId] [int] NULL,
	[BloodGroup] [nvarchar](50) NULL,
	[PhotoMediaId] [bigint] NULL,
	[BirthMark] [nvarchar](100) NULL,
	[AttendanceIntegrationId] [varchar](100) NULL,
	[GeneralOccupationMasterId] [smallint] NULL,
	[AnniversaryDate] [datetime] NULL,
	[Custom1] [nvarchar](max) NULL,
	[Custom2] [nvarchar](max) NULL,
	[Custom3] [nvarchar](max) NULL,
	[Custom4] [nvarchar](max) NULL,
	[Custom5] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralPerson] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralPerson] ADD  CONSTRAINT [DF_GeneralPerson_CallingCode]  DEFAULT ((0)) FOR [CallingCode]
GO