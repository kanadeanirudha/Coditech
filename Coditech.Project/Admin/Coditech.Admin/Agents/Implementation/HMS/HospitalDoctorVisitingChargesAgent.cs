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
    public class HospitalDoctorVisitingChargesAgent : BaseAgent, IHospitalDoctorVisitingChargesAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalDoctorVisitingChargesClient _hospitalDoctorVisitingChargesClient;
        #endregion

        #region Public Constructor
        public HospitalDoctorVisitingChargesAgent(ICoditechLogging coditechLogging, IHospitalDoctorVisitingChargesClient hospitalDoctorVisitingChargesClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalDoctorVisitingChargesClient = GetClient<IHospitalDoctorVisitingChargesClient>(hospitalDoctorVisitingChargesClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalDoctorVisitingChargesListViewModel GetHospitalDoctorVisitingChargesList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("HospitalDoctor", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FromDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UptoDate", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("AppointmentTypeEnumId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Charges", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("Remark", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorVisitingChargesListResponse response = _hospitalDoctorVisitingChargesClient.List(selectedCentreCode, selectedDepartmentId, true, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorVisitingChargesListModel hospitaldoctorvisitingchargesList = new HospitalDoctorVisitingChargesListModel { HospitalDoctorVisitingChargesList = response?.HospitalDoctorVisitingChargesList };
            HospitalDoctorVisitingChargesListViewModel listViewModel = new HospitalDoctorVisitingChargesListViewModel();
            listViewModel.HospitalDoctorVisitingChargesList = hospitaldoctorvisitingchargesList?.HospitalDoctorVisitingChargesList?.ToViewModel<HospitalDoctorVisitingChargesViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorVisitingChargesList.Count, BindColumns());
            return listViewModel;
        }

        //Create General HospitalDoctorVisitingCharges.
        public virtual HospitalDoctorVisitingChargesViewModel CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            try
            {
                HospitalDoctorVisitingChargesResponse response = _hospitalDoctorVisitingChargesClient.CreateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel.ToModel<HospitalDoctorVisitingChargesModel>());
                HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel = response?.HospitalDoctorVisitingChargesModel;
                return IsNotNull(hospitalDoctorVisitingChargesModel) ? hospitalDoctorVisitingChargesModel.ToViewModel<HospitalDoctorVisitingChargesViewModel>() : new HospitalDoctorVisitingChargesViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalDoctorVisitingChargesViewModel)GetViewModelWithErrorMessage(hospitalDoctorVisitingChargesViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalDoctorVisitingChargesViewModel)GetViewModelWithErrorMessage(hospitalDoctorVisitingChargesViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return (HospitalDoctorVisitingChargesViewModel)GetViewModelWithErrorMessage(hospitalDoctorVisitingChargesViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general HospitalDoctorVisitingCharges by general hospitaldoctorvisitingcharges master id.
        public virtual HospitalDoctorVisitingChargesViewModel GetHospitalDoctorVisitingCharges(short hospitalDoctorVisitingChargesId)
        {
            HospitalDoctorVisitingChargesResponse response = _hospitalDoctorVisitingChargesClient.GetHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesId);
            return response?.HospitalDoctorVisitingChargesModel.ToViewModel<HospitalDoctorVisitingChargesViewModel>();
        }

        //Update hospitalDoctorVisitingCharges.
        public virtual HospitalDoctorVisitingChargesViewModel UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Info);
               HospitalDoctorVisitingChargesResponse response = _hospitalDoctorVisitingChargesClient.UpdateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel.ToModel<HospitalDoctorVisitingChargesModel>());
                HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel = response?.HospitalDoctorVisitingChargesModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalDoctorVisitingChargesModel) ? hospitalDoctorVisitingChargesModel.ToViewModel<HospitalDoctorVisitingChargesViewModel>() : (HospitalDoctorVisitingChargesViewModel)GetViewModelWithErrorMessage(new HospitalDoctorVisitingChargesViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return (HospitalDoctorVisitingChargesViewModel)GetViewModelWithErrorMessage(hospitalDoctorVisitingChargesViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete hospitalDoctorVisitingCharges.
        public virtual bool DeleteHospitalDoctorVisitingCharges(string hospitalDoctorVisitingChargesId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalDoctorVisitingChargesClient.DeleteHospitalDoctorVisitingCharges(new ParameterModel { Ids = hospitalDoctorVisitingChargesId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalDoctorVisitingCharges;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
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
                ColumnName = "Hospital Doctor",
                ColumnCode = "HospitalDoctor",
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
                ColumnName = "Appointment",
                ColumnCode = "AppointmentTypeEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Charges",
                ColumnCode = "Charges",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Remark",
                ColumnCode = "Remark",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all hospitaldoctorvisitingcharges list from database 
        public virtual HospitalDoctorVisitingChargesListResponse GetHospitalDoctorVisitingChargesList()
        {
            HospitalDoctorVisitingChargesListResponse hospitaldoctorvisitingchargesList = _hospitalDoctorVisitingChargesClient.List(null, null, null, 1, int.MaxValue);
            return hospitaldoctorvisitingchargesList?.HospitalDoctorVisitingChargesList?.Count > 0 ? hospitaldoctorvisitingchargesList : new HospitalDoctorVisitingChargesListResponse();
        }
        #endregion
    }
}
