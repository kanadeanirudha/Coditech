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
    public class GeneralWhatsAppProviderService : IGeneralWhatsAppProviderMasterService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ICoditechLogging coditechLogging;
        private readonly ICoditechRepository<GeneralWhatsAppProvider> _generalWhatsAppProviderMasterRepository;
        private IServiceProvider _serviceProvider;
        private readonly ICoditechLogging _coditechLogging;

        public GeneralWhatsAppProviderService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalWhatsAppProviderMasterRepository = new CoditechRepository<GeneralWhatsAppProvider>(_serviceProvider.GetService<Coditech_Entities>());

        }

        public virtual GeneralWhatsAppProviderListModel GetWhatsAppProviderList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralWhatsAppProviderModel> objStoredProc = new CoditechViewRepository<GeneralWhatsAppProviderModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralWhatsAppProviderModel> WhatsAppProviderList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetWhatsAppProviderList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralWhatsAppProviderListModel listModel = new GeneralWhatsAppProviderListModel();

            listModel.GeneralWhatsAppProviderList = WhatsAppProviderList?.Count > 0 ? WhatsAppProviderList : new List<GeneralWhatsAppProviderModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create WhatsApp Provider.
        public virtual GeneralWhatsAppProviderModel CreateWhatsAppProvider(GeneralWhatsAppProviderModel generalWhatsAppProviderModel)
        {
            if (IsNull(generalWhatsAppProviderModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsWhatsAppProviderCodeAlreadyExist(generalWhatsAppProviderModel.ProviderName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "WhatsApp Provider"));

            GeneralWhatsAppProvider generalWhatsAppProviderMaster = generalWhatsAppProviderModel.FromModelToEntity<GeneralWhatsAppProvider>();

            //Create new WhatsAppProvider and return it.
            GeneralWhatsAppProvider WhatsAppProviderData = _generalWhatsAppProviderMasterRepository.Insert(generalWhatsAppProviderMaster);
            if (WhatsAppProviderData?.GeneralWhatsAppProviderId > 0)
            {
                generalWhatsAppProviderModel.GeneralWhatsAppProviderId = WhatsAppProviderData.GeneralWhatsAppProviderId;
            }
            else
            {
                generalWhatsAppProviderModel.HasError = true;
                generalWhatsAppProviderModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalWhatsAppProviderModel;
        }

        //Get WhatsAppProvider by WhatsAppProvider id.
        public virtual GeneralWhatsAppProviderModel GetWhatsAppProvider(short WhatsAppProviderId)
        {
            if (WhatsAppProviderId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "WhatsAppProviderID"));

            //Get the WhatsAppProvider Details based on id.
            GeneralWhatsAppProvider generalWhatsAppProviderMaster = _generalWhatsAppProviderMasterRepository.Table.FirstOrDefault(x => x.GeneralWhatsAppProviderId == WhatsAppProviderId);
            GeneralWhatsAppProviderModel generalWhatsAppProviderModel = generalWhatsAppProviderMaster?.FromEntityToModel<GeneralWhatsAppProviderModel>();
            return generalWhatsAppProviderModel;
        }
        //Update WhatsAppProvider.
        public virtual bool UpdateWhatsAppProvider(GeneralWhatsAppProviderModel generalWhatsAppProviderModel)
        {
            if (IsNull(generalWhatsAppProviderModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalWhatsAppProviderModel.GeneralWhatsAppProviderId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "WhatsAppProviderID"));

            if (IsWhatsAppProviderCodeAlreadyExist(generalWhatsAppProviderModel.ProviderCode, generalWhatsAppProviderModel.GeneralWhatsAppProviderId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "WhatsAppProvider Code"));

            GeneralWhatsAppProvider generalWhatsAppProviderMaster = generalWhatsAppProviderModel.FromModelToEntity<GeneralWhatsAppProvider>();

            //Update WhatsAppProvider
            bool isWhatsAppProviderUpdated = _generalWhatsAppProviderMasterRepository.Update(generalWhatsAppProviderMaster);
            if (!isWhatsAppProviderUpdated)
            {
                generalWhatsAppProviderModel.HasError = true;
                generalWhatsAppProviderModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isWhatsAppProviderUpdated;
        }
        //Delete WhatsAppProvider.
        public virtual bool DeleteWhatsAppProvider(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "WhatsAppProviderID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("WhatsAppProviderId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteWhatsAppProvider @WhatsAppProviderId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if WhatsAppProvider code is already present or not.
        protected virtual bool IsWhatsAppProviderCodeAlreadyExist(string ProviderName, short generalWhatsAppProviderMasterId = 0)
         => _generalWhatsAppProviderMasterRepository.Table.Any(x => x.ProviderName == ProviderName && (x.GeneralWhatsAppProviderId != generalWhatsAppProviderMasterId || generalWhatsAppProviderMasterId == 0));
        #endregion
    }
}
