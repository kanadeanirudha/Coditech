CREATE      PROCEDURE [dbo].[Coditech_GetCityList]
(   @WhereClause  VARCHAR(MAX),
    @Rows         INT          = 100,
    @PageNo       INT          = 1,
    @Order_BY     VARCHAR(100) = '',
	@RowsCount    INT OUT
    )
	--exec Coditech_GetCityList @WhereClause=null,@Rows=25,@PageNo=1,@Order_BY='',@RowsCount= null
AS
   
     BEGIN
         SET NOCOUNT ON;
		 
         BEGIN TRY
             DECLARE @SQL NVARCHAR(MAX);
             DECLARE @TBL_CityDetail TABLE
			 (
				 GeneralCityMasterId	int,
				 CityName				NVARCHAR(100),
				 RegionName				NVARCHAR(50),
				 TinNumber			    smallint,
				 RowId					INT,
				 CountNo				INT
			 )

             SET @SQL = '
						;with Cte_filterCity AS 
						(
							SELECT GeneralCityMasterId,CityName,RegionName,TinNumber,'
							+dbo.Fn_GetPagingRowId(@Order_By,'CityName')+',Count(*)Over() CountNo 
							FROM GeneralCityMaster a
							inner join GeneralRegionMaster b on (a.GeneralRegionMasterId = b.GeneralRegionMasterId)
							where 1=1  '+[dbo].[Fn_GetFilterWhereClause](@WhereClause)+'
						)
						SELECT  GeneralCityMasterId,CityName,RegionName,TinNumber,RowId,CountNo
						FROM Cte_filterCity
						'+dbo.Fn_GetPaginationWhereClause(@PageNo,@rows)
						print @SQL
			 INSERT INTO @TBL_CityDetail (GeneralCityMasterId,CityName,RegionName,TinNumber,RowId,CountNo )
			 EXEC(@SQL)

			 SET @RowsCount =ISNULL((SELECT TOP 1 CountNo  FROM @TBL_CityDetail),0)
			 SELECT GeneralCityMasterId,CityName,RegionName,TinNumber
			 FROM @TBL_CityDetail
			 	 
         END TRY
         BEGIN CATCH
          DECLARE @Status BIT ;
		  SET @Status = 0;
		  DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_GetCityList @WhereClause = '+cast (@WhereClause AS VARCHAR(50))+',@Rows='+CAST(@Rows AS VARCHAR(50))+',@PageNo='+CAST(@PageNo AS VARCHAR(50))+',@Order_BY='+@Order_BY+',@RowsCount='+CAST(@RowsCount AS VARCHAR(50))+',@Status='+CAST(@Status AS VARCHAR(10));
              			 
          SELECT 0 AS ID,CAST(0 AS BIT) AS Status;                    
		  

          --EXEC Znode_InsertProcedureErrorLog
          --  @ProcedureName = 'Coditech_GetCityList',
          --  @ErrorInProcedure = @Error_procedure,
          --  @ErrorMessage = @ErrorMessage,
          --  @ErrorLine = @ErrorLine,
          --  @ErrorCall = @ErrorCall;
         END CATCH;
     END;
GO


