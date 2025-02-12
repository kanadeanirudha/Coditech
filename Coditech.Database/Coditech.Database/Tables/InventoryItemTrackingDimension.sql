CREATE TABLE [dbo].[InventoryItemTrackingDimension](
	[InventoryItemTrackingDimensionId] [smallint] IDENTITY(1,1) NOT NULL,
	[TrackingDimensionName] [varchar](100) NOT NULL,
	[TrackingDimensionCode] [varchar](100) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryItemTrackingDimension] PRIMARY KEY CLUSTERED 
(
	[InventoryItemTrackingDimensionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
