using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class PaymentGatewayDetailsService : IPaymentGatewayDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<PaymentGatewayDetails> _paymentGatewayDetailsRepository;
        private readonly ICoditechRepository<PaymentGateways> _paymentGatewaysRepository;

        public PaymentGatewayDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _paymentGatewayDetailsRepository = new CoditechRepository<PaymentGatewayDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _paymentGatewaysRepository = new CoditechRepository<PaymentGateways>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual PaymentGatewayDetailsListModel GetPaymentGatewayDetailsList(string selectedCentreCode, byte paymentGatewayId)
        {
            PaymentGatewayDetailsListModel listModel = new PaymentGatewayDetailsListModel();

            List<PaymentGatewayDetails> list = _paymentGatewayDetailsRepository.Table.Where(x => x.CentreCode == selectedCentreCode && x.PaymentGatewayId == paymentGatewayId)?.ToList();
            listModel.PaymentGatewayDetailsList = new List<PaymentGatewayDetailsModel>();
            PaymentGateways paymentGateways = _paymentGatewaysRepository.Table.Where(x => x.IsActive && x.PaymentGatewayId == paymentGatewayId)?.FirstOrDefault();
            listModel.PaymentGatewayDetailsList.Add(new PaymentGatewayDetailsModel()
            {
                Mode = "Test Mode",
                IsLiveMode = false,
                PaymentGatewayDetailId = list?.Count() > 0 ? Convert.ToByte(list.FirstOrDefault(x => !x.IsLiveMode)?.PaymentGatewayDetailId) : (byte)0,
                PaymentCode = paymentGateways?.PaymentCode
            });
            listModel.PaymentGatewayDetailsList.Add(new PaymentGatewayDetailsModel()
            {
                Mode = "Live Mode",
                IsLiveMode = true,
                PaymentGatewayDetailId = list?.Count() > 0 ? Convert.ToByte(list.FirstOrDefault(x => x.IsLiveMode)?.PaymentGatewayDetailId) : (byte)0,
                PaymentCode = paymentGateways?.PaymentCode
            });
            return listModel;
        }
        //Create PaymentGatewayDetails.
        public virtual PaymentGatewayDetailsModel CreatePaymentGatewayDetails(PaymentGatewayDetailsModel paymentGatewayDetailsModel)
        {
            paymentGatewayDetailsModel.CentreCode = paymentGatewayDetailsModel.CentreCode;
            paymentGatewayDetailsModel.PaymentGatewayId = paymentGatewayDetailsModel.PaymentGatewayId;
            paymentGatewayDetailsModel.IsLiveMode = paymentGatewayDetailsModel.IsLiveMode;

            if (IsNull(paymentGatewayDetailsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            PaymentGatewayDetails paymentGatewayDetails = paymentGatewayDetailsModel.FromModelToEntity<PaymentGatewayDetails>();

            //Create new PaymentGatewayDetails and return it.
            PaymentGatewayDetails paymentGatewayDetailsData = _paymentGatewayDetailsRepository.Insert(paymentGatewayDetails);
            if (paymentGatewayDetailsData?.PaymentGatewayDetailId > 0)
            {
                paymentGatewayDetailsModel.PaymentGatewayDetailId = paymentGatewayDetailsData.PaymentGatewayDetailId;
            }
            else
            {
                paymentGatewayDetailsModel.HasError = true;
                paymentGatewayDetailsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return paymentGatewayDetailsModel;
        }
        //Get PaymentGatewayDetails by PaymentGatewayDetails id.
        public virtual PaymentGatewayDetailsModel GetPaymentGatewayDetails(int paymentGatewayDetailId)
        {
            if (paymentGatewayDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayDetailId"));

            PaymentGatewayDetails paymentGatewayDetails = _paymentGatewayDetailsRepository.Table.Where(x => x.PaymentGatewayDetailId == paymentGatewayDetailId)?.FirstOrDefault();
            PaymentGatewayDetailsModel paymentGatewayDetailsModel = paymentGatewayDetails?.FromEntityToModel<PaymentGatewayDetailsModel>();
            paymentGatewayDetailsModel.PaymentCode = _paymentGatewaysRepository.Table.Where(x => x.IsActive && x.PaymentGatewayId == paymentGatewayDetailsModel.PaymentGatewayId)?.Select(x => x.PaymentCode)?.FirstOrDefault();
            return paymentGatewayDetailsModel;
        }

        //Update PaymentGatewayDetails.
        public virtual bool UpdatePaymentGatewayDetails(PaymentGatewayDetailsModel paymentGatewayDetailsModel)
        {
            if (IsNull(paymentGatewayDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (paymentGatewayDetailsModel.PaymentGatewayDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayDetailId"));

            PaymentGatewayDetails paymentGatewayDetails = paymentGatewayDetailsModel.FromModelToEntity<PaymentGatewayDetails>();

            bool isPaymentGatewayDetailsUpdated = _paymentGatewayDetailsRepository.Update(paymentGatewayDetails);
            if (!isPaymentGatewayDetailsUpdated)
            {
                paymentGatewayDetailsModel.HasError = true;
                paymentGatewayDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPaymentGatewayDetailsUpdated;
        }
        //Delete PaymentGatewayDetails.
        public virtual bool DeletePaymentGatewayDetails(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayDetailID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("PaymentGatewayDetailId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeletePaymentGatewayDetails @PaymentGatewayDetailId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

    }
}
