using Coditech.Admin.Helpers;
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
    public class GeneralUserModuleAgent : BaseAgent, IGeneralUserModuleAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralUserModuleClient _generalUserModuleClient;
        #endregion

        #region Public Constructor
        public GeneralUserModuleAgent(ICoditechLogging coditechLogging, IGeneralUserModuleClient generalUserModuleClient)
        {
            _coditechLogging = coditechLogging;
            _generalUserModuleClient = GetClient<IGeneralUserModuleClient>(generalUserModuleClient);
        }
        #endregion

        #region Public Methods
        public virtual UserModuleListViewModel GetUserModuleList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("ModuleName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleRelatedWith", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleFormName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleTooltip", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleSeqNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            UserModuleListResponse response = _generalUserModuleClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            UserModuleListModel userModuleList = new UserModuleListModel { ModuleList = response?.ModuleList };
            UserModuleListViewModel listViewModel = new UserModuleListViewModel();
            listViewModel.ModuleList = userModuleList?.ModuleList?.ToViewModel<UserModuleViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.ModuleList.Count, BindColumns());
            return listViewModel;
        }

        //Create General UserModule.
        public virtual UserModuleViewModel CreateUserModule(UserModuleViewModel generalUserModuleViewModel)
        {
            try
            {
                GeneralUserModuleResponse response = _generalUserModuleClient.CreateUserModule(generalUserModuleViewModel.ToModel<UserModuleModel>());
                UserModuleModel generalUserModuleModel = response?.UserModuleModel;
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalUserModuleModel) ? generalUserModuleModel.ToViewModel<UserModuleViewModel>() : new UserModuleViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (UserModuleViewModel)GetViewModelWithErrorMessage(generalUserModuleViewModel, ex.ErrorMessage);
                    default:
                        return (UserModuleViewModel)GetViewModelWithErrorMessage(generalUserModuleViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return (UserModuleViewModel)GetViewModelWithErrorMessage(generalUserModuleViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general UserModule by general UserModule master id.
        public virtual UserModuleViewModel GetUserModule(short userModuleMasterId)
        {
            GeneralUserModuleResponse response = _generalUserModuleClient.GetUserModule(userModuleMasterId);
            return response?.UserModuleModel.ToViewModel<UserModuleViewModel>();
        }

        //Update generalUserModule.
        public virtual UserModuleViewModel UpdateUserModule(UserModuleViewModel generalUserModuleViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Info);
                GeneralUserModuleResponse response = _generalUserModuleClient.UpdateUserModule(generalUserModuleViewModel.ToModel<UserModuleModel>());
                UserModuleModel generalUserModuleModel = response?.UserModuleModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Info);
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalUserModuleModel) ? generalUserModuleModel.ToViewModel<UserModuleViewModel>() : (UserModuleViewModel)GetViewModelWithErrorMessage(new UserModuleViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
                return (UserModuleViewModel)GetViewModelWithErrorMessage(generalUserModuleViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalUserModule.
        public virtual bool DeleteUserModule(string userModuleMasterId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalUserModuleClient.DeleteUserModule(new ParameterModel { Ids = userModuleMasterId });
                SessionProxyHelper.RemoveAndBindUserDetails();
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteUserModuleMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserModuleMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Module Name",
                ColumnCode = "ModuleName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Module Code",
                ColumnCode = "ModuleCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Module Seq Number",
                ColumnCode = "ModuleSeqNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Module Tool tip",
                ColumnCode = "ModuleTooltip",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Module Icon",
                ColumnCode = "ModuleIconName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Active Flag",
                ColumnCode = "ModuleActiveFlag",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
