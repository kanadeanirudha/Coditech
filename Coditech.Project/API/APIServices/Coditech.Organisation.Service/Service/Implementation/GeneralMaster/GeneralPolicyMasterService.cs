
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
    public class GeneralPolicyMasterService : IGeneralPolicyMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralPolicyMaster> _generalPolicyMasterRepository;
        public GeneralPolicyMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalPolicyMasterRepository = new CoditechRepository<GeneralPolicyMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralPolicyListModel GetPolicyList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralPolicyModel> objStoredProc = new CoditechViewRepository<GeneralPolicyModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralPolicyModel> GeneralPolicyList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralPolicyList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralPolicyListModel listModel = new GeneralPolicyListModel();

            listModel.GeneralPolicyList = GeneralPolicyList?.Count > 0 ? GeneralPolicyList : new List<GeneralPolicyModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create General Policy.
        public virtual GeneralPolicyModel CreatePolicy(GeneralPolicyModel generalPolicyModel)
        {
            if (IsNull(generalPolicyModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsPolicyCodeAlreadyExist(generalPolicyModel.PolicyName, generalPolicyModel.GeneralPolicyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Policy Name"));

            GeneralPolicyMaster generalPolicyMaster = generalPolicyModel.FromModelToEntity<GeneralPolicyMaster>();

            //Create new Policy and return it.
            GeneralPolicyMaster policyData = _generalPolicyMasterRepository.Insert(generalPolicyMaster);
            if (policyData?.GeneralPolicyMasterId > 0)
            {
                generalPolicyModel.GeneralPolicyMasterId = policyData.GeneralPolicyMasterId;
            }
            else
            {
                generalPolicyModel.HasError = true;
                generalPolicyModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalPolicyModel;
        }

        //Get Policy by Policy id.
        public virtual GeneralPolicyModel GetPolicy(short generalPolicyMasterId)
        {
            if (generalPolicyMasterId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralPolicyMasterIdID"));

            //Get the Policy Details based on id.
            GeneralPolicyMaster generalPolicyMaster = _generalPolicyMasterRepository.Table.FirstOrDefault(x => x.GeneralPolicyMasterId == generalPolicyMasterId);
            GeneralPolicyModel generalPolicyModel = generalPolicyMaster?.FromEntityToModel<GeneralPolicyModel>();
            return generalPolicyModel;
        }

        //Update Policy.
        public virtual bool UpdatePolicy(GeneralPolicyModel generalPolicyModel)
        {
            if (IsNull(generalPolicyModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPolicyModel.GeneralPolicyMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PolicyID"));

            if (IsPolicyCodeAlreadyExist(generalPolicyModel.PolicyName, generalPolicyModel.GeneralPolicyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Policy Name"));

            GeneralPolicyMaster generalPolicyMaster = generalPolicyModel.FromModelToEntity<GeneralPolicyMaster>();

            //Update Policy
            bool isPolicyUpdated = _generalPolicyMasterRepository.Update(generalPolicyMaster);
            if (!isPolicyUpdated)
            {
                generalPolicyModel.HasError = true;
                generalPolicyModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPolicyUpdated;
        }

        //Delete Policy.
        public virtual bool DeletePolicy(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralPolicyMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralPolicyMasterId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeletePolicy @GeneralPolicyMasterId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Policy code is already present or not.
        protected virtual bool IsPolicyCodeAlreadyExist(string policyName, short generalPolicyMasterId = 0)
         => _generalPolicyMasterRepository.Table.Any(x => x.PolicyName == policyName && (x.GeneralPolicyMasterId != generalPolicyMasterId || generalPolicyMasterId == 0));
        #endregion
    }
}
