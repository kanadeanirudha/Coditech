using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace Coditech.Common.API
{
    public abstract class BaseController : ControllerBase
    {
      
        /// <summary>
        /// Creates an OK response message, converts non-string types into JSON string before writing it into response.Content.
        /// and if a string is passed in 'data', it will write given 'data' string directly into response.Content without any handling
        /// Instead of this method non-generic CreateOKResponse(string data); should be used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">If string, it must be an JSON serialized string, If any other object, it will be converted into JSON string.</param>
        /// <returns>HttpResponseMessage having content object in StringContent format having UTF-8 encoding and application/json mimetype</returns>
        protected IActionResult CreateOKResponse<T>(T data)
        {
            string? d;
            if (typeof(T) == typeof(string))
            {
                d = data as string;
            }
            else
            {
                //We can directly use Response.CreateResponse(OK, data), but that will use WebAPI framework to serialize the object
                //Rather, we want to use our own JSON serializer, which creates a minified JSON, if requested.
                //JsonConvert uses global setting, setup in Global.asax.cs to minify the JSON
                d = ApiHelper.ToJson(data);
            }

            return CreateOKResponse(d);
        }
        /// <summary>
        /// Creates an OK response message, and writes given 'data' string directly into response.Content without any handling
        /// Instead of this method non-generic CreateOKResponse(string data); should be used.
        /// </summary>
        /// <typeparam name="T">Old implementation, now we don't need Type generic, but still </typeparam>
        /// <param name="data">Must be a JSON serialized string, anything else may fail silently or create issue in API client.</param>
        /// <returns>HttpResponseMessage having content object in StringContent format having UTF-8 encoding and application/json mimetype</returns>

        protected IActionResult CreateOKResponse<T>(string data)
        {
            return CreateOKResponse(data);
        }
        /// <summary>
        /// Creates an OK response message, and writes given 'data' string directly into response.Content without any handling
        /// </summary>
        /// <param name="data">Must be a JSON serialized string, anything else may fail silently or create issue in API client.</param>
        /// <returns>HttpResponseMessage having content object in StringContent format having UTF-8 encoding and application/json mimetype</returns>
        protected IActionResult CreateOKResponse(string data)
        {
            ObjectResult response = new ObjectResult(data)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            return response;
        }
        protected IActionResult CreateCreatedResponse<T>(T data) => new CreatedResult("Created", data);

        protected IActionResult CreateUnauthorizedResponse<T>(T data)
        {
            return StatusCode(401, data);
        }
        protected IActionResult CreateInternalServerErrorResponse<T>(T data)
        {
            return StatusCode(500, data);
        }
        protected IActionResult CreateInternalServerErrorResponse() => StatusCode(500);
        protected IActionResult CreateNoContentResponse() => NoContent();
    }
}