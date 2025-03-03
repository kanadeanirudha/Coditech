using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class AccGLTransactionService : BaseService, IAccGLTransactionService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccGLTransaction> _accGLTransactionRepository;
        public AccGLTransactionService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLTransactionRepository = new CoditechRepository<AccGLTransaction>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual AccGLTransactionListModel AccGLTransactionList(string selectedCentreCode ,  int accGLTransaction, short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccGLTransactionModel> objStoredProc = new CoditechViewRepository<AccGLTransactionModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@AccSetupBalanceSheetTypeId", accSetupBalanceSheetTypeId, ParameterDirection.Input, DbType.Byte);
            objStoredProc.SetParameter("@AccSetupGLTransactionId", accGLTransaction, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@GeneralFinancialYearId", generalFinancialYearId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@AccGLTransactionTypeId", accSetupTransactionTypeId, ParameterDirection.Input, DbType.Int16);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccGLTransactionModel> accGLTransactionList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccGLTransactionList  @AccSetupGLTransactionId, @GeneralFinancialYearId ,@AccGLTransactionTypeId,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 7, out pageListModel.TotalRowCount)?.ToList();
            AccGLTransactionListModel listModel = new AccGLTransactionListModel();

            listModel.AccGLTransactionList = accGLTransactionList?.Count > 0 ? accGLTransactionList : new List<AccGLTransactionModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create GLTransaction.sss
        public virtual AccGLTransactionModel CreateGLTransaction(AccGLTransactionModel accGLTransactionModel)
        {
            if (IsNull(accGLTransactionModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            //if (IsGLTransactionNameAlreadyExist(accSetupGLTransactionModel.AccBalancesheetHeadDesc))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "GLTransaction Name"));

            AccGLTransaction accGLTransaction = accGLTransactionModel.FromModelToEntity<AccGLTransaction>();

            //Create new GLTransaction and return it.
            AccGLTransaction accGLTransactionData = _accGLTransactionRepository.Insert(accGLTransaction);
            if (accGLTransactionData?.AccGLTransactionId > 0)
            {
                accGLTransactionModel.AccGLTransactionId = accGLTransactionData.AccGLTransactionId;
            }
            else
            {
                accGLTransactionModel.HasError = true;
                accGLTransactionModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accGLTransactionModel;
        }
        //Get GLTransaction by AccGLTransactionId.
        public virtual AccGLTransactionModel GetGLTransaction(long accGLTransaction)
        {
            if (accGLTransaction <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GLTransactionId"));

            //Get the accGLTransactionData Details based on id.
            AccGLTransaction accGLTransactionData = _accGLTransactionRepository.Table.FirstOrDefault(x => x.AccGLTransactionId == accGLTransaction);
            AccGLTransactionModel accSetupGLTransactionModel = accGLTransactionData.FromEntityToModel<AccGLTransactionModel>();
            return accSetupGLTransactionModel;
        }

        //Update GLTransaction.
        public virtual bool UpdateGLTransaction(AccGLTransactionModel accSetupGLTransactionModel)
        {
            //if (IsNull(accSetupGLTransactionModel))
            //    throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            //if (accSetupGLTransactionModel.AccGLTransactionId < 1)
            //    throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GLTransactionId"));

            //if (IsGLTransactionNameAlreadyExist(accSetupGLTransactionModel.AccBalancesheetHeadDesc, accSetupGLTransactionModel.AccGLTransactionId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "GLTransaction Name"));

            AccGLTransaction accSetupGLTransaction = accSetupGLTransactionModel.FromModelToEntity<AccGLTransaction>();

            //Update GLTransaction
            bool isGLTransactionUpdated = _accGLTransactionRepository.Update(accSetupGLTransaction);
            if (!isGLTransactionUpdated)
            {
                accSetupGLTransactionModel.HasError = true;
                accSetupGLTransactionModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGLTransactionUpdated;
        }
        ////Delete GLTransaction.
        //public virtual bool DeleteGLTransaction(ParameterModel parameterModel)
        //{
        //    if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
        //        throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccGLTransactionId"));

        //    CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
        //    objStoredProc.SetParameter("AccGLTransactionId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
        //    objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
        //    int status = 0;
        //    objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccGLTransaction @AccGLTransactionId,  @Status OUT", 1, out status);
        //    return status == 1 ? true : false;
        //}
        #region Protected Method
        //Check if GLTransaction Name is already present or not.
        //protected virtual bool IsGLTransactionNameAlreadyExist(string balancesheetName, int accGLTransaction = 0)
        // => _accGLTransactionRepository.Table.Any(x => x.AccBalancesheetHeadDesc == balancesheetName && (x.AccGLTransactionId != accGLTransaction || accGLTransaction == 0));
        #endregion
    }
}

