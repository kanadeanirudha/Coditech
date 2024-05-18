using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryStorageDimensionGroupService
    {
        InventoryStorageDimensionGroupListModel GetInventoryStorageDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryStorageDimensionGroupModel CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel model);
        InventoryStorageDimensionGroupModel GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId);
        bool UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel model);
        bool DeleteInventoryStorageDimensionGroup(ParameterModel parameterModel);
    }
}
