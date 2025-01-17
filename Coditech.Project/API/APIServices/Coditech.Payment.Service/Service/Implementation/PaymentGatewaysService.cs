using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class PaymentGatewaysService : IPaymentGatewaysService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<PaymentGateways> _paymentGatewaysRepository;
        public PaymentGatewaysService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _paymentGatewaysRepository = new CoditechRepository<PaymentGateways>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual PaymentGatewaysListModel GetPaymentGatewaysList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<PaymentGatewaysModel> objStoredProc = new CoditechViewRepository<PaymentGatewaysModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<PaymentGatewaysModel> PaymentGatewaysList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetPaymentGatewaysList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            PaymentGatewaysListModel listModel = new PaymentGatewaysListModel();

            listModel.PaymentGatewaysList = PaymentGatewaysList?.Count > 0 ? PaymentGatewaysList : new List<PaymentGatewaysModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create PaymentGateways.
        public virtual PaymentGatewaysModel CreatePaymentGateways(PaymentGatewaysModel paymentGatewaysModel)
        {
            if (IsNull(paymentGatewaysModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsPaymentCodeAlreadyExist(paymentGatewaysModel.PaymentCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Payment Code"));

            PaymentGateways paymentGateways = paymentGatewaysModel.FromModelToEntity<PaymentGateways>();

            //Create new PaymentGateway and return it.
            PaymentGateways paymentGatewaysData = _paymentGatewaysRepository.Insert(paymentGateways);
            if (paymentGatewaysData?.PaymentGatewayId > 0)
            {
                paymentGatewaysModel.PaymentGatewayId = paymentGatewaysData.PaymentGatewayId;
            }
            else
            {
                paymentGatewaysModel.HasError = true;
                paymentGatewaysModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return paymentGatewaysModel;
        }
        //Get PaymentGateways by PaymentGateway id.
        public virtual PaymentGatewaysModel GetPaymentGateways(byte paymentGatewayId)
        {
            if (paymentGatewayId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayID"));

            //Get the Country Details based on id.
            PaymentGateways paymentGateways = _paymentGatewaysRepository.Table.FirstOrDefault(x => x.PaymentGatewayId == paymentGatewayId);
            PaymentGatewaysModel paymentGatewaysModel = paymentGateways?.FromEntityToModel<PaymentGatewaysModel>();
            return paymentGatewaysModel;
        }
        //Update paymentGateways.
        public virtual bool UpdatePaymentGateways(PaymentGatewaysModel paymentGatewaysModel)
        {
            if (IsNull(paymentGatewaysModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (paymentGatewaysModel.PaymentGatewayId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayID"));

            if (IsPaymentCodeAlreadyExist(paymentGatewaysModel.PaymentCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Payment Code"));

            PaymentGateways paymentGateways = paymentGatewaysModel.FromModelToEntity<PaymentGateways>();

            //Update Country
            bool isPaymentGatewaysUpdated = _paymentGatewaysRepository.Update(paymentGateways);
            if (!isPaymentGatewaysUpdated)
            {
                paymentGatewaysModel.HasError = true;
                paymentGatewaysModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPaymentGatewaysUpdated;
        }
        //Delete PaymentGateways.
        public virtual bool DeletePaymentGateways(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PaymentGatewayID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("PaymentGatewayId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeletePaymentGateways @PaymentGatewayId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Country code is already present or not.
        protected virtual bool IsPaymentCodeAlreadyExist(string paymentCode, byte paymentGatewayId = 0)
         => _paymentGatewaysRepository.Table.Any(x => x.PaymentCode == paymentCode && (x.PaymentGatewayId != paymentGatewayId || paymentGatewayId == 0));
        #endregion

    }
}
