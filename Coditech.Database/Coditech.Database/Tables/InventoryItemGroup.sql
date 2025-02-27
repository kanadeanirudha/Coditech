CREATE TABLE [dbo].[InventoryItemGroup](
	[InventoryItemGroupId] [smallint] IDENTITY(1,1) NOT NULL,
	[ItemGroupName] [varchar](100) NOT NULL,
	[ItemGroupCode] [varchar](100) NOT NULL,
	[ConsiderInProdReport] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryItemGroup] PRIMARY KEY CLUSTERED 
(
	[InventoryItemGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
