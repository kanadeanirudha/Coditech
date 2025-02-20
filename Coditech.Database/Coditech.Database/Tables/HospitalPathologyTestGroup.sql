CREATE TABLE [dbo].[HospitalPathologyTestGroup](
	[HospitalPathologyTestGroupId] [int] IDENTITY(1,1) NOT NULL,
	[PathologyTestGroupName] [nvarchar](200) NOT NULL,
	[HospitalPathologyTestGroupParentId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_HospitalPathologyTestGroup] PRIMARY KEY CLUSTERED 
(
	[HospitalPathologyTestGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[HospitalPathologyTestGroup] ADD  CONSTRAINT [DF_HospitalPathologyTestGroup_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
