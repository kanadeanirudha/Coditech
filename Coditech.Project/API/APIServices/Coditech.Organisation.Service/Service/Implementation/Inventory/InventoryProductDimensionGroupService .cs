
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
    public class InventoryProductDimensionGroupService : IInventoryProductDimensionGroupService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryProductDimensionGroup> _inventoryProductDimensionGroupRepository;
        private readonly ICoditechRepository<InventoryProductDimensionGroupMapper> _inventoryProductDimensionGroupMapperRepository;
        private readonly ICoditechRepository<InventoryProductDimension> _inventoryProductDimensionRepository;
        public InventoryProductDimensionGroupService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryProductDimensionGroupRepository = new CoditechRepository<InventoryProductDimensionGroup>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryProductDimensionGroupMapperRepository = new CoditechRepository<InventoryProductDimensionGroupMapper>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryProductDimensionRepository = new CoditechRepository<InventoryProductDimension>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryProductDimensionGroupListModel GetInventoryProductDimensionGroupList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryProductDimensionGroupModel> objStoredProc = new CoditechViewRepository<InventoryProductDimensionGroupModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryProductDimensionGroupModel> inventoryProductDimensionGroupList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryProductDimensionGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryProductDimensionGroupListModel listModel = new InventoryProductDimensionGroupListModel();

            listModel.InventoryProductDimensionGroupList = inventoryProductDimensionGroupList?.Count > 0 ? inventoryProductDimensionGroupList : new List<InventoryProductDimensionGroupModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create InventoryProductDimensionGroup.
        public virtual InventoryProductDimensionGroupModel CreateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel)
        {
            if (IsNull(inventoryProductDimensionGroupModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsInventoryProductDimensionGroupCodeAlreadyExist(inventoryProductDimensionGroupModel.ProductDimensionGroupCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ProductDimensionGroupCode"));

            InventoryProductDimensionGroup inventoryProductDimensionGroup = inventoryProductDimensionGroupModel.FromModelToEntity<InventoryProductDimensionGroup>();

            //Create new InventoryProductDimensionGroup and return it.
            InventoryProductDimensionGroup inventoryProductDimensionGroupData = _inventoryProductDimensionGroupRepository.Insert(inventoryProductDimensionGroup);
            if (inventoryProductDimensionGroupData?.InventoryProductDimensionGroupId > 0)
            {
                inventoryProductDimensionGroupModel.InventoryProductDimensionGroupId = inventoryProductDimensionGroupData.InventoryProductDimensionGroupId;
                InserUpdateInventoryProductDimensionGroupMapper(inventoryProductDimensionGroupModel);
            }
            else
            {
                inventoryProductDimensionGroupModel.HasError = true;
                inventoryProductDimensionGroupModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryProductDimensionGroupModel;
        }

        //Get InventoryProductDimensionGroup by InventoryProductDimensionGroup id.
        public virtual InventoryProductDimensionGroupModel GetInventoryProductDimensionGroup(int inventoryProductDimensionGroupId)
        {
            InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel = new InventoryProductDimensionGroupModel();
            List<InventoryProductDimensionGroupMapper> inventoryProductDimensionGroupMapperList = new List<InventoryProductDimensionGroupMapper>();
            //Get the InventoryProductDimensionGroup Details based on id.
            if (inventoryProductDimensionGroupId > 0)
            {
                InventoryProductDimensionGroup inventoryProductDimensionGroup = _inventoryProductDimensionGroupRepository.Table.Where(x => x.InventoryProductDimensionGroupId == inventoryProductDimensionGroupId)?.FirstOrDefault();
                inventoryProductDimensionGroupModel = inventoryProductDimensionGroup?.FromEntityToModel<InventoryProductDimensionGroupModel>();
                inventoryProductDimensionGroupMapperList = _inventoryProductDimensionGroupMapperRepository.Table.Where(x => x.InventoryProductDimensionGroupId == inventoryProductDimensionGroupId)?.ToList();
            }

            List<InventoryProductDimension> inventoryProductDimensionList = _inventoryProductDimensionRepository.Table.ToList();
            foreach (InventoryProductDimension inventoryProductDimension in inventoryProductDimensionList)
            {
                InventoryProductDimensionGroupMapperModel inventoryProductDimensionGroupMapperModel = new InventoryProductDimensionGroupMapperModel()
                {
                    InventoryProductDimensionId = inventoryProductDimension.InventoryProductDimensionId,
                    ProductDimensionName = inventoryProductDimension.ProductDimensionName
                };

                if (inventoryProductDimensionGroupMapperList?.Count > 0)
                {
                    InventoryProductDimensionGroupMapper inventoryProductDimensionGroupMapper = inventoryProductDimensionGroupMapperList.FirstOrDefault(x => x.InventoryProductDimensionId == inventoryProductDimension.InventoryProductDimensionId);
                    if (IsNotNull(inventoryProductDimensionGroupMapper))
                    {
                        inventoryProductDimensionGroupMapperModel.InventoryProductDimensionGroupMapperId = inventoryProductDimensionGroupMapper.InventoryProductDimensionGroupMapperId;
                        inventoryProductDimensionGroupMapperModel.ForPurchase = inventoryProductDimensionGroupMapper.ForPurchase;
                        inventoryProductDimensionGroupMapperModel.ForSale = inventoryProductDimensionGroupMapper.ForSale;
                        inventoryProductDimensionGroupMapperModel.IsActive = inventoryProductDimensionGroupMapper.IsActive;
                        inventoryProductDimensionGroupMapperModel.DisplayOrder = inventoryProductDimensionGroupMapper.DisplayOrder;
                    }
                }
                inventoryProductDimensionGroupModel.InventoryProductDimensionGroupMapperList.Add(inventoryProductDimensionGroupMapperModel);
            }
            return inventoryProductDimensionGroupModel;
        }

        //Update InventoryProductDimensionGroup.
        public virtual bool UpdateInventoryProductDimensionGroup(InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel)
        {
            if (IsNull(inventoryProductDimensionGroupModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryProductDimensionGroupModel.InventoryProductDimensionGroupId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryProductDimensionGroupID"));

            if (IsInventoryProductDimensionGroupCodeAlreadyExist(inventoryProductDimensionGroupModel.ProductDimensionGroupCode, inventoryProductDimensionGroupModel.InventoryProductDimensionGroupId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "ProductDimensionGroupCode"));

            InventoryProductDimensionGroup inventoryProductDimensionGroup = inventoryProductDimensionGroupModel.FromModelToEntity<InventoryProductDimensionGroup>();

            //Update InventoryProductDimensionGroup
            bool isInventoryProductDimensionGroupUpdated = _inventoryProductDimensionGroupRepository.Update(inventoryProductDimensionGroup);
            if (!isInventoryProductDimensionGroupUpdated)
            {
                inventoryProductDimensionGroupModel.HasError = true;
                inventoryProductDimensionGroupModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            else
            {
                InserUpdateInventoryProductDimensionGroupMapper(inventoryProductDimensionGroupModel);
            }
            return isInventoryProductDimensionGroupUpdated;
        }

        //Delete InventoryProductDimensionGroup.
        public virtual bool DeleteInventoryProductDimensionGroup(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryProductDimensionGroupID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryProductDimensionGroupId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryProductDimensionGroup @InventoryProductDimensionGroupId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if InventoryProductDimensionGroup code is already present or not.
        protected virtual bool IsInventoryProductDimensionGroupCodeAlreadyExist(string inventoryProductDimensionGroupCode, int inventoryProductDimensionGroupId = 0)
         => _inventoryProductDimensionGroupRepository.Table.Any(x => x.ProductDimensionGroupCode == inventoryProductDimensionGroupCode && (x.InventoryProductDimensionGroupId != inventoryProductDimensionGroupId || inventoryProductDimensionGroupId == 0));

        protected virtual void InserUpdateInventoryProductDimensionGroupMapper(InventoryProductDimensionGroupModel inventoryProductDimensionGroupModel)
        {
            if (inventoryProductDimensionGroupModel?.InventoryProductDimensionGroupMapperList?.Count > 0)
            {
                List<InventoryProductDimensionGroupMapper> inventoryProductDimensionGroupMapperInsertList = new List<InventoryProductDimensionGroupMapper>();
                List<InventoryProductDimensionGroupMapper> inventoryProductDimensionGroupMapperUpdateList = new List<InventoryProductDimensionGroupMapper>();
                foreach (InventoryProductDimensionGroupMapperModel item in inventoryProductDimensionGroupModel.InventoryProductDimensionGroupMapperList)
                {
                    InventoryProductDimensionGroupMapper inventoryProductDimensionGroupMapper = item.FromModelToEntity<InventoryProductDimensionGroupMapper>();
                    inventoryProductDimensionGroupMapper.InventoryProductDimensionGroupId = inventoryProductDimensionGroupModel.InventoryProductDimensionGroupId;
                    if (item.InventoryProductDimensionGroupMapperId > 0)
                        inventoryProductDimensionGroupMapperUpdateList.Add(inventoryProductDimensionGroupMapper);
                    else
                        inventoryProductDimensionGroupMapperInsertList.Add(inventoryProductDimensionGroupMapper);
                }
                if (inventoryProductDimensionGroupMapperInsertList?.Count > 0)
                    _inventoryProductDimensionGroupMapperRepository.Insert(inventoryProductDimensionGroupMapperInsertList);

                if (inventoryProductDimensionGroupMapperUpdateList?.Count > 0)
                    _inventoryProductDimensionGroupMapperRepository.BatchUpdate(inventoryProductDimensionGroupMapperUpdateList);
            }
        }

        #endregion
    }
}
