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
    public class HospitalDoctorAllocatedOPDRoomAgent : BaseAgent, IHospitalDoctorAllocatedOPDRoomAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalDoctorAllocatedOPDRoomClient _hospitalDoctorAllocatedOPDRoomClient;
        #endregion

        #region Public Constructor
        public HospitalDoctorAllocatedOPDRoomAgent(ICoditechLogging coditechLogging, IHospitalDoctorAllocatedOPDRoomClient hospitalDoctorAllocatedOPDRoomClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalDoctorAllocatedOPDRoomClient = GetClient<IHospitalDoctorAllocatedOPDRoomClient>(hospitalDoctorAllocatedOPDRoomClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalDoctorAllocatedOPDRoomListViewModel GetHospitalDoctorAllocatedOPDRoomList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel)
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
                filters.Add("RoomName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorAllocatedOPDRoomListResponse response = _hospitalDoctorAllocatedOPDRoomClient.List(selectedCentreCode, selectedDepartmentId, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorAllocatedOPDRoomListModel hospitalDoctorAllocatedOPDRoomList = new HospitalDoctorAllocatedOPDRoomListModel { HospitalDoctorAllocatedOPDRoomList = response?.HospitalDoctorAllocatedOPDRoomList };
            HospitalDoctorAllocatedOPDRoomListViewModel listViewModel = new HospitalDoctorAllocatedOPDRoomListViewModel();
            listViewModel.HospitalDoctorAllocatedOPDRoomList = hospitalDoctorAllocatedOPDRoomList?.HospitalDoctorAllocatedOPDRoomList?.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorAllocatedOPDRoomList.Count, BindColumns());
            return listViewModel;
        }

        //GetHospitalDoctorAllocatedOPDRoom by hospital Doctor Allocated OPD Room Id.
        public virtual HospitalDoctorAllocatedOPDRoomViewModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId)
        {
            HospitalDoctorAllocatedOPDRoomResponse response = _hospitalDoctorAllocatedOPDRoomClient.GetHospitalDoctorAllocatedOPDRoom(hospitalDoctorId, hospitalDoctorAllocatedOPDRoomId);
            return response?.HospitalDoctorAllocatedOPDRoomModel.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>();
        }

        //Update HospitalDoctorAllocatedOPDRoom.
        public virtual HospitalDoctorAllocatedOPDRoomViewModel UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel)
        {
            try
            {
                string SelectedCentreCode = hospitalDoctorAllocatedOPDRoomViewModel.SelectedCentreCode;
                string SelectedDepartmentId = hospitalDoctorAllocatedOPDRoomViewModel.SelectedDepartmentId;
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Info);
                HospitalDoctorAllocatedOPDRoomResponse response = _hospitalDoctorAllocatedOPDRoomClient.UpdateHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomViewModel.ToModel<HospitalDoctorAllocatedOPDRoomModel>());
                HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = response?.HospitalDoctorAllocatedOPDRoomModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Info);
                hospitalDoctorAllocatedOPDRoomViewModel = IsNotNull(hospitalDoctorAllocatedOPDRoomModel) ? hospitalDoctorAllocatedOPDRoomModel.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>() : (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(new HospitalDoctorAllocatedOPDRoomViewModel(), GeneralResources.UpdateErrorMessage);
                hospitalDoctorAllocatedOPDRoomViewModel.SelectedCentreCode = SelectedCentreCode;
                hospitalDoctorAllocatedOPDRoomViewModel.SelectedDepartmentId = SelectedDepartmentId;
                return hospitalDoctorAllocatedOPDRoomViewModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(hospitalDoctorAllocatedOPDRoomViewModel, GeneralResources.UpdateErrorMessage);
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
                ColumnName = "Medical Specilization",
                ColumnCode = "MedicalSpecilization",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Room Name",
                ColumnCode = "RoomName",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
