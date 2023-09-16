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
    public class GeneralNationalityAgent : BaseAgent, IGeneralNationalityAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralNationalityClient _generalNationalityClient;
        #endregion

        #region Public Constructor
        public GeneralNationalityAgent(ICoditechLogging coditechLogging, IGeneralNationalityClient generalNationalityClient)
        {
            _coditechLogging = coditechLogging;
            _generalNationalityClient = GetClient<IGeneralNationalityClient>(generalNationalityClient);
        }
        #endregion

        #region Public Methods
        public GeneralNationalityListViewModel GetNationalityList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("Description", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DefaultFlag", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "Description" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralNationalityListResponse response = _generalNationalityClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralNationalityListModel nationalityList = new GeneralNationalityListModel { GeneralNationalityList = response?.GeneralNationalityList };
            GeneralNationalityListViewModel listViewModel = new GeneralNationalityListViewModel();
            listViewModel.GeneralNationalityList = nationalityList?.GeneralNationalityList?.ToViewModel<GeneralNationalityViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralNationalityList.Count, BindColumns());
            return listViewModel;
        }

        //Create General Nationality.
        public virtual GeneralNationalityViewModel CreateNationality(GeneralNationalityViewModel generalNationalityViewModel)
        {
            try
            {
                GeneralNationalityResponse response = _generalNationalityClient.CreateNationality(generalNationalityViewModel.ToModel<GeneralNationalityModel>());
                GeneralNationalityModel generalNationalityModel = response?.GeneralNationalityModel;
                return IsNotNull(generalNationalityModel) ? generalNationalityModel.ToViewModel<GeneralNationalityViewModel>() : new GeneralNationalityViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralNationalityViewModel)GetViewModelWithErrorMessage(generalNationalityViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralNationalityViewModel)GetViewModelWithErrorMessage(generalNationalityViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return (GeneralNationalityViewModel)GetViewModelWithErrorMessage(generalNationalityViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Nationality by general nationality master id.
        public virtual GeneralNationalityViewModel GetNationality(short nationalityId)
        {
            GeneralNationalityResponse response = _generalNationalityClient.GetNationality(nationalityId);
            return response?.GeneralNationalityModel.ToViewModel<GeneralNationalityViewModel>();
        }

        //Update Nationality.
        public virtual GeneralNationalityViewModel UpdateNationality(GeneralNationalityViewModel generalNationalityViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Info);
                GeneralNationalityResponse response = _generalNationalityClient.UpdateNationality(generalNationalityViewModel.ToModel<GeneralNationalityModel>());
                GeneralNationalityModel generalNationalityModel = response?.GeneralNationalityModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Info);
                return IsNotNull(generalNationalityModel) ? generalNationalityModel.ToViewModel<GeneralNationalityViewModel>() : (GeneralNationalityViewModel)GetViewModelWithErrorMessage(new GeneralNationalityViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return (GeneralNationalityViewModel)GetViewModelWithErrorMessage(generalNationalityViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Nationality.
        public virtual bool DeleteNationality(string nationalityId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalNationalityClient.DeleteNationality(new ParameterModel { Ids = nationalityId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralNationalityMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
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
                ColumnName = "Nationality",
                ColumnCode = "Description",
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
