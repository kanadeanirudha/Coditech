using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class HospitalDoctorOPDScheduleAgent : BaseAgent, IHospitalDoctorOPDScheduleAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalDoctorOPDScheduleClient _hospitalDoctorOPDScheduleClient;
        #endregion

        #region Public Constructor
        public HospitalDoctorOPDScheduleAgent(ICoditechLogging coditechLogging, IHospitalDoctorOPDScheduleClient hospitalDoctorOPDScheduleClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalDoctorOPDScheduleClient = GetClient<IHospitalDoctorOPDScheduleClient>(hospitalDoctorOPDScheduleClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalDoctorOPDScheduleListViewModel GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("HospitalDoctorId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("WeekDayEnumId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("OPDTimesOfDay", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("FromTime", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UptoTime", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TimesSlothInMinute", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("TimeZone", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorOPDScheduleListResponse response = _hospitalDoctorOPDScheduleClient.List(selectedCentreCode, selectedDepartmentId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorOPDScheduleListModel hospitalDoctorOPDScheduleList = new HospitalDoctorOPDScheduleListModel { HospitalDoctorOPDScheduleList = response?.HospitalDoctorOPDScheduleList };
            HospitalDoctorOPDScheduleListViewModel listViewModel = new HospitalDoctorOPDScheduleListViewModel();
            listViewModel.HospitalDoctorOPDScheduleList = hospitalDoctorOPDScheduleList?.HospitalDoctorOPDScheduleList?.ToViewModel<HospitalDoctorOPDScheduleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorOPDScheduleList.Count, BindColumns());
            return listViewModel;
        }

        //GetHospitalDoctorOPDSchedule by hospital Doctor Allocated OPD Room Id.
        public virtual HospitalDoctorOPDScheduleViewModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId, int hospitalDoctorOPDScheduleId)
        {
            HospitalDoctorOPDScheduleResponse response = _hospitalDoctorOPDScheduleClient.GetHospitalDoctorOPDSchedule(hospitalDoctorId, hospitalDoctorOPDScheduleId);
            return response?.HospitalDoctorOPDScheduleModel.ToViewModel<HospitalDoctorOPDScheduleViewModel>();
        }

        //Update HospitalDoctorOPDSchedule.
        public virtual HospitalDoctorOPDScheduleViewModel UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel)
        {
            try
            {
                string SelectedCentreCode = hospitalDoctorOPDScheduleViewModel.HospitalDoctorOPDSchedule;
                string SelectedDepartmentId = hospitalDoctorOPDScheduleViewModel.SelectedDepartmentId;
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Info);
                HospitalDoctorOPDScheduleResponse response = _hospitalDoctorOPDScheduleClient.UpdateHospitalDoctorOPDSchedule(hospitalDoctorOPDScheduleViewModel.ToModel<HospitalDoctorOPDScheduleModel>());
                HospitalDoctorOPDScheduleModel hospitalDoctorOPDScheduleModel = response?.HospitalDoctorOPDScheduleModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalDoctorOPDScheduleModel) ? hospitalDoctorOPDScheduleModel.ToViewModel<HospitalDoctorOPDScheduleViewModel>() : (HospitalDoctorOPDScheduleViewModel)GetViewModelWithErrorMessage(new HospitalDoctorOPDScheduleViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorOPDSchedule.ToString(), TraceLevel.Error);
                return (HospitalDoctorOPDScheduleViewModel)GetViewModelWithErrorMessage(hospitalDoctorOPDScheduleViewModel, GeneralResources.UpdateErrorMessage);
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
                ColumnCode = "HospitalDoctorId",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "WeekDay Enum",
                ColumnCode = "WeekDayEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "OPD Times",
                ColumnCode = "OPDTimesOfDay",
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
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Times Sloth",
                ColumnCode = "TimesSlothInMinute",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Time Zone",
                ColumnCode = "TimeZone",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
