CREATE TABLE [dbo].[InventoryItemTrackingDimensionGroup](
	[InventoryItemTrackingDimensionGroupId] [int] IDENTITY(1,1) NOT NULL,
	[ItemTrackingDimensionGroupName] [nvarchar](100) NULL,
	[ItemTrackingDimensionGroupCode] [nvarchar](100) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryItemTrackingDimensionGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
