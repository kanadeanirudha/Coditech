using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class DBTMDeviceRegistrationDetailsAgent : BaseAgent, IDBTMDeviceRegistrationDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IDBTMDeviceRegistrationDetailsClient _dBTMDeviceRegistrationDetailsClient;
        #endregion

        #region Public Constructor
        public DBTMDeviceRegistrationDetailsAgent(ICoditechLogging coditechLogging, IDBTMDeviceRegistrationDetailsClient dBTMDeviceRegistrationDetailsClient)
        {
            _coditechLogging = coditechLogging;
            _dBTMDeviceRegistrationDetailsClient = GetClient<IDBTMDeviceRegistrationDetailsClient>(dBTMDeviceRegistrationDetailsClient);
        }
        #endregion

        #region Public Methods
        public virtual DBTMDeviceRegistrationDetailsListViewModel GetDBTMDeviceRegistrationDetailsList(DataTableViewModel dataTableModel)
        {
            long userMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.UserMasterId ?? 0;
            if (userMasterId > 0)
            {
                FilterCollection filters = new FilterCollection();
                dataTableModel = dataTableModel ?? new DataTableViewModel();
                if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
                {
                    filters.Add("DeviceName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                    filters.Add("DeviceSerialCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);

                }
                SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? " " : dataTableModel.SortByColumn, dataTableModel.SortBy);

                DBTMDeviceRegistrationDetailsListResponse response = _dBTMDeviceRegistrationDetailsClient.List(userMasterId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
                DBTMDeviceRegistrationDetailsListModel dBTMDeviceRegistrationDetailsList = new DBTMDeviceRegistrationDetailsListModel { RegistrationDetailsList = response?.RegistrationDetailsList };
                DBTMDeviceRegistrationDetailsListViewModel listViewModel = new DBTMDeviceRegistrationDetailsListViewModel();
                listViewModel.RegistrationDetailsList = dBTMDeviceRegistrationDetailsList?.RegistrationDetailsList?.ToViewModel<DBTMDeviceRegistrationDetailsViewModel>().ToList();

                SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.RegistrationDetailsList.Count, BindColumns());
                return listViewModel;
            }
            return new DBTMDeviceRegistrationDetailsListViewModel();
        }

        //Create DBTMDeviceRegistrationDetails.
        public virtual DBTMDeviceRegistrationDetailsViewModel CreateRegistrationDetails(DBTMDeviceRegistrationDetailsViewModel dBTMDeviceRegistrationDetailsViewModel)
        {
            try
            {
                DBTMDeviceRegistrationDetailsResponse response = _dBTMDeviceRegistrationDetailsClient.CreateRegistrationDetails(dBTMDeviceRegistrationDetailsViewModel.ToModel<DBTMDeviceRegistrationDetailsModel>());
                DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel = response?.DBTMDeviceRegistrationDetailsModel;
                return IsNotNull(dBTMDeviceRegistrationDetailsModel) ? dBTMDeviceRegistrationDetailsModel.ToViewModel<DBTMDeviceRegistrationDetailsViewModel>() : new DBTMDeviceRegistrationDetailsViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (DBTMDeviceRegistrationDetailsViewModel)GetViewModelWithErrorMessage(dBTMDeviceRegistrationDetailsViewModel, ex.ErrorMessage);
                    default:
                        return (DBTMDeviceRegistrationDetailsViewModel)GetViewModelWithErrorMessage(dBTMDeviceRegistrationDetailsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Error);
                return (DBTMDeviceRegistrationDetailsViewModel)GetViewModelWithErrorMessage(dBTMDeviceRegistrationDetailsViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get DBTMDeviceRegistrationDetails by dBTMDeviceRegistrationDetail id.
        public virtual DBTMDeviceRegistrationDetailsViewModel GetRegistrationDetails(long dBTMDeviceRegistrationDetailId)
        {
            DBTMDeviceRegistrationDetailsResponse response = _dBTMDeviceRegistrationDetailsClient.GetRegistrationDetails(dBTMDeviceRegistrationDetailId);
            return response?.DBTMDeviceRegistrationDetailsModel.ToViewModel<DBTMDeviceRegistrationDetailsViewModel>();
        }

        //Update DBTMDeviceRegistrationDetails.
        public virtual DBTMDeviceRegistrationDetailsViewModel UpdateRegistrationDetails(DBTMDeviceRegistrationDetailsViewModel dBTMDeviceRegistrationDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Info);
                DBTMDeviceRegistrationDetailsResponse response = _dBTMDeviceRegistrationDetailsClient.UpdateRegistrationDetails(dBTMDeviceRegistrationDetailsViewModel.ToModel<DBTMDeviceRegistrationDetailsModel>());
                DBTMDeviceRegistrationDetailsModel dBTMDeviceRegistrationDetailsModel = response?.DBTMDeviceRegistrationDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Info);
                return IsNotNull(dBTMDeviceRegistrationDetailsModel) ? dBTMDeviceRegistrationDetailsModel.ToViewModel<DBTMDeviceRegistrationDetailsViewModel>() : (DBTMDeviceRegistrationDetailsViewModel)GetViewModelWithErrorMessage(new DBTMDeviceRegistrationDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Error);
                return (DBTMDeviceRegistrationDetailsViewModel)GetViewModelWithErrorMessage(dBTMDeviceRegistrationDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete DBTMDeviceRegistrationDetails.
        public virtual bool DeleteRegistrationDetails(string dBTMDeviceRegistrationDetailIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _dBTMDeviceRegistrationDetailsClient.DeleteRegistrationDetails(new ParameterModel { Ids = dBTMDeviceRegistrationDetailIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteDBTMDeviceRegistrationDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMDeviceRegistrationDetails.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "DBTM Device",
            //    ColumnCode = "DBTMDeviceMasterId",
            //    IsSortable = true,
            //});
          
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Device Serial Code",
                ColumnCode = "DeviceSerialCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Device Name",
                ColumnCode = "DeviceName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Purchase Date",
                ColumnCode = "PurchaseDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()

            {
                ColumnName = "Is Master Device",
                ColumnCode = "IsMasterDevice",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
