Create   PROCEDURE [dbo].[Coditech_GetAdminRoleList]
(   
	@CentreCode		VARCHAR(60),
	@DepartmentId   INT,
    @WhereClause	VARCHAR(MAX),
    @Rows			INT          = 100,
    @PageNo			INT          = 1,
    @Order_BY		VARCHAR(100) = '',
	@RowsCount		INT OUT
)
	--exec Coditech_GetAdminRoleList @CentreCode='HO',@DepartmentId=1,@WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null

AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX), @AdminSanctionPostIds nvarchar(255);
             DECLARE @TBL_AdminRoleDetail TABLE
			 (
				 AdminRoleMasterId					int,
				 AdminRoleCode					NVARCHAR(60),
				 SanctionPostName		        NVARCHAR(400),
				 MonitoringLevel				NVARCHAR(12),
				 IsActive						bit,
				 RowId							INT,
				 CountNo						INT
			 )

			SELECT @AdminSanctionPostIds = COALESCE(@AdminSanctionPostIds + ',', '') + CAST(AdminSanctionPostId AS varchar(5))
			FROM AdminSanctionPost a
			WHERE a.CentreCode = @CentreCode and a.DepartmentID=@DepartmentId

			 SET @SQL = '
						;with Cte_filterAdminRole AS 
						(
							SELECT a.AdminRoleMasterId,a.AdminRoleCode,a.SanctionPostName,a.IsActive,a.MonitoringLevel,'
							+dbo.Fn_GetPagingRowId(@Order_By,'AdminRoleCode')+',Count(*)Over() CountNo 
							FROM AdminRoleMaster a
							where a.AdminSanctionPostId in( '+@AdminSanctionPostIds+') '+ [dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  AdminRoleMasterId,AdminRoleCode,SanctionPostName,IsActive,MonitoringLevel,RowId,CountNo
						FROM Cte_filterAdminRole
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_AdminRoleDetail (AdminRoleMasterId,AdminRoleCode,SanctionPostName,IsActive,MonitoringLevel,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_AdminRoleDetail),0)
			 SELECT AdminRoleMasterId,AdminRoleCode,SanctionPostName,IsActive,MonitoringLevel
			 FROM @TBL_AdminRoleDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetAdminRoleList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetAdminRoleList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
