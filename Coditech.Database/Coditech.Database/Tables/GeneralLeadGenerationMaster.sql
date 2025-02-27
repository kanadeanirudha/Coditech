CREATE TABLE [dbo].[GeneralLeadGenerationMaster](
	[GeneralLeadGenerationMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NULL,
	[UserTypeCode] [varchar](30) NULL,
	[PersonTitle] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NULL,
	[GenderEnumId] [int] NOT NULL,
	[EmailId] [varchar](250) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[MobileNumber] [nvarchar](15) NOT NULL,
	[LeadGenerationSourceEnumId] [int] NOT NULL,
	[LeadGenerationCategoryEnumIds] [varchar](max) NOT NULL,
	[LeadGenerationStatusEnumId] [int] NOT NULL,
	[Comments] [nvarchar](500) NULL,
	[Location] [nvarchar](500) NULL,
	[IsConverted] [bit] NOT NULL,
	[Custom1] [nvarchar](max) NULL,
	[Custom2] [nvarchar](max) NULL,
	[Custom3] [nvarchar](max) NULL,
	[Custom4] [nvarchar](max) NULL,
	[Custom5] [nvarchar](max) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_LeadGenerationMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralLeadGenerationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralLeadGenerationMaster] ADD  CONSTRAINT [DF_LeadGenerationMaster_IsConverted]  DEFAULT ((0)) FOR [IsConverted]
GO
