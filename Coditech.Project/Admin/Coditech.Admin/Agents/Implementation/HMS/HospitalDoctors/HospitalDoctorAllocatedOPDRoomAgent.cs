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
        public virtual HospitalDoctorAllocatedOPDRoomListViewModel GetHospitalDoctorAllocatedOPDRoomList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("RoomName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "RoomName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorAllocatedOPDRoomListResponse response = _hospitalDoctorAllocatedOPDRoomClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorAllocatedOPDRoomListModel hospitalDoctorAllocatedOPDRoomList = new HospitalDoctorAllocatedOPDRoomListModel { HospitalDoctorAllocatedOPDRoomList = response?.HospitalDoctorAllocatedOPDRoomList };
            HospitalDoctorAllocatedOPDRoomListViewModel listViewModel = new HospitalDoctorAllocatedOPDRoomListViewModel();
            listViewModel.HospitalDoctorAllocatedOPDRoomList = hospitalDoctorAllocatedOPDRoomList?.HospitalDoctorAllocatedOPDRoomList?.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorAllocatedOPDRoomList.Count, BindColumns());
            return listViewModel;
        }

        //Create Hospital Doctor Allocated OPD Room.
        public virtual HospitalDoctorAllocatedOPDRoomViewModel CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel)
        {
            try
            {
                HospitalDoctorAllocatedOPDRoomResponse response = _hospitalDoctorAllocatedOPDRoomClient.CreateHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomViewModel.ToModel<HospitalDoctorAllocatedOPDRoomModel>());
                HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = response?.HospitalDoctorAllocatedOPDRoomModel;
                return IsNotNull(hospitalDoctorAllocatedOPDRoomModel) ? hospitalDoctorAllocatedOPDRoomModel.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>() : new HospitalDoctorAllocatedOPDRoomViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(hospitalDoctorAllocatedOPDRoomViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(hospitalDoctorAllocatedOPDRoomViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(hospitalDoctorAllocatedOPDRoomViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //GetHospitalDoctorAllocatedOPDRoom by hospital Doctor Allocated OPD Room Id.
        public virtual HospitalDoctorAllocatedOPDRoomViewModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId)
        {
            HospitalDoctorAllocatedOPDRoomResponse response = _hospitalDoctorAllocatedOPDRoomClient.GetHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomId);
            return response?.HospitalDoctorAllocatedOPDRoomModel.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>();
        }

        //Update HospitalDoctorAllocatedOPDRoom.
        public virtual HospitalDoctorAllocatedOPDRoomViewModel UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Info);
                HospitalDoctorAllocatedOPDRoomResponse response = _hospitalDoctorAllocatedOPDRoomClient.UpdateHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomViewModel.ToModel<HospitalDoctorAllocatedOPDRoomModel>());
                HospitalDoctorAllocatedOPDRoomModel hospitalDoctorAllocatedOPDRoomModel = response?.HospitalDoctorAllocatedOPDRoomModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Info);
                return IsNotNull(hospitalDoctorAllocatedOPDRoomModel) ? hospitalDoctorAllocatedOPDRoomModel.ToViewModel<HospitalDoctorAllocatedOPDRoomViewModel>() : (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(new HospitalDoctorAllocatedOPDRoomViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
                return (HospitalDoctorAllocatedOPDRoomViewModel)GetViewModelWithErrorMessage(hospitalDoctorAllocatedOPDRoomViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalDoctorAllocatedOPDRoom.
        public virtual bool DeleteHospitalDoctorAllocatedOPDRoom(string hospitalDoctorAllocatedOPDRoomId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalDoctorAllocatedOPDRoomClient.DeleteHospitalDoctorAllocatedOPDRoom(new ParameterModel { Ids = hospitalDoctorAllocatedOPDRoomId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalDoctorAllocatedOPDRoom;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorAllocatedOPDRoom.ToString(), TraceLevel.Error);
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
                ColumnName = "Room Name",
                ColumnCode = "RoomName",
                IsSortable = true,
            });
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Country Code",
            //    ColumnCode = "CountryCode",
            //    IsSortable = true,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Is Default",
            //    ColumnCode = "DefaultFlag",
            //});
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all HospitalDoctorAllocatedOPDRoom list from database 
        public virtual HospitalDoctorAllocatedOPDRoomListResponse GetHospitalDoctorAllocatedOPDRoomList()
        {
            HospitalDoctorAllocatedOPDRoomListResponse hospitalDoctorAllocatedOPDRoomList = _hospitalDoctorAllocatedOPDRoomClient.List(null, null, null, 1, int.MaxValue);
            return hospitalDoctorAllocatedOPDRoomList?.HospitalDoctorAllocatedOPDRoomList?.Count > 0 ? hospitalDoctorAllocatedOPDRoomList : new HospitalDoctorAllocatedOPDRoomListResponse();
        }
        #endregion
    }
}
