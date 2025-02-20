CREATE TABLE [dbo].[HospitalPathologyTestPrices](
	[HospitalPathologyTestPricesId] [bigint] IDENTITY(1,1) NOT NULL,
	[HospitalPathologyPriceCategoryEnumId] [int] NOT NULL,
	[HospitalPathologyTestId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPathologyTestPrices] PRIMARY KEY CLUSTERED 
(
	[HospitalPathologyTestPricesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalPathologyTestPrices] ADD  CONSTRAINT [DF_HospitalPathologyTestPrices_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
