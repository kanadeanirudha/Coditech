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

namespace Coditech.Admin.Agents
{
    public class HospitalDoctorsAgent : BaseAgent, IHospitalDoctorsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalDoctorsClient _hospitalDoctorsClient;
        #endregion

        #region Public Constructor
        public HospitalDoctorsAgent(ICoditechLogging coditechLogging, IHospitalDoctorsClient hospitalDoctorsClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalDoctorsClient = GetClient<IHospitalDoctorsClient>(hospitalDoctorsClient);
        }
        #endregion

        #region Public Methods
        #region HospitalDoctors
        public virtual HospitalDoctorsListViewModel GetHospitalDoctorsList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalDoctorsListResponse response = _hospitalDoctorsClient.List(selectedCentreCode, selectedDepartmentId, true, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalDoctorsListModel hospitalDoctorsList = new HospitalDoctorsListModel { HospitalDoctorsList = response?.HospitalDoctorsList };
            HospitalDoctorsListViewModel listViewModel = new HospitalDoctorsListViewModel();
            listViewModel.HospitalDoctorsList = hospitalDoctorsList?.HospitalDoctorsList?.ToViewModel<HospitalDoctorsViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalDoctorsList.Count, BindColumns());
            return listViewModel;
        }

        //Create Hospital Doctors.
        public virtual HospitalDoctorsViewModel CreateHospitalDoctors(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            try
            {
                HospitalDoctorsResponse response = _hospitalDoctorsClient.CreateHospitalDoctors(hospitalDoctorsViewModel.ToModel<HospitalDoctorsModel>());
                HospitalDoctorsModel hospitalDoctorsModel = response?.HospitalDoctorsModel;
                return HelperUtility.IsNotNull(hospitalDoctorsModel) ? hospitalDoctorsModel.ToViewModel<HospitalDoctorsViewModel>() : new HospitalDoctorsViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalDoctorsViewModel)GetViewModelWithErrorMessage(hospitalDoctorsViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalDoctorsViewModel)GetViewModelWithErrorMessage(hospitalDoctorsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return (HospitalDoctorsViewModel)GetViewModelWithErrorMessage(hospitalDoctorsViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get HospitalDoctors by hospital Doctor id.
        public virtual HospitalDoctorsViewModel GetHospitalDoctors(int hospitalDoctorId)
        {
            HospitalDoctorsResponse response = _hospitalDoctorsClient.GetHospitalDoctors(hospitalDoctorId);
            return response?.HospitalDoctorsModel.ToViewModel<HospitalDoctorsViewModel>();
        }

        //Update  Hospital Doctors.
        public virtual HospitalDoctorsViewModel UpdateHospitalDoctors(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Info);
                HospitalDoctorsResponse response = _hospitalDoctorsClient.UpdateHospitalDoctors(hospitalDoctorsViewModel.ToModel<HospitalDoctorsModel>());
                HospitalDoctorsModel hospitalDoctorsModel = response?.HospitalDoctorsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(hospitalDoctorsModel) ? hospitalDoctorsModel.ToViewModel<HospitalDoctorsViewModel>() : (HospitalDoctorsViewModel)GetViewModelWithErrorMessage(new HospitalDoctorsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                return (HospitalDoctorsViewModel)GetViewModelWithErrorMessage(hospitalDoctorsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalDoctors.
        public virtual bool DeleteHospitalDoctors(string hospitalDoctorIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalDoctorsClient.DeleteHospitalDoctors(new ParameterModel { Ids = hospitalDoctorIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalDoctorsDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctors.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion
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
                ColumnName = "Building Room Name",
                ColumnCode = "BuildingRoomName",
                IsSortable = true,
            });
            return datatableColumnList;
        }

    }
}
#endregion