using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryItemGroupService
    {
        InventoryItemGroupListModel GetInventoryItemGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryItemGroupModel CreateInventoryItemGroup(InventoryItemGroupModel model);
        InventoryItemGroupModel GetInventoryItemGroup(short inventoryCategoryId);
        bool UpdateInventoryItemGroup(InventoryItemGroupModel model);
        bool DeleteInventoryItemGroup(ParameterModel parameterModel);
    }
}
