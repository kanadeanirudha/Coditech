using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryProductDimensionService
    {
        InventoryProductDimensionListModel GetInventoryProductDimensionList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryProductDimensionModel CreateInventoryProductDimension(InventoryProductDimensionModel model);
        InventoryProductDimensionModel GetInventoryProductDimension(short inventoryCategoryId);
        bool UpdateInventoryProductDimension(InventoryProductDimensionModel model);
        bool DeleteInventoryProductDimension(ParameterModel parameterModel);
    }
}
