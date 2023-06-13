using System.Net;

namespace Coditech.Common.Exceptions
{
    public class CoditechException : Exception
    {
        public int? ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public Dictionary<string, string> ErrorDetailList { get; private set; }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        public CoditechException()
            : base("Coditech Exception")
        {
        }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        public CoditechException(int? errorCode, string errorMessage)
            : base(errorMessage ?? "CoditechException with errorCode" + errorCode.GetValueOrDefault().ToString())
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="errorDetailList">The error details.</param>
        public CoditechException(int? errorCode, string errorMessage, Dictionary<string, string> errorDetailList)
            : base(errorMessage ?? "CoditechException with errorCode" + errorCode.GetValueOrDefault().ToString())
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorDetailList = errorDetailList;
        }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        public CoditechException(int? errorCode, string errorMessage, HttpStatusCode statusCode)
            : base(errorMessage ?? "CoditechException with status code " + statusCode.ToString())
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        public CoditechException(int? errorCode, string errorMessage, int statusCode)
            : base(errorMessage ?? "CoditechException with status code " + statusCode.ToString())
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StatusCode = (HttpStatusCode)statusCode;
        }

        /// <summary>
        /// Creates a new CoditechException.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="errorDetailList">The error details.</param>
        public CoditechException(int? errorCode, string errorMessage, HttpStatusCode statusCode, Dictionary<string, string> errorDetailList)
            : base(errorMessage ?? "CoditechException with status code " + statusCode.ToString())
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            ErrorDetailList = errorDetailList;
        }
    }
}
