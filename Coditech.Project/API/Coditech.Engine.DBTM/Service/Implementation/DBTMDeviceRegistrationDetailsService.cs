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
    public class DBTMDeviceRegistrationDetailsService : IDBTMDeviceRegistrationDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<DBTMDeviceRegistrationDetails> _dBTMDeviceRegistrationDetailsRepository;
        private readonly ICoditechRepository<DBTMDeviceMaster> _dBTMDeviceMasterRepository;
        public DBTMDeviceRegistrationDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _dBTMDeviceRegistrationDetailsRepository = new CoditechRepository<DBTMDeviceRegistrationDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _dBTMDeviceMasterRepository = new CoditechRepository<DBTMDeviceMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual DBTMDeviceRegistrationDetailsListModel GetDBTMDeviceRegistrationDetailsList(long userMasterId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<DBTMDeviceRegistrationDetailsModel> objStoredProc = new CoditechViewRepository<DBTMDeviceRegistrationDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@UserId", userMasterId, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<DBTMDeviceRegistrationDetailsModel> dBTMDeviceRegistrationDetailsList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetDBTMDeviceRegistrationDetailsList @UserId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            DBTMDeviceRegistrationDetailsListModel listModel = new DBTMDeviceRegistrationDetailsListModel();

            listModel.RegistrationDetailsList = dBTMDeviceRegistrationDetailsList?.Count > 0 ? dBTMDeviceRegistrationDetailsList : new List<DBTMDeviceRegistrationDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create DBTMDeviceRegistrationDetails.
        public virtual DBTMDeviceRegistrationDetailsModel CreateRegistrationDetails(DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel)
        {
            if (IsNull(dBTMDeviceRegistrationDetailsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (string.IsNullOrEmpty(dBTMDeviceRegistrationDetailsModel.DeviceSerialCode))
                throw new CoditechException(ErrorCodes.InvalidData, "Device Serial Code is required.");

            if (IsDeviceSerialCodeAlreadyExist(dBTMDeviceRegistrationDetailsModel.DeviceSerialCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Device Serial Code"));

            DBTMDeviceMaster dBTMDeviceMaster = _dBTMDeviceMasterRepository.Table.FirstOrDefault(x => x.DeviceSerialCode == dBTMDeviceRegistrationDetailsModel.DeviceSerialCode);
            if (dBTMDeviceMaster == null)
            {
                dBTMDeviceMaster = new DBTMDeviceMaster
                {
                    DeviceSerialCode = dBTMDeviceRegistrationDetailsModel.DeviceSerialCode,
                    DeviceName = "Test2",
                    StatusEnumId = 2,
                    IsMasterDevice = true,
                    IsActive = true,
                    RegistrationDate = DateTime.Now,
                    WarrantyExpirationPeriodInMonth = 8,
                    CreatedDate = DateTime.Now,
                };

                dBTMDeviceMaster = _dBTMDeviceMasterRepository.Insert(dBTMDeviceMaster);
            }
            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = new DBTMDeviceRegistrationDetails()
            {
                DBTMDeviceMasterId = dBTMDeviceMaster.DBTMDeviceMasterId,
                EntityId = dBTMDeviceRegistrationDetailsModel.EntityId,
                UserType = UserTypeEnum.Employee.ToString(),
                PurchaseDate = DateTime.Today,
                WarrantyExpirationDate = DateTime.Today.AddMonths(dBTMDeviceMaster.WarrantyExpirationPeriodInMonth),
            };

            //Create new DBTMDeviceRegistrationDetails and return it.
            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetailsData = _dBTMDeviceRegistrationDetailsRepository.Insert(dBTMDeviceRegistrationDetails);
            if (dBTMDeviceRegistrationDetailsData?.DBTMDeviceRegistrationDetailId > 0)
            {
                dBTMDeviceRegistrationDetailsModel.DBTMDeviceRegistrationDetailId = dBTMDeviceRegistrationDetailsData.DBTMDeviceRegistrationDetailId;

            }
            else
            {
                dBTMDeviceRegistrationDetailsModel.HasError = true;
                dBTMDeviceRegistrationDetailsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return dBTMDeviceRegistrationDetailsModel;
        }

        //Get DBTMDeviceRegistrationDetails by dBTMDeviceRegistrationDetailId.
        public virtual DBTMDeviceRegistrationDetailsModel GetRegistrationDetails(long dBTMDeviceRegistrationDetailId)
        {
            if (dBTMDeviceRegistrationDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailId"));

            //Get the DBTMDeviceRegistrationDetails Details based on id.
            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = _dBTMDeviceRegistrationDetailsRepository.Table.Where(x => x.DBTMDeviceRegistrationDetailId == dBTMDeviceRegistrationDetailId)?.FirstOrDefault();
            DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel = dBTMDeviceRegistrationDetails?.FromEntityToModel<DBTMDeviceRegistrationDetailsModel>();
            return dBTMDeviceRegistrationDetailsModel;
        }

        //Update DBTMDeviceRegistrationDetails.
        public virtual bool UpdateRegistrationDetails(DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel)
        {
            if (IsNull(dBTMDeviceRegistrationDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (dBTMDeviceRegistrationDetailsModel.DBTMDeviceRegistrationDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailID"));

            DBTMDeviceRegistrationDetails dBTMDeviceRegistrationDetails = dBTMDeviceRegistrationDetailsModel.FromModelToEntity<DBTMDeviceRegistrationDetails>();

            //Update DBTMDeviceRegistrationDetails
            bool isdBTMDeviceRegistrationDetailsUpdated = _dBTMDeviceRegistrationDetailsRepository.Update(dBTMDeviceRegistrationDetails);
            if (!isdBTMDeviceRegistrationDetailsUpdated)
            {
                dBTMDeviceRegistrationDetailsModel.HasError = true;
                dBTMDeviceRegistrationDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isdBTMDeviceRegistrationDetailsUpdated;
        }

        //Delete DBTMDeviceRegistrationDetails.
        public virtual bool DeleteRegistrationDetails(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "DBTMDeviceRegistrationDetailId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("DBTMDeviceRegistrationDetailId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteDBTMDeviceRegistrationDetails @DBTMDeviceRegistrationDetailId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if DeviceSerialCode is already present or not.
        protected virtual bool IsDeviceSerialCodeAlreadyExist(string deviceSerialCode, long dBTMDeviceMasterId = 0)

        => _dBTMDeviceMasterRepository.Table.Any(x => x.DeviceSerialCode == deviceSerialCode && (x.DBTMDeviceMasterId != dBTMDeviceMasterId || dBTMDeviceMasterId == 0));
        #endregion
    }
}
