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
    public class GeneralEnumaratorGroupAgent : BaseAgent, IGeneralEnumaratorGroupAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralEnumaratorGroupClient _generalEnumaratorGroupClient;
        #endregion

        #region Public Constructor
        public GeneralEnumaratorGroupAgent(ICoditechLogging coditechLogging, IGeneralEnumaratorGroupClient generalEnumaratorGroupClient)
        {
            _coditechLogging = coditechLogging;
            _generalEnumaratorGroupClient = GetClient<IGeneralEnumaratorGroupClient>(generalEnumaratorGroupClient);
        }
        #endregion

        #region Public Methods

        #region EnumaratorGroup
        public virtual GeneralEnumaratorGroupListViewModel GetEnumaratorGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("EnumGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DisplayText", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "EnumGroupCode" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralEnumaratorGroupListResponse response = _generalEnumaratorGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralEnumaratorGroupListModel enumGroupList = new GeneralEnumaratorGroupListModel { GeneralEnumaratorGroupList = response?.GeneralEnumaratorGroupList };
            GeneralEnumaratorGroupListViewModel listViewModel = new GeneralEnumaratorGroupListViewModel();
            listViewModel.GeneralEnumaratorGroupList = enumGroupList?.GeneralEnumaratorGroupList?.ToViewModel<GeneralEnumaratorGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralEnumaratorGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General EnumaratorGroup.
        public virtual GeneralEnumaratorGroupViewModel CreateEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            try
            {
                GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.CreateEnumaratorGroup(generalEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel generalEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalEnumaratorGroupModel) ? generalEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : new GeneralEnumaratorGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general EnumaratorGroup by general Enumarator master id.
        public virtual GeneralEnumaratorGroupViewModel GetEnumaratorGroup(int generalEnumaratorGroupId)
        {
            GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.GetEnumaratorGroup(generalEnumaratorGroupId);
            return response?.GeneralEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>();
        }

        //Update generalEnumaratorGroup.
        public virtual GeneralEnumaratorGroupViewModel UpdateEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Info);
                GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.UpdateEnumaratorGroup(generalEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel generalEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Info);
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalEnumaratorGroupModel) ? generalEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(new GeneralEnumaratorGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalEnumaratorGroup.
        public virtual bool DeleteEnumaratorGroup(string generalEnumaratorGroupId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalEnumaratorGroupClient.DeleteEnumaratorGroup(new ParameterModel { Ids = generalEnumaratorGroupId });
                SessionProxyHelper.RemoveAndBindUserDetails();
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralEnumaratorGroup;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        #endregion

        #region Enumarator

        //Get general Enumarator by general Enumarator master id.
        public virtual GeneralEnumaratorViewModel GetEnumarator(int generalEnumaratorId)
        {
            GeneralEnumaratorResponse response = _generalEnumaratorGroupClient.GetEnumarator(generalEnumaratorId);
            return response?.GeneralEnumaratorModel.ToViewModel<GeneralEnumaratorViewModel>();
        }

        //Insert Update generalEnumarator.
        public virtual GeneralEnumaratorViewModel InsertUpdateEnumarator(GeneralEnumaratorViewModel generalEnumaratorViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Info);
                GeneralEnumaratorResponse response = _generalEnumaratorGroupClient.InsertUpdateEnumarator(generalEnumaratorViewModel.ToModel<GeneralEnumaratorModel>());
                GeneralEnumaratorModel generalEnumaratorModel = response?.GeneralEnumaratorModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Info);
                SessionProxyHelper.RemoveAndBindUserDetails();
                return IsNotNull(generalEnumaratorModel) ? generalEnumaratorModel.ToViewModel<GeneralEnumaratorViewModel>() : (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(new GeneralEnumaratorViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorViewModel)GetViewModelWithErrorMessage(generalEnumaratorViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalEnumarator.
        public virtual bool DeleteEnumarator(string generalEnumaratorId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalEnumaratorGroupClient.DeleteEnumarator(new ParameterModel { Ids = generalEnumaratorId });
                SessionProxyHelper.RemoveAndBindUserDetails();
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralEnumarator;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Error);
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
                ColumnName = "EnumGroup Code",
                ColumnCode = "EnumGroupCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Display Text",
                ColumnCode = "Display Text",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}
