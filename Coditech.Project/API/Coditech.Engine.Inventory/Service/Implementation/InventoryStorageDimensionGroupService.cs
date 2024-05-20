
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
    public class InventoryStorageDimensionGroupService : IInventoryStorageDimensionGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryStorageDimensionGroup> _inventoryStorageDimensionGroupRepository;
        private readonly ICoditechRepository<InventoryStorageDimensionGroupMapper> _inventoryStorageDimensionGroupMapperRepository;
        private readonly ICoditechRepository<InventoryItemStorageDimension> _inventoryStorageDimensionRepository;
        public InventoryStorageDimensionGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryStorageDimensionGroupRepository = new CoditechRepository<InventoryStorageDimensionGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryStorageDimensionGroupMapperRepository = new CoditechRepository<InventoryStorageDimensionGroupMapper>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryStorageDimensionRepository = new CoditechRepository<InventoryItemStorageDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryStorageDimensionGroupListModel GetInventoryStorageDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryStorageDimensionGroupModel> objStoredProc = new CoditechViewRepository<InventoryStorageDimensionGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryStorageDimensionGroupModel> inventoryStorageDimensionGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryStorageDimensionGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryStorageDimensionGroupListModel listModel = new InventoryStorageDimensionGroupListModel();

            listModel.InventoryStorageDimensionGroupList = inventoryStorageDimensionGroupList?.Count > 0 ? inventoryStorageDimensionGroupList : new List<InventoryStorageDimensionGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryStorageDimensionGroup.
        public virtual InventoryStorageDimensionGroupModel CreateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel)
        {
            if (IsNull(inventoryStorageDimensionGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryStorageDimensionGroupCodeAlreadyExist(inventoryStorageDimensionGroupModel.StorageDimensionGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "StorageDimensionGroupCode"));

            InventoryStorageDimensionGroup inventoryStorageDimensionGroup = inventoryStorageDimensionGroupModel.FromModelToEntity<InventoryStorageDimensionGroup>();

            //Create new InventoryStorageDimensionGroup and return it.
            InventoryStorageDimensionGroup inventoryStorageDimensionGroupData = _inventoryStorageDimensionGroupRepository.Insert(inventoryStorageDimensionGroup);
            if (inventoryStorageDimensionGroupData?.InventoryStorageDimensionGroupId > 0)
            {
                inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupId = inventoryStorageDimensionGroupData.InventoryStorageDimensionGroupId;
                InserUpdateInventoryStorageDimensionGroupMapper(inventoryStorageDimensionGroupModel);
            }
            else
            {
                inventoryStorageDimensionGroupModel.HasError = true;
                inventoryStorageDimensionGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryStorageDimensionGroupModel;
        }

        //Get InventoryStorageDimensionGroup by InventoryStorageDimensionGroup id.
        public virtual InventoryStorageDimensionGroupModel GetInventoryStorageDimensionGroup(int inventoryStorageDimensionGroupId)
        {
            InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel = new InventoryStorageDimensionGroupModel();
            List<InventoryStorageDimensionGroupMapper> inventoryStorageDimensionGroupMapperList = new List<InventoryStorageDimensionGroupMapper>();
            //Get the InventoryStorageDimensionGroup Details based on id.
            if (inventoryStorageDimensionGroupId > 0)
            {
                InventoryStorageDimensionGroup inventoryStorageDimensionGroup = _inventoryStorageDimensionGroupRepository.Table.Where(x => x.InventoryStorageDimensionGroupId == inventoryStorageDimensionGroupId)?.FirstOrDefault();
                inventoryStorageDimensionGroupModel = inventoryStorageDimensionGroup?.FromEntityToModel<InventoryStorageDimensionGroupModel>();
                inventoryStorageDimensionGroupMapperList = _inventoryStorageDimensionGroupMapperRepository.Table.Where(x => x.InventoryStorageDimensionGroupId == inventoryStorageDimensionGroupId)?.ToList();
            }

            List<InventoryItemStorageDimension> inventoryStorageDimensionList = _inventoryStorageDimensionRepository.Table.ToList();
            foreach (InventoryItemStorageDimension inventoryStorageDimension in inventoryStorageDimensionList)
            {
                InventoryStorageDimensionGroupMapperModel inventoryStorageDimensionGroupMapperModel = new InventoryStorageDimensionGroupMapperModel()
                {
                    InventoryStorageDimensionId = inventoryStorageDimension.InventoryItemStorageDimensionId,
                    StorageDimensionName = inventoryStorageDimension.StorageDimensionName
                };

                if (inventoryStorageDimensionGroupMapperList?.Count > 0)
                {
                    InventoryStorageDimensionGroupMapper inventoryStorageDimensionGroupMapper = inventoryStorageDimensionGroupMapperList.FirstOrDefault(x => x.InventoryStorageDimensionId == inventoryStorageDimension.InventoryItemStorageDimensionId);
                    if (IsNotNull(inventoryStorageDimensionGroupMapper))
                    {
                        inventoryStorageDimensionGroupMapperModel.InventoryStorageDimensionGroupMapperId = inventoryStorageDimensionGroupMapper.InventoryStorageDimensionGroupMapperId;
                        inventoryStorageDimensionGroupMapperModel.Active = inventoryStorageDimensionGroupMapper.Active;
                        inventoryStorageDimensionGroupMapperModel.BlankReceiptAllowed = inventoryStorageDimensionGroupMapper.BlankReceiptAllowed;
                        inventoryStorageDimensionGroupMapperModel.BlankIssueAllowed = inventoryStorageDimensionGroupMapper.BlankIssueAllowed;
                        inventoryStorageDimensionGroupMapperModel.CoveragePlanByDimension = inventoryStorageDimensionGroupMapper.CoveragePlanByDimension;
                        inventoryStorageDimensionGroupMapperModel.FinancialInventory = inventoryStorageDimensionGroupMapper.FinancialInventory;
                        inventoryStorageDimensionGroupMapperModel.ForPurchasePrices = inventoryStorageDimensionGroupMapper.ForPurchasePrices;
                        inventoryStorageDimensionGroupMapperModel.ForSalePrices = inventoryStorageDimensionGroupMapper.ForSalePrices;
                        inventoryStorageDimensionGroupMapperModel.PhysicalInventory = inventoryStorageDimensionGroupMapper.PhysicalInventory;
                        inventoryStorageDimensionGroupMapperModel.PrimaryStocking = inventoryStorageDimensionGroupMapper.PrimaryStocking;
                        inventoryStorageDimensionGroupMapperModel.Reference = inventoryStorageDimensionGroupMapper.Reference;
                        inventoryStorageDimensionGroupMapperModel.Transfer = inventoryStorageDimensionGroupMapper.Transfer;
                        inventoryStorageDimensionGroupMapperModel.DisplayOrder = inventoryStorageDimensionGroupMapper.DisplayOrder;
                        
                    }
                }
                inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupMapperList.Add(inventoryStorageDimensionGroupMapperModel);
            }
            return inventoryStorageDimensionGroupModel;
        }

        //Update InventoryStorageDimensionGroup.
        public virtual bool UpdateInventoryStorageDimensionGroup(InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel)
        {
            if (IsNull(inventoryStorageDimensionGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryStorageDimensionGroupID"));

            if (IsInventoryStorageDimensionGroupCodeAlreadyExist(inventoryStorageDimensionGroupModel.StorageDimensionGroupCode, inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "StorageDimensionGroupCode"));

            InventoryStorageDimensionGroup inventoryStorageDimensionGroup = inventoryStorageDimensionGroupModel.FromModelToEntity<InventoryStorageDimensionGroup>();

            //Update InventoryStorageDimensionGroup
            bool isInventoryStorageDimensionGroupUpdated = _inventoryStorageDimensionGroupRepository.Update(inventoryStorageDimensionGroup);
            if (!isInventoryStorageDimensionGroupUpdated)
            {
                inventoryStorageDimensionGroupModel.HasError = true;
                inventoryStorageDimensionGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            else
            {
                InserUpdateInventoryStorageDimensionGroupMapper(inventoryStorageDimensionGroupModel);
            }
            return isInventoryStorageDimensionGroupUpdated;
        }

        //Delete InventoryStorageDimensionGroup.
        public virtual bool DeleteInventoryStorageDimensionGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryStorageDimensionGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryStorageDimensionGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryStorageDimensionGroup @InventoryStorageDimensionGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryStorageDimensionGroup code is already present or not.
        protected virtual bool IsInventoryStorageDimensionGroupCodeAlreadyExist(string inventoryStorageDimensionGroupCode, int inventoryStorageDimensionGroupId = 0)
         => _inventoryStorageDimensionGroupRepository.Table.Any(x => x.StorageDimensionGroupCode == inventoryStorageDimensionGroupCode && (x.InventoryStorageDimensionGroupId != inventoryStorageDimensionGroupId || inventoryStorageDimensionGroupId == 0));

        protected virtual void InserUpdateInventoryStorageDimensionGroupMapper(InventoryStorageDimensionGroupModel inventoryStorageDimensionGroupModel)
        {
            if (inventoryStorageDimensionGroupModel?.InventoryStorageDimensionGroupMapperList?.Count > 0)
            {
                List<InventoryStorageDimensionGroupMapper> inventoryStorageDimensionGroupMapperInsertList = new List<InventoryStorageDimensionGroupMapper>();
                List<InventoryStorageDimensionGroupMapper> inventoryStorageDimensionGroupMapperUpdateList = new List<InventoryStorageDimensionGroupMapper>();
                foreach (InventoryStorageDimensionGroupMapperModel item in inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupMapperList)
                {
                    InventoryStorageDimensionGroupMapper inventoryStorageDimensionGroupMapper = item.FromModelToEntity<InventoryStorageDimensionGroupMapper>();
                    inventoryStorageDimensionGroupMapper.InventoryStorageDimensionGroupId = inventoryStorageDimensionGroupModel.InventoryStorageDimensionGroupId;
                    if (item.InventoryStorageDimensionGroupMapperId > 0)
                        inventoryStorageDimensionGroupMapperUpdateList.Add(inventoryStorageDimensionGroupMapper);
                    else
                        inventoryStorageDimensionGroupMapperInsertList.Add(inventoryStorageDimensionGroupMapper);
                }
                if (inventoryStorageDimensionGroupMapperInsertList?.Count > 0)
                    _inventoryStorageDimensionGroupMapperRepository.Insert(inventoryStorageDimensionGroupMapperInsertList);

                if (inventoryStorageDimensionGroupMapperUpdateList?.Count > 0)
                    _inventoryStorageDimensionGroupMapperRepository.BatchUpdate(inventoryStorageDimensionGroupMapperUpdateList);
            }
        }

        #endregion
    }
}
