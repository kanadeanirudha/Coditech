using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryCategoryService
    {
        InventoryCategoryListModel GetInventoryCategoryList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryCategoryModel CreateInventoryCategory(InventoryCategoryModel model);
        InventoryCategoryModel GetInventoryCategory(short inventoryCategoryId);
        bool UpdateInventoryCategory(InventoryCategoryModel model);
        bool DeleteInventoryCategory(ParameterModel parameterModel);
    }
}
