CREATE TABLE [dbo].[GeneralNationalityMaster](
	[GeneralNationalityMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[DefaultFlag] [bit] NOT NULL,
	[IsUserDefined] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [pkGeneralNationalityMasterId] PRIMARY KEY CLUSTERED 
(
	[GeneralNationalityMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralNationalityMaster] ADD  DEFAULT ((0)) FOR [IsUserDefined]
GO
