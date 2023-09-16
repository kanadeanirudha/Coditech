CREATE TABLE [dbo].[GeneralNationalityMaster](
	[GeneralNationalityMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[DefaultFlag] [bit] NOT NULL,
	[IsUserDefined] [bit] NULL DEFAULT 0,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [pkGeneralNationalityMasterId] PRIMARY KEY CLUSTERED 
(
	[GeneralNationalityMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO