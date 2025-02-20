
CREATE TABLE [dbo].[GeneralFinancialYear](
	[GeneralFinancialYearId] [smallint] IDENTITY(1,1) NOT NULL,
	[FromDate] [date] NOT NULL,
	[Todate] [date] NOT NULL,
	[CentreCode] [nvarchar](50) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralFinancialYear] PRIMARY KEY CLUSTERED 
(
	[GeneralFinancialYearId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
