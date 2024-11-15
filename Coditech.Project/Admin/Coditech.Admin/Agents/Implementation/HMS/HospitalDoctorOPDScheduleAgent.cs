using Coditech.Admin.Utilities;
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
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorOPDScheduleListResponse response = _hospitalDoctorOPDScheduleClient.List(selectedCentreCode, selectedDepartmentId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorOPDScheduleListModel hospitalDoctorOPDScheduleList = new HospitalDoctorOPDScheduleListModel { HospitalDoctorOPDScheduleList = response?.HospitalDoctorOPDScheduleList };
            HospitalDoctorOPDScheduleListViewModel listViewModel = new HospitalDoctorOPDScheduleListViewModel();
            listViewModel.HospitalDoctorOPDScheduleList = hospitalDoctorOPDScheduleList?.HospitalDoctorOPDScheduleList?.ToViewModel<HospitalDoctorOPDScheduleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorOPDScheduleList.Count, BindColumns());
            return listViewModel;
        }

        //GetHospitalDoctorOPDSchedule by hospital Doctor Allocated OPD Room Id.
        public virtual HospitalDoctorOPDScheduleViewModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId, int weekDayEnumId)
        {
            HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel = new HospitalDoctorOPDScheduleViewModel();
          
            List<GeneralEnumaratorModel> generalEnumaratorList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList;
            generalEnumaratorList = generalEnumaratorList.Where(x => x.EnumGroupCode == "WeekDays")?.OrderBy(y => y.SequenceNumber)?.ToList();
            weekDayEnumId = weekDayEnumId == 0 && generalEnumaratorList?.Count > 0 ? generalEnumaratorList.FirstOrDefault().GeneralEnumaratorId : weekDayEnumId;
            
            HospitalDoctorOPDScheduleResponse response = _hospitalDoctorOPDScheduleClient.GetHospitalDoctorOPDSchedule(hospitalDoctorId, weekDayEnumId);
            hospitalDoctorOPDScheduleViewModel = response?.HospitalDoctorOPDScheduleModel.ToViewModel<HospitalDoctorOPDScheduleViewModel>();
            hospitalDoctorOPDScheduleViewModel.WeekDaysList = generalEnumaratorList;
            hospitalDoctorOPDScheduleViewModel.WeekDayEnumId = weekDayEnumId;
            return hospitalDoctorOPDScheduleViewModel;
        }

        //Update HospitalDoctorOPDSchedule.
        public virtual HospitalDoctorOPDScheduleViewModel UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel)
        {
            try
            {
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
                ColumnName = "Email Id",
                ColumnCode = "EmailId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Medical Specialization",
                ColumnCode = "MedicalSpecialization",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
