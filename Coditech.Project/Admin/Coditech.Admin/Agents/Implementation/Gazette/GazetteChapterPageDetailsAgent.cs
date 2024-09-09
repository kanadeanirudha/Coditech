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
    public class GazetteChaptersPageDetailAgent : BaseAgent, IGazetteChaptersPageDetailAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGazetteChaptersPageDetailClient _gazetteChaptersPageDetailClient;
        #endregion

        #region Public Constructor
        public GazetteChaptersPageDetailAgent(ICoditechLogging coditechLogging, IGazetteChaptersPageDetailClient gazetteChaptersPageDetailClient)
        {
            _coditechLogging = coditechLogging;
            _gazetteChaptersPageDetailClient = GetClient<IGazetteChaptersPageDetailClient>(gazetteChaptersPageDetailClient);
        }
        #endregion

        #region Public Methods
        public virtual GazetteChaptersPageDetailListViewModel GetGazetteChaptersPageDetailList(DataTableViewModel dataTableModel)
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

            GazetteChaptersPageDetailListResponse response = _gazetteChaptersPageDetailClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GazetteChaptersPageDetailListModel gazetteChaptersPageDetailList = new GazetteChaptersPageDetailListModel { GazetteChaptersPageDetailList = response?.GazetteChaptersPageDetailList };
            GazetteChaptersPageDetailListViewModel listViewModel = new GazetteChaptersPageDetailListViewModel();
            listViewModel.GazetteChaptersPageDetailList = gazetteChaptersPageDetailList?.GazetteChaptersPageDetailList?.ToViewModel<GazetteChaptersPageDetailViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GazetteChaptersPageDetailList.Count, BindColumns());
            return listViewModel;
        }

        //Create Gazette Chapters Page Detail.
        public virtual GazetteChaptersPageDetailViewModel CreateGazetteChaptersPageDetail(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel)
        {
            try
            {
                GazetteChaptersPageDetailResponse response = _gazetteChaptersPageDetailClient.CreateGazetteChaptersPageDetail(gazetteChaptersPageDetailViewModel.ToModel<GazetteChaptersPageDetailModel>());
                GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel = response?.GazetteChaptersPageDetailModel;
                return IsNotNull(gazetteChaptersPageDetailModel) ? gazetteChaptersPageDetailModel.ToViewModel<GazetteChaptersPageDetailViewModel>() : new GazetteChaptersPageDetailViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GazetteChaptersPageDetailViewModel)GetViewModelWithErrorMessage(gazetteChaptersPageDetailViewModel, ex.ErrorMessage);
                    default:
                        return (GazetteChaptersPageDetailViewModel)GetViewModelWithErrorMessage(gazetteChaptersPageDetailViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return (GazetteChaptersPageDetailViewModel)GetViewModelWithErrorMessage(gazetteChaptersPageDetailViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get gazette Chapters by gazette Chapters Page Detail id.
        public virtual GazetteChaptersPageDetailViewModel GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId)
        {
            GazetteChaptersPageDetailResponse response = _gazetteChaptersPageDetailClient.GetGazetteChaptersPageDetail(gazetteChaptersPageDetailId);
            return response?.GazetteChaptersPageDetailModel.ToViewModel<GazetteChaptersPageDetailViewModel>();
        }

        //Update gazetteChaptersPageDetail.
        public virtual GazetteChaptersPageDetailViewModel UpdateGazetteChaptersPageDetail(GazetteChaptersPageDetailViewModel gazetteChaptersPageDetailViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Info);
                GazetteChaptersPageDetailResponse response = _gazetteChaptersPageDetailClient.UpdateGazetteChaptersPageDetail(gazetteChaptersPageDetailViewModel.ToModel<GazetteChaptersPageDetailModel>());
                GazetteChaptersPageDetailModel gazetteChaptersPageDetailModel = response?.GazetteChaptersPageDetailModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Info);
                return IsNotNull(gazetteChaptersPageDetailModel) ? gazetteChaptersPageDetailModel.ToViewModel<GazetteChaptersPageDetailViewModel>() : (GazetteChaptersPageDetailViewModel)GetViewModelWithErrorMessage(new GazetteChaptersPageDetailViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
                return (GazetteChaptersPageDetailViewModel)GetViewModelWithErrorMessage(gazetteChaptersPageDetailViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete gazetteChaptersPageDetail.
        public virtual bool DeleteGazetteChaptersPageDetail(string gazetteChaptersPageDetailId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _gazetteChaptersPageDetailClient.DeleteGazetteChaptersPageDetail(new ParameterModel { Ids = gazetteChaptersPageDetailId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGazetteChaptersPageDetail;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GazetteChapterPageDetail.ToString(), TraceLevel.Error);
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
