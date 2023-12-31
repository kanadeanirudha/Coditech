﻿namespace Coditech.Common.Helper
{
    public class PageListViewModel
    {
        public PageListViewModel()
        {
            DatatableColumnList = new List<DatatableColumns>();
        }
        public int Page { get; set; }
        public int RecordPerPage { get; set; }
        public int TotalResults { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public int TotalRecordCount { get; set; } = 0;
        public string SearchBy { get; set; } = string.Empty;
        public string SortByColumn { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public List<DatatableColumns> DatatableColumnList { get; set; }
        public bool IsActionColumn { get; set; }
    }
}
