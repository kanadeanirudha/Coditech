using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

namespace Coditech.Admin.Agents
{
    public class HospitalRegistrationFeeAgent : BaseAgent, IHospitalRegistrationFeeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalRegistrationFeeClient _hospitalRegistrationFeeClient;
        #endregion

        #region Public Constructor
        public HospitalRegistrationFeeAgent(ICoditechLogging coditechLogging, IHospitalRegistrationFeeClient hospitalRegistrationFeeClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalRegistrationFeeClient = GetClient<IHospitalRegistrationFeeClient>(hospitalRegistrationFeeClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalRegistrationFeeListViewModel GetHospitalRegistrationFeeList(string selectedCentreCode, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("RegistrationService", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Charges", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalRegistrationFeeListResponse response = _hospitalRegistrationFeeClient.List(selectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalRegistrationFeeListModel hospitalRegistrationFeeList = new HospitalRegistrationFeeListModel { HospitalRegistrationFeeList = response?.HospitalRegistrationFeeList };
            HospitalRegistrationFeeListViewModel listViewModel = new HospitalRegistrationFeeListViewModel();
            listViewModel.HospitalRegistrationFeeList = hospitalRegistrationFeeList?.HospitalRegistrationFeeList?.ToViewModel<HospitalRegistrationFeeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalRegistrationFeeList.Count, BindColumns());
            return listViewModel;
        }

        //Create HospitalRegistrationFee.
        public virtual HospitalRegistrationFeeViewModel CreateRegistrationFee(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel)
        {
            try
            {
                HospitalRegistrationFeeResponse response = _hospitalRegistrationFeeClient.CreateRegistrationFee(hospitalRegistrationFeeViewModel.ToModel<HospitalRegistrationFeeModel>());
                HospitalRegistrationFeeModel hospitalRegistrationFeeModel = response?.HospitalRegistrationFeeModel;
                return HelperUtility.IsNotNull(hospitalRegistrationFeeModel) ? hospitalRegistrationFeeModel.ToViewModel<HospitalRegistrationFeeViewModel>() : new HospitalRegistrationFeeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalRegistrationFeeViewModel)GetViewModelWithErrorMessage(hospitalRegistrationFeeViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalRegistrationFeeViewModel)GetViewModelWithErrorMessage(hospitalRegistrationFeeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return (HospitalRegistrationFeeViewModel)GetViewModelWithErrorMessage(hospitalRegistrationFeeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general HospitalRegistrationFee by hospitalRegistrationFee id.
        public virtual HospitalRegistrationFeeViewModel GetRegistrationFee(int hospitalRegistrationFeeId)
        {
            HospitalRegistrationFeeResponse response = _hospitalRegistrationFeeClient.GetRegistrationFee(hospitalRegistrationFeeId);
            return response?.HospitalRegistrationFeeModel.ToViewModel<HospitalRegistrationFeeViewModel>();
        }

        //Update  HospitalRegistrationFee.
        public virtual HospitalRegistrationFeeViewModel UpdateRegistrationFee(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Info);
                HospitalRegistrationFeeResponse response = _hospitalRegistrationFeeClient.UpdateRegistrationFee(hospitalRegistrationFeeViewModel.ToModel<HospitalRegistrationFeeModel>());
                HospitalRegistrationFeeModel hospitalRegistrationFeeModel = response?.HospitalRegistrationFeeModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(hospitalRegistrationFeeModel) ? hospitalRegistrationFeeModel.ToViewModel<HospitalRegistrationFeeViewModel>() : (HospitalRegistrationFeeViewModel)GetViewModelWithErrorMessage(new HospitalRegistrationFeeViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                return (HospitalRegistrationFeeViewModel)GetViewModelWithErrorMessage(hospitalRegistrationFeeViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalRegistrationFee.
        public virtual bool DeleteRegistrationFee(string hospitalRegistrationFeeId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalRegistrationFeeClient.DeleteRegistrationFee(new ParameterModel { Ids = hospitalRegistrationFeeId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalRegistrationFee;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalRegistrationFee.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Registration Service",
                ColumnCode = "ItemName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "From Date",
                ColumnCode = "FromDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Upto Date",
                ColumnCode = "UptoDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Charges",
                ColumnCode = "Charges",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}

