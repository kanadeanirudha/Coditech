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
    public class GeneralTaxGroupMasterService : IGeneralTaxGroupMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralTaxGroupMaster> _generalTaxGroupMasterRepository;
        public GeneralTaxGroupMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalTaxGroupMasterRepository = new CoditechRepository<GeneralTaxGroupMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralTaxGroupMasterListModel GetTaxGroupMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralTaxGroupMasterModel> objStoredProc = new CoditechViewRepository<GeneralTaxGroupMasterModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", null/*pageListModel?.SPWhereClause*/, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralTaxGroupMasterModel> taxGroupMasterList = objStoredProc.ExecuteStoredProcedureList("RARIndia_GetTaxGroupList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralTaxGroupMasterListModel listModel = new GeneralTaxGroupMasterListModel();

            listModel.GeneralTaxGroupMasterList = taxGroupMasterList?.Count > 0 ? taxGroupMasterList : new List<GeneralTaxGroupMasterModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Tax Group Master.
        public GeneralTaxGroupMasterModel CreateTaxGroupMaster(GeneralTaxGroupMasterModel generalTaxGroupMasterModel)
        {
            if (IsNull(generalTaxGroupMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsNameAlreadyExist(generalTaxGroupMasterModel.TaxGroupName))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Tax Group Name"));
            }
            GeneralTaxGroupMaster generalTaxGroupMaster = generalTaxGroupMasterModel.FromModelToEntity<GeneralTaxGroupMaster>();

            //Create new Tax Group Master and return it.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Insert(generalTaxGroupMaster);
            if (taxGroupMasterData?.GeneralTaxGroupMasterId > 0)
            {
                generalTaxGroupMasterModel.GeneralTaxGroupMasterId = taxGroupMasterData.GeneralTaxGroupMasterId;
            }
            else
            {
                generalTaxGroupMasterModel.HasError = true;
                generalTaxGroupMasterModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalTaxGroupMasterModel;
        }

        //Get Tax Group Master by GeneralTaxGroupMasterId.
        public GeneralTaxGroupMasterModel GetTaxGroupMaster(short taxGroupMasterId)
        {
            if (taxGroupMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            //Get the Tax Group Master Details based on id.
            GeneralTaxGroupMaster taxGroupMasterData = _generalTaxGroupMasterRepository.Table.FirstOrDefault(x => x.GeneralTaxGroupMasterId == taxGroupMasterId);
            GeneralTaxGroupMasterModel generalTaxGroupMasterModel = taxGroupMasterData.FromEntityToModel<GeneralTaxGroupMasterModel>();
            return generalTaxGroupMasterModel;
        }

        //Update TaxGroupMaster.
        public virtual bool UpdateTaxGroupMaster(GeneralTaxGroupMasterModel generalTaxGroupMasterModel)
        {
            if (IsNull(generalTaxGroupMasterModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalTaxGroupMasterModel.GeneralTaxGroupMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            GeneralTaxGroupMaster generalTaxGroupMaster = generalTaxGroupMasterModel.FromModelToEntity<GeneralTaxGroupMaster>();
            //Update TaxGroupMaster
            bool isTaxGroupMasterUpdated = _generalTaxGroupMasterRepository.Update(generalTaxGroupMaster);
            if (!isTaxGroupMasterUpdated)
            {
                generalTaxGroupMasterModel.HasError = true;
                generalTaxGroupMasterModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isTaxGroupMasterUpdated;
        }

        //Delete TaxGroupMaster.
        public virtual bool DeleteTaxGroupMaster(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "TaxGroupMasterId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("TaxGroupMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("RARIndia_DeleteTaxGroupMaster @TaxGroupMasterId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Tax Group Name is already present or not.
        protected virtual bool IsNameAlreadyExist(string taxGroupName)
         => _generalTaxGroupMasterRepository.Table.Any(x => x.TaxGroupName == taxGroupName);
        #endregion
    }
}
