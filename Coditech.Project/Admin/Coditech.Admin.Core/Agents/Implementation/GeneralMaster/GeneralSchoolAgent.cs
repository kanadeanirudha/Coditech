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
    public class GeneralSchoolAgent : BaseAgent, IGeneralSchoolAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralSchoolClient _generalSchoolClient;
        #endregion

        #region Public Constructor
        public GeneralSchoolAgent(ICoditechLogging coditechLogging, IGeneralSchoolClient generalSchoolClient)
        {
            _coditechLogging = coditechLogging;
            _generalSchoolClient = GetClient<IGeneralSchoolClient>(generalSchoolClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralSchoolListViewModel GetSchoolList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("SchoolName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("SchoolCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CallingCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }
            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "SchoolName" : dataTableModel.SortByColumn, dataTableModel.SortBy);
            GeneralSchoolListResponse response = _generalSchoolClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralSchoolListModel schoolList = new GeneralSchoolListModel { GeneralSchoolList = response?.GeneralSchoolList };
            GeneralSchoolListViewModel listViewModel = new GeneralSchoolListViewModel();
            listViewModel.GeneralSchoolList = schoolList?.GeneralSchoolList?.ToViewModel<GeneralSchoolViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralSchoolList.Count, BindColumns());
            return listViewModel;
        }

        //Create General School.
        public virtual GeneralSchoolViewModel CreateSchool(GeneralSchoolViewModel generalSchoolViewModel)
        {
            try
            {
                GeneralSchoolResponse response = _generalSchoolClient.CreateSchool(generalSchoolViewModel.ToModel<GeneralSchoolModel>());
                GeneralSchoolModel generalSchoolModel = response?.GeneralSchoolModel;
                return IsNotNull(generalSchoolModel) ? generalSchoolModel.ToViewModel<GeneralSchoolViewModel>() : new GeneralSchoolViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general School by general School master id.
        public virtual GeneralSchoolViewModel GetSchool(short generalSchoolId)
        {
            GeneralSchoolResponse response = _generalSchoolClient.GetSchool(generalSchoolId);
            return response?.GeneralSchoolModel.ToViewModel<GeneralSchoolViewModel>();
        }

        //Update generalSchool.
        public virtual GeneralSchoolViewModel UpdateSchool(GeneralSchoolViewModel generalSchoolViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Info);
                GeneralSchoolResponse response = _generalSchoolClient.UpdateSchool(generalSchoolViewModel.ToModel<GeneralSchoolModel>());
                GeneralSchoolModel generalSchoolModel = response?.GeneralSchoolModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Info);
                return IsNotNull(generalSchoolModel) ? generalSchoolModel.ToViewModel<GeneralSchoolViewModel>() : (GeneralSchoolViewModel)GetViewModelWithErrorMessage(new GeneralSchoolViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return (GeneralSchoolViewModel)GetViewModelWithErrorMessage(generalSchoolViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalSchool.
        public virtual bool DeleteSchool(string generalSchoolId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalSchoolClient.DeleteSchool(new ParameterModel { Ids = generalSchoolId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralSchoolMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
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
                ColumnName = "School Name",
                ColumnCode = "SchoolName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "School Code",
                ColumnCode = "SchoolCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Country",
                ColumnCode = "CountryName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "State",
                ColumnCode = "RegionName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "City",
                ColumnCode = "CityName",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all School list from database 
        public virtual GeneralSchoolListResponse GetSchoolList()
        {
            GeneralSchoolListResponse schoolList = _generalSchoolClient.List(null, null, null, 1, int.MaxValue);
            return schoolList?.GeneralSchoolList?.Count > 0 ? schoolList : new GeneralSchoolListResponse();
        }
        #endregion
    }
}
