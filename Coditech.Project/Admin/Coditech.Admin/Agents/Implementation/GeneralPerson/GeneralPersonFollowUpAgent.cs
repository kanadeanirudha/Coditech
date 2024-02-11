using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.GeneralPerson.GeneralPersonFollowUp;
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
    public class GeneralPersonFollowUpAgent : BaseAgent, IGeneralPersonFollowUpAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralPersonFollowUpClient _generalPersonFollowUpClient;
        private readonly IUserClient _userClient;
        #endregion

        #region Public Constructor
        public GeneralPersonFollowUpAgent(ICoditechLogging coditechLogging, IGeneralPersonFollowUpClient generalPersonFollowUpClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _generalPersonFollowUpClient = GetClient<IGeneralPersonFollowUpClient>(generalPersonFollowUpClient);
            _userClient = GetClient<IUserClient>(userClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralPersonFollowUpListViewModel GetPersonFollowUpList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PersonId", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PersonCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralPersonFollowUpListResponse response = _generalPersonFollowUpClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralPersonFollowUpListModel PersonFollowUpList = new GeneralPersonFollowUpListModel { GeneralPersonFollowUpList = response?.GeneralPersonFollowUpList };
            GeneralPersonFollowUpListViewModel listViewModel = new GeneralPersonFollowUpListViewModel();
            listViewModel.GeneralPersonFollowUpList = PersonFollowUpList?.GeneralPersonFollowUpList?.ToViewModel<GeneralPersonFollowUpViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralPersonFollowUpList.Count, BindColumns());
            return listViewModel;
        }

        //Create General PersonFollowUp.
        public virtual GeneralPersonFollowUpViewModel CreatePersonFollowUp(GeneralPersonFollowUpViewModel generalPersonFollowUpViewModel)
        {
            try
            {
                GeneralPersonFollowUpResponse response = _generalPersonFollowUpClient.CreatePersonFollowUp(generalPersonFollowUpViewModel.ToModel<GeneralPersonFollowUpModel>());
                GeneralPersonFollowUpModel generalPersonFollowUpModel = response?.GeneralPersonFollowUpModel;
                return IsNotNull(generalPersonFollowUpModel) ? generalPersonFollowUpModel.ToViewModel<GeneralPersonFollowUpViewModel>() : new GeneralPersonFollowUpViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(generalPersonFollowUpViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(generalPersonFollowUpViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(generalPersonFollowUpViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general PersonFollowUp by general PersonFollowUp  id.
        public virtual GeneralPersonFollowUpViewModel GetPersonFollowUp(long generalPersonFollowUpId)
        {
            GeneralPersonFollowUpResponse response = _generalPersonFollowUpClient.GetPersonFollowUp(generalPersonFollowUpId);
            return response?.GeneralPersonFollowUpModel.ToViewModel<GeneralPersonFollowUpViewModel>();
        }

        //Update generalPersonFollowUp.
        public virtual GeneralPersonFollowUpViewModel UpdatePersonFollowUp(GeneralPersonFollowUpViewModel generalPersonFollowUpViewModel)
        {
            try
            {
                if (generalPersonFollowUpViewModel.IsSetReminder)
                {
                    GeneralPersonModel generalPersonModel = generalPersonFollowUpViewModel.ToModel<GeneralPersonModel>();
                   // generalPersonModel.UserType = generalPersonFollowUpViewModel.UserTypeCode;
                    GeneralPersonResponse generalPersonResponse = _userClient.InsertPersonInformation(generalPersonModel);
                    if (generalPersonResponse?.GeneralPersonModel.PersonId <= 0)
                    {
                        return (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(generalPersonFollowUpViewModel, GeneralResources.UpdateErrorMessage);
                    }
                }
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Info);
                GeneralPersonFollowUpResponse response = _generalPersonFollowUpClient.UpdatePersonFollowUp(generalPersonFollowUpViewModel.ToModel<GeneralPersonFollowUpModel>());
                GeneralPersonFollowUpModel generalPersonFollowUpModel = response?.GeneralPersonFollowUpModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Info);
                return IsNotNull(generalPersonFollowUpModel) ? generalPersonFollowUpModel.ToViewModel<GeneralPersonFollowUpViewModel>() : (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(new GeneralPersonFollowUpViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
                return (GeneralPersonFollowUpViewModel)GetViewModelWithErrorMessage(generalPersonFollowUpViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalPersonFollowUp.
        public virtual bool DeletePersonFollowUp(string generalPersonFollowUpId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalPersonFollowUpClient.DeletePersonFollowUp(new ParameterModel { Ids = generalPersonFollowUpId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralPersonFollowUp;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonFollowUp.ToString(), TraceLevel.Error);
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
                ColumnName = "Follow Taken Id",
                ColumnCode = "FollowTakenId",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Follow Taken For",
                ColumnCode = "FollowTakenFor",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Follow Up Type",
                ColumnCode = "FollowupTypesEnumId",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Follow Up Comment",
                ColumnCode = "FollowupComment",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Set Reminder",
                ColumnCode = "IsSetReminder",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Reminder Date",
                ColumnCode = "ReminderDate",
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all PersonFollowUp list from database 
        public virtual GeneralPersonFollowUpListResponse GetPersonFollowUpList()
        {
            GeneralPersonFollowUpListResponse PersonFollowUpList = _generalPersonFollowUpClient.List(null, null, null, 1, int.MaxValue);
            return PersonFollowUpList?.GeneralPersonFollowUpList?.Count > 0 ? PersonFollowUpList : new GeneralPersonFollowUpListResponse();
        }
        #endregion
    }
}
