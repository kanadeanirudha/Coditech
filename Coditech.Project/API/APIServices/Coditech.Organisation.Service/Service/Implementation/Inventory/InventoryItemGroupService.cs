using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryItemGroupService : IInventoryItemGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemGroup> _inventoryItemGroupRepository;
        public InventoryItemGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemGroupRepository = new CoditechRepository<InventoryItemGroup>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryItemGroupListModel GetInventoryItemGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryItemGroupModel> objStoredProc = new CoditechViewRepository<InventoryItemGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryItemGroupModel> InventoryItemGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryItemGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryItemGroupListModel listModel = new InventoryItemGroupListModel();

            listModel.InventoryItemGroupList = InventoryItemGroupList?.Count > 0 ? InventoryItemGroupList : new List<InventoryItemGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryItemGroup.
        public virtual InventoryItemGroupModel CreateInventoryItemGroup(InventoryItemGroupModel inventoryItemGroupModel)
        {
            if (IsNull(inventoryItemGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemGroupCodeAlreadyExist(inventoryItemGroupModel.ItemGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Inventory Item Group Code"));

            InventoryItemGroup inventoryItemGroup = inventoryItemGroupModel.FromModelToEntity<InventoryItemGroup>();

            //Create new InventoryItemGroup and return it.
            InventoryItemGroup inventoryItemGroupData = _inventoryItemGroupRepository.Insert(inventoryItemGroup);
            if (inventoryItemGroupData?.InventoryItemGroupId > 0)
            {
                inventoryItemGroupModel.InventoryItemGroupId = inventoryItemGroupData.InventoryItemGroupId;
            }
            else
            {
                inventoryItemGroupModel.HasError = true;
                inventoryItemGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemGroupModel;
        }

        //Get InventoryItemGroup by InventoryItemGroup id.
        public virtual InventoryItemGroupModel GetInventoryItemGroup(short inventoryItemGroupId)
        {
            if (inventoryItemGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemGroupID"));

            //Get the InventoryItemGroup Details based on id.
            InventoryItemGroup inventoryItemGroup = _inventoryItemGroupRepository.Table.FirstOrDefault(x => x.InventoryItemGroupId == inventoryItemGroupId);
            InventoryItemGroupModel inventoryItemGroupModel = inventoryItemGroup?.FromEntityToModel<InventoryItemGroupModel>();
            return inventoryItemGroupModel;
        }

        //Update InventoryItemGroup.
        public virtual bool UpdateInventoryItemGroup(InventoryItemGroupModel inventoryItemGroupModel)
        {
            if (IsNull(inventoryItemGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryItemGroupModel.InventoryItemGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemGroupID"));

            if (IsInventoryItemGroupCodeAlreadyExist(inventoryItemGroupModel.ItemGroupCode, inventoryItemGroupModel.InventoryItemGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryItemGroup Code"));

            InventoryItemGroup inventoryItemGroup = inventoryItemGroupModel.FromModelToEntity<InventoryItemGroup>();

            //Update InventoryItemGroup
            bool isInventoryItemGroupUpdated = _inventoryItemGroupRepository.Update(inventoryItemGroup);
            if (!isInventoryItemGroupUpdated)
            {
                inventoryItemGroupModel.HasError = true;
                inventoryItemGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryItemGroupUpdated;
        }

        //Delete InventoryItemGroup.
        public virtual bool DeleteInventoryItemGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryItemGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryItemGroup @InventoryItemGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryItemGroup code is already present or not.
        protected virtual bool IsInventoryItemGroupCodeAlreadyExist(string itemGroupCode, short inventoryItemGroupId = 0)
         => _inventoryItemGroupRepository.Table.Any(x => x.ItemGroupCode == itemGroupCode && (x.InventoryItemGroupId != inventoryItemGroupId || inventoryItemGroupId == 0));
        #endregion
    }
}
