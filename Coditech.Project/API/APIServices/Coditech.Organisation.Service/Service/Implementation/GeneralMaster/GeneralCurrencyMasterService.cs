
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
    public class GeneralCurrencyMasterService : IGeneralCurrencyMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralCurrencyMaster> _generalCurrencyMasterRepository;
        public GeneralCurrencyMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalCurrencyMasterRepository = new CoditechRepository<GeneralCurrencyMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralCurrencyMasterListModel GetCurrencyList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralCurrencyMasterModel> objStoredProc = new CoditechViewRepository<GeneralCurrencyMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCurrencyMasterModel> CurrencyList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetCurrencyList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralCurrencyMasterListModel listModel = new GeneralCurrencyMasterListModel();

            listModel.GeneralCurrencyMasterList = CurrencyList?.Count > 0 ? CurrencyList : new List<GeneralCurrencyMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Currency.
        public virtual GeneralCurrencyMasterModel CreateCurrency(GeneralCurrencyMasterModel generalCurrencyMasterModel)
        {
            if (IsNull(generalCurrencyMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCurrencyCodeAlreadyExist(generalCurrencyMasterModel.CurrencyName, generalCurrencyMasterModel.GeneralCurrencyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Currency Name"));

            GeneralCurrencyMaster generalCurrencyMaster = generalCurrencyMasterModel.FromModelToEntity<GeneralCurrencyMaster>();

            //Create new Currency and return it.
            GeneralCurrencyMaster currencyData = _generalCurrencyMasterRepository.Insert(generalCurrencyMaster);
            if (currencyData?.GeneralCurrencyMasterId > 0)
            {
                generalCurrencyMasterModel.GeneralCurrencyMasterId = currencyData.GeneralCurrencyMasterId;
            }
            else
            {
                generalCurrencyMasterModel.HasError = true;
                generalCurrencyMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCurrencyMasterModel;
        }

        //Get Currency by Currency id.
        public virtual GeneralCurrencyMasterModel GetCurrency(short generalCurrencyMasterId)
        {
            if (generalCurrencyMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralCurrencyMasterId"));

            //Get the Currency Details based on id.
            GeneralCurrencyMaster generalCurrencyMaster = _generalCurrencyMasterRepository.Table.FirstOrDefault(x => x.GeneralCurrencyMasterId == generalCurrencyMasterId);
            GeneralCurrencyMasterModel generalCurrencyMasterModel = generalCurrencyMaster?.FromEntityToModel<GeneralCurrencyMasterModel>();
            return generalCurrencyMasterModel;
        }

        //Update Country.
        public virtual bool UpdateCurrency(GeneralCurrencyMasterModel generalCurrencyMasterModel)
        {
            if (IsNull(generalCurrencyMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCurrencyMasterModel.GeneralCurrencyMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralCurrencyMasterId"));

            if (IsCurrencyCodeAlreadyExist(generalCurrencyMasterModel.CurrencyName, generalCurrencyMasterModel.GeneralCurrencyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Currency Name"));

            GeneralCurrencyMaster generalCurrencyMaster = generalCurrencyMasterModel.FromModelToEntity<GeneralCurrencyMaster>();

            //Update Country
            bool isCurrencyUpdated = _generalCurrencyMasterRepository.Update(generalCurrencyMaster);
            if (!isCurrencyUpdated)
            {
                generalCurrencyMasterModel.HasError = true;
                generalCurrencyMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isCurrencyUpdated;
        }

        //Delete Country.
        public virtual bool DeleteCurrency(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralCurrencyMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralCurrencyMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteCurrency @GeneralCurrencyMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Currency code is already present or not.
        protected virtual bool IsCurrencyCodeAlreadyExist(string currencyName, short generalCurrencyMasterId = 0)
         => _generalCurrencyMasterRepository.Table.Any(x => x.CurrencyName == currencyName && (x.GeneralCurrencyMasterId != generalCurrencyMasterId || generalCurrencyMasterId == 0));
        #endregion
    }
}
