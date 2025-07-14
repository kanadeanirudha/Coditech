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
    public class OrganisationCentrewiseJoiningCodeAgent : BaseAgent, IOrganisationCentrewiseJoiningCodeAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewiseJoiningCodeClient _organisationCentrewiseJoiningCodeClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewiseJoiningCodeAgent(ICoditechLogging coditechLogging, IOrganisationCentrewiseJoiningCodeClient organisationCentrewiseJoiningCodeClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewiseJoiningCodeClient = GetClient<IOrganisationCentrewiseJoiningCodeClient>(organisationCentrewiseJoiningCodeClient);
        }
        #endregion
            
        #region Public Methods
        public virtual OrganisationCentrewiseJoiningCodeListViewModel GetOrganisationCentrewiseJoiningCodeList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dataTableModel.SelectedCentreCode);

            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("JoiningCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "IsExpired" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentrewiseJoiningCodeListResponse response = _organisationCentrewiseJoiningCodeClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentrewiseJoiningCodeListModel organisationCentrewiseJoiningCodeList = new OrganisationCentrewiseJoiningCodeListModel { OrganisationCentrewiseJoiningCodeList = response?.OrganisationCentrewiseJoiningCodeList };
            OrganisationCentrewiseJoiningCodeListViewModel listViewModel = new OrganisationCentrewiseJoiningCodeListViewModel();
            listViewModel.OrganisationCentrewiseJoiningCodeList = organisationCentrewiseJoiningCodeList?.OrganisationCentrewiseJoiningCodeList?.ToViewModel<OrganisationCentrewiseJoiningCodeViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentrewiseJoiningCodeList.Count, BindColumns());
            return listViewModel;
        }
        //Create Organisation Centrewise Joining Code
        public virtual OrganisationCentrewiseJoiningCodeViewModel CreateOrganisationCentrewiseJoiningCode(OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel)
        {
            try
            {
                OrganisationCentrewiseJoiningCodeResponse response = _organisationCentrewiseJoiningCodeClient.CreateOrganisationCentrewiseJoiningCode(organisationCentrewiseJoiningCodeViewModel.ToModel<OrganisationCentrewiseJoiningCodeModel>());
                OrganisationCentrewiseJoiningCodeModel organisationCentrewiseJoiningCodeModel = response?.OrganisationCentrewiseJoiningCodeModel;
                return IsNotNull(organisationCentrewiseJoiningCodeModel) ? organisationCentrewiseJoiningCodeModel.ToViewModel<OrganisationCentrewiseJoiningCodeViewModel>() : new OrganisationCentrewiseJoiningCodeViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        // This method is used to Send Joining Code the user
        public virtual OrganisationCentrewiseJoiningCodeViewModel OrganisationCentrewiseJoiningCodeSend(string joiningCode, string emailId, string mobileNumber)
        {
            OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel = new OrganisationCentrewiseJoiningCodeViewModel();
            try
            {
                OrganisationCentrewiseJoiningCodeResponse organisationCentrewiseJoiningCodeResponse = _organisationCentrewiseJoiningCodeClient.OrganisationCentrewiseJoiningCodeSend(joiningCode, emailId, mobileNumber);
                //if (resetPasswordSendLinkResponse != null && !resetPasswordSendLinkResponse.HasError)
                //{
                //    return resetPasswordSendLinkViewModel;
                //}
                //else
                //{
                //    return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(resetPasswordSendLinkViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
                //}
                OrganisationCentrewiseJoiningCodeModel organisationCentrewiseJoiningCodeModel = organisationCentrewiseJoiningCodeResponse?.OrganisationCentrewiseJoiningCodeModel;
                return IsNotNull(organisationCentrewiseJoiningCodeModel) ? organisationCentrewiseJoiningCodeModel.ToViewModel<OrganisationCentrewiseJoiningCodeViewModel>() : new OrganisationCentrewiseJoiningCodeViewModel();
            }
            catch (CoditechException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.NotFound:
                        return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, "Please make sure that the EmailId you entered is correct.");
                    case ErrorCodes.ContactAdministrator:
                        return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, $"Access Denied. {GeneralResources.ErrorMessage_PleaseContactYourAdministrator}");
                    default:
                        return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, $"{GeneralResources.ErrorMessage_PleaseContactYourAdministrator}");
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseJoiningCodeViewModel)GetViewModelWithErrorMessage(organisationCentrewiseJoiningCodeViewModel, GeneralResources.ErrorMessage_PleaseContactYourAdministrator);
            }
        }

        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Joining Code",
                ColumnCode = "JoiningCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Expired",
                ColumnCode = "IsExpired",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
