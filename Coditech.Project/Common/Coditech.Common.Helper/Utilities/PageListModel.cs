using Coditech.Common.Helper.Constants;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Helper.Utilities.Maps;

using System.Collections.Specialized;

namespace Coditech.Common.Helper
{
    public class PageListModel
    {
        #region private variables
        private readonly FilterCollection _filters;
        private readonly NameValueCollection _sorts;
        #endregion

        #region Public Properties
        public int PagingStart;
        public int PagingLength;
        public int TotalRowCount;
        public string OrderBy
        {
            get
            {
                return DynamicClauseHelper.GenerateDynamicOrderByClause(_sorts);
            }
        }

        public string SPWhereClause
        {
            get
            {
                return DynamicClauseHelper.GenerateDynamicWhereClauseForSP(_filters.ToFilterDataCollection());
            }
        }

        public EntityWhereClauseModel EntityWhereClause
        {
            get
            {
                return DynamicClauseHelper.GenerateDynamicWhereClauseWithFilter(_filters.ToFilterDataCollection());
            }
        }
        #endregion

        #region constructor
        public PageListModel(FilterCollection filters, NameValueCollection sorts, int pagingStart, int pagingLength)
        {
            _filters = filters;
            _sorts = sorts;
            PagingStart = pagingStart > 0 ? pagingStart : 1;
            PagingLength = pagingLength > 0 ? pagingLength : int.MaxValue;
        }

        public PageListModel(FilterCollection filters, NameValueCollection sorts, NameValueCollection page)
        {
            _filters = filters;
            _sorts = sorts;
            SetPaging(page, out PagingStart, out PagingLength);
        }
        #endregion

        #region Private Method
        private void SetPaging(NameValueCollection page, out int pagingStart, out int pagingLength)
        {
            // We use int.MaxValue for the paging length to ensure we always get total results back
            pagingStart = 1;
            pagingLength = int.MaxValue;
            if (!Equals(page, null) && page.HasKeys())
            {
                // Only do if both index and size are given
                if (!string.IsNullOrEmpty(page.Get(PageKeys.Index)) && !string.IsNullOrEmpty(page.Get(PageKeys.Size)))
                {
                    pagingStart = Convert.ToInt32(page.Get(PageKeys.Index));
                    pagingLength = Convert.ToInt32(page.Get(PageKeys.Size));
                }
            }
        }
        #endregion
    }
}
