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
        public virtual GeneralEnumaratorGroupListViewModel GetGeneralEnumaratorGroupList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("DisaplyText", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("GeneralEnumaratorGroupCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "DisaplyText" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralEnumaratorGroupListResponse response = _generalEnumaratorGroupClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralEnumaratorGroupListModel generalEnumaratorGroupList = new GeneralEnumaratorGroupListModel { GeneralEnumaratorGroupList = response?.GeneralEnumaratorGroupList };
            GeneralEnumaratorGroupListViewModel listViewModel = new GeneralEnumaratorGroupListViewModel();
            listViewModel.GeneralEnumaratorGroupList = generalEnumaratorGroupList?.GeneralEnumaratorGroupList?.ToViewModel<GeneralEnumaratorGroupViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralEnumaratorGroupList.Count, BindColumns());
            return listViewModel;
        }

        //Create General GeneralEnumaratorGroup.
        public virtual GeneralEnumaratorGroupViewModel CreateGeneralEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            try
            {
                GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.CreateGeneralEnumaratorGroup(generalEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel generalEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                return IsNotNull(generalEnumaratorGroupModel) ? generalEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : new GeneralEnumaratorGroupViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general GeneralEnumaratorGroup by general generalEnumaratorGroup master id.
        public virtual GeneralEnumaratorGroupViewModel GetGeneralEnumaratorGroup(int generalEnumaratorGroupId)
        {
            GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.GetGeneralEnumaratorGroup(generalEnumaratorGroupId);
            return response?.GeneralEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>();
        }

        //Update generalEnumaratorGroup.
        public virtual GeneralEnumaratorGroupViewModel UpdateGeneralEnumaratorGroup(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Info);
                GeneralEnumaratorGroupResponse response = _generalEnumaratorGroupClient.UpdateGeneralEnumaratorGroup(generalEnumaratorGroupViewModel.ToModel<GeneralEnumaratorGroupModel>());
                GeneralEnumaratorGroupModel generalEnumaratorGroupModel = response?.GeneralEnumaratorGroupModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalEnumaratorGroupModel) ? generalEnumaratorGroupModel.ToViewModel<GeneralEnumaratorGroupViewModel>() : (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(new GeneralEnumaratorGroupViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return (GeneralEnumaratorGroupViewModel)GetViewModelWithErrorMessage(generalEnumaratorGroupViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalEnumaratorGroup.
        public virtual bool DeleteGeneralEnumaratorGroup(string generalEnumaratorGroupId, out string errorMessage)            
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalEnumaratorGroupClient.DeleteGeneralEnumaratorGroup(new ParameterModel { Ids = generalEnumaratorGroupId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Warning);
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
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "GeneralEnumaratorGroup Name",
                ColumnCode = "DisaplyText",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "EnumGroup Code",
                ColumnCode = "EnumGroupCode",
                IsSortable = true,
            });
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Is Default",
            //    ColumnCode = "DefaultFlag",
            //});
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all generalEnumaratorGroup list from database 
        public virtual GeneralEnumaratorGroupListResponse GetGeneralEnumaratorGroupList()
        {
            GeneralEnumaratorGroupListResponse generalEnumaratorGroupList = _generalEnumaratorGroupClient.List(null, null, null, 1, int.MaxValue);
            return generalEnumaratorGroupList?.GeneralEnumaratorGroupList?.Count > 0 ? generalEnumaratorGroupList : new GeneralEnumaratorGroupListResponse();
        }
        #endregion

    }
}
