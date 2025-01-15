using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryItemStorageDimensionService
    {
        InventoryItemStorageDimensionListModel GetInventoryItemStorageDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryItemStorageDimensionModel CreateInventoryItemStorageDimension(InventoryItemStorageDimensionModel model);
        InventoryItemStorageDimensionModel GetInventoryItemStorageDimension(short inventoryItemStorageDimensionId);
        bool UpdateInventoryItemStorageDimension(InventoryItemStorageDimensionModel model);
        bool DeleteInventoryItemStorageDimension(ParameterModel parameterModel);
    }
}
