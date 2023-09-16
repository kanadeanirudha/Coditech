CREATE TABLE [dbo].[GeneralTaxGroupMasterDetails](
	[GeneralTaxGroupMasterDetailsId] [smallint] IDENTITY(1,1) NOT NULL,
	[GenTaxGroupMasterId] [tinyint] NOT NULL,
	[GenTaxMasterId] [smallint] NOT NULL,
	[IsOtherState] [bit] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PKGeneralTaxGroupMasterDetailsId] PRIMARY KEY CLUSTERED 
(
	[GeneralTaxGroupMasterDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralTaxGroupMasterDetails]  WITH CHECK ADD  CONSTRAINT [FKGeneralTaxGroupMasterDetailsGenTaxGroupMasterId] FOREIGN KEY([GenTaxGroupMasterId])
REFERENCES [dbo].[GeneralTaxGroupMaster] ([GeneralTaxGroupMasterId])
GO

ALTER TABLE [dbo].[GeneralTaxGroupMasterDetails] CHECK CONSTRAINT [FKGeneralTaxGroupMasterDetailsGenTaxGroupMasterId]
GO

ALTER TABLE [dbo].[GeneralTaxGroupMasterDetails]  WITH CHECK ADD  CONSTRAINT [FKGeneralTaxGroupMasterDetailsGenTaxMasterId] FOREIGN KEY([GenTaxMasterId])
REFERENCES [dbo].[GeneralTaxMaster] ([GeneralTaxMasterId])
GO

ALTER TABLE [dbo].[GeneralTaxGroupMasterDetails] CHECK CONSTRAINT [FKGeneralTaxGroupMasterDetailsGenTaxMasterId]
GO


