namespace Coditech.Common.Exceptions
{
    public class ErrorCodes
    {
        public const Int32 WebAPIKeyNotFound = 1;
        public const Int32 UnAuthorized = 2;
        #region Misconfiguration related error codes
        public const Int32 InvalidDomainConfiguration = 8001;
        public const Int32 InvalidSqlConfiguration = 8002;
        public const Int32 InvalidMongoConfiguration = 8003;
        public const Int32 InvalidCoditechLicense = 8884;
        #endregion

        public const Int32 NullModel = 1;
        public const Int32 AlreadyExist = 2;
        public const Int32 AtLeastSelectOne = 3;
        public const Int32 AssociationDeleteError = 4;
        public const Int32 InvalidData = 5;
        public const Int32 NotFound = 6;
        public const Int32 NotPermitted = 7;
        public const Int32 IdLessThanOne = 8;
        public const Int32 ExceptionalError = 9;
        public const Int32 ContactAdministrator = 10;
        public const Int32 InValidOTP = 11;

        public const Int32 InvalidFileName = 1000;
        public const Int32 InvalidFileExtension = 1001;
        public const Int32 FileSizeLimitExceed = 1002;
        public const Int32 InvalidFolderPath = 1003;
    }
}