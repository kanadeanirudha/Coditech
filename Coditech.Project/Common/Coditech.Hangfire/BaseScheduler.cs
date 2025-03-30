namespace Coditech.Hangfire
{
    public abstract class BaseScheduler
    {
        #region  Protected properties and fields
        protected const string UserHeader = "LoginAsUserId";
        protected const string AuthorizationHeader = "Authorization";
        protected int UnAuthorizedErrorCode = 32;
        protected int RequestTimeout = 600000;//10 min
        protected string LoggingComponent = "ERP";
        #endregion

        #region Public Methods
        #endregion
    }
}
