CREATE TABLE [dbo].[InventoryStorageDimensionGroupMapper](
	[InventoryStorageDimensionGroupMapperId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryStorageDimensionGroupId] [int] NOT NULL,
	[InventoryStorageDimensionId] [smallint] NOT NULL,
	[Active] [bit] NOT NULL,
	[BlankReceiptAllowed] [bit] NOT NULL,
	[BlankIssueAllowed] [bit] NOT NULL,
	[CoveragePlanByDimension] [bit] NOT NULL,
	[FinancialInventory] [bit] NOT NULL,
	[ForPurchasePrices] [bit] NOT NULL,
	[ForSalePrices] [bit] NOT NULL,
	[PhysicalInventory] [bit] NOT NULL,
	[PrimaryStocking] [bit] NOT NULL,
	[Reference] [nvarchar](200) NOT NULL,
	[Transfer] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryStorageDimensionGroupMapper] PRIMARY KEY CLUSTERED 
(
	[InventoryStorageDimensionGroupMapperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
