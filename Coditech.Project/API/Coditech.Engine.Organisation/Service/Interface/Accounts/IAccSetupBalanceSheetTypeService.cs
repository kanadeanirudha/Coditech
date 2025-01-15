using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;
namespace Coditech.API.Service
{
    public interface IAccSetupBalanceSheetTypeService
    {
        AccSetupBalanceSheetTypeListModel GetAccSetupBalanceSheetTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
       
    }
}
