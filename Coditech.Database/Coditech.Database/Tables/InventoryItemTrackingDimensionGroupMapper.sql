CREATE TABLE [dbo].[InventoryItemTrackingDimensionGroupMapper](
	[InventoryItemTrackingDimensionGroupMapperId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryItemTrackingDimensionGroupId] [int] NOT NULL,
	[InventoryItemTrackingDimensionId] [smallint] NOT NULL,
	[Active] [bit] NOT NULL,
	[ActiveInSalesProcess] [bit] NOT NULL,
	[PrimaryStocking] [bit] NOT NULL,
	[BlankReceiptAllowed] [bit] NOT NULL,
	[BlankIssueAllowed] [bit] NOT NULL,
	[PhysicalInventory] [bit] NOT NULL,
	[FinancialInventory] [bit] NOT NULL,
	[CoveragePlanByDimension] [bit] NOT NULL,
	[ForPurchasePrices] [bit] NOT NULL,
	[ForSalePrices] [bit] NOT NULL,
	[Transfer] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
	[DisplayOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryItemTrackingDimensionGroupMapperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
