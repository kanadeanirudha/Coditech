CREATE TABLE [dbo].[GeneralLocationMaster](
	[GeneralLocationMasterId] [int] IDENTITY(1,1) NOT NULL,
	[LocationAddress] [nvarchar](500) NOT NULL,
	[DefaultFlag] [bit] NOT NULL,
	[RegionId] [int] NOT NULL,
	[PostCode] [char](10) NOT NULL,
	[CityId] [int] NOT NULL,
	[Latitude] [varchar](20) NULL,
	[Longitude] [varchar](20) NULL,
	[IsUserDefined] [bit] NOT NULL,
	[IsProviance] [bit] NOT NULL,
	[IsTahsil] [bit] NOT NULL,
	[Accuracy] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralLocationMasterId] PRIMARY KEY CLUSTERED 
(
	[GeneralLocationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralLocationMaster] ADD  DEFAULT ((0)) FOR [DefaultFlag]
GO

ALTER TABLE [dbo].[GeneralLocationMaster] ADD  DEFAULT ((0)) FOR [IsUserDefined]
GO
