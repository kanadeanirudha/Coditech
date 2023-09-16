create     PROCEDURE [dbo].[Coditech_DeleteEmployeeDesignationMaster]
(
       @EmployeeDesignationMasterId VARCHAR(max) ,
       @Status      BIT OUT
)
AS
/*
Summary: This Procedure is used to delete EmployeeDesignationMaster details
Unit Testing:
EXEC Coditech_DeleteEmployeeDesignationMaster @EmployeeDesignationMasterId='1'
*/
     BEGIN
         BEGIN TRAN;
         BEGIN TRY
             SET NOCOUNT ON;
             DECLARE @EmployeeDesignationMaster TABLE (
                                      EmployeeDesignationMasterId INT
                                      );
             INSERT INTO @EmployeeDesignationMaster SELECT item FROM dbo.Split ( @EmployeeDesignationMasterId , ',');
           
             DECLARE @DeletEmployeeDesignationMasterId TABLE (
                                              EmployeeDesignationMasterId INT
                                              );
             INSERT INTO @DeletEmployeeDesignationMasterId
                    SELECT a.EmployeeDesignationMasterId
                    FROM [dbo].EmployeeDesignationMaster AS a INNER JOIN @EmployeeDesignationMaster AS b ON ( a.EmployeeDesignationMasterId = b.EmployeeDesignationMasterId );
           
             DELETE FROM EmployeeDesignationMaster
             WHERE EXISTS ( SELECT TOP 1 1
                            FROM @DeletEmployeeDesignationMasterId AS b
                            WHERE b.EmployeeDesignationMasterId = EmployeeDesignationMaster.EmployeeDesignationMasterId
                          );
           
             SET @Status = 1;
             IF ( SELECT COUNT(1)
                  FROM @DeletEmployeeDesignationMasterId
                ) = ( SELECT COUNT(1)
                      FROM @EmployeeDesignationMaster
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
             --DECLARE @Error_procedure VARCHAR(1000)= ERROR_PROCEDURE(), @ErrorMessage NVARCHAR(MAX)= ERROR_MESSAGE(), @ErrorLine VARCHAR(100)= ERROR_LINE(), @ErrorCall NVARCHAR(MAX)= 'EXEC Coditech_DeleteDepartment @EmployeeDesignationMasterId = '+@EmployeeDesignationMasterId+',@Status='+CAST(@Status AS VARCHAR(200));
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
GO


