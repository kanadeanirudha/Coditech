﻿namespace Coditech.Common.Helper.Utilities
{
    public class FilterDataCollection : List<FilterDataTuple>
    {
        public void Add(string filterName, string filterOperator, string filterValue) => Add(new FilterDataTuple(filterName, filterOperator, filterValue));
    }
}
