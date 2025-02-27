CREATE TABLE [dbo].[EmployeeService](
	[EmployeeServiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[EmployeeCode] [nvarchar](200) NOT NULL,
	[EmployeeDesignationMasterId] [smallint] NULL,
	[JoiningDate] [date] NULL,
	[PromotionDemotionDate] [date] NULL,
	[EmployeeStageEnumId] [int] NULL,
	[DateOfLeaving] [date] NULL,
	[IsCurrentPosition] [bit] NOT NULL,
	[SalaryGradeCode] [nvarchar](100) NULL,
	[PayScale] [varchar](100) NULL,
	[OrderDate] [date] NULL,
	[OrderNumber] [nvarchar](100) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeService] PRIMARY KEY CLUSTERED 
(
	[EmployeeServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EmployeeService] ADD  CONSTRAINT [DF_EmployeeService_IsCurrentPosition]  DEFAULT ((0)) FOR [IsCurrentPosition]
GO

ALTER TABLE [dbo].[EmployeeService]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeService_EmployeeDesignationMaster] FOREIGN KEY([EmployeeDesignationMasterId])
REFERENCES [dbo].[EmployeeDesignationMaster] ([EmployeeDesignationMasterId])
GO

ALTER TABLE [dbo].[EmployeeService] CHECK CONSTRAINT [FK_EmployeeService_EmployeeDesignationMaster]
GO

ALTER TABLE [dbo].[EmployeeService]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeService_EmployeeMaster] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[EmployeeMaster] ([EmployeeId])
GO

ALTER TABLE [dbo].[EmployeeService] CHECK CONSTRAINT [FK_EmployeeService_EmployeeMaster]
GO