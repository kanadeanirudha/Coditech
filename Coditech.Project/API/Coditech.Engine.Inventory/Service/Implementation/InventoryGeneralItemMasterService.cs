using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class InventoryGeneralItemMasterService : BaseService, IInventoryGeneralItemMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<InventoryGeneralItemMaster> _inventoryGeneralItemMasterRepository;
        private readonly ICoditechRepository<InventoryGeneralItemLine> _inventoryGeneralItemLineRepository;
        public InventoryGeneralItemMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _inventoryGeneralItemMasterRepository = new CoditechRepository<InventoryGeneralItemMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _inventoryGeneralItemLineRepository = new CoditechRepository<InventoryGeneralItemLine>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual InventoryGeneralItemMasterListModel GetInventoryGeneralItemMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<InventoryGeneralItemMasterModel> objStoredProc = new CoditechViewRepository<InventoryGeneralItemMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<InventoryGeneralItemMasterModel> inventoryGeneralItemMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetInventoryGeneralItemMasterList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            InventoryGeneralItemMasterListModel listModel = new InventoryGeneralItemMasterListModel();

            listModel.InventoryGeneralItemMasterList = inventoryGeneralItemMasterList?.Count > 0 ? inventoryGeneralItemMasterList : new List<InventoryGeneralItemMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Inventory General Item.
        public virtual InventoryGeneralItemMasterModel CreateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel)
        {
            if (IsNull(inventoryGeneralItemMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsItemDetailsAlreadyExist(inventoryGeneralItemMasterModel.ItemNumber))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Item Number"));

            InventoryGeneralItemMaster inventoryGeneralItemMaster = inventoryGeneralItemMasterModel.FromModelToEntity<InventoryGeneralItemMaster>();

            //Create new Inventory General Item and return it.
            InventoryGeneralItemMaster inventoryGeneralItemMasterData = _inventoryGeneralItemMasterRepository.Insert(inventoryGeneralItemMaster);
            if (inventoryGeneralItemMasterData?.InventoryGeneralItemMasterId > 0)
            {
                inventoryGeneralItemMasterModel.InventoryGeneralItemMasterId = inventoryGeneralItemMasterData.InventoryGeneralItemMasterId;
                InventoryGeneralItemLine inventoryGeneralItemLine = new InventoryGeneralItemLine()
                {
                    InventoryGeneralItemMasterId = inventoryGeneralItemMasterModel.InventoryGeneralItemMasterId,
                    SKU = inventoryGeneralItemMasterModel.ItemNumber,
                    ItemName = inventoryGeneralItemMasterModel.ItemName,
                    IsBaseUom = inventoryGeneralItemMasterModel.IsBaseUom,
                    InventoryBaseUoMMasterId = inventoryGeneralItemMasterModel.InventoryBaseUoMMasterId,
                    IsActive = inventoryGeneralItemMasterModel.IsActive
                };
                _inventoryGeneralItemLineRepository.Insert(inventoryGeneralItemLine);
            }
            else
            {
                inventoryGeneralItemMasterModel.HasError = true;
                inventoryGeneralItemMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return inventoryGeneralItemMasterModel;
        }

        //Get Inventory General Item by InventoryGeneralItemid.
        public virtual InventoryGeneralItemMasterModel GetInventoryGeneralItemMaster(int inventoryGeneralItemMasterId)
        {
            if (inventoryGeneralItemMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "inventoryGeneralItemMasterId"));

            //Get the  Inventory General Item based on id.
            InventoryGeneralItemMaster inventoryGeneralItemMaster = _inventoryGeneralItemMasterRepository.Table.FirstOrDefault(x => x.InventoryGeneralItemMasterId == inventoryGeneralItemMasterId);
            InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel = inventoryGeneralItemMaster?.FromEntityToModel<InventoryGeneralItemMasterModel>();
            return inventoryGeneralItemMasterModel;
        }

        //Update Inventory General Item.
        public virtual bool UpdateInventoryGeneralItemMaster(InventoryGeneralItemMasterModel inventoryGeneralItemMasterModel)
        {
            if (IsNull(inventoryGeneralItemMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (inventoryGeneralItemMasterModel.InventoryGeneralItemMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryGeneralItemMasterID"));

            if (IsItemDetailsAlreadyExist(inventoryGeneralItemMasterModel.ItemNumber, inventoryGeneralItemMasterModel.InventoryGeneralItemMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "InventoryGeneralItemMaster Code"));

            InventoryGeneralItemMaster inventoryGeneralItemMaster = inventoryGeneralItemMasterModel.FromModelToEntity<InventoryGeneralItemMaster>();

            //Update Inventory General Item
            bool isInventoryGeneralItemMasterUpdated = _inventoryGeneralItemMasterRepository.Update(inventoryGeneralItemMaster);
            if (!isInventoryGeneralItemMasterUpdated)
            {
                inventoryGeneralItemMasterModel.HasError = true;
                inventoryGeneralItemMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isInventoryGeneralItemMasterUpdated;
        }

        //Delete Inventory General Item.
        public virtual bool DeleteInventoryGeneralItemMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "InventoryGeneralItemMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("InventoryGeneralItemMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteInventoryGeneralItemMaster @InventoryGeneralItemMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        public virtual InventoryGeneralItemMasterListModel GetGeneralServicesList(string searchText)
        {
            InventoryGeneralItemMasterListModel listModel = new InventoryGeneralItemMasterListModel()
            {
                InventoryGeneralItemMasterList = new List<InventoryGeneralItemMasterModel>()
            };

            int productTypeEnumId = GetEnumIdByEnumCode("Service");

            listModel.InventoryGeneralItemMasterList = (from a in _inventoryGeneralItemMasterRepository.Table
                                                        join b in _inventoryGeneralItemLineRepository.Table
                                                        on a.InventoryGeneralItemMasterId equals b.InventoryGeneralItemMasterId
                                                        where a.ProductTypeEnumId == productTypeEnumId && a.IsActive &&
                                                             (searchText.Contains(a.ItemName) || searchText.Contains(a.ItemNumber) ||
                                                              searchText.Contains(a.HSNSACCode) || searchText.Contains(b.SKU) || searchText == null
                                                              )
                                                        select new InventoryGeneralItemMasterModel()
                                                        {
                                                            InventoryGeneralItemLineId = b.InventoryGeneralItemLineId,
                                                            HSNSACCode = a.HSNSACCode,
                                                            ItemNumber = a.ItemNumber,
                                                            ItemName = a.ItemName
                                                        })?.ToList();
            return listModel;
        }

        #region Protected Method
        //Check if Item Details is already present or not.
        protected virtual bool IsItemDetailsAlreadyExist(string itemNumber, int inventoryGeneralItemMasterId = 0)
         => _inventoryGeneralItemMasterRepository.Table.Any(x => x.ItemNumber == itemNumber && (x.InventoryGeneralItemMasterId != inventoryGeneralItemMasterId || inventoryGeneralItemMasterId == 0));
        #endregion
    }
}
