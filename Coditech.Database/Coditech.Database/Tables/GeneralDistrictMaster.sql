CREATE TABLE [dbo].[GeneralDistrictMaster](
	[GeneralDistrictMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[DistrictName] [nvarchar](200) NOT NULL,
	[GeneralRegionMasterId] [smallint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralDistrictMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralDistrictMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO