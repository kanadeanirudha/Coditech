
CREATE TABLE [dbo].[GeneralTaxMaster](
	[GeneralTaxMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[TaxName] [nvarchar](50) NULL,
	[TaxRate] [money] NULL,
	[SalesGLAccount] [int] NULL,
	[PurchasingGLAccount] [int] NULL,
	[IsCompoundTax] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsOtherState] [bit] NULL,
 CONSTRAINT [PKGeneralTaxMasterID] PRIMARY KEY CLUSTERED 
(
	[GeneralTaxMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UKGeneralTaxMasterTaxName] UNIQUE NONCLUSTERED 
(
	[TaxName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
