CREATE TABLE [dbo].[HospitalDoctorVisitingCharges](
	[HospitalDoctorVisitingChargesId] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryGeneralItemLineId] [int] NOT NULL,
	[HospitalDoctorId] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[UptoDate] [date] NULL,
	[AppointmentTypeEnumId] [int] NOT NULL,
	[Charges] [money] NOT NULL,
	[IsTaxExclusive] [bit] NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalDoctorVisitingCharges] PRIMARY KEY CLUSTERED 
(
	[HospitalDoctorVisitingChargesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalDoctorVisitingCharges] ADD  CONSTRAINT [DF_HospitalDoctorVisitingCharges_InventoryGeneralItemLineId]  DEFAULT ((0)) FOR [InventoryGeneralItemLineId]
GO

ALTER TABLE [dbo].[HospitalDoctorVisitingCharges] ADD  CONSTRAINT [DF_HospitalDoctorVisitingCharges_IsTaxExclusive]  DEFAULT ((0)) FOR [IsTaxExclusive]
GO
