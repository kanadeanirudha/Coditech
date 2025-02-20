CREATE TABLE [dbo].[InventoryGeneralItemLine](
	[InventoryGeneralItemLineId] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryGeneralItemMasterId] [int] NOT NULL,
	[SKU] [varchar](100) NOT NULL,
	[ItemName] [nvarchar](200) NOT NULL,
	[BarCode] [varchar](20) NULL,
	[Price] [money] NOT NULL,
	[IsBaseUom] [bit] NOT NULL,
	[InventoryBaseUoMMasterId] [smallint] NULL,
	[InventoryLowerLevelUoMMasterId] [smallint] NULL,
	[ConversionFactor] [decimal](10, 4) NULL,
	[IsOrderingUnit] [bit] NOT NULL,
	[IsSaleUnit] [bit] NOT NULL,
	[IsIssueUnit] [bit] NOT NULL,
	[Length] [decimal](18, 4) NOT NULL,
	[Width] [decimal](18, 4) NOT NULL,
	[Height] [decimal](18, 4) NOT NULL,
	[Volume] [decimal](18, 4) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryGeneralItemLine] PRIMARY KEY CLUSTERED 
(
	[InventoryGeneralItemLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_Price]  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_IsBaseUom]  DEFAULT ((1)) FOR [IsBaseUom]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_IsOrderingUnit]  DEFAULT ((0)) FOR [IsOrderingUnit]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_Table_1_IsOrderingUnit1]  DEFAULT ((0)) FOR [IsSaleUnit]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_IsIssueUnit]  DEFAULT ((0)) FOR [IsIssueUnit]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_Length]  DEFAULT ((0)) FOR [Length]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_Width]  DEFAULT ((0)) FOR [Width]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_Height]  DEFAULT ((0)) FOR [Height]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_Volume]  DEFAULT ((0)) FOR [Volume]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] ADD  CONSTRAINT [DF_InventoryGeneralItemLine_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine]  WITH CHECK ADD  CONSTRAINT [FK_InventoryGeneralItemLine_InventoryGeneralItemMaster] FOREIGN KEY([InventoryGeneralItemMasterId])
REFERENCES [dbo].[InventoryGeneralItemMaster] ([InventoryGeneralItemMasterId])
GO

ALTER TABLE [dbo].[InventoryGeneralItemLine] CHECK CONSTRAINT [FK_InventoryGeneralItemLine_InventoryGeneralItemMaster]
GO
