using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITaskApprovalSettingService
    {
        TaskApprovalSettingListModel GetTaskApprovalSettingList(string selectedCentreCode,FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
            
    }
}
