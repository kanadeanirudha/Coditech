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
    public class GeneralEmailTemplateAgent : BaseAgent, IGeneralEmailTemplateAgent
    {

        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralEmailTemplateClient _generalEmailTemplateClient;
        #endregion

        #region Public Constructor
        public GeneralEmailTemplateAgent(ICoditechLogging coditechLogging, IGeneralEmailTemplateClient generalEmailTemplateClient)
        {
            _coditechLogging = coditechLogging;
            _generalEmailTemplateClient = GetClient<IGeneralEmailTemplateClient>(generalEmailTemplateClient);
        }
        #endregion

        #region Public Methods
        public virtual GeneralEmailTemplateListViewModel GetEmailTemplateList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = new FilterCollection();
            filters.Add(FilterKeys.IsEmailTemplate, ProcedureFilterOperators.Is, "1");
            return GetTemplateList(ref dataTableModel, filters);
        }
        public virtual GeneralEmailTemplateListViewModel GetSMSTemplateList(DataTableViewModel dataTableModel)
        {

            FilterCollection filters = new FilterCollection();
            filters.Add("IsSmsTemplate", ProcedureFilterOperators.Is, "1");
            return GetTemplateList(ref dataTableModel, filters);
        }

        public virtual GeneralEmailTemplateListViewModel GetWhatsAppTemplateList(DataTableViewModel dataTableModel)
        {

            FilterCollection filters = new FilterCollection();
            filters.Add("IsWhatsAppTemplate", ProcedureFilterOperators.Is, "1");
            return GetTemplateList(ref dataTableModel, filters);
        }

        //Create General Email.
        public virtual GeneralEmailTemplateViewModel CreateEmailTemplate(GeneralEmailTemplateViewModel generalEmailTemplateViewModel)
        {
            try
            {
                GeneralEmailTemplateResponse response = _generalEmailTemplateClient.CreateEmailTemplate(generalEmailTemplateViewModel.ToModel<GeneralEmailTemplateModel>());
                GeneralEmailTemplateModel generalEmailTemplateModel = response?.GeneralEmailTemplateModel;
                return IsNotNull(generalEmailTemplateModel) ? generalEmailTemplateModel.ToViewModel<GeneralEmailTemplateViewModel>() : new GeneralEmailTemplateViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralEmailTemplateViewModel)GetViewModelWithErrorMessage(generalEmailTemplateViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralEmailTemplateViewModel)GetViewModelWithErrorMessage(generalEmailTemplateViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return (GeneralEmailTemplateViewModel)GetViewModelWithErrorMessage(generalEmailTemplateViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general Email by general email template id.
        public virtual GeneralEmailTemplateViewModel GetEmailTemplate(short generalEmailTemplateId)
        {
            GeneralEmailTemplateResponse response = _generalEmailTemplateClient.GetEmailTemplate(generalEmailTemplateId);
            return response?.GeneralEmailTemplateModel.ToViewModel<GeneralEmailTemplateViewModel>();
        }

        //Update generalEmail.
        public virtual GeneralEmailTemplateViewModel UpdateEmailTemplate(GeneralEmailTemplateViewModel generalEmailTemplateViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Info);
                GeneralEmailTemplateResponse response = _generalEmailTemplateClient.UpdateEmailTemplate(generalEmailTemplateViewModel.ToModel<GeneralEmailTemplateModel>());
                GeneralEmailTemplateModel generalEmailTemplateModel = response?.GeneralEmailTemplateModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Info);
                return IsNotNull(generalEmailTemplateModel) ? generalEmailTemplateModel.ToViewModel<GeneralEmailTemplateViewModel>() : (GeneralEmailTemplateViewModel)GetViewModelWithErrorMessage(new GeneralEmailTemplateViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                return (GeneralEmailTemplateViewModel)GetViewModelWithErrorMessage(generalEmailTemplateViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete generalEmailTemplate.
        public virtual bool DeleteEmailTemplate(string generalEmailTemplateId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _generalEmailTemplateClient.DeleteEmailTemplate(new ParameterModel { Ids = generalEmailTemplateId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteGeneralEmailTemplate;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmailTemplate.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        #endregion

        #region protected

        protected virtual GeneralEmailTemplateListViewModel GetTemplateList(ref DataTableViewModel dataTableModel, FilterCollection filters)
        {
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters.Add("EmailTemplateName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("EmailTemplateCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? string.Empty : dataTableModel.SortByColumn, dataTableModel.SortBy);

            GeneralEmailTemplateListResponse response = _generalEmailTemplateClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            GeneralEmailTemplateListModel EmailTemplateList = new GeneralEmailTemplateListModel { GeneralEmailTemplateList = response?.GeneralEmailTemplateList };
            GeneralEmailTemplateListViewModel listViewModel = new GeneralEmailTemplateListViewModel();
            listViewModel.GeneralEmailTemplateList = EmailTemplateList?.GeneralEmailTemplateList?.ToViewModel<GeneralEmailTemplateViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.GeneralEmailTemplateList.Count, BindColumns());
            return listViewModel;
        }

        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Template Name",
                ColumnCode = "EmailTemplateName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Template Code",
                ColumnCode = "EmailTemplateCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Is Active",
                ColumnCode = "IsActive",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
