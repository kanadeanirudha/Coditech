CREATE TABLE [dbo].[DBTMTraineeDetails](
	[DBTMTraineeDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[CentreCode] [nvarchar](15) NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[PersonCode] [nvarchar](200) NOT NULL,
	[UserType] [varchar](30) NOT NULL,
	[PastInjuries] [nvarchar](500) NULL,
	[MedicalHistory] [nvarchar](500) NULL,
	[GroupEnumId] [int] NULL,
	[SourceEnumId] [int] NULL,
	[OtherInformation] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[Weight] [decimal](3, 3) NULL,
	[Height] [decimal](3, 3) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DBTMTraineeDetails] PRIMARY KEY CLUSTERED 
(
	[DBTMTraineeDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DBTMTraineeDetails] ADD  CONSTRAINT [DF_DBTMTraineeDetails_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO