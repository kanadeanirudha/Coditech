using Microsoft.AspNetCore.Mvc.Filters;

using System.Web;

namespace Coditech.Common.Helper.Utilities
{
    public class BindQueryFilter : IActionFilter
    {
        private const string ApiUriItemSeparator = ",";
        private const string ApiUriKeyValueSeparator = "~";
        private const string CommaReplacer = "^";
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["filter"] = GetFilterCollection(context);
            context.ActionArguments["sort"] = GetSortCollection(context);
            context.ActionArguments["expand"] = GetExpandCollection(context);
        }

        public ExpandCollection GetExpandCollection(ActionExecutingContext context)
        {
            string _queryString = context.HttpContext.Request.QueryString.ToString();
            string param = "expand";
            var keyValuePairs = new ExpandCollection();
            var query = HttpUtility.ParseQueryString(_queryString.ToLower());

            var uriItemSeparator = ApiUriItemSeparator;
            var uriKeyValueSeparator = ApiUriKeyValueSeparator;

            foreach (var key in query.AllKeys)
            {
                if (key.ToLower() == param)
                {
                    var value = query.Get(key);
                    var items = value.Split(uriItemSeparator.ToCharArray());

                    foreach (var item in items)
                    {
                        if (item.Contains(uriKeyValueSeparator))
                        {
                            var set = item.Split(uriKeyValueSeparator.ToCharArray());
                            keyValuePairs.Add(set[0].ToLower());
                        }
                        else
                        {
                            // Just make the value the same as the key, for consistency of code in other places
                            keyValuePairs.Add(item.ToLower());
                        }
                    }

                    break;
                }
            }
            return keyValuePairs;
        }

        public FilterCollection GetFilterCollection(ActionExecutingContext context)
        {
            string _queryString = context.HttpContext.Request.QueryString.ToString();
            string param = "filter";
            var filters = new FilterCollection();
            var query = HttpUtility.ParseQueryString(_queryString.ToLower());

            var uriItemSeparator = ApiUriItemSeparator;
            var uriKeyValueSeparator = ApiUriKeyValueSeparator;
            string commaReplacer = CommaReplacer;

            foreach (var key in query.AllKeys)
            {
                if (key.ToLower() == param)
                {
                    var value = query.Get(key);
                    var items = value.Split(uriItemSeparator.ToCharArray());

                    foreach (var item in items)
                    {
                        if (item.Contains(uriKeyValueSeparator))
                        {
                            var tuple = item.Split(uriKeyValueSeparator.ToCharArray());
                            var filterKey = tuple[0].ToLower().Trim();
                            var filterOperator = tuple[1].ToLower().Trim();
                            var filterValue = tuple[2].ToLower().Trim();

                            filters.Add(new FilterTuple(filterKey, filterOperator, HttpUtility.HtmlDecode(filterValue?.Replace(commaReplacer, ","))));
                        }
                    }
                    break;
                }
            }

            return filters;
        }

        public SortCollection GetSortCollection(ActionExecutingContext context)
        {
            string _queryString = context.HttpContext.Request.QueryString.ToString();
            string param = "sort";
            var keyValuePairs = new SortCollection();
            var query = HttpUtility.ParseQueryString(_queryString.ToLower());

            var uriItemSeparator = ApiUriItemSeparator;
            var uriKeyValueSeparator = ApiUriKeyValueSeparator;

            foreach (var key in query.AllKeys)
            {
                if (key.ToLower() == param)
                {
                    var value = query.Get(key);
                    var items = value.Split(uriItemSeparator.ToCharArray());

                    foreach (var item in items)
                    {
                        if (item.Contains(uriKeyValueSeparator))
                        {
                            var set = item.Split(uriKeyValueSeparator.ToCharArray());
                            keyValuePairs.Add(set[0], HttpUtility.HtmlDecode(set[1]).ToLower());
                        }
                        else
                        {
                            // Just make the value the same as the key, for consistency of code in other places
                            keyValuePairs.Add(item.ToLower(), item.ToLower());
                        }
                    }

                    break;
                }
            }
            return keyValuePairs;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}
