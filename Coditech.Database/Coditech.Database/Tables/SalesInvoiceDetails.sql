CREATE TABLE [dbo].[SalesInvoiceDetails](
	[SalesInvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SalesInvoiceMasterId] [bigint] NOT NULL,
	[InventoryGeneralItemLineId] [bigint] NOT NULL,
	[ItemQuantity] [decimal](10, 3) NULL,
	[ItemAmount] [money] NOT NULL,
	[ItemTaxAmount] [money] NOT NULL,
	[ItemShippingAmount] [money] NOT NULL,
	[GeneralTaxGroupMasterId] [tinyint] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SalesInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[SalesInvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] ADD  CONSTRAINT [DF_SalesInvoiceDetails_DiscountAmount]  DEFAULT ((0)) FOR [ItemAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] ADD  CONSTRAINT [DF_SalesInvoiceDetails_TaxAmount]  DEFAULT ((0)) FOR [ItemTaxAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] ADD  CONSTRAINT [DF_SalesInvoiceDetails_ShippingAmount]  DEFAULT ((0)) FOR [ItemShippingAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_GeneralTaxGroupMaster] FOREIGN KEY([GeneralTaxGroupMasterId])
REFERENCES [dbo].[GeneralTaxGroupMaster] ([GeneralTaxGroupMasterId])
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_GeneralTaxGroupMaster]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_InventoryGeneralItemLine] FOREIGN KEY([InventoryGeneralItemLineId])
REFERENCES [dbo].[InventoryGeneralItemLine] ([InventoryGeneralItemLineId])
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_InventoryGeneralItemLine]
GO

ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoiceMaster] FOREIGN KEY([SalesInvoiceMasterId])
REFERENCES [dbo].[SalesInvoiceMaster] ([SalesInvoiceMasterId])
GO

ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoiceMaster]
GO
