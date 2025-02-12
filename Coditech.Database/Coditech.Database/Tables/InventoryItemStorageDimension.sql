CREATE TABLE [dbo].[InventoryItemStorageDimension](
	[InventoryItemStorageDimensionId] [smallint] IDENTITY(1,1) NOT NULL,
	[StorageDimensionName] [varchar](100) NOT NULL,
	[StorageDimensionCode] [varchar](100) NOT NULL,
	[ParentInventoryItemStorageDimensionId] [smallint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryItemStorageDimension] PRIMARY KEY CLUSTERED 
(
	[InventoryItemStorageDimensionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
