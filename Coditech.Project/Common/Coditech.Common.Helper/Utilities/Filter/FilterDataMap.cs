﻿namespace Coditech.Common.Helper.Utilities.Maps
{
    public static class FilterDataMap
    {
        public static FilterDataCollection ToFilterDataCollection(this FilterCollection filters)
        {
            FilterDataCollection dataCollection = new FilterDataCollection();
            if (!Equals(filters, null))
            {
                dataCollection.AddRange(filters.ToModel<FilterDataTuple, FilterTuple>());
            }
            return dataCollection;
        }
    }
}
