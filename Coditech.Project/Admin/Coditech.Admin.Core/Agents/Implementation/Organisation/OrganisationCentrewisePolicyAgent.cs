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
    public class OrganisationCentrewisePolicyAgent : BaseAgent, IOrganisationCentrewisePolicyAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentrewisePolicyClient _organisationCentrewisePolicyClient;
        #endregion

        #region Public Constructor
        public OrganisationCentrewisePolicyAgent(ICoditechLogging coditechLogging, IOrganisationCentrewisePolicyClient organisationCentrewisePolicyClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentrewisePolicyClient = GetClient<IOrganisationCentrewisePolicyClient>(organisationCentrewisePolicyClient);
        }
        #endregion
        #region Public Methods
        public virtual GeneralPolicyDetailsListViewModel GetOrganisationCentrewisePolicyList(string centreCode)
        {
            DataTableViewModel dataTableViewModel =  new DataTableViewModel();
            GeneralPolicyDetailsListResponse response = _organisationCentrewisePolicyClient.List(centreCode);
            GeneralPolicyDetailsListModel organisationCentrewisePolicyList = new GeneralPolicyDetailsListModel { GeneralPolicyDetailsList = response?.GeneralPolicyDetailsList };
            GeneralPolicyDetailsListViewModel listViewModel = new GeneralPolicyDetailsListViewModel();
            listViewModel.GeneralPolicyDetailsList = organisationCentrewisePolicyList?.GeneralPolicyDetailsList?.ToViewModel<GeneralPolicyDetailsViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableViewModel, listViewModel.GeneralPolicyDetailsList.Count, BindColumns());
            return listViewModel;
        }

        //Get Organisation Centrewise Policy by Organisation Centrewise Policy Details id.
        public virtual GeneralPolicyDetailsViewModel GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId)
        {
            GeneralPolicyDetailsResponse response = _organisationCentrewisePolicyClient.GetCentrewisePolicyDetails(centreCode, generalPolicyRulesId);
            return response?.GeneralPolicyDetailsModel.ToViewModel<GeneralPolicyDetailsViewModel>();
        }

        //Update Organisation Centrewise Policy.
        public virtual GeneralPolicyDetailsViewModel CentrewisePolicyDetails(GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Info);
                GeneralPolicyDetailsResponse response = _organisationCentrewisePolicyClient.CentrewisePolicyDetails(generalPolicyDetailsViewModel.ToModel<GeneralPolicyDetailsModel>());
                GeneralPolicyDetailsModel generalPolicyDetailsModel = response?.GeneralPolicyDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(generalPolicyDetailsModel) ? generalPolicyDetailsModel.ToViewModel<GeneralPolicyDetailsViewModel>() : (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(new GeneralPolicyDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
                return (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(generalPolicyDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete CentrewisePolicy.
        public virtual bool DeleteCentrewisePolicy(string generalPolicyRulesId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _organisationCentrewisePolicyClient.DeleteCentrewisePolicy(new ParameterModel { Ids = generalPolicyRulesId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralPolicyMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewisePolicy.ToString(), TraceLevel.Error);
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
                ColumnName = "Policy Code",
                ColumnCode = "PolicyCode",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Question Description",
                ColumnCode = "PolicyQuestionDescription",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Answered",
                ColumnCode = "PolicyAnswered",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
