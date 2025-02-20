CREATE TABLE [dbo].[InventoryProductDimensionGroupMapper](
	[InventoryProductDimensionGroupMapperId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryProductDimensionGroupId] [int] NOT NULL,
	[InventoryProductDimensionId] [tinyint] NOT NULL,
	[ForPurchase] [bit] NOT NULL,
	[ForSale] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryProductDimensionGroupMapper] PRIMARY KEY CLUSTERED 
(
	[InventoryProductDimensionGroupMapperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
