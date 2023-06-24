using Coditech.Common.API.Model;

namespace Coditech.Common.Helper
{
    public static class HelperUtility
    {
        #region Private Variables
        private delegate bool ParseDelegate<T>(string s, out T result);
        #endregion
        //Returns true if the passed value is not null, else return false.
        public static bool IsNotNull(object value)
            => !Equals(value, null);

        //Returns true if the passed value is null else false.
        public static bool IsNull(object value)
            => Equals(value, null);

        //Try parse boolean value 
        public static bool TryParseBoolean(this string value)
        {
            return TryParse<bool>(value, bool.TryParse);
        }

        //Method to perform tryparse
        private static T TryParse<T>(this string value, ParseDelegate<T> parse) where T : struct
        {
            T result;
            parse(value, out result);
            return result;
        }
        public static void BindPageListModel(this BaseListModel baseListModel, PageListModel pageListModel)
        {
            if (IsNotNull(pageListModel))
            {
                baseListModel.TotalResults = pageListModel.TotalRowCount;
                baseListModel.PageIndex = pageListModel.PagingStart;
                baseListModel.PageSize = pageListModel.PagingLength;
            }
        }

        public static bool IsAdminUser(string userType)
           => Equals(userType, "A");
    }
}
