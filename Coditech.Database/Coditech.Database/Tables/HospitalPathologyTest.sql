CREATE TABLE [dbo].[HospitalPathologyTest](
	[HospitalPathologyTestId] [bigint] IDENTITY(1,1) NOT NULL,
	[HospitalPathologyTestGroupId] [int] NOT NULL,
	[PathologyTestName] [nvarchar](200) NOT NULL,
	[TestSampleTypeEnumId] [int] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPathologyTest] PRIMARY KEY CLUSTERED 
(
	[HospitalPathologyTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
