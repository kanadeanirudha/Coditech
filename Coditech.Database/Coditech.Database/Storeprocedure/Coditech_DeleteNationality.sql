CREATE   PROCEDURE [dbo].[Coditech_DeleteNationality]
(
       @NationalityId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete Nationality details
Unit Testing:
EXEC Coditech_DeleteNationality @NationalityId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @Nationality TABLE (
                                      NationalityId INT
                                      );
             INSERT INTO @Nationality SELECT item FROM dbo.Split ( @NationalityId , ',');
           
             DECLARE @DeleteNationalityId TABLE (
                                              NationalityId INT
                                              );
             INSERT INTO @DeleteNationalityId
                    SELECT a.GeneralNationalityMasterId
                    FROM [dbo].GeneralNationalityMaster AS a INNER JOIN @Nationality AS b ON ( a.GeneralNationalityMasterId = b.NationalityId );
           
             DELETE FROM GeneralNationalityMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteNationalityId AS b
                            WHERE b.NationalityId = GeneralNationalityMaster.GeneralNationalityMasterId
                          );
           
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeleteNationalityId
                ) = ( SELECT COUNT(1)
                      FROM @Nationality
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
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteNationality @NationalityId = '+@NationalityId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteNationality',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
GO


