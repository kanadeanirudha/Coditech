
CREATE TABLE [dbo].[GeneralTaxGroupMaster](
	[GeneralTaxGroupMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TaxGroupName] [nvarchar](50) NOT NULL,
	[TaxGroupRate] [money] NOT NULL,
	[IsOtherState] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralTaxGroupMaster] PRIMARY KEY CLUSTERED 
(
	[GeneralTaxGroupMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralTaxGroupMaster] ADD  CONSTRAINT [DF_GeneralTaxGroupMaster_IsOtherState]  DEFAULT ((0)) FOR [IsOtherState]
GO