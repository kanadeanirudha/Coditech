using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryItemTrackingDimensionGroupService
    {
        InventoryItemTrackingDimensionGroupListModel GetInventoryItemTrackingDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryItemTrackingDimensionGroupModel CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel model);
        InventoryItemTrackingDimensionGroupModel GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId);
        bool UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel model);
        bool DeleteInventoryItemTrackingDimensionGroup(ParameterModel parameterModel);
    }
}
