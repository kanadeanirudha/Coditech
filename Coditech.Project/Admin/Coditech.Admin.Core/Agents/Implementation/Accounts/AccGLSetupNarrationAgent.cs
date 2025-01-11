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
using static Coditech.Common.Helper.HelperUtility;
using System.Diagnostics;
using Coditech.API.Data;
namespace Coditech.Admin.Agents
{
    public class AccGLSetupNarrationAgent : BaseAgent, IAccGLSetupNarrationAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccGLSetupNarrationClient _accGLSetupNarrationClient;
        #endregion

        #region Public Constructor
        public AccGLSetupNarrationAgent(ICoditechLogging coditechLogging, IAccGLSetupNarrationClient accGLSetupNarrationClient)
        {
            _coditechLogging = coditechLogging;
            _accGLSetupNarrationClient = GetClient<IAccGLSetupNarrationClient>(accGLSetupNarrationClient);
        }
        #endregion

        #region Public Methods

        public virtual AccGLSetupNarrationListViewModel GetNarrationList(DataTableViewModel dataTableModel)
        {
            AccGLSetupNarrationListResponse response = _accGLSetupNarrationClient.List(dataTableModel.SelectedCentreCode);
            AccGLSetupNarrationListModel accGLSetupNarrationList = new AccGLSetupNarrationListModel { AccGLSetupNarrationList = response?.AccGLSetupNarrationList };
            AccGLSetupNarrationListViewModel listViewModel = new AccGLSetupNarrationListViewModel();
            listViewModel.AccGLSetupNarrationList = response?.AccGLSetupNarrationList?.ToViewModel<AccGLSetupNarrationViewModel>().ToList();
            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.AccGLSetupNarrationList.Count, BindColumns());
            return listViewModel;
        }


        //Create AccGLSetupNarration.
        public virtual AccGLSetupNarrationViewModel CreateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {

            try
            {
                AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.CreateNarration(accGLSetupNarrationViewModel.ToModel<AccGLSetupNarrationModel>());
                AccGLSetupNarrationModel accGLSetupNarrationModel = response?.AccGLSetupNarrationModel;
                return IsNotNull(accGLSetupNarrationModel) ? accGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>() : new AccGLSetupNarrationViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, ex.ErrorMessage);
                    default:
                        return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get AccGLSetupNarration by Narration id.
        public virtual AccGLSetupNarrationViewModel GetNarration(int accGLSetupNarrationId)
        {
            AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.GetNarration(accGLSetupNarrationId);
            return response?.AccGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>();
        }

        //Update AccGLSetupNarration.
        public virtual AccGLSetupNarrationViewModel UpdateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Info);
                AccGLSetupNarrationResponse response = _accGLSetupNarrationClient.UpdateNarration(accGLSetupNarrationViewModel.ToModel<AccGLSetupNarrationModel>());
                AccGLSetupNarrationModel accGLSetupNarrationModel = response?.AccGLSetupNarrationModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Info);
                return IsNotNull(accGLSetupNarrationViewModel) ? accGLSetupNarrationModel.ToViewModel<AccGLSetupNarrationViewModel>() : (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(new AccGLSetupNarrationViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccGLSetupNarration.ToString(), TraceLevel.Error);
                return (AccGLSetupNarrationViewModel)GetViewModelWithErrorMessage(accGLSetupNarrationViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Narration ID",
            //    ColumnCode = "AccGLSetupNarrationId",
            //    IsSortable = true,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{    
            //    ColumnName = "Narration ID",
            //    ColumnCode = "AccGLSetupNarrationId",
            //    IsSortable = true,
            //});
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Narration Type",
                ColumnCode = "NarrationType",
                IsSortable = true,
            });
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "Narration Description",
            //    ColumnCode = "NarrationDescription",
            //    IsSortable = false,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "IsActive",
            //    ColumnCode = "IsActive",
            //    IsSortable = true,
            //});
            //datatableColumnList.Add(new DatatableColumns()
            //{
            //    ColumnName = "IsSystemGenerated",
            //    ColumnCode = "IsSystemGenerated",
            //    IsSortable = true,
            //});
            return datatableColumnList;
        }
       #endregion
    }
}
