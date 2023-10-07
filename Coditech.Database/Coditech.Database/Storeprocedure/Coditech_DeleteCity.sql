CREATE   PROCEDURE [dbo].[Coditech_DeleteCity]
(
       @CityId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete City details
Unit Testing:
EXEC Coditech_DeleteCity @CityId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @City TABLE (
                                      CityId INT
                                      );
             INSERT INTO @City SELECT item FROM dbo.Split ( @CityId , ',');
           
             DECLARE @DeleteCityId TABLE (
                                              CityId INT
                                              );
             INSERT INTO @DeleteCityId
                    SELECT a.GeneralCityMasterId
                    FROM [dbo].GeneralCityMaster AS a INNER JOIN @City AS b ON ( a.GeneralCityMasterId = b.CityId );
           
             DELETE FROM GeneralCityMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteCityId AS b
                            WHERE b.CityId = GeneralCityMaster.GeneralCityMasterId
                          );
           
             SET @Status = 1;
				 IF ( SELECT COUNT(1)
					  FROM @DeleteCityId
					) = ( SELECT COUNT(1)
						  FROM @City
						)
                 BEGIN
                     SELECT 1 AS ID , CAST(1 AS BIT) AS Status;
                 END;
             ELSE
                 BEGIN
                     SELECT 0 AS ID , CAST(0 AS BIT) AS Status;
                 END;
             COMMIT TRAN;
         END TRY
         BEGIN CATCH
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteCity @CityId = '+@CityId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteCity',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
