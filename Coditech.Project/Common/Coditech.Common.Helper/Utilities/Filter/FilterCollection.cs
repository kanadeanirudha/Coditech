﻿namespace Coditech.Common.Helper.Utilities
{
    public class FilterCollection : List<FilterTuple>
    {
        public void Add(string filterName, string filterOperator, string filterValue) => Add(new FilterTuple(filterName, filterOperator, filterValue));
    }
}
