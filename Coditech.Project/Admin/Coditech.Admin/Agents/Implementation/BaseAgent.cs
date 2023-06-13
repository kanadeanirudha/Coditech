using Coditech.API.Client;

namespace Coditech.Admin.Agents
{
    public abstract class BaseAgent
    {
        /// <summary>
        /// Get API client object with current domain name and key.
        /// </summary>
        /// <typeparam name="T">The type of API client object.</typeparam>
        /// <returns>An API client object of type T.</returns>
        protected T GetClient<T>(T obj) where T : IBaseClient
        {
            if (!(obj is BaseClient)) return obj;
            (obj as BaseClient). DomainName = "";
            (obj as BaseClient).DomainKey = "";
            //if (HttpContextHelper.Current.User != null && HttpContextHelper.Current.User.Identity.IsAuthenticated)
            //{
            //    //var model = SessionProxyHelper.GetUserDetails();
            //    //if (HelperUtility.IsNotNull(model))
            //    //{
            //    //    (obj as BaseClient).UserId = model.UserId;
            //    //    (obj as BaseClient).RefreshCache = true;
            //    //}
            //}
            //TODO Znode Team Hornbills 
            //ICustomHeaders _headerAgent = GetService<ICustomHeaders>();

            //if (HelperUtility.IsNotNull(_headerAgent))
            //{
            //    Dictionary<string, string> headers = _headerAgent.SetCustomHeaderOfClient();

            //    int? count = headers?.Count;

            //    if (count > 0)
            //    {
            //        for (int i = 0; i < count; i++)
            //        {
            //            switch (i.ToString())
            //            {
            //                case "0":
            //                    (obj as BaseClient).Custom1 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "1":
            //                    (obj as BaseClient).Custom2 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "2":
            //                    (obj as BaseClient).Custom3 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "3":
            //                    (obj as BaseClient).Custom4 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //                case "4":
            //                    (obj as BaseClient).Custom5 = $"{headers.ElementAt(i).Key}:{headers.ElementAt(i).Value}";
            //                    break;
            //            }
            //        }
            //    }
            //}
            return obj;
        }
    }
}
