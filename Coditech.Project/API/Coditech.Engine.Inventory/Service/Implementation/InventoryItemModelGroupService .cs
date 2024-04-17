
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryItemModelGroupService : IInventoryItemModelGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemModelGroup> _inventoryItemModelGroupRepository;
        public InventoryItemModelGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemModelGroupRepository = new CoditechRepository<InventoryItemModelGroup>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryItemModelGroupListModel GetInventoryItemModelGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryItemModelGroupModel> objStoredProc = new CoditechViewRepository<InventoryItemModelGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryItemModelGroupModel> InventoryItemModelGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryItemModelGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryItemModelGroupListModel listModel = new InventoryItemModelGroupListModel();

            listModel.InventoryItemModelGroupList = InventoryItemModelGroupList?.Count > 0 ? InventoryItemModelGroupList : new List<InventoryItemModelGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryItemModelGroup.
        public virtual InventoryItemModelGroupModel CreateInventoryItemModelGroup(InventoryItemModelGroupModel inventoryItemModelGroupModel)
        {
            if (IsNull(inventoryItemModelGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemModelGroupCodeAlreadyExist(inventoryItemModelGroupModel.ItemModelGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ItemModelGroupCode"));

            InventoryItemModelGroup inventoryItemModelGroup = inventoryItemModelGroupModel.FromModelToEntity<InventoryItemModelGroup>();

            //Create new InventoryItemModelGroup and return it.
            InventoryItemModelGroup inventoryItemModelGroupData = _inventoryItemModelGroupRepository.Insert(inventoryItemModelGroup);
            if (inventoryItemModelGroupData?.InventoryItemModelGroupId > 0)
            {
                inventoryItemModelGroupModel.InventoryItemModelGroupId = inventoryItemModelGroupData.InventoryItemModelGroupId;
            }
            else
            {
                inventoryItemModelGroupModel.HasError = true;
                inventoryItemModelGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemModelGroupModel;
        }

        //Get InventoryItemModelGroup by InventoryItemModelGroup id.
        public virtual InventoryItemModelGroupModel GetInventoryItemModelGroup(short inventoryItemModelGroupId)
        {
            if (inventoryItemModelGroupId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemModelGroupID"));

            //Get the InventoryItemModelGroup Details based on id.
            InventoryItemModelGroup inventoryItemModelGroup = _inventoryItemModelGroupRepository.Table.FirstOrDefault(x => x.InventoryItemModelGroupId == inventoryItemModelGroupId);
            InventoryItemModelGroupModel inventoryItemModelGroupModel = inventoryItemModelGroup?.FromEntityToModel<InventoryItemModelGroupModel>();
            return inventoryItemModelGroupModel;
        }

        //Update InventoryItemModelGroup.
        public virtual bool UpdateInventoryItemModelGroup(InventoryItemModelGroupModel inventoryItemModelGroupModel)
        {
            if (IsNull(inventoryItemModelGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryItemModelGroupModel.InventoryItemModelGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemModelGroupID"));

            if (IsInventoryItemModelGroupCodeAlreadyExist(inventoryItemModelGroupModel.ItemModelGroupCode, inventoryItemModelGroupModel.InventoryItemModelGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ItemModelGroupCode"));

            InventoryItemModelGroup inventoryItemModelGroup = inventoryItemModelGroupModel.FromModelToEntity<InventoryItemModelGroup>();

            //Update InventoryItemModelGroup
            bool isInventoryItemModelGroupUpdated = _inventoryItemModelGroupRepository.Update(inventoryItemModelGroup);
            if (!isInventoryItemModelGroupUpdated)
            {
                inventoryItemModelGroupModel.HasError = true;
                inventoryItemModelGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryItemModelGroupUpdated;
        }

        //Delete InventoryItemModelGroup.
        public virtual bool DeleteInventoryItemModelGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemModelGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryItemModelGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryItemModelGroup @InventoryItemModelGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryItemModelGroup code is already present or not.
        protected virtual bool IsInventoryItemModelGroupCodeAlreadyExist(string inventoryItemModelGroupName, short inventoryItemModelGroupId = 0)
         => _inventoryItemModelGroupRepository.Table.Any(x => x.ItemModelGroupName == inventoryItemModelGroupName && (x.InventoryItemModelGroupId != inventoryItemModelGroupId || inventoryItemModelGroupId == 0));
        #endregion
    }
}
