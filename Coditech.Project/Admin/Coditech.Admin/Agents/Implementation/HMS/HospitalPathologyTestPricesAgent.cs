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

namespace Coditech.Admin.Agents
{
    public class HospitalPathologyTestPricesAgent : BaseAgent, IHospitalPathologyTestPricesAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IHospitalPathologyTestPricesClient _hospitalPathologyTestPricesClient;
        #endregion

        #region Public Constructor
        public HospitalPathologyTestPricesAgent(ICoditechLogging coditechLogging, IHospitalPathologyTestPricesClient hospitalPathologyTestPricesClient)
        {
            _coditechLogging = coditechLogging;
            _hospitalPathologyTestPricesClient = GetClient<IHospitalPathologyTestPricesClient>(hospitalPathologyTestPricesClient);
        }
        #endregion

        #region Public Methods
        public virtual HospitalPathologyTestPricesListViewModel GetHospitalPathologyTestPricesList(int hospitalPathologyPriceCategoryEnumId,DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PathologyTestName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            HospitalPathologyTestPricesListResponse response = _hospitalPathologyTestPricesClient.List(hospitalPathologyPriceCategoryEnumId,null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            HospitalPathologyTestPricesListModel hospitalPathologyTestPricesList = new HospitalPathologyTestPricesListModel { HospitalPathologyTestPricesList = response?.HospitalPathologyTestPricesList };
            HospitalPathologyTestPricesListViewModel listViewModel = new HospitalPathologyTestPricesListViewModel();
            listViewModel.HospitalPathologyTestPricesList = hospitalPathologyTestPricesList?.HospitalPathologyTestPricesList?.ToViewModel<HospitalPathologyTestPricesViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.HospitalPathologyTestPricesList.Count, BindColumns());
            return listViewModel;
        }

        //Create HospitalPathologyTestPrices.
        public virtual HospitalPathologyTestPricesViewModel CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel)
        {
            try
            {
                HospitalPathologyTestPricesResponse response = _hospitalPathologyTestPricesClient.CreateHospitalPathologyTestPrices(hospitalPathologyTestPricesViewModel.ToModel<HospitalPathologyTestPricesModel>());
                HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel = response?.HospitalPathologyTestPricesModel;
                return HelperUtility.IsNotNull(hospitalPathologyTestPricesModel) ? hospitalPathologyTestPricesModel.ToViewModel<HospitalPathologyTestPricesViewModel>() : new HospitalPathologyTestPricesViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (HospitalPathologyTestPricesViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestPricesViewModel, ex.ErrorMessage);
                    default:
                        return (HospitalPathologyTestPricesViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestPricesViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestPricesViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestPricesViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get general HospitalPathologyTestPrices by hospital Pathology Test Prices Id.
        public virtual HospitalPathologyTestPricesViewModel GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId)
        {
            HospitalPathologyTestPricesResponse response = _hospitalPathologyTestPricesClient.GetHospitalPathologyTestPrices(hospitalPathologyTestPricesId);
            return response?.HospitalPathologyTestPricesModel.ToViewModel<HospitalPathologyTestPricesViewModel>();
        }

        //Update  HospitalPathologyTestPrices.
        public virtual HospitalPathologyTestPricesViewModel UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Info);
                HospitalPathologyTestPricesResponse response = _hospitalPathologyTestPricesClient.UpdateHospitalPathologyTestPrices(hospitalPathologyTestPricesViewModel.ToModel<HospitalPathologyTestPricesModel>());
                HospitalPathologyTestPricesModel hospitalPathologyTestPricesModel = response?.HospitalPathologyTestPricesModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(hospitalPathologyTestPricesModel) ? hospitalPathologyTestPricesModel.ToViewModel<HospitalPathologyTestPricesViewModel>() : (HospitalPathologyTestPricesViewModel)GetViewModelWithErrorMessage(new HospitalPathologyTestPricesViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
                return (HospitalPathologyTestPricesViewModel)GetViewModelWithErrorMessage(hospitalPathologyTestPricesViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete HospitalPathologyTestPrices.
        public virtual bool DeleteHospitalPathologyTestPrices(string hospitalPathologyTestPricesIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _hospitalPathologyTestPricesClient.DeleteHospitalPathologyTestPrices(new ParameterModel { Ids = hospitalPathologyTestPricesIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeleteHospitalPathologyTestPrices;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalPathologyTestPrices.ToString(), TraceLevel.Error);
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
                ColumnName = "Pathology Test Name",
                ColumnCode = "PathologyTestName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Hospital Pathology Price Category",
                ColumnCode = "HospitalPathologyPriceCategory",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Price",
                ColumnCode = "Price",
                IsSortable = true,
            });
            return datatableColumnList;
        }
        #endregion
    }
}

