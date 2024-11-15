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
    public class HospitalPatientRegistrationAgent : BaseAgent, IHospitalPatientRegistrationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPatientRegistrationClient _hospitalPatientRegistrationClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public HospitalPatientRegistrationAgent(ICoditechLogging coditechLogging, IHospitalPatientRegistrationClient hospitalPatientRegistrationClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPatientRegistrationClient = GetClient<IHospitalPatientRegistrationClient>(hospitalPatientRegistrationClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        #region PatientRegistration
        public virtual HospitalPatientRegistrationListViewModel GetHospitalPatientRegistrationList(string selectedCentreCode, DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("FirstName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("LastName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MobileNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("UAHNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "FirstName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPatientRegistrationListResponse response = _hospitalPatientRegistrationClient.List(selectedCentreCode, null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPatientRegistrationListModel hospitalPatientRegistrationList = new HospitalPatientRegistrationListModel { HospitalPatientRegistrationList = response?.HospitalPatientRegistrationList };
            HospitalPatientRegistrationListViewModel listViewModel = new HospitalPatientRegistrationListViewModel();
            listViewModel.HospitalPatientRegistrationList = hospitalPatientRegistrationList?.HospitalPatientRegistrationList?.ToViewModel<HospitalPatientRegistrationViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPatientRegistrationList.Count, BindColumns());
            return listViewModel;
        }

        //Create PatientRegistration
        public virtual HospitalPatientRegistrationCreateEditViewModel CreatePatientRegistration(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel)
        {
            try
            {
                hospitalPatientRegistrationCreateEditViewModel.UserType = UserTypeEnum.Patient.ToString();
                GeneralPersonModel generalPersonModel = hospitalPatientRegistrationCreateEditViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.SelectedCentreCode = hospitalPatientRegistrationCreateEditViewModel.SelectedCentreCode;
                generalPersonModel.HospitalPatientTypeId = hospitalPatientRegistrationCreateEditViewModel.HospitalPatientTypeId;

                GeneralPersonResponse response = _userClient.InsertPersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<HospitalPatientRegistrationCreateEditViewModel>() : new HospitalPatientRegistrationCreateEditViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPatientRegistrationCreateEditViewModel)GetViewModelWithErrorMessage(hospitalPatientRegistrationCreateEditViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPatientRegistrationCreateEditViewModel)GetViewModelWithErrorMessage(hospitalPatientRegistrationCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return (HospitalPatientRegistrationCreateEditViewModel)GetViewModelWithErrorMessage(hospitalPatientRegistrationCreateEditViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get PatientRegistration Details by personId.
        public virtual HospitalPatientRegistrationCreateEditViewModel GetPatientRegistrationPersonalDetails(long hospitalPatientRegistrationId, long personId)
        {
            GeneralPersonResponse response = _userClient.GetPersonInformation(personId);
            HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel = response?.GeneralPersonModel.ToViewModel<HospitalPatientRegistrationCreateEditViewModel>();
            if (IsNotNull(hospitalPatientRegistrationCreateEditViewModel))
            {
                HospitalPatientRegistrationResponse hospitalPatientRegistrationResponse = _hospitalPatientRegistrationClient.GetPatientRegistrationOtherDetail(hospitalPatientRegistrationId);
                if (IsNotNull(hospitalPatientRegistrationResponse))
                {
                    hospitalPatientRegistrationCreateEditViewModel.SelectedCentreCode = hospitalPatientRegistrationResponse.HospitalPatientRegistrationModel.CentreCode;
                    hospitalPatientRegistrationCreateEditViewModel.HospitalPatientTypeId = hospitalPatientRegistrationResponse.HospitalPatientRegistrationModel.HospitalPatientTypeId;
                }
                hospitalPatientRegistrationCreateEditViewModel.HospitalPatientRegistrationId = hospitalPatientRegistrationId;
                hospitalPatientRegistrationCreateEditViewModel.PersonId = personId;
            }
            return hospitalPatientRegistrationCreateEditViewModel;
        }

        //Update PatientRegistration Personal Details
        public virtual HospitalPatientRegistrationCreateEditViewModel UpdatePatientRegistrationPersonalDetails(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Info);
                GeneralPersonModel generalPersonModel = hospitalPatientRegistrationCreateEditViewModel.ToModel<GeneralPersonModel>();
                generalPersonModel.EntityId = hospitalPatientRegistrationCreateEditViewModel.HospitalPatientRegistrationId;
                generalPersonModel.UserType = UserTypeEnum.Patient.ToString();
                GeneralPersonResponse response = _userClient.UpdatePersonInformation(generalPersonModel);
                generalPersonModel = response?.GeneralPersonModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<HospitalPatientRegistrationCreateEditViewModel>() : (HospitalPatientRegistrationCreateEditViewModel)GetViewModelWithErrorMessage(new HospitalPatientRegistrationCreateEditViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
                return (HospitalPatientRegistrationCreateEditViewModel)GetViewModelWithErrorMessage(hospitalPatientRegistrationCreateEditViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete PatientRegistration.
        public virtual bool DeletePatientRegistration(string hospitalPatientRegistrationIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPatientRegistrationClient.DeletePatientRegistration(new ParameterModel { Ids = hospitalPatientRegistrationIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeletePatientRegistrationDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPatientRegistration.ToString(), TraceLevel.Error);
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
                ColumnName = "UAH Number",
                ColumnCode = "UAHNumber",
                IsSortable = true,
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
                ColumnName = "Gender",
                ColumnCode = "GenderEnumId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Contact",
                ColumnCode = "MobileNumber",
                IsSortable = true,
            });          
            return datatableColumnList;
        }

    }
}
#endregion