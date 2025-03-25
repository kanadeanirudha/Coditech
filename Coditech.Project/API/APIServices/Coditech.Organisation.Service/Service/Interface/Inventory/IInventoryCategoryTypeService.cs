using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryCategoryTypeService
    {
        InventoryCategoryTypeListModel GetInventoryCategoryTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryCategoryTypeModel CreateInventoryCategoryType(InventoryCategoryTypeModel model);
        InventoryCategoryTypeModel GetInventoryCategoryType(byte inventoryCategoryTypeMasterId);
        bool UpdateInventoryCategoryType(InventoryCategoryTypeModel model);
        bool DeleteInventoryCategoryType(ParameterModel parameterModel);
    }
}
