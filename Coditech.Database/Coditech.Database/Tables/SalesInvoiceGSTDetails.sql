CREATE TABLE [dbo].[SalesInvoiceGSTDetails](
	[SalesInvoiceGSTDetailsId] [bigint] NOT NULL,
	[SalesInvoiceGSTMasterId] [bigint] NOT NULL,
	[SalesInvoiceMasterId] [bigint] NOT NULL,
	[GSTEInvoiceDetails] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SalesInvoiceGSTDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceGSTDetails_SalesInvoiceGSTMaster] FOREIGN KEY([SalesInvoiceGSTMasterId])
REFERENCES [dbo].[SalesInvoiceGSTMaster] ([SalesInvoiceGSTMasterId])
GO

ALTER TABLE [dbo].[SalesInvoiceGSTDetails] CHECK CONSTRAINT [FK_SalesInvoiceGSTDetails_SalesInvoiceGSTMaster]
GO

ALTER TABLE [dbo].[SalesInvoiceGSTDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceGSTDetails_SalesInvoiceMaster] FOREIGN KEY([SalesInvoiceMasterId])
REFERENCES [dbo].[SalesInvoiceMaster] ([SalesInvoiceMasterId])
GO

ALTER TABLE [dbo].[SalesInvoiceGSTDetails] CHECK CONSTRAINT [FK_SalesInvoiceGSTDetails_SalesInvoiceMaster]
GO
