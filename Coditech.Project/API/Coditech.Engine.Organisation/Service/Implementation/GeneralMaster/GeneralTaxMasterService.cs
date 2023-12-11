using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class GeneralTaxMasterService : IGeneralTaxMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralTaxMaster> _generalTaxMasterRepository;
        public GeneralTaxMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalTaxMasterRepository = new CoditechRepository<GeneralTaxMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralTaxMasterListModel GetTaxMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralTaxMasterModel> objStoredProc = new CoditechViewRepository<GeneralTaxMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTaxMasterModel> taxMasterList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetTaxList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralTaxMasterListModel listModel = new GeneralTaxMasterListModel();

            listModel.GeneralTaxMasterList = taxMasterList?.Count > 0 ? taxMasterList : new List<GeneralTaxMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Tax Master.
        public virtual GeneralTaxMasterModel CreateTaxMaster(GeneralTaxMasterModel generalTaxMasterModel)
        {
            if (IsNull(generalTaxMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsTaxNameAlreadyExist(generalTaxMasterModel.TaxName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Name"));
           
            GeneralTaxMaster generalTaxMaster = generalTaxMasterModel.FromModelToEntity<GeneralTaxMaster>();

            //Create new Tax Master and return it.
            GeneralTaxMaster taxMasterData = _generalTaxMasterRepository.Insert(generalTaxMaster);
            if (taxMasterData?.GeneralTaxMasterId > 0)
            {
                generalTaxMasterModel.GeneralTaxMasterId = taxMasterData.GeneralTaxMasterId;
            }
            else
            {
                generalTaxMasterModel.HasError = true;
                generalTaxMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTaxMasterModel;
        }

        //Get Tax Master by GeneralTaxMasterId.
        public virtual GeneralTaxMasterModel GetTaxMaster(short taxMasterId)
        {
            if (taxMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxMasterId"));

            //Get the Tax Master Details based on id.
            GeneralTaxMaster taxMasterData = _generalTaxMasterRepository.Table.FirstOrDefault(x => x.GeneralTaxMasterId == taxMasterId);
            GeneralTaxMasterModel generalTaxMasterModel = taxMasterData.FromEntityToModel<GeneralTaxMasterModel>();
            return generalTaxMasterModel;
        }

        //Update TaxMaster.
        public virtual bool UpdateTaxMaster(GeneralTaxMasterModel generalTaxMasterModel)
        {
            if (IsNull(generalTaxMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTaxMasterModel.GeneralTaxMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxMasterId"));
           
            if (IsTaxNameAlreadyExist(generalTaxMasterModel.TaxName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Name"));

            GeneralTaxMaster generalTaxMaster = generalTaxMasterModel.FromModelToEntity<GeneralTaxMaster>();

            //Update TaxMaster
            bool isTaxMasterUpdated = _generalTaxMasterRepository.Update(generalTaxMaster);
            if (!isTaxMasterUpdated)
            {
                generalTaxMasterModel.HasError = true;
                generalTaxMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTaxMasterUpdated;
        }

        //Delete TaxMaster.
        public virtual bool DeleteTaxMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaxMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteTaxMaster @TaxMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Tax Name is already present or not.
        protected virtual bool IsTaxNameAlreadyExist(string taxName, short generalTaxMasterId = 0)
         => _generalTaxMasterRepository.Table.Any(x => x.TaxName == taxName && (x.GeneralTaxMasterId != generalTaxMasterId || generalTaxMasterId == 0));
        #endregion
    }
}
