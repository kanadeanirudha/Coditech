CREATE      PROCEDURE [dbo].[Coditech_GetOrganisationCentreList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetOrganisationCentreList @WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_OrganisationCentreDetails TABLE
			 (
				 OrganisationCentreMasterId			smallint,
				 CentreCode							NVARCHAR(15),
				 CentreName							NVARCHAR(100),
				 HoCoRoScFlag						varchar(5),
				 RowId								INT,
				 CountNo							INT
			 )

             SET @SQL = '
						;with Cte_filterOrganisationCentre AS 
						(
							SELECT OrganisationCentreMasterId,CentreCode,CentreName,HoCoRoScFlag,'
							+dbo.Fn_GetPagingRowId(@Order_By,'CentreName')+',Count(*)Over() CountNo 
							FROM OrganisationCentreMaster where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  OrganisationCentreMasterId,CentreCode,CentreName,HoCoRoScFlag,RowId,CountNo
						FROM Cte_filterOrganisationCentre
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_OrganisationCentreDetails (OrganisationCentreMasterId,CentreCode,CentreName,HoCoRoScFlag,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_OrganisationCentreDetails),0)
			 SELECT OrganisationCentreMasterId,CentreCode,CentreName,HoCoRoScFlag
			 FROM @TBL_OrganisationCentreDetails
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetOrganisationCentreList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetOrganisationCentreList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
GO


