using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryUoMMasterService
    {
        InventoryUoMMasterListModel GetInventoryUoMMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryUoMMasterModel CreateInventoryUoMMaster(InventoryUoMMasterModel model);
        InventoryUoMMasterModel GetInventoryUoMMaster(short inventoryCategoryId);
        bool UpdateInventoryUoMMaster(InventoryUoMMasterModel model);
        bool DeleteInventoryUoMMaster(ParameterModel parameterModel);
    }
}
