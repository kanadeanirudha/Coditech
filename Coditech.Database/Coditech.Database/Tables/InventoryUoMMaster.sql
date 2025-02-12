CREATE TABLE [dbo].[InventoryUoMMaster](
	[InventoryUoMMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[UomCode] [varchar](50) NOT NULL,
	[UoMDescription] [varchar](200) NOT NULL,
	[CommercialDescription] [varchar](200) NULL,
	[GeneralMeasurementUnitMasterId] [smallint] NOT NULL,
	[ConvertionFactor] [decimal](18, 6) NULL,
	[AdditiveConstant] [varchar](10) NULL,
	[DecimalPlacesUpto] [tinyint] NOT NULL,
	[DecimalRounding] [tinyint] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryUoMMaster] PRIMARY KEY CLUSTERED 
(
	[InventoryUoMMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
