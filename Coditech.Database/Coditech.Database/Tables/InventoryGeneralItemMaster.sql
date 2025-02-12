CREATE TABLE [dbo].[InventoryGeneralItemMaster](
	[InventoryGeneralItemMasterId] [int] IDENTITY(1,1) NOT NULL,
	[InventoryCategoryId] [smallint] NOT NULL,
	[ProductTypeEnumId] [int] NOT NULL,
	[ItemNumber] [varchar](50) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[ItemDescription] [nvarchar](100) NULL,
	[HSNSACCode] [varchar](20) NULL,
	[ProductSubTypeEnumId] [int] NULL,
	[GeneralTaxGroupMasterId] [tinyint] NOT NULL,
	[InventoryModelEnumId] [int] NULL,
	[InventoryProductDimentionGroupId] [int] NULL,
	[InventoryItemGroupId] [int] NULL,
	[InventoryStorageDimentionGroupId] [int] NULL,
	[InventoryTrackingDimentionGroupId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryGeneralItemMaster] PRIMARY KEY CLUSTERED 
(
	[InventoryGeneralItemMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InventoryGeneralItemMaster] ADD  CONSTRAINT [DF_InventoryGeneralItemMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[InventoryGeneralItemMaster]  WITH CHECK ADD  CONSTRAINT [FK_InventoryGeneralItemMaster_GeneralTaxGroupMaster] FOREIGN KEY([GeneralTaxGroupMasterId])
REFERENCES [dbo].[GeneralTaxGroupMaster] ([GeneralTaxGroupMasterId])
GO

ALTER TABLE [dbo].[InventoryGeneralItemMaster] CHECK CONSTRAINT [FK_InventoryGeneralItemMaster_GeneralTaxGroupMaster]
GO

ALTER TABLE [dbo].[InventoryGeneralItemMaster]  WITH CHECK ADD  CONSTRAINT [FK_InventoryGeneralItemMaster_InventoryCategory] FOREIGN KEY([InventoryCategoryId])
REFERENCES [dbo].[InventoryCategory] ([InventoryCategoryId])
GO

ALTER TABLE [dbo].[InventoryGeneralItemMaster] CHECK CONSTRAINT [FK_InventoryGeneralItemMaster_InventoryCategory]
GO
