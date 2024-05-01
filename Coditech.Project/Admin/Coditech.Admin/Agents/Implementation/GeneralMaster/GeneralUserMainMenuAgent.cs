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
        public virtual GeneralUserMainMenuListViewModel GetUserMainMenuList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("MenuName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("MenuCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralUserMainMenuListResponse response = _generalUserMainMenuClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralUserMainMenuListModel userMainMenuList = new GeneralUserMainMenuListModel { GeneralUserMainMenuList = response?.GeneralUserMainMenuList };
            GeneralUserMainMenuListViewModel listViewModel = new GeneralUserMainMenuListViewModel();
            listViewModel.GeneralUserMainMenuList = userMainMenuList?.GeneralUserMainMenuList?.ToViewModel<GeneralUserMainnMenuViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralUserMainMenuList.Count, BindColumns());
            return listViewModel;
        }

        //Create General UserMainMenu.
        public virtual GeneralUserMainnMenuViewModel CreateUserMainMenu(GeneralUserMainnMenuViewModel generalUserMainnMenuViewModel)
        {
            try
            {
                GeneralUserMainMenuResponse response = _generalUserMainMenuClient.CreateUserMainMenu(generalUserMainnMenuViewModel.ToModel<GeneralUserMainMenuModel>());
                GeneralUserMainMenuModel generalUserMainMenuModel = response?.GeneralUserMainMenuModel;
                return IsNotNull(generalUserMainMenuModel) ? generalUserMainMenuModel.ToViewModel<GeneralUserMainnMenuViewModel>() : new GeneralUserMainnMenuViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralUserMainnMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralUserMainnMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return (GeneralUserMainnMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general UserMainMenu by general UserMainMenu master id.
        public virtual GeneralUserMainnMenuViewModel GetUserMainMenu(short generalUserMainMenuId)
        {
            GeneralUserMainMenuResponse response = _generalUserMainMenuClient.GetUserMainMenu(generalUserMainMenuId);
            return response?.GeneralUserMainMenuModel.ToViewModel<GeneralUserMainnMenuViewModel>();
        }

        //Update generalUserMainMenu.
        public virtual GeneralUserMainnMenuViewModel UpdateUserMainMenu(GeneralUserMainnMenuViewModel generalUserMainnMenuViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Info);
                GeneralUserMainMenuResponse response = _generalUserMainMenuClient.UpdateUserMainMenu(generalUserMainnMenuViewModel.ToModel<GeneralUserMainMenuModel>());
                GeneralUserMainMenuModel generalUserMainMenuModel = response?.GeneralUserMainMenuModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalUserMainMenuModel) ? generalUserMainMenuModel.ToViewModel<GeneralUserMainnMenuViewModel>() : (GeneralUserMainnMenuViewModel)GetViewModelWithErrorMessage(new GeneralUserMainnMenuViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return (GeneralUserMainnMenuViewModel)GetViewModelWithErrorMessage(generalUserMainnMenuViewModel, GeneralResources.UpdateErrorMessage);
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
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralUserMainMenuMaster;
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
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "MenuInstalled Flag",
                ColumnCode = "MenuInstalledFlag",
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
