using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
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
    public class GeneralPolicyAgent : BaseAgent, IGeneralPolicyAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralPolicyClient _generalPolicyClient;
        #endregion

        #region Public Constructor
        public GeneralPolicyAgent(ICoditechLogging coditechLogging, IGeneralPolicyClient generalPolicyClient)
        {
            _coditechLogging = coditechLogging;
            _generalPolicyClient = GetClient<IGeneralPolicyClient>(generalPolicyClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralPolicyListViewModel GetPolicyList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PolicyName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PolicyCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PolicyRelatedToModuleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "PolicyName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralPolicyListResponse response = _generalPolicyClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralPolicyListModel generalPolicyList = new GeneralPolicyListModel { GeneralPolicyList = response?.GeneralPolicyList };
            GeneralPolicyListViewModel listViewModel = new GeneralPolicyListViewModel();
            listViewModel.GeneralPolicyList = generalPolicyList?.GeneralPolicyList?.ToViewModel<GeneralPolicyViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralPolicyList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Policy.
        public virtual GeneralPolicyViewModel CreatePolicy(GeneralPolicyViewModel generalPolicyViewModel)
        {
            try
            {
                GeneralPolicyResponse response = _generalPolicyClient.CreatePolicy(generalPolicyViewModel.ToModel<GeneralPolicyModel>());
                GeneralPolicyModel generalPolicyModel = response?.GeneralPolicyModel;
                return IsNotNull(generalPolicyModel) ? generalPolicyModel.ToViewModel<GeneralPolicyViewModel>() : new GeneralPolicyViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Policy by general Policy master id.
        public virtual GeneralPolicyViewModel GetPolicy(string policyCode)
        {
            GeneralPolicyResponse response = _generalPolicyClient.GetPolicy(policyCode);
            return response?.GeneralPolicyModel.ToViewModel<GeneralPolicyViewModel>();
        }

        //Update generalPolicy.
        public virtual GeneralPolicyViewModel UpdatePolicy(GeneralPolicyViewModel generalPolicyViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                GeneralPolicyResponse response = _generalPolicyClient.UpdatePolicy(generalPolicyViewModel.ToModel<GeneralPolicyModel>());
                GeneralPolicyModel generalPolicyModel = response?.GeneralPolicyModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalPolicyModel) ? generalPolicyModel.ToViewModel<GeneralPolicyViewModel>() : (GeneralPolicyViewModel)GetViewModelWithErrorMessage(new GeneralPolicyViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return (GeneralPolicyViewModel)GetViewModelWithErrorMessage(generalPolicyViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalPolicy.
        public virtual bool DeletePolicy(string policyCode, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalPolicyClient.DeletePolicy(new ParameterModel { Ids = policyCode });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        //Policy Rules List.
        public virtual GeneralPolicyRulesListViewModel GetGeneralPolicyRulesList(string policyCode, DataTableViewModel dataTableModel)
        {
            GeneralPolicyRulesListResponse response = _generalPolicyClient.GetGeneralPolicyRulesList(policyCode, null);
            GeneralPolicyRulesListModel generalPolicyRulesListModel = new GeneralPolicyRulesListModel { GeneralPolicyRulesList = response?.GeneralPolicyRulesList };
            GeneralPolicyRulesListViewModel listViewModel = new GeneralPolicyRulesListViewModel();
            listViewModel.GeneralPolicyRulesList = generalPolicyRulesListModel?.GeneralPolicyRulesList?.ToViewModel<GeneralPolicyRulesViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralPolicyRulesList.Count, BindPolicyRulesColumns());
            listViewModel.PolicyCode =policyCode;
            return listViewModel;
        }

        //Create General Policy Rules.
        public virtual GeneralPolicyRulesViewModel CreatePolicyRules(GeneralPolicyRulesViewModel generalPolicyRulesViewModel)
        {
            try
            {
                GeneralPolicyRulesResponse response = _generalPolicyClient.CreatePolicyRules(generalPolicyRulesViewModel.ToModel<GeneralPolicyRulesModel>());
                GeneralPolicyRulesModel generalPolicyRulesModel = response?.GeneralPolicyRulesModel;
                return IsNotNull(generalPolicyRulesModel) ? generalPolicyRulesModel.ToViewModel<GeneralPolicyRulesViewModel>() : new GeneralPolicyRulesViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Policy by general Policy Rules id.
        public virtual GeneralPolicyRulesViewModel GetPolicyRules(short generalPolicyRulesId, string policyApplicableStatus)
        {
            GeneralPolicyRulesResponse response = _generalPolicyClient.GetPolicyRules(generalPolicyRulesId, policyApplicableStatus);
            return response?.GeneralPolicyRulesModel.ToViewModel<GeneralPolicyRulesViewModel>();
        }

        //Update generalPolicyRules.
        public virtual GeneralPolicyRulesViewModel UpdatePolicyRules(GeneralPolicyRulesViewModel generalPolicyRulesViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                GeneralPolicyRulesResponse response = _generalPolicyClient.UpdatePolicyRules(generalPolicyRulesViewModel.ToModel<GeneralPolicyRulesModel>());
                GeneralPolicyRulesModel generalPolicyRulesModel = response?.GeneralPolicyRulesModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalPolicyRulesModel) ? generalPolicyRulesModel.ToViewModel<GeneralPolicyRulesViewModel>() : (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(new GeneralPolicyRulesViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return (GeneralPolicyRulesViewModel)GetViewModelWithErrorMessage(generalPolicyRulesViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalPolicyRules.
        public virtual bool DeletePolicyRules(string generalPolicyRulesId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalPolicyClient.DeletePolicyRules(new ParameterModel { Ids = generalPolicyRulesId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        //Get general Policy by general Policy Details id.
        public virtual GeneralPolicyDetailsViewModel GetPolicyDetails(short generalPolicyDetailsId)
        {
            GeneralPolicyDetailsResponse response = _generalPolicyClient.GetPolicyDetails(generalPolicyDetailsId);
            return response?.GeneralPolicyDetailsModel.ToViewModel<GeneralPolicyDetailsViewModel>();
        }

        //Update generalPolicyDetails.
        public virtual GeneralPolicyDetailsViewModel UpdatePolicyDetails(GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                GeneralPolicyDetailsResponse response = _generalPolicyClient.UpdatePolicyDetails(generalPolicyDetailsViewModel.ToModel<GeneralPolicyDetailsModel>());
                GeneralPolicyDetailsModel generalPolicyDetailsModel = response?.GeneralPolicyDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalPolicyDetailsModel) ? generalPolicyDetailsModel.ToViewModel<GeneralPolicyDetailsViewModel>() : (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(new GeneralPolicyDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(generalPolicyDetailsViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(generalPolicyDetailsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PolicyMaster.ToString(), TraceLevel.Error);
                return (GeneralPolicyDetailsViewModel)GetViewModelWithErrorMessage(generalPolicyDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Name",
                ColumnCode = "PolicyName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Code",
                ColumnCode = "PolicyCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Related To Module Code",
                ColumnCode = "PolicyRelatedToModuleCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Applicable Status",
                ColumnCode = "PolicyApplicableStatus",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsPolicyActive",
                IsSortable = true,
            });
            return datatableColumnList;
        }

        protected virtual List<DatatableColumns> BindPolicyRulesColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Question Description",
                ColumnCode = "PolicyQuestionDescription",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Applicable Status",
                ColumnCode = "PolicyApplicableStatus",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Range",
                ColumnCode = "PolicyRange",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Default Answer",
                ColumnCode = "PolicyDefaultAnswer",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Policy Ans Type",
                ColumnCode = "PolicyAnsType",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Range Separate By",
                ColumnCode = "RangeSeparateBy",              
            });
            return datatableColumnList;
        }
        #endregion
    }
}
