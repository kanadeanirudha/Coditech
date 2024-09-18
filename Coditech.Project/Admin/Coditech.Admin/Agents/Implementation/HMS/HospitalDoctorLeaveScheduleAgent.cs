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
    public class HospitalDoctorLeaveScheduleAgent : BaseAgent, IHospitalDoctorLeaveScheduleAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalDoctorLeaveScheduleClient _hospitalDoctorLeaveScheduleClient;
        #endregion

        #region Public Constructor
        public HospitalDoctorLeaveScheduleAgent(ICoditechLogging coditechLogging, IHospitalDoctorLeaveScheduleClient hospitalDoctorLeaveScheduleClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalDoctorLeaveScheduleClient = GetClient<IHospitalDoctorLeaveScheduleClient>(hospitalDoctorLeaveScheduleClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalDoctorLeaveScheduleListViewModel GetHospitalDoctorLeaveScheduleList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FromTime", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UptoTime", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorLeaveScheduleListResponse response = _hospitalDoctorLeaveScheduleClient.List(selectedCentreCode, selectedDepartmentId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorLeaveScheduleListModel hospitalDoctorLeaveScheduleList = new HospitalDoctorLeaveScheduleListModel { HospitalDoctorLeaveScheduleList = response?.HospitalDoctorLeaveScheduleList };
            HospitalDoctorLeaveScheduleListViewModel listViewModel = new HospitalDoctorLeaveScheduleListViewModel();
            listViewModel.HospitalDoctorLeaveScheduleList = hospitalDoctorLeaveScheduleList?.HospitalDoctorLeaveScheduleList?.ToViewModel<HospitalDoctorLeaveScheduleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorLeaveScheduleList.Count, BindColumns());
            return listViewModel;
        }

        //Create Hospital Doctor Leave Schedule.
        public virtual HospitalDoctorLeaveScheduleViewModel CreateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel)
        {
            try
            {
                HospitalDoctorLeaveScheduleResponse response = _hospitalDoctorLeaveScheduleClient.CreateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel.ToModel<HospitalDoctorLeaveScheduleModel>());
                HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel = response?.HospitalDoctorLeaveScheduleModel;
                return IsNotNull(hospitalDoctorLeaveScheduleModel) ? hospitalDoctorLeaveScheduleModel.ToViewModel<HospitalDoctorLeaveScheduleViewModel>() : new HospitalDoctorLeaveScheduleViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalDoctorLeaveScheduleViewModel)GetViewModelWithErrorMessage(hospitalDoctorLeaveScheduleViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalDoctorLeaveScheduleViewModel)GetViewModelWithErrorMessage(hospitalDoctorLeaveScheduleViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return (HospitalDoctorLeaveScheduleViewModel)GetViewModelWithErrorMessage(hospitalDoctorLeaveScheduleViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //GetHospitalDoctorLeaveSchedule by hospital Doctor Leave Schedule Id.
        public virtual HospitalDoctorLeaveScheduleViewModel GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId)
        {
            HospitalDoctorLeaveScheduleResponse response = _hospitalDoctorLeaveScheduleClient.GetHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleId);
            return response?.HospitalDoctorLeaveScheduleModel.ToViewModel<HospitalDoctorLeaveScheduleViewModel>();
        }

        //Update HospitalDoctorLeaveSchedule.
        public virtual HospitalDoctorLeaveScheduleViewModel UpdateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Info);
                HospitalDoctorLeaveScheduleResponse response = _hospitalDoctorLeaveScheduleClient.UpdateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel.ToModel<HospitalDoctorLeaveScheduleModel>());
                HospitalDoctorLeaveScheduleModel hospitalDoctorLeaveScheduleModel = response?.HospitalDoctorLeaveScheduleModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalDoctorLeaveScheduleModel) ? hospitalDoctorLeaveScheduleModel.ToViewModel<HospitalDoctorLeaveScheduleViewModel>() : (HospitalDoctorLeaveScheduleViewModel)GetViewModelWithErrorMessage(new HospitalDoctorLeaveScheduleViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
                return (HospitalDoctorLeaveScheduleViewModel)GetViewModelWithErrorMessage(hospitalDoctorLeaveScheduleViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalDoctorLeaveSchedule.
        public virtual bool DeleteHospitalDoctorLeaveSchedule(string hospitalDoctorLeaveScheduleId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalDoctorLeaveScheduleClient.DeleteHospitalDoctorLeaveSchedule(new ParameterModel { Ids = hospitalDoctorLeaveScheduleId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalDoctorLeaveSchedule;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorLeaveSchedule.ToString(), TraceLevel.Error);
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
                ColumnName = "Image",
                ColumnCode = "Image",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "First Name",
                ColumnCode = "FirstName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Last Name",
                ColumnCode = "LastName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Leave Date",
                ColumnCode = "LeaveDate",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Full Day",
                ColumnCode = "IsFullDay",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "From Time",
                ColumnCode = "FromTime",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Upto Time",
                ColumnCode = "UptoTime",
                IsSortable = true,
            });
          
            return datatableColumnList;
        }
        #endregion
    }
}
