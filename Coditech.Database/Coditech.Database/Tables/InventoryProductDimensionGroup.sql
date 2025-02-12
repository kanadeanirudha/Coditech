CREATE TABLE [dbo].[InventoryProductDimensionGroup](
	[InventoryProductDimensionGroupId] [int] IDENTITY(1,1) NOT NULL,
	[ProductDimensionGroupName] [varchar](100) NOT NULL,
	[ProductDimensionGroupCode] [varchar](100) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryProductDimensionGroup] PRIMARY KEY CLUSTERED 
(
	[InventoryProductDimensionGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
