CREATE TABLE [dbo].[HospitalRegistrationFee](
	[HospitalRegistrationFeeId] [int] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[InventoryGeneralItemLineId] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[UptoDate] [date] NULL,
	[Charges] [money] NOT NULL,
	[IsTaxExclusive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalRegistrationFee] PRIMARY KEY CLUSTERED 
(
	[HospitalRegistrationFeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalRegistrationFee] ADD  CONSTRAINT [DF_HospitalRegistrationFee_InventoryGeneralItemLineId]  DEFAULT ((0)) FOR [InventoryGeneralItemLineId]
GO

ALTER TABLE [dbo].[HospitalRegistrationFee] ADD  CONSTRAINT [DF_HospitalRegistrationFee_IsTaxExclusive]  DEFAULT ((0)) FOR [IsTaxExclusive]
GO
