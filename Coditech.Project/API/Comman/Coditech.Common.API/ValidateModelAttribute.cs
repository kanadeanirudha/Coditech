using Coditech.Common.API.Model.Response;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Coditech.Common.API
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = modelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new KeyValuePair<string, string>(s.Key, string.IsNullOrEmpty(s.Value.Errors.First().ErrorMessage) ? s.Value.Errors.First().Exception.Message : s.Value.Errors.First().ErrorMessage))
                    .ToDictionary(x => x.Key, x => x.Value);
                if (errors != null)
                {
                    var data = new BaseResponse { HasError = true, ErrorMessage = string.Empty, CustomModelState = errors };
                    actionContext.Result = new BadRequestObjectResult(data);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var modelState = actionExecutedContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = modelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage))
                    .ToDictionary(x => x.Key, x => x.Value);
                if (errors != null)
                {
                    var data = new BaseResponse { HasError = true, ErrorMessage = string.Empty, CustomModelState = errors };
                    actionExecutedContext.Result = new BadRequestObjectResult(data);
                }
            }
        }
    }
}