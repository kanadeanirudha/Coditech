
CREATE TABLE [dbo].[GeneralTaxGroupMaster](
	[GeneralTaxGroupMasterId] [tinyint] IDENTITY(1,1) NOT NULL,
	[TaxGroupName] [nvarchar](50) NULL,
	[TaxGroupRate] [money] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralTaxGroupMasterID] PRIMARY KEY CLUSTERED 
(
	[GeneralTaxGroupMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

