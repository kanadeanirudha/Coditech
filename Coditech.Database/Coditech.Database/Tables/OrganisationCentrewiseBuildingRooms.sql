CREATE TABLE [dbo].[OrganisationCentrewiseBuildingRooms](
	[OrganisationCentrewiseBuildingRoomId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationCentrewiseBuildingMasterId] [int] NOT NULL,
	[BuildingFloorEnumId] [int] NOT NULL,
	[RoomName] [nvarchar](100) NOT NULL,
	[Area] [smallint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrganisationCentrewiseBuildingRooms] PRIMARY KEY CLUSTERED 
(
	[OrganisationCentrewiseBuildingRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
