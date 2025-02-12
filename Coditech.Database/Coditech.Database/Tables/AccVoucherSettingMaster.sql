
CREATE TABLE [dbo].[AccVoucherSettingMaster](
	[AccVoucherSettingMasterId] [int] IDENTITY(1,1) NOT NULL,
	[AltSetupGLID] [int] NULL,
	[AccSetupBalanceSheetID] [int] NOT NULL,
	[TransactionType] [varchar](1) NOT NULL,
	[TransactionTypeCode] [varchar](10) NULL,
	[VoucherNumber] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKAccVoucherSettingMasterId] PRIMARY KEY CLUSTERED 
(
	[AccVoucherSettingMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccVoucherSettingMaster] ADD  CONSTRAINT [DEF_AccVoucherSettingMaster_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

