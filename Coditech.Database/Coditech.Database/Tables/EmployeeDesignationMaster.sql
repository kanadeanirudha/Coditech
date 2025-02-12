CREATE TABLE [dbo].[EmployeeDesignationMaster](
	[EmployeeDesignationMasterId] [smallint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[DesignationLevel] [varchar](10) NULL,
	[Grade] [varchar](10) NULL,
	[ShortCode] [varchar](50) NULL,
	[EmpDesigType] [varchar](50) NULL,
	[RelatedWith] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PkEmployeeDesignationID] PRIMARY KEY CLUSTERED 
(
	[EmployeeDesignationMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeDesignationMaster] ADD  CONSTRAINT [DF__EmployeeD__IsAct__47A6A41B]  DEFAULT ((0)) FOR [IsActive]
GO