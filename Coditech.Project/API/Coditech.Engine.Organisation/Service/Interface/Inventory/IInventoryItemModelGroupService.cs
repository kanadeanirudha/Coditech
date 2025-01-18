using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IInventoryItemModelGroupService
    {
        InventoryItemModelGroupListModel GetInventoryItemModelGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        InventoryItemModelGroupModel CreateInventoryItemModelGroup(InventoryItemModelGroupModel model);
        InventoryItemModelGroupModel GetInventoryItemModelGroup(short InventoryItemModelGroupId);
        bool UpdateInventoryItemModelGroup(InventoryItemModelGroupModel model);
        bool DeleteInventoryItemModelGroup(ParameterModel parameterModel);
    }
}
