Create PROCEDURE [dbo].[Coditech_DeleteDepartment]
(
       @DepartmentId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete Department details
Unit Testing:
EXEC Coditech_DeleteDepartment @DepartmentId='2'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @Department TABLE (
                                      DepartmentId INT
                                      );
             INSERT INTO @Department SELECT item FROM dbo.Split ( @DepartmentId , ',');
           
             DECLARE @DeleteDepartmentId TABLE (
                                              DepartmentId INT
                                              );
             INSERT INTO @DeleteDepartmentId
                    SELECT a.GeneralDepartmentMasterId
                    FROM [dbo].GeneralDepartmentMaster AS a INNER JOIN @Department AS b ON ( a.GeneralDepartmentMasterId = b.DepartmentId );
           
             DELETE FROM GeneralDepartmentMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeleteDepartmentId AS b
                            WHERE b.DepartmentId = GeneralDepartmentMaster.GeneralDepartmentMasterId
                          );
           
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeleteDepartmentId
                ) = ( SELECT COUNT(1)
                      FROM @Department
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
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteDepartment @DepartmentId = '+@DepartmentId+',@Status='+CAST(@Status AS VARCHAR(200));
             SET @Status = 0;
             SELECT 0 AS ID,
                    CAST(0 AS BIT) AS Status;
			 ROLLBACK TRAN DeleteAccount;
             --EXEC Znode_InsertProcedureErrorLog
             --     @ProcedureName = 'Coditech_DeleteDepartment',
             --     @ErrorInProcedure = @Error_procedure,
             --     @ErrorMessage = @ErrorMessage,
             --     @ErrorLine = @ErrorLine,
             --     @ErrorCall = @ErrorCall;
            
         END CATCH;
     END;
