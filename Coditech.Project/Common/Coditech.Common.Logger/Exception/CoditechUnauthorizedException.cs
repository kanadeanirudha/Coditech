using System.Net;

namespace Coditech.Common.Exceptions
{
	public class CoditechUnauthorizedException : CoditechException
	{
		/// <summary>
		/// Creates a new CoditechUnauthorizedException.
		/// </summary>
		public CoditechUnauthorizedException()
		{
		}

		/// <summary>
		/// Creates a new CoditechUnauthorizedException.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="errorMessage">The error message.</param>
		public CoditechUnauthorizedException(int? errorCode, string errorMessage) : base(errorCode, errorMessage)
		{
		}

		/// <summary>
		/// Creates a new CoditechUnauthorizedException.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		/// <param name="errorMessage">The error message.</param>
		/// <param name="statusCode">The HTTP status code.</param>
		public CoditechUnauthorizedException(int? errorCode, string errorMessage, HttpStatusCode statusCode) : base(errorCode, errorMessage, statusCode)
		{
		}
	}
}
