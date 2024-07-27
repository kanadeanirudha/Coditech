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
    public class GeneralSmsProviderService : IGeneralSmsProviderMasterService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ICoditechLogging coditechLogging;
        private readonly ICoditechRepository<GeneralSmsProvider> _generalSmsProviderMasterRepository;
        private IServiceProvider _serviceProvider;
        private readonly ICoditechLogging _coditechLogging;

        public GeneralSmsProviderService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider;
           _coditechLogging = coditechLogging;
            _generalSmsProviderMasterRepository = new CoditechRepository<GeneralSmsProvider>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual GeneralSmsProviderListModel GetSmsProviderList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralSmsProviderModel> objStoredProc = new CoditechViewRepository<GeneralSmsProviderModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralSmsProviderModel> SmsProviderList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetSmsProviderList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralSmsProviderListModel listModel = new GeneralSmsProviderListModel();

            listModel.GeneralSmsProviderList = SmsProviderList?.Count > 0 ? SmsProviderList : new List<GeneralSmsProviderModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Sms Provider.
        public virtual GeneralSmsProviderModel CreateSmsProvider(GeneralSmsProviderModel generalSmsProviderModel)
        {
            if (IsNull(generalSmsProviderModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsSmsProviderCodeAlreadyExist(generalSmsProviderModel.ProviderName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Sms Provider"));

            GeneralSmsProvider generalSmsProviderMaster = generalSmsProviderModel.FromModelToEntity<GeneralSmsProvider>();

            //Create new SmsProvider and return it.
            GeneralSmsProvider SmsProviderData = _generalSmsProviderMasterRepository.Insert(generalSmsProviderMaster);
            if (SmsProviderData?.GeneralSmsProviderId > 0)
            {
                generalSmsProviderModel.GeneralSmsProviderId = SmsProviderData.GeneralSmsProviderId;
            }
            else
            {
                generalSmsProviderModel.HasError = true;
                generalSmsProviderModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalSmsProviderModel;
        }

        //Get SmsProvider by SmsProvider id.
        public virtual GeneralSmsProviderModel GetSmsProvider(short SmsProviderId)
        {
            if (SmsProviderId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SmsProviderID"));

            //Get the SmsProvider Details based on id.
            GeneralSmsProvider generalSmsProviderMaster = _generalSmsProviderMasterRepository.Table.FirstOrDefault(x => x.GeneralSmsProviderId == SmsProviderId);
            GeneralSmsProviderModel generalSmsProviderModel = generalSmsProviderMaster?.FromEntityToModel<GeneralSmsProviderModel>();
            return generalSmsProviderModel;
        }


        //Update SmsProvider.
        public virtual bool UpdateSmsProvider(GeneralSmsProviderModel generalSmsProviderModel)
        {
            if (IsNull(generalSmsProviderModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalSmsProviderModel.GeneralSmsProviderId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SmsProviderID"));

            if (IsSmsProviderCodeAlreadyExist(generalSmsProviderModel.ProviderCode, generalSmsProviderModel.GeneralSmsProviderId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "SmsProvider Code"));

            GeneralSmsProvider generalSmsProviderMaster = generalSmsProviderModel.FromModelToEntity<GeneralSmsProvider>();

            //Update SmsProvider
            bool isSmsProviderUpdated = _generalSmsProviderMasterRepository.Update(generalSmsProviderMaster);
            if (!isSmsProviderUpdated)
            {
                generalSmsProviderModel.HasError = true;
                generalSmsProviderModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isSmsProviderUpdated;
        }

        //Delete SmsProvider.
        public virtual bool DeleteSmsProvider(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "SmsProviderID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("SmsProviderId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteSmsProvider @SmsProviderId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if SmsProvider code is already present or not.
        protected virtual bool IsSmsProviderCodeAlreadyExist(string ProviderName, short generalSmsProviderMasterId = 0)
         => _generalSmsProviderMasterRepository.Table.Any(x => x.ProviderName == ProviderName && (x.GeneralSmsProviderId != generalSmsProviderMasterId || generalSmsProviderMasterId == 0));
        #endregion
    }
}
   