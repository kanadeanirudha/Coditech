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
    public class GymMemberDetailsAgent : BaseAgent, IGymMemberDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralCountryClient _generalCountryClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GymMemberDetailsAgent(ICoditechLogging coditechLogging, IGeneralCountryClient generalCountryClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _generalCountryClient = GetClient<IGeneralCountryClient>(generalCountryClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual GymMemberDetailsListViewModel GetGymMemberDetailsList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CountryName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CountryCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CountryName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralCountryListResponse response = _generalCountryClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralCountryListModel countryList = new GeneralCountryListModel { GeneralCountryList = response?.GeneralCountryList };
            GeneralCountryListViewModel listViewModel = new GeneralCountryListViewModel();
            listViewModel.GeneralCountryList = countryList?.GeneralCountryList?.ToViewModel<GeneralCountryViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralCountryList.Count, BindColumns());
            //Todo
            return /*listViewModel*/ new GymMemberDetailsListViewModel();
        }

        //Create Member Details
        public virtual GymCreateEditMemberViewModel CreateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                gymCreateEditMemberViewModel.UserType = UserTypeEnum.GymMember.ToString();
                GeneralPersonResponse response = _userClient.InsertPersonInformation(gymCreateEditMemberViewModel.ToModel<GeneralPersonModel>());
                GeneralPersonModel generalPersonModel = response?.GeneralPersonModel;
                return IsNotNull(generalPersonModel) ? generalPersonModel.ToViewModel<GymCreateEditMemberViewModel>() : new GymCreateEditMemberViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, ex.ErrorMessage);
                    default:
                        return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Get Member Details by personId.
        public virtual GymCreateEditMemberViewModel GetMemberDetails(long personId)
        {
            //GeneralCountryResponse response = _generalCountryClient.GetCountry(generalCountryId);
            //return response?.GeneralCountryModel.ToViewModel<GeneralCountryViewModel>();

            return new GymCreateEditMemberViewModel();
        }

        //Update Member Details
        public virtual GymCreateEditMemberViewModel UpdateMemberDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                //GeneralCountryResponse response = _generalCountryClient.UpdateCountry(generalCountryViewModel.ToModel<GeneralCountryModel>());
                //GeneralCountryModel generalCountryModel = response?.GeneralCountryModel;
                //_coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                //return IsNotNull(generalCountryModel) ? generalCountryModel.ToViewModel<GeneralCountryViewModel>() : (GeneralCountryViewModel)GetViewModelWithErrorMessage(new GeneralCountryViewModel(), GeneralResources.UpdateErrorMessage);
                return new GymCreateEditMemberViewModel();
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
                return (GymCreateEditMemberViewModel)GetViewModelWithErrorMessage(gymCreateEditMemberViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalCountry.
        public virtual bool DeleteMembers(string gymMemberDetailIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalCountryClient.DeleteCountry(new ParameterModel { Ids = gymMemberDetailIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralCountryMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Gym.ToString(), TraceLevel.Error);
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
                ColumnName = "Country Name",
                ColumnCode = "CountryName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Country Code",
                ColumnCode = "CountryCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Default",
                ColumnCode = "DefaultFlag",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
