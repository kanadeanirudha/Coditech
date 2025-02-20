CREATE TABLE [dbo].[InventoryItemModelGroup](
	[InventoryItemModelGroupId] [smallint] IDENTITY(1,1) NOT NULL,
	[ItemModelGroupName] [varchar](100) NOT NULL,
	[ItemModelGroupCode] [varchar](100) NOT NULL,
	[InventoryModelEnumId] [int] NOT NULL,
	[StockedProduct] [bit] NOT NULL,
	[PostPhysicalInventory] [bit] NOT NULL,
	[PostFinancialInventory] [bit] NOT NULL,
	[IsIncludePhysicalValue] [bit] NOT NULL,
	[IsFixedReceiptPrice] [bit] NOT NULL,
	[PostDeferredRevenueAccountOnSale] [bit] NOT NULL,
	[AccrueLiabilityOnProductReceipt] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryItemModelGroup] PRIMARY KEY CLUSTERED 
(
	[InventoryItemModelGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
