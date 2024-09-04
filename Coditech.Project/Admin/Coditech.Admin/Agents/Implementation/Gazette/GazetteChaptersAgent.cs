using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GazetteChaptersAgent : BaseAgent, IGazetteChaptersAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGazetteChaptersClient _gazetteChaptersClient;
        #endregion

        #region Public Constructor
        public GazetteChaptersAgent(ICoditechLogging coditechLogging, IGazetteChaptersClient gazetteChaptersClient)
        {
            _coditechLogging = coditechLogging;
            _gazetteChaptersClient = GetClient<IGazetteChaptersClient>(gazetteChaptersClient);
        }
        #endregion

        #region Public Methods
        public virtual GazetteChaptersListViewModel GetGazetteChaptersList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("ChapterNumber", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("ChapterName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("DistrictName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);               
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GazetteChaptersListResponse response = _gazetteChaptersClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GazetteChaptersListModel gazetteChaptersList = new GazetteChaptersListModel { GazetteChaptersList = response?.GazetteChaptersList };
            GazetteChaptersListViewModel listViewModel = new GazetteChaptersListViewModel();
            listViewModel.GazetteChaptersList = gazetteChaptersList?.GazetteChaptersList?.ToViewModel<GazetteChaptersViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GazetteChaptersList.Count, BindColumns());
            return listViewModel;
        }

        //Create Gazette Chapters.
        public virtual GazetteChaptersViewModel CreateGazetteChapters(GazetteChaptersViewModel gazetteChaptersViewModel)
        {
            try
            {
                GazetteChaptersResponse response = _gazetteChaptersClient.CreateGazetteChapters(gazetteChaptersViewModel.ToModel<GazetteChaptersModel>());
                GazetteChaptersModel gazetteChaptersModel = response?.GazetteChaptersModel;
                return IsNotNull(gazetteChaptersModel) ? gazetteChaptersModel.ToViewModel<GazetteChaptersViewModel>() : new GazetteChaptersViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GazetteChaptersViewModel)GetViewModelWithErrorMessage(gazetteChaptersViewModel, ex.ErrorMessage);
                    default:
                        return (GazetteChaptersViewModel)GetViewModelWithErrorMessage(gazetteChaptersViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return (GazetteChaptersViewModel)GetViewModelWithErrorMessage(gazetteChaptersViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get gazette Chapters by gazette Chapters id.
        public virtual GazetteChaptersViewModel GetGazetteChapters(int gazetteChaptersId)
        {
            GazetteChaptersResponse response = _gazetteChaptersClient.GetGazetteChapters(gazetteChaptersId);
            return response?.GazetteChaptersModel.ToViewModel<GazetteChaptersViewModel>();
        }

        //Update gazetteChapters.
        public virtual GazetteChaptersViewModel UpdateGazetteChapters(GazetteChaptersViewModel gazetteChaptersViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Info);
                GazetteChaptersResponse response = _gazetteChaptersClient.UpdateGazetteChapters(gazetteChaptersViewModel.ToModel<GazetteChaptersModel>());
                GazetteChaptersModel gazetteChaptersModel = response?.GazetteChaptersModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Info);
                return IsNotNull(gazetteChaptersModel) ? gazetteChaptersModel.ToViewModel<GazetteChaptersViewModel>() : (GazetteChaptersViewModel)GetViewModelWithErrorMessage(new GazetteChaptersViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
                return (GazetteChaptersViewModel)GetViewModelWithErrorMessage(gazetteChaptersViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete gazetteChapters.
        public virtual bool DeleteGazetteChapters(string gazetteChaptersId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _gazetteChaptersClient.DeleteGazetteChapters(new ParameterModel { Ids = gazetteChaptersId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGazetteChapters;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapter.ToString(), TraceLevel.Error);
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
                ColumnName = "Chapter Number",
                ColumnCode = "ChapterNumber",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Chapter Name",
                ColumnCode = "ChapterName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "District Name",
                ColumnCode = "DistrictName",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion       
    }
}
