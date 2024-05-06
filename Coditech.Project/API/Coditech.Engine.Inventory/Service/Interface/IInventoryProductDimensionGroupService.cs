using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryProductDimensionGroupService
    {
        InventoryProductDimensionGroupListModel GetInventoryProductDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryProductDimensionGroupModel CreateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel model);
        InventoryProductDimensionGroupModel GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId);
        bool UpdateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel model);
        bool DeleteInventoryProductDimensionGroup(ParameterModel parameterModel);
    }
}
