CREATE TABLE [dbo].[OrganisationCentrePrintingFormat](
	[OrganisationCentrePrintingFormatId] [smallint] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[PrintingLine1] [nvarchar](100) NULL,
	[PrintingLine2] [nvarchar](100) NULL,
	[PrintingLine3] [nvarchar](100) NULL,
	[PrintingLine4] [nvarchar](100) NULL,
	[Logo] [varbinary](max) NULL,
	[LogoType] [varchar](50) NULL,
	[LogoFilename] [varchar](50) NULL,
	[LogoFileWidth] [varchar](50) NULL,
	[LogoFileHeight] [varchar](50) NULL,
	[LogoFileSize] [varchar](50) NULL,
	[PrintingLinebelowLogo] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKOrganisationStudyCentrePrintingFormatId] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrePrintingFormatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


