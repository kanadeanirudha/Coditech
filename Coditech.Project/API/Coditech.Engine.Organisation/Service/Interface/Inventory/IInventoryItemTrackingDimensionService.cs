using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryItemTrackingDimensionService
    {
        InventoryItemTrackingDimensionListModel GetInventoryItemTrackingDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryItemTrackingDimensionModel CreateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel model);
        InventoryItemTrackingDimensionModel GetInventoryItemTrackingDimension(short inventoryCategoryId);
        bool UpdateInventoryItemTrackingDimension(InventoryItemTrackingDimensionModel model);
        bool DeleteInventoryItemTrackingDimension(ParameterModel parameterModel);
    }
}
