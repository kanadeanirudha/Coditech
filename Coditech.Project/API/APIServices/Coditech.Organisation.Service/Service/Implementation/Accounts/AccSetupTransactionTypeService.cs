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
    public class AccSetupTransactionTypeService : IAccSetupTransactionTypeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupTransactionType> _accSetupTransactionTypeRepository;
        public AccSetupTransactionTypeService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupTransactionTypeRepository = new CoditechRepository<AccSetupTransactionType>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccSetupTransactionTypeListModel GetTransactionTypeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupTransactionTypeModel> objStoredProc = new CoditechViewRepository<AccSetupTransactionTypeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupTransactionTypeModel> AccSetupTransactionTypeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTransactionTypeList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            AccSetupTransactionTypeListModel listModel = new AccSetupTransactionTypeListModel();

            listModel.AccSetupTransactionTypeList = AccSetupTransactionTypeList?.Count > 0 ? AccSetupTransactionTypeList : new List<AccSetupTransactionTypeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create AccSetupTransactionType.
        public virtual AccSetupTransactionTypeModel CreateTransactionType(AccSetupTransactionTypeModel accSetupTransactionTypeModel)
        {
            if (IsNull(accSetupTransactionTypeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsAccSetupFinancialYearEntryAlreadyExist(AccSetupFinancialYearModel.StartYearDate, accSetupFinancialYearModel.EndYearDate, accSetupFinancialYearModel.AccSetupFinancialYearId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));


            AccSetupTransactionType AccSetupTransactionType = accSetupTransactionTypeModel.FromModelToEntity<AccSetupTransactionType>();

            //Create new AccSetupTransactionType and return it.
            AccSetupTransactionType AccSetupTransactionTypeData = _accSetupTransactionTypeRepository.Insert(AccSetupTransactionType);
            if (AccSetupTransactionType?.AccSetupTransactionTypeId > 0)
            {
                accSetupTransactionTypeModel.AccSetupTransactionTypeId = AccSetupTransactionTypeData.AccSetupTransactionTypeId;
            }
            else
            {
                accSetupTransactionTypeModel.HasError = true;
                accSetupTransactionTypeModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accSetupTransactionTypeModel;
        }

        //Get FinancialYear by FinancialYear id.
        public virtual AccSetupTransactionTypeModel GetTransactionType(short accSetupTransactionTypeId)
        {
            if (accSetupTransactionTypeId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupTransactionType"));

            //Get the FinancialYear Details based on id.
            AccSetupTransactionType accSetupTransactionType = _accSetupTransactionTypeRepository.Table.FirstOrDefault(x => x.AccSetupTransactionTypeId == accSetupTransactionTypeId);
            AccSetupTransactionTypeModel accSetupTransactionTypeModel = accSetupTransactionType?.FromEntityToModel<AccSetupTransactionTypeModel>();
            return accSetupTransactionTypeModel;
        }

        //Update AccSetupTransactionType.
        public virtual bool UpdateTransactionType(AccSetupTransactionTypeModel accSetupTransactionTypeModel)
        {
            if (IsNull(accSetupTransactionTypeModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (IsAccSetupTransactionTypeExist(accSetupTransactionTypeModel.TransactionTypeCode, accSetupTransactionTypeModel.AccSetupTransactionTypeId))
               throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));


            if (accSetupTransactionTypeModel.AccSetupTransactionTypeId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupTransactionTypeId"));


            AccSetupTransactionType accSetupTransactionType = accSetupTransactionTypeModel.FromModelToEntity<AccSetupTransactionType>();

            //Update AccSetupTransactionType
            bool isTransactionTypeUpdated = _accSetupTransactionTypeRepository.Update(accSetupTransactionType);
            if (!isTransactionTypeUpdated)
            {
                accSetupTransactionTypeModel.HasError = true;
                accSetupTransactionTypeModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTransactionTypeUpdated;
        }

        //Delete AccSetupTransactionType.
        public virtual bool DeleteTransactionType(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupTransactionTypeId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccSetupTransactionTypeId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTransactionType @AccSetupTransactionTypeId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        ////Check if AccSetupTransactionType code is already present or not.
        protected virtual bool IsAccSetupTransactionTypeExist(string transactionTypeCode, short accSetupTransactionTypeId = 0)
         => _accSetupTransactionTypeRepository.Table.Any(x => x.TransactionTypeCode == transactionTypeCode && (x.AccSetupTransactionTypeId != accSetupTransactionTypeId || accSetupTransactionTypeId == 0));

        // protected virtual bool IsAccSetupTransactionTypeEntryAlreadyExist(DateTime StartYearDate, DateTime EndYearDate, short AccSetupTransactionTypeId = 0)
        //=> _accSetupTransactionTypeRepository.Table.Any(x => x.StartYearDate == StartYearDate && x.EndYearDate == EndYearDate && (x.accSetupTransactionTypeId != accSetupTransactionTypeId || accSetupFinancialYearId == 0));

        #endregion
    }
}