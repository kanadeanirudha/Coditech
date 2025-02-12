CREATE TABLE [dbo].[SalesInvoiceMaster](
	[SalesInvoiceMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[InvoiceNumber] [nvarchar](200) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[UserType] [varchar](15) NOT NULL,
	[NetAmount] [money] NOT NULL,
	[TaxAmount] [money] NOT NULL,
	[ShippingAmount] [money] NOT NULL,
	[DiscountAmount] [money] NOT NULL,
	[BillAmount] [money] NOT NULL,
	[TotalAmount] [money] NOT NULL,
	[RoundUpAmount] [money] NOT NULL,
	[AccountBalanceSheetId] [smallint] NOT NULL,
	[PaymentTypeEnumId] [int] NULL,
	[TransactionReference] [nvarchar](500) NULL,
	[Remark] [nvarchar](200) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SalesInvoiceMaster] PRIMARY KEY CLUSTERED 
(
	[SalesInvoiceMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SalesInvoiceMaster] ADD  CONSTRAINT [DF_SalesInvoiceMaster_TaxAmount]  DEFAULT ((0)) FOR [TaxAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceMaster] ADD  CONSTRAINT [DF_SalesInvoiceMaster_ShippingAmount]  DEFAULT ((0)) FOR [ShippingAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceMaster] ADD  CONSTRAINT [DF_SalesInvoiceMaster_DiscountAmount]  DEFAULT ((0)) FOR [DiscountAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceMaster] ADD  CONSTRAINT [DF_SalesInvoiceMaster_RoundUpAmount]  DEFAULT ((0)) FOR [RoundUpAmount]
GO

ALTER TABLE [dbo].[SalesInvoiceMaster] ADD  CONSTRAINT [DF_SalesInvoiceMaster_AccountBalanceSheetId]  DEFAULT ((0)) FOR [AccountBalanceSheetId]
GO
