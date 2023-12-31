CREATE  PROCEDURE [dbo].[Coditech_GetAdminSanctionPostList]
(   
	@CentreCode		VARCHAR(60),
	@DepartmentId   SMALLINT,
    @WhereClause	VARCHAR(MAX),
    @Rows			INT          = 100,
    @PageNo			INT          = 1,
    @Order_BY		VARCHAR(100) = '',
	@RowsCount		INT OUT
)
	--exec Coditech_GetAdminSanctionPostList @CentreCode='HO',@DepartmentId=7,@WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_AdminSanctionPostDetail TABLE
			 (
				 AdminSanctionPostId			int,
				 SanctionPostCode				NVARCHAR(60),
				 SanctionedPostDescription		NVARCHAR(200),
				 NoOfPost						smallint,
				 IsActive						bit,
				 RowId							INT,
				 CountNo						INT
			 )
		
             SET @SQL = '
						;with Cte_filterAdminSanctionPost AS 
						(
							SELECT AdminSanctionPostId,SanctionPostCode,SanctionedPostDescription,IsActive,NoOfPost,'
							+dbo.Fn_GetPagingRowId(@Order_By,'SanctionPostCode')+',Count(*)Over() CountNo 
							FROM AdminSanctionPost where CentreCode='''+@CentreCode+''' and DepartmentID='+CAST(@DepartmentId AS VARCHAR(10))+' '+ [dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  AdminSanctionPostId,SanctionPostCode,SanctionedPostDescription,IsActive,NoOfPost,RowId,CountNo
						FROM Cte_filterAdminSanctionPost
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_AdminSanctionPostDetail (AdminSanctionPostId,SanctionPostCode,SanctionedPostDescription,IsActive,NoOfPost,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_AdminSanctionPostDetail),0)
			 SELECT AdminSanctionPostId,SanctionPostCode,SanctionedPostDescription,IsActive,NoOfPost
			 FROM @TBL_AdminSanctionPostDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetAdminSanctionPostList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetAdminSanctionPostList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
