CREATE TABLE [dbo].[InventoryProductDimension](
	[InventoryProductDimensionId] [tinyint] IDENTITY(1,1) NOT NULL,
	[ProductDimensionName] [varchar](50) NOT NULL,
	[ProductDimensionCode] [varchar](50) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryProductDimension] PRIMARY KEY CLUSTERED 
(
	[InventoryProductDimensionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
