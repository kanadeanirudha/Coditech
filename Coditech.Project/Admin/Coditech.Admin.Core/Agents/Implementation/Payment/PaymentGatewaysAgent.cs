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
    public class PaymentGatewaysAgent : BaseAgent, IPaymentGatewaysAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IPaymentGatewaysClient _paymentGatewaysClient;
        #endregion

        #region Public Constructor
        public PaymentGatewaysAgent(ICoditechLogging coditechLogging, IPaymentGatewaysClient paymentGatewaysClient)
        {
            _coditechLogging = coditechLogging;
            _paymentGatewaysClient = GetClient<IPaymentGatewaysClient>(paymentGatewaysClient);
        }
        #endregion

        #region Public Methods
        public virtual PaymentGatewaysListViewModel GetPaymentGatewaysList(DataTableViewModel dataTableModel)
        {
            FilterCollection filters = null;
            dataTableModel = dataTableModel ?? new DataTableViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SearchBy))
            {
                filters = new FilterCollection();
                filters.Add("PaymentName", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
                filters.Add("PaymentCode", ProcedureFilterOperators.Like, dataTableModel.SearchBy);
            }

            SortCollection sortlist = SortingData(dataTableModel.SortByColumn = string.IsNullOrEmpty(dataTableModel.SortByColumn) ? "" : dataTableModel.SortByColumn, dataTableModel.SortBy);

            PaymentGatewaysListResponse response = _paymentGatewaysClient.List(null, filters, sortlist, dataTableModel.PageIndex, dataTableModel.PageSize);
            PaymentGatewaysListModel paymentGatewaysList = new PaymentGatewaysListModel { PaymentGatewaysList = response?.PaymentGatewaysList };
            PaymentGatewaysListViewModel listViewModel = new PaymentGatewaysListViewModel();
            listViewModel.PaymentGatewaysList = paymentGatewaysList?.PaymentGatewaysList?.ToViewModel<PaymentGatewaysViewModel>().ToList();

            SetListPagingData(listViewModel.PageListViewModel, response, dataTableModel, listViewModel.PaymentGatewaysList.Count, BindColumns());
            return listViewModel;
        }
        //Create Payment Gateways
        public virtual PaymentGatewaysViewModel CreatePaymentGateways(PaymentGatewaysViewModel paymentGatewaysViewModel)
        {
            try
            {
                PaymentGatewaysResponse response = _paymentGatewaysClient.CreatePaymentGateways(paymentGatewaysViewModel.ToModel<PaymentGatewaysModel>());
                PaymentGatewaysModel paymentGatewaysModel = response?.PaymentGatewaysModel;
                return IsNotNull(paymentGatewaysModel) ? paymentGatewaysModel.ToViewModel<PaymentGatewaysViewModel>() : new PaymentGatewaysViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, ex.ErrorMessage);
                    default:
                        return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }
        //Get Payment Gateways by Payment Gateways id.
        public virtual PaymentGatewaysViewModel GetPaymentGateways(byte paymentGatewayId)
        {
            PaymentGatewaysResponse response = _paymentGatewaysClient.GetPaymentGateways(paymentGatewayId);
            return response?.PaymentGatewaysModel.ToViewModel<PaymentGatewaysViewModel>();
        }
        //Update Payment Gateways.
        public virtual PaymentGatewaysViewModel UpdatePaymentGateways(PaymentGatewaysViewModel paymentGatewaysViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Info);
                PaymentGatewaysResponse response = _paymentGatewaysClient.UpdatePaymentGateways(paymentGatewaysViewModel.ToModel<PaymentGatewaysModel>());
                PaymentGatewaysModel paymentGatewaysModel = response?.PaymentGatewaysModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Info);
                return IsNotNull(paymentGatewaysModel) ? paymentGatewaysModel.ToViewModel<PaymentGatewaysViewModel>() : (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(new PaymentGatewaysViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, ex.ErrorMessage);
                    default:
                        return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
                return (PaymentGatewaysViewModel)GetViewModelWithErrorMessage(paymentGatewaysViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete  paymentGateways..
        public virtual bool DeletePaymentGateways(string paymentGatewayId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _paymentGatewaysClient.DeletePaymentGateways(new ParameterModel { Ids = paymentGatewayId });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeletePaymentGateways;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGateways.ToString(), TraceLevel.Error);
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
                ColumnName = "Payment Name",
                ColumnCode = "PaymentName",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "Payment Code",
                ColumnCode = "PaymentCode",
                IsSortable = true,
            });
            datatableColumnList.Add(new DatatableColumns()
            {
                ColumnName = "IsActive",
                ColumnCode = "IsActive",
                IsSortable = true,
            });
            
            return datatableColumnList;
        }
        #endregion
        #region
        // it will return get all PaymentGateways list from database 
        public virtual PaymentGatewaysListResponse GetPaymentGatewaysList()
        {
            PaymentGatewaysListResponse paymentGatewaysList = _paymentGatewaysClient.List(null, null, null, 1, int.MaxValue);
            return paymentGatewaysList?.PaymentGatewaysList?.Count > 0 ? paymentGatewaysList : new PaymentGatewaysListResponse();
        }
        #endregion
    }
}
