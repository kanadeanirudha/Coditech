CREATE TABLE [dbo].[InventoryCategory](
	[InventoryCategoryId] [smallint] IDENTITY(1,1) NOT NULL,
	[ParentInventoryCategoryId] [smallint] NOT NULL,
	[CategoryCode] [varchar](50) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[ItemPrefix] [nvarchar](20) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_InventoryCategory] PRIMARY KEY CLUSTERED 
(
	[InventoryCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InventoryCategory] ADD  CONSTRAINT [DF_InventoryCategory_ParentInventoryCategoryId]  DEFAULT ((0)) FOR [ParentInventoryCategoryId]
GO
