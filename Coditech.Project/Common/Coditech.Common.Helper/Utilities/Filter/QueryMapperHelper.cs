using System.Collections.Specialized;

namespace Coditech.Common.Helper.Utilities
{
    public static class QueryMapperHelper
    {
        public static NameValueCollection ToNameValueCollectionExpands(this ExpandCollection expandCollection)
        {
            var keyValuePairs = new NameValueCollection();

            if (HelperUtility.IsNotNull(expandCollection))
            {
                foreach (var expand in expandCollection)
                {
                    if (HelperUtility.IsNotNull(expand))
                        keyValuePairs.Add(expand, expand?.ToLower());
                }
            }
            return keyValuePairs;

        }

        public static NameValueCollection ToNameValueCollectionSort(this SortCollection sortCollection)
        {
            var keyValuePairs = new NameValueCollection();
            if (HelperUtility.IsNotNull(sortCollection))
            {
                foreach (var sort in sortCollection)
                {
                    if (sort.Key != null)
                    {
                        keyValuePairs.Add(sort.Key, sort.Value);

                    }
                }
            }
            return keyValuePairs;
        }

        public static NameValueCollection BindPage(int pageIndex, int pageSize)
        {
            NameValueCollection keyValuePairs = new NameValueCollection();
            if (pageIndex > 0 && pageSize > 0)
            {
                keyValuePairs.Add("index", pageIndex.ToString());
                keyValuePairs.Add("size", pageSize.ToString());

            }

            return keyValuePairs;
        }
    }
}
