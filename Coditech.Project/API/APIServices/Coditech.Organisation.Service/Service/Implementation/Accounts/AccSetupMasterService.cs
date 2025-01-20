
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
    public class AccSetupMasterService : IAccSetupMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccSetupMaster> _accSetupMasterRepository;
        public AccSetupMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accSetupMasterRepository = new CoditechRepository<AccSetupMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccSetupMasterListModel GetAccSetupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<AccSetupMasterModel> objStoredProc = new CoditechViewRepository<AccSetupMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode,ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<AccSetupMasterModel> AccSetupMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetAccSetupMasterList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            AccSetupMasterListModel listModel = new AccSetupMasterListModel();

            listModel.AccSetupMasterList = AccSetupMasterList?.Count > 0 ? AccSetupMasterList : new List<AccSetupMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create AccSetupMaster.
        public virtual AccSetupMasterModel CreateAccSetupMaster(AccSetupMasterModel accSetupMasterModel)
        {
            if (IsNull(accSetupMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsAccSetupMasterEntryAlreadyExist( accSetupMasterModel.FiscalYearDay, accSetupMasterModel.FiscalYearMonth, accSetupMasterModel.AccSetupMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Fiscal Year"));


            AccSetupMaster accSetupMaster = accSetupMasterModel.FromModelToEntity<AccSetupMaster>();

            //Create new AccSetupMaster and return it.
            AccSetupMaster AccSetupMasterData = _accSetupMasterRepository.Insert(accSetupMaster);
            if (AccSetupMasterData?.AccSetupMasterId > 0)
            {
                accSetupMasterModel.AccSetupMasterId = AccSetupMasterData.AccSetupMasterId;
            }
            else
            {
                accSetupMasterModel.HasError = true;
                accSetupMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accSetupMasterModel;
        }

        //Get AccSetupMaster by AccSetupMaster id.
        public virtual AccSetupMasterModel GetAccSetupMaster(short accSetupMasterId)
        {
            if (accSetupMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "accSetupMasterId"));

            //Get the AccSetupMaster Details based on id.
            AccSetupMaster accSetupMaster = _accSetupMasterRepository.Table.FirstOrDefault(x => x.AccSetupMasterId == accSetupMasterId);
            AccSetupMasterModel accSetupMasterModel = accSetupMaster?.FromEntityToModel<AccSetupMasterModel>();
            return accSetupMasterModel;
        }

        //Update AccSetupMaster.
        public virtual bool UpdateAccSetupMaster(AccSetupMasterModel accSetupMasterModel)
        {
            if (IsNull(accSetupMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (IsAccSetupMasterEntryAlreadyExist( accSetupMasterModel.FiscalYearDay, accSetupMasterModel.FiscalYearMonth, accSetupMasterModel.AccSetupMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Financial Year"));


            if (accSetupMasterModel.AccSetupMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupMasterID"));

            
            AccSetupMaster accSetupMaster = accSetupMasterModel.FromModelToEntity<AccSetupMaster>();

            //Update AccSetupMaster
            bool isAccSetupMasterUpdated = _accSetupMasterRepository.Update(accSetupMaster);
            if (!isAccSetupMasterUpdated)
            {
                accSetupMasterModel.HasError = true;
                accSetupMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isAccSetupMasterUpdated;
        }

        //Delete AccSetupMaster.
        public virtual bool DeleteAccSetupMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "AccSetupMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("AccSetupMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteAccSetupMaster @AccSetupMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if AccSetupMaster code is already present or not.
        //protected virtual bool IsAccSetupMasterCodeAlreadyExist(string financialYearName, short accSetupMasterId = 0)
        // => _accSetupMasterRepository.Table.Any(x => x.AccSetupMasterName == financialYearName && (x.AccSetupMasterMasterId != accSetupMasterId || accSetupMasterId == 0));

        protected virtual bool IsAccSetupMasterEntryAlreadyExist( byte FiscalYearDay, byte FiscalYearMonth, short accSetupMasterId = 0)
        => _accSetupMasterRepository.Table.Any(x =>x.FiscalYearDay == FiscalYearDay && x.FiscalYearMonth == FiscalYearMonth && (x.AccSetupMasterId != accSetupMasterId || accSetupMasterId == 0));

        #endregion
    }
}
