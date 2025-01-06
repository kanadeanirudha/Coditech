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
    public class PaymentGatewayDetailsAgent : BaseAgent, IPaymentGatewayDetailsAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IPaymentGatewayDetailsClient _paymentGatewayDetailsClient;
        #endregion

        #region Public Constructor
        public PaymentGatewayDetailsAgent(ICoditechLogging coditechLogging, IPaymentGatewayDetailsClient paymentGatewayDetailsClient, IUserClient userClient)
        {
            _coditechLogging = coditechLogging;
            _paymentGatewayDetailsClient = GetClient<IPaymentGatewayDetailsClient>(paymentGatewayDetailsClient);
        }
        #endregion

        #region Public Methods
        #region PaymentGatewayDetails
        public virtual PaymentGatewayDetailsListViewModel GetPaymentGatewayDetailsList(DataTableViewModel dataTableModel, byte paymentGatewayId)
        {
            PaymentGatewayDetailsListResponse response = _paymentGatewayDetailsClient.List(dataTableModel.SelectedCentreCode, paymentGatewayId);
            PaymentGatewayDetailsListModel paymentGatewayDetailsList = new PaymentGatewayDetailsListModel { PaymentGatewayDetailsList = response?.PaymentGatewayDetailsList };
            PaymentGatewayDetailsListViewModel listViewModel = new PaymentGatewayDetailsListViewModel();
            listViewModel.PaymentGatewayDetailsList = response?.PaymentGatewayDetailsList?.ToViewModel<PaymentGatewayDetailsViewModel>().ToList();

            return listViewModel;
        }

        //Create PaymentGatewayDetails.
        public virtual PaymentGatewayDetailsViewModel CreatePaymentGatewayDetails(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel)
        {
            try
            {
                PaymentGatewayDetailsResponse response = _paymentGatewayDetailsClient.CreatePaymentGatewayDetails(paymentGatewayDetailsViewModel.ToModel<PaymentGatewayDetailsModel>());
                PaymentGatewayDetailsModel paymentGatewayDetailsModel = response?.PaymentGatewayDetailsModel;
                return IsNotNull(paymentGatewayDetailsModel) ? paymentGatewayDetailsModel.ToViewModel<PaymentGatewayDetailsViewModel>() : new PaymentGatewayDetailsViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (PaymentGatewayDetailsViewModel)GetViewModelWithErrorMessage(paymentGatewayDetailsViewModel, ex.ErrorMessage);
                    default:
                        return (PaymentGatewayDetailsViewModel)GetViewModelWithErrorMessage(paymentGatewayDetailsViewModel, GeneralResources.ErrorFailedToCreate);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return (PaymentGatewayDetailsViewModel)GetViewModelWithErrorMessage(paymentGatewayDetailsViewModel, GeneralResources.ErrorFailedToCreate);
            }
        }

        //Get PaymentGatewayDetails by  PaymentGatewayDetails id.
        public virtual PaymentGatewayDetailsViewModel GetPaymentGatewayDetails(int paymentGatewayDetailId)
        {
            PaymentGatewayDetailsResponse response = _paymentGatewayDetailsClient.GetPaymentGatewayDetails(paymentGatewayDetailId);
            return response?.PaymentGatewayDetailsModel.ToViewModel<PaymentGatewayDetailsViewModel>();
        }

        //Update PaymentGatewayDetails.
        public virtual PaymentGatewayDetailsViewModel UpdatePaymentGatewayDetails(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel)
        {
            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Info);
                PaymentGatewayDetailsResponse response = _paymentGatewayDetailsClient.UpdatePaymentGatewayDetails(paymentGatewayDetailsViewModel.ToModel<PaymentGatewayDetailsModel>());
                PaymentGatewayDetailsModel paymentGatewayDetailsModel = response?.PaymentGatewayDetailsModel;
                _coditechLogging.LogMessage("Agent method execution done.", CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Info);
                return HelperUtility.IsNotNull(paymentGatewayDetailsModel) ? paymentGatewayDetailsModel.ToViewModel<PaymentGatewayDetailsViewModel>() : (PaymentGatewayDetailsViewModel)GetViewModelWithErrorMessage(new PaymentGatewayDetailsViewModel(), GeneralResources.UpdateErrorMessage);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                return (PaymentGatewayDetailsViewModel)GetViewModelWithErrorMessage(paymentGatewayDetailsViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Delete PaymentGatewayDetails.
        public virtual bool DeletePaymentGatewayDetails(string paymentGatewayDetailsIds, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _paymentGatewayDetailsClient.DeletePaymentGatewayDetails(new ParameterModel { Ids = paymentGatewayDetailsIds });
                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AssociationDeleteError:
                        errorMessage = AdminResources.ErrorDeletePaymentGatewayDetails;
                        return false;
                    default:
                        errorMessage = GeneralResources.ErrorFailedToDelete;
                        return false;
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PaymentGatewayDetails.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion
        #endregion

    }
}
