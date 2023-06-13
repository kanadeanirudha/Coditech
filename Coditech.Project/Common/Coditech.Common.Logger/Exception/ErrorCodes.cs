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
    }
}