CREATE TABLE [dbo].[OrganisationCentrewiseBuildingMaster](
	[OrganisationCentrewiseBuildingMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[BuildingName] [nvarchar](100) NOT NULL,
	[Area] [smallint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseBuildingMaster] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseBuildingMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
