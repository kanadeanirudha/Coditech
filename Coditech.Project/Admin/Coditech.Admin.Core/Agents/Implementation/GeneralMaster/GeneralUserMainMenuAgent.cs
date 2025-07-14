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
    public class GeneralUserMainMenuAgent : BaseAgent, IGeneralUserMainMenuAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralUserMainMenuClient _generalUserMainMenuClient;
        #endregion

        #region Public Constructor
        public GeneralUserMainMenuAgent(ICoditechLogging coditechLogging, IGeneralUserMainMenuClient generalUserMainMenuClient)
        {
            _coditechLogging = coditechLogging;
            _generalUserMainMenuClient = GetClient<IGeneralUserMainMenuClient>(generalUserMainMenuClient);
        }
        #endregion

        #region Public Methods
        public virtual UserMainMenuListViewModel GetUserMainMenuList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("MenuName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MenuCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ParentMenuCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ModuleCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralUserMainMenuListResponse response = _generalUserMainMenuClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            UserMainMenuListModel userMainMenuList = new UserMainMenuListModel { GeneralUserMainMenuList = response?.GeneralUserMainMenuList };
            UserMainMenuListViewModel listViewModel = new UserMainMenuListViewModel();
            listViewModel.MenuList = userMainMenuList?.GeneralUserMainMenuList?.ToViewModel<UserMainMenuViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.MenuList.Count, BindColumns());
            return listViewModel;
        }

        //Create General UserMainMenu.
        public virtual UserMainMenuViewModel CreateUserMainMenu(UserMainMenuViewModel generalUserMainnMenuViewModel)
        {
            try
            {
                GeneralUserMainMenuResponse response = _generalUserMainMenuClient.CreateUserMainMenu(generalUserMainnMenuViewModel.ToModel<UserMainMenuModel>());
                UserMainMenuModel generalUserMainMenuModel = response?.GeneralUserMainMenuModel;
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalUserMainMenuModel) ? generalUserMainMenuModel.ToViewModel<UserMainMenuViewModel>() : new UserMainMenuViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (UserMainMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, ex.ErrorMessage);
                    default:
                        return (UserMainMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return (UserMainMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general UserMainMenu by general UserMainMenu master id.
        public virtual UserMainMenuViewModel GetUserMainMenu(short generalUserMainMenuId)
        {
            GeneralUserMainMenuResponse response = _generalUserMainMenuClient.GetUserMainMenu(generalUserMainMenuId);
            return response?.GeneralUserMainMenuModel.ToViewModel<UserMainMenuViewModel>();
        }

        //Update generalUserMainMenu.
        public virtual UserMainMenuViewModel UpdateUserMainMenu(UserMainMenuViewModel generalUserMainnMenuViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Info);
                GeneralUserMainMenuResponse response = _generalUserMainMenuClient.UpdateUserMainMenu(generalUserMainnMenuViewModel.ToModel<UserMainMenuModel>());
                UserMainMenuModel generalUserMainMenuModel = response?.GeneralUserMainMenuModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalUserMainMenuModel) ? generalUserMainMenuModel.ToViewModel<UserMainMenuViewModel>() : (UserMainMenuViewModel)GetViewModelWithErrorMessage(new UserMainMenuViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return (UserMainMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalUserMainMenu.
        public virtual bool DeleteUserMainMenu(string generalUserMainMenuId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalUserMainMenuClient.DeleteUserMainMenu(new ParameterModel { Ids = generalUserMainMenuId });
                SessionProxyHelper.RemoveAndBindUserDetails();
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteUserMainMenuMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "Menu Name",
                ColumnCode = "MenuName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Menu Code",
                ColumnCode = "MenuCode",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Parent Menu Code",
                ColumnCode = "ParentMenuCode",
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
                ColumnName = "Menu Display SeqNo",
                ColumnCode = "MenuDisplaySeqNo",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Menu Icon",
                ColumnCode = "MenuIconName",
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Enable",
                ColumnCode = "IsEnable",
                IsSortable=true
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all UserMainMenu list from database 
        public virtual GeneralUserMainMenuListResponse GetUserMainMenuList()
        {
            GeneralUserMainMenuListResponse userMainMenuList = _generalUserMainMenuClient.List(null, null, null, 1, int.MaxValue);
            return userMainMenuList?.GeneralUserMainMenuList?.Count > 0 ? userMainMenuList : new GeneralUserMainMenuListResponse();
        }
        #endregion
    }
}
