﻿using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Model;
using Coditech.Resources;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class OrganisationCentreAgent : BaseAgent, IOrganisationCentreAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IOrganisationCentreClient _organisationCentreClient;
        #endregion

        #region Public Constructor
        public OrganisationCentreAgent(ICoditechLogging coditechLogging, IOrganisationCentreClient organisationCentreClient)
        {
            _coditechLogging = coditechLogging;
            _organisationCentreClient = GetClient<IOrganisationCentreClient>(organisationCentreClient);
        }
        #endregion

        #region Public Methods
        public virtual OrganisationCentreListViewModel GetOrganisationCentreList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("CentreName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("CentreCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "CentreName" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            OrganisationCentreListResponse response = _organisationCentreClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            OrganisationCentreListModel organisationCentreList = new OrganisationCentreListModel { OrganisationCentreList = response?.OrganisationCentreList };
            OrganisationCentreListViewModel listViewModel = new OrganisationCentreListViewModel();
            listViewModel.OrganisationCentreList = organisationCentreList?.OrganisationCentreList?.ToViewModel<OrganisationCentreViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.OrganisationCentreList.Count, BindColumns());
            return listViewModel;
        }

        //Create Organisation Centre.
        public virtual OrganisationCentreViewModel CreateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel)
        {
            try
            {
                OrganisationCentreResponse response = _organisationCentreClient.CreateOrganisationCentre(organisationCentreViewModel.ToModel<OrganisationCentreModel>());
                OrganisationCentreModel organisationCentreModel = response?.OrganisationCentreModel;
                return IsNotNull(organisationCentreModel) ? organisationCentreModel.ToViewModel<OrganisationCentreViewModel>() : new OrganisationCentreViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, ex.ErrorMessage);
                    default:
                        return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get Organisation Centre by organisationCentreId.
        public virtual OrganisationCentreViewModel GetOrganisationCentre(short organisationCentreId)
        {
            OrganisationCentreResponse response = _organisationCentreClient.GetOrganisationCentre(organisationCentreId);
            return response?.OrganisationCentreModel.ToViewModel<OrganisationCentreViewModel>();
        }

        //Update Organisation Centre.
        public virtual OrganisationCentreViewModel UpdateOrganisationCentre(OrganisationCentreViewModel organisationCentreViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                OrganisationCentreResponse response = _organisationCentreClient.UpdateOrganisationCentre(organisationCentreViewModel.ToModel<OrganisationCentreModel>());
                OrganisationCentreModel organisationCentreModel = response?.OrganisationCentreModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                return IsNotNull(organisationCentreModel) ? organisationCentreModel.ToViewModel<OrganisationCentreViewModel>() : (OrganisationCentreViewModel)GetViewModelWithErrorMessage(new OrganisationCentreViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                return (OrganisationCentreViewModel)GetViewModelWithErrorMessage(organisationCentreViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete Organisation Centre.
        public virtual bool DeleteOrganisationCentre(string organisationCentreId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _organisationCentreClient.DeleteOrganisationCentre(new ParameterModel { Ids = organisationCentreId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteOrganisationCentreMaster;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentre.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }

        //Get Organisation Centre Printing Format by organisationCentreId.
        public virtual OrganisationCentrePrintingFormatViewModel GetPrintingFormat(short organisationCentreId)
        {
            OrganisationCentrePrintingFormatResponse response = _organisationCentreClient.GetPrintingFormat(organisationCentreId);
            return response?.OrganisationCentrePrintingFormatModel.ToViewModel<OrganisationCentrePrintingFormatViewModel>();
        }

        //Update Organisation Centre Printing Format.
        public virtual OrganisationCentrePrintingFormatViewModel UpdatePrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Info);
                OrganisationCentrePrintingFormatResponse response = _organisationCentreClient.UpdatePrintingFormat(organisationCentrePrintingFormatViewModel.ToModel<OrganisationCentrePrintingFormatModel>());
                OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = response?.OrganisationCentrePrintingFormatModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Info);
                return IsNotNull(organisationCentrePrintingFormatModel) ? organisationCentrePrintingFormatModel.ToViewModel<OrganisationCentrePrintingFormatViewModel>() : (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(new OrganisationCentrePrintingFormatViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PrintingFormat.ToString(), TraceLevel.Error);
                return (OrganisationCentrePrintingFormatViewModel)GetViewModelWithErrorMessage(organisationCentrePrintingFormatViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Get Organisation Centrewise GST Credential by organisationCentreId.
        public virtual OrganisationCentrewiseGSTCredentialViewModel GetCentrewiseGSTSetup(short organisationCentreId)
        {
            OrganisationCentrewiseGSTCredentialResponse response = _organisationCentreClient.GetCentrewiseGSTSetup(organisationCentreId);
            return response?.OrganisationCentrewiseGSTCredentialModel.ToViewModel<OrganisationCentrewiseGSTCredentialViewModel>();
        }

        //Update Organisation Centrewise GST Credential.
        public virtual OrganisationCentrewiseGSTCredentialViewModel UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialViewModel organisationCentrewiseGSTCredentialViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Info);
                OrganisationCentrewiseGSTCredentialResponse response = _organisationCentreClient.UpdateCentrewiseGSTSetup(organisationCentrewiseGSTCredentialViewModel.ToModel<OrganisationCentrewiseGSTCredentialModel>());
                OrganisationCentrewiseGSTCredentialModel organisationCentrewiseGSTCredentialModel = response?.OrganisationCentrewiseGSTCredentialModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Info);
                return IsNotNull(organisationCentrewiseGSTCredentialModel) ? organisationCentrewiseGSTCredentialModel.ToViewModel<OrganisationCentrewiseGSTCredentialViewModel>() : (OrganisationCentrewiseGSTCredentialViewModel)GetViewModelWithErrorMessage(new OrganisationCentrewiseGSTCredentialViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CentrewiseGST.ToString(), TraceLevel.Error);
                return (OrganisationCentrewiseGSTCredentialViewModel)GetViewModelWithErrorMessage(organisationCentrewiseGSTCredentialViewModel, GeneralResources.UpdateErrorMessage);
            }
        }
        #endregion

        #region protected
        protected virtual List<DatatableColumns> BindColumns()
        {
            List<DatatableColumns> datatableColumnList = new List<DatatableColumns>();
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Centre Name",
                ColumnCode = "CentreName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Centre Code",
                ColumnCode = "CentreCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Office Type",
                ColumnCode = "HoCoRoScFlag",
            });
            return datatableColumnList;
        }
        #endregion
    }
}
