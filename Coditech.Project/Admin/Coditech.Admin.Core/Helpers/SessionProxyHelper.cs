using Coditech.Admin.Utilities;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using static Coditech.Common.Helper.CoditechDependencyResolver;

namespace Coditech.Admin.Helpers
{
    public class SessionProxyHelper
    {
        private static readonly ICoditechLogging _coditechLogging = GetService<ICoditechLogging>();


        //Summary
        // To Check whether Current login user is Admin or Not.
        public static bool IsAdminUser()
        {
            bool? isAdminUser = SessionHelper.GetDataFromSession<bool?>(AdminConstants.IsAdminUserSessionKey);
            if (Equals(isAdminUser, null))
            {
                isAdminUser = GetUserDetails()?.IsAdminUser;
                SessionHelper.SaveDataInSession<bool?>(AdminConstants.IsAdminUserSessionKey, isAdminUser);
            }

            return isAdminUser == true;
        }


        //Get the Login User Details based on the user name. To bind user id in Api request.
        public static UserModel GetUserDetails(string userId = null)
        {
            UserModel model = null;
            try
            {
                model = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession);

                if (Equals(model, null))
                {
                    if (HttpContextHelper.Current.User != null)
                    {
                        //Get the User Details. 
                        //Don't Use the Agent here, it will cause the infinite looping. As this method gets called from the BaseAgent.
                        UserClient client = new UserClient();
                        model = client.GetUserDetailByUserName(userId ?? HttpContextHelper.Current.User.Identity.Name);

                        if (!Equals(model, null))
                        {
                            SessionHelper.SaveDataInSession<UserModel>(AdminConstants.UserDataSession, model);
                        }
                        client = null;
                    }
                }
            }
            catch
            {
            }
            return model;
        }

        public static void RemoveAndBindUserDetails()
        {
            SessionHelper.RemoveDataFromSession(AdminConstants.UserDataSession);
            GetUserDetails();
        }
    }
}
