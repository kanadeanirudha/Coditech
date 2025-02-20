CREATE TABLE [dbo].[SalesInvoiceGSTMaster](
	[SalesInvoiceGSTMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[SalesInvoiceMasterId] [bigint] NOT NULL,
	[AcknowledgementNo] [varchar](50) NOT NULL,
	[AcknowledgementDate] [varchar](50) NOT NULL,
	[Irn] [varchar](200) NOT NULL,
	[QrCodeImage] [varchar](max) NULL,
	[IsCancelledEInvoice] [bit] NOT NULL,
	[CancelledEInvoiceDate] [varchar](200) NULL,
	[CancelledEInvoiceReason] [varchar](200) NULL,
	[CancelledEInvoiceDescription] [varchar](200) NULL,
	[EwbNo] [varchar](200) NULL,
	[EwbDt] [varchar](200) NULL,
	[EwbValidTill] [varchar](200) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_SalesInvoiceGSTMaster] PRIMARY KEY CLUSTERED 
(
	[SalesInvoiceGSTMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SalesInvoiceGSTMaster]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceGSTMaster_SalesInvoiceMaster] FOREIGN KEY([SalesInvoiceMasterId])
REFERENCES [dbo].[SalesInvoiceMaster] ([SalesInvoiceMasterId])
GO

ALTER TABLE [dbo].[SalesInvoiceGSTMaster] CHECK CONSTRAINT [FK_SalesInvoiceGSTMaster_SalesInvoiceMaster]
GO
