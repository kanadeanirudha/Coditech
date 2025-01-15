using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
	public interface IInventoryGeneralItemMasterService
    {
        InventoryGeneralItemMasterListModel GetInventoryGeneralItemMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryGeneralItemMasterModel CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel model);
        InventoryGeneralItemMasterModel GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId);
        bool UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel model);
        bool DeleteInventoryGeneralItemMaster(ParameterModel parameterModel);
        InventoryGeneralItemMasterListModel GetGeneralServicesList(string searchText);
    }
}
