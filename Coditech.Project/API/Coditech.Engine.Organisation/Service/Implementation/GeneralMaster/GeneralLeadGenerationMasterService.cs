
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger; 
using Coditech.Common.Service;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralLeadGenerationMasterService : BaseService, IGeneralLeadGenerationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralLeadGenerationMaster> _generalLeadGenerationMasterRepository;
        public GeneralLeadGenerationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalLeadGenerationMasterRepository = new CoditechRepository<GeneralLeadGenerationMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralLeadGenerationListModel GetLeadGenerationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralLeadGenerationModel> objStoredProc = new CoditechViewRepository<GeneralLeadGenerationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralLeadGenerationModel> LeadGenerationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetLeadGenerationList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralLeadGenerationListModel listModel = new GeneralLeadGenerationListModel();

            listModel.GeneralLeadGenerationList = LeadGenerationList?.Count > 0 ? LeadGenerationList : new List<GeneralLeadGenerationModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Lead Generation.
        public virtual GeneralLeadGenerationModel CreateLeadGeneration(GeneralLeadGenerationModel generalLeadGenerationModel)
        {
            if (IsNull(generalLeadGenerationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            GeneralLeadGenerationMaster generalLeadGenerationMaster = generalLeadGenerationModel.FromModelToEntity<GeneralLeadGenerationMaster>();

            //Create new LeadGeneration and return it.
            GeneralLeadGenerationMaster LeadGenerationData = _generalLeadGenerationMasterRepository.Insert(generalLeadGenerationMaster);
            if (LeadGenerationData?.GeneralLeadGenerationMasterId > 0)
            {
                generalLeadGenerationModel.GeneralLeadGenerationMasterId = LeadGenerationData.GeneralLeadGenerationMasterId;
            }
            else
            {
                generalLeadGenerationModel.HasError = true;
                generalLeadGenerationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalLeadGenerationModel;
        }

        //Get LeadGeneration by LeadGeneration id.
        public virtual GeneralLeadGenerationModel GetLeadGeneration(long LeadGenerationId)
        {
            if (LeadGenerationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LeadGenerationID"));

          
            GeneralLeadGenerationMaster generalLeadGenerationMaster = _generalLeadGenerationMasterRepository.Table.Where(x => x.GeneralLeadGenerationMasterId == LeadGenerationId)?.FirstOrDefault();
            GeneralLeadGenerationModel generalLeadGenerationModel = generalLeadGenerationMaster?.FromEntityToModel<GeneralLeadGenerationModel>();
            if (generalLeadGenerationModel?.GeneralLeadGenerationMasterId > 0)
            {
                GeneralPersonModel generalPersonModel = GetGeneralPersonDetailsByEntityType(generalLeadGenerationModel.GeneralLeadGenerationMasterId, UserTypeEnum.Employee.ToString());
                if (IsNotNull(generalPersonModel))
                {
                    generalLeadGenerationModel.SelectedCentreCode = generalPersonModel.SelectedCentreCode;

                }
            }
            return generalLeadGenerationModel;
        }

        //Update LeadGeneration.
        public virtual bool UpdateLeadGeneration(GeneralLeadGenerationModel generalLeadGenerationModel)
        {
            if (IsNull(generalLeadGenerationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalLeadGenerationModel.GeneralLeadGenerationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LeadGenerationID"));

            //if (IsLeadGenerationCodeAlreadyExist(generalLeadGenerationModel.FirstName, generalLeadGenerationModel.GeneralLeadGenerationMasterId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "FirstName"));

            GeneralLeadGenerationMaster generalLeadGenerationMaster = generalLeadGenerationModel.FromModelToEntity<GeneralLeadGenerationMaster>();

            //Update LeadGeneration
            bool isLeadGenerationUpdated = _generalLeadGenerationMasterRepository.Update(generalLeadGenerationMaster);
            if (!isLeadGenerationUpdated)
            {
                generalLeadGenerationModel.HasError = true;
                generalLeadGenerationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isLeadGenerationUpdated;
        }

        //Delete LeadGeneration.
        public virtual bool DeleteLeadGeneration(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LeadGenerationID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("LeadGenerationId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteLeadGeneration @LeadGenerationId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if LeadGeneration code is already present or not.
        protected virtual bool IsLeadGenerationCodeAlreadyExist(string firstName, long generalLeadGenerationMasterId = 0)
         => _generalLeadGenerationMasterRepository.Table.Any(x => x.FirstName == firstName && (x.GeneralLeadGenerationMasterId != generalLeadGenerationMasterId || generalLeadGenerationMasterId == 0));
        #endregion
    }
}
