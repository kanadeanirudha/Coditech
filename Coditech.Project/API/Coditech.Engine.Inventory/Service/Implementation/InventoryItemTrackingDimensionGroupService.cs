
using Coditech.API.Data;
using Coditech.API.Data.DataModel.Inventory;
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
    public class InventoryItemTrackingDimensionGroupService : IInventoryItemTrackingDimensionGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryItemTrackingDimensionGroup> _inventoryItemTrackingDimensionGroupRepository;
        private readonly ICoditechRepository<InventoryItemTrackingDimensionGroupMapper> _inventoryItemTrackingDimensionGroupMapperRepository;
        private readonly ICoditechRepository<InventoryItemTrackingDimension> _inventoryItemTrackingDimensionRepository;
        public InventoryItemTrackingDimensionGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryItemTrackingDimensionGroupRepository = new CoditechRepository<InventoryItemTrackingDimensionGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryItemTrackingDimensionGroupMapperRepository = new CoditechRepository<InventoryItemTrackingDimensionGroupMapper>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryItemTrackingDimensionRepository = new CoditechRepository<InventoryItemTrackingDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryItemTrackingDimensionGroupListModel GetInventoryItemTrackingDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryItemTrackingDimensionGroupModel> objStoredProc = new CoditechViewRepository<InventoryItemTrackingDimensionGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryItemTrackingDimensionGroupModel> inventoryItemTrackingDimensionGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryItemTrackingDimensionGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryItemTrackingDimensionGroupListModel listModel = new InventoryItemTrackingDimensionGroupListModel();

            listModel.InventoryItemTrackingDimensionGroupList = inventoryItemTrackingDimensionGroupList?.Count > 0 ? inventoryItemTrackingDimensionGroupList : new List<InventoryItemTrackingDimensionGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryItemTrackingDimensionGroup.
        public virtual InventoryItemTrackingDimensionGroupModel CreateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel)
        {
            if (IsNull(inventoryItemTrackingDimensionGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryItemTrackingDimensionGroupCodeAlreadyExist(inventoryItemTrackingDimensionGroupModel.ItemTrackingDimensionGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ItemTrackingDimensionGroupCode"));

            InventoryItemTrackingDimensionGroup inventoryItemTrackingDimensionGroup = inventoryItemTrackingDimensionGroupModel.FromModelToEntity<InventoryItemTrackingDimensionGroup>();

            //Create new InventoryItemTrackingDimensionGroup and return it.
            InventoryItemTrackingDimensionGroup inventoryItemTrackingDimensionGroupData = _inventoryItemTrackingDimensionGroupRepository.Insert(inventoryItemTrackingDimensionGroup);
            if (inventoryItemTrackingDimensionGroupData?.InventoryItemTrackingDimensionGroupId > 0)
            {
                inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupId = inventoryItemTrackingDimensionGroupData.InventoryItemTrackingDimensionGroupId;
                InserUpdateInventoryItemTrackingDimensionGroupMapper(inventoryItemTrackingDimensionGroupModel);
            }
            else
            {
                inventoryItemTrackingDimensionGroupModel.HasError = true;
                inventoryItemTrackingDimensionGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryItemTrackingDimensionGroupModel;
        }

        //Get InventoryItemTrackingDimensionGroup by InventoryItemTrackingDimensionGroup id.
        public virtual InventoryItemTrackingDimensionGroupModel GetInventoryItemTrackingDimensionGroup(int inventoryItemTrackingDimensionGroupId)
        {
            InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel = new InventoryItemTrackingDimensionGroupModel();
            List<InventoryItemTrackingDimensionGroupMapper> inventoryItemTrackingDimensionGroupMapperList = new List<InventoryItemTrackingDimensionGroupMapper>();
            //Get the InventoryItemTrackingDimensionGroup Details based on id.
            if (inventoryItemTrackingDimensionGroupId > 0)
            {
                InventoryItemTrackingDimensionGroup inventoryItemTrackingDimensionGroup = _inventoryItemTrackingDimensionGroupRepository.Table.Where(x => x.InventoryItemTrackingDimensionGroupId == inventoryItemTrackingDimensionGroupId)?.FirstOrDefault();
                inventoryItemTrackingDimensionGroupModel = inventoryItemTrackingDimensionGroup?.FromEntityToModel<InventoryItemTrackingDimensionGroupModel>();
                inventoryItemTrackingDimensionGroupMapperList = _inventoryItemTrackingDimensionGroupMapperRepository.Table.Where(x => x.InventoryItemTrackingDimensionGroupId == inventoryItemTrackingDimensionGroupId)?.ToList();
            }

            List<InventoryItemTrackingDimension> inventoryItemTrackingDimensionList = _inventoryItemTrackingDimensionRepository.Table.ToList();
            foreach (InventoryItemTrackingDimension inventoryItemTrackingDimension in inventoryItemTrackingDimensionList)
            {
                InventoryItemTrackingDimensionGroupMapperModel inventoryItemTrackingDimensionGroupMapperModel = new InventoryItemTrackingDimensionGroupMapperModel()
                {
                    InventoryItemTrackingDimensionId = inventoryItemTrackingDimension.InventoryItemTrackingDimensionId,
                    ItemTrackingDimensionName = inventoryItemTrackingDimension.TrackingDimensionName
                };

                if (inventoryItemTrackingDimensionGroupMapperList?.Count > 0)
                {
                    InventoryItemTrackingDimensionGroupMapper inventoryItemTrackingDimensionGroupMapper = inventoryItemTrackingDimensionGroupMapperList.FirstOrDefault(x => x.InventoryItemTrackingDimensionId == inventoryItemTrackingDimension.InventoryItemTrackingDimensionId);
                    if (IsNotNull(inventoryItemTrackingDimensionGroupMapper))
                    {
                        inventoryItemTrackingDimensionGroupMapperModel.InventoryItemTrackingDimensionGroupMapperId = inventoryItemTrackingDimensionGroupMapper.InventoryItemTrackingDimensionGroupMapperId;
                        inventoryItemTrackingDimensionGroupMapperModel.Active = inventoryItemTrackingDimensionGroupMapper.Active;
                        inventoryItemTrackingDimensionGroupMapperModel.ActiveInSalesProcess = inventoryItemTrackingDimensionGroupMapper.ActiveInSalesProcess;
                        inventoryItemTrackingDimensionGroupMapperModel.PrimaryStocking = inventoryItemTrackingDimensionGroupMapper.PrimaryStocking;
                        inventoryItemTrackingDimensionGroupMapperModel.BlankReceiptAllowed = inventoryItemTrackingDimensionGroupMapper.BlankReceiptAllowed;
                        inventoryItemTrackingDimensionGroupMapperModel.BlankIssueAllowed = inventoryItemTrackingDimensionGroupMapper.BlankIssueAllowed;
                        inventoryItemTrackingDimensionGroupMapperModel.PhysicalInventory = inventoryItemTrackingDimensionGroupMapper.PhysicalInventory;
                        inventoryItemTrackingDimensionGroupMapperModel.FinancialInventory = inventoryItemTrackingDimensionGroupMapper.FinancialInventory;
                        inventoryItemTrackingDimensionGroupMapperModel.CoveragePlanByDimension = inventoryItemTrackingDimensionGroupMapper.CoveragePlanByDimension;
                        inventoryItemTrackingDimensionGroupMapperModel.ForPurchasePrices = inventoryItemTrackingDimensionGroupMapper.ForPurchasePrices;
                        inventoryItemTrackingDimensionGroupMapperModel.ForSalePrices = inventoryItemTrackingDimensionGroupMapper.ForSalePrices;
                        inventoryItemTrackingDimensionGroupMapperModel.Transfer = inventoryItemTrackingDimensionGroupMapper.Transfer;
                        inventoryItemTrackingDimensionGroupMapperModel.DisplayOrder = inventoryItemTrackingDimensionGroupMapper.DisplayOrder;

                    }
                }
                inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupMapperList.Add(inventoryItemTrackingDimensionGroupMapperModel);
            }
            return inventoryItemTrackingDimensionGroupModel;
        }

        //Update InventoryItemTrackingDimensionGroup.
        public virtual bool UpdateInventoryItemTrackingDimensionGroup(InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel)
        {
            if (IsNull(inventoryItemTrackingDimensionGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemTrackingDimensionGroupID"));

            if (IsInventoryItemTrackingDimensionGroupCodeAlreadyExist(inventoryItemTrackingDimensionGroupModel.ItemTrackingDimensionGroupCode, inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ItemTrackingDimensionGroupCode"));

            InventoryItemTrackingDimensionGroup inventoryItemTrackingDimensionGroup = inventoryItemTrackingDimensionGroupModel.FromModelToEntity<InventoryItemTrackingDimensionGroup>();

            //Update InventoryItemTrackingDimensionGroup
            bool isInventoryItemTrackingDimensionGroupUpdated = _inventoryItemTrackingDimensionGroupRepository.Update(inventoryItemTrackingDimensionGroup);
            if (!isInventoryItemTrackingDimensionGroupUpdated)
            {
                inventoryItemTrackingDimensionGroupModel.HasError = true;
                inventoryItemTrackingDimensionGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            else
            {
                InserUpdateInventoryItemTrackingDimensionGroupMapper(inventoryItemTrackingDimensionGroupModel);
            }
            return isInventoryItemTrackingDimensionGroupUpdated;
        }

        //Delete InventoryItemTrackingDimensionGroup.
        public virtual bool DeleteInventoryItemTrackingDimensionGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryItemTrackingDimensionGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryItemTrackingDimensionGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryItemTrackingDimensionGroup @InventoryItemTrackingDimensionGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryItemTrackingDimensionGroup code is already present or not.
        protected virtual bool IsInventoryItemTrackingDimensionGroupCodeAlreadyExist(string inventoryItemTrackingDimensionGroupCode, int inventoryItemTrackingDimensionGroupId = 0)
         => _inventoryItemTrackingDimensionGroupRepository.Table.Any(x => x.ItemTrackingDimensionGroupCode == inventoryItemTrackingDimensionGroupCode && (x.InventoryItemTrackingDimensionGroupId != inventoryItemTrackingDimensionGroupId || inventoryItemTrackingDimensionGroupId == 0));

        protected virtual void InserUpdateInventoryItemTrackingDimensionGroupMapper(InventoryItemTrackingDimensionGroupModel inventoryItemTrackingDimensionGroupModel)
        {
            if (inventoryItemTrackingDimensionGroupModel?.InventoryItemTrackingDimensionGroupMapperList?.Count > 0)
            {
                List<InventoryItemTrackingDimensionGroupMapper> inventoryItemTrackingDimensionGroupMapperInsertList = new List<InventoryItemTrackingDimensionGroupMapper>();
                List<InventoryItemTrackingDimensionGroupMapper> inventoryItemTrackingDimensionGroupMapperUpdateList = new List<InventoryItemTrackingDimensionGroupMapper>();
                foreach (InventoryItemTrackingDimensionGroupMapperModel item in inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupMapperList)
                {
                    InventoryItemTrackingDimensionGroupMapper inventoryItemTrackingDimensionGroupMapper = item.FromModelToEntity<InventoryItemTrackingDimensionGroupMapper>();
                    inventoryItemTrackingDimensionGroupMapper.InventoryItemTrackingDimensionGroupId = inventoryItemTrackingDimensionGroupModel.InventoryItemTrackingDimensionGroupId;
                    if (item.InventoryItemTrackingDimensionGroupMapperId > 0)
                        inventoryItemTrackingDimensionGroupMapperUpdateList.Add(inventoryItemTrackingDimensionGroupMapper);
                    else
                        inventoryItemTrackingDimensionGroupMapperInsertList.Add(inventoryItemTrackingDimensionGroupMapper);
                }
                if (inventoryItemTrackingDimensionGroupMapperInsertList?.Count > 0)
                    _inventoryItemTrackingDimensionGroupMapperRepository.Insert(inventoryItemTrackingDimensionGroupMapperInsertList);

                if (inventoryItemTrackingDimensionGroupMapperUpdateList?.Count > 0)
                    _inventoryItemTrackingDimensionGroupMapperRepository.BatchUpdate(inventoryItemTrackingDimensionGroupMapperUpdateList);
            }
        }

        #endregion
    }
}
