using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Coditech.Admin.Helpers
{
    public static class ControllerExtensions
    {
        public static RedirectToActionResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action) where T : Controller
        {
            if (!(action?.Body is MethodCallExpression body)) throw new ArgumentException("Expression must be a method call.");
            if (body.Object != action.Parameters[0]) throw new ArgumentException("Method call must target lambda argument.");

            string actionName = body.Method.Name;

            var actionNameAttributes = body.Method.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (actionNameAttributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute)actionNameAttributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof(T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }
            var routeValue = ((Controller)controller).RedirectToAction(action.Name);
            //routeValue.RouteValues["Controller"] = routeValue.RouteValues["Controller"];

            return new RedirectToActionResult(actionName, controllerName, null);
        }

    }
}
