
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
        private readonly ICoditechRepository<GeneralPolicyRules> _generalPolicyRulesRepository;
        public GeneralPolicyMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalPolicyMasterRepository = new CoditechRepository<GeneralPolicyMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalPolicyRulesRepository = new CoditechRepository<GeneralPolicyRules>(_serviceProvider.GetService<Coditech_Entities>());
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

            if (IsPolicyCodeAlreadyExist(generalPolicyModel.PolicyCode, generalPolicyModel.GeneralPolicyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Policy Code"));

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
        public virtual GeneralPolicyModel GetPolicy(string policyCode)
        {
            //Get the Policy Details based on id.
            GeneralPolicyMaster generalPolicyMaster = _generalPolicyMasterRepository.Table.Where(x => x.PolicyCode == policyCode).FirstOrDefault();
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

            if (IsPolicyCodeAlreadyExist(generalPolicyModel.PolicyCode, generalPolicyModel.GeneralPolicyMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Policy Code"));

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
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PolicyCode"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("PolicyCode", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralPolicy @PolicyCode,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        //General Policy Rules
        public virtual GeneralPolicyRulesListModel  GetGeneralPolicyRulesList(string policyCode,FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralPolicyRulesModel> objStoredProc = new CoditechViewRepository<GeneralPolicyRulesModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@PolicyCode", policyCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralPolicyRulesModel> GeneralPolicyRulesList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralPolicyRulesList @PolicyCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GeneralPolicyRulesListModel listModel = new GeneralPolicyRulesListModel();

            listModel.GeneralPolicyRulesList = GeneralPolicyRulesList?.Count > 0 ? GeneralPolicyRulesList : new List<GeneralPolicyRulesModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create General Policy Rules.
        public virtual GeneralPolicyRulesModel CreatePolicyRules(GeneralPolicyRulesModel generalPolicyRulesModel)
        {
            if (IsNull(generalPolicyRulesModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            GeneralPolicyRules generalPolicyRulesMaster = generalPolicyRulesModel.FromModelToEntity<GeneralPolicyRules>();

            //Create new Policy and return it.
            GeneralPolicyRules policyRulesData = _generalPolicyRulesRepository.Insert(generalPolicyRulesMaster);
            if (policyRulesData?.GeneralPolicyRulesId > 0)
            {
                generalPolicyRulesModel.GeneralPolicyRulesId = policyRulesData.GeneralPolicyRulesId;
            }
            else
            {
                generalPolicyRulesModel.HasError = true;
                generalPolicyRulesModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalPolicyRulesModel;
        }

        //Get Policy Rules by Policy Rules id.
        public virtual GeneralPolicyRulesModel GetPolicyRules(short generalPolicyRulesId)
        {
            if (generalPolicyRulesId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralPolicyRulesID"));

            //Get the Policy Details based on id.
            GeneralPolicyRules generalPolicyRules = _generalPolicyRulesRepository.Table.Where(x => x.GeneralPolicyRulesId == generalPolicyRulesId).FirstOrDefault();
            GeneralPolicyRulesModel generalPolicyRulesModel = generalPolicyRules?.FromEntityToModel<GeneralPolicyRulesModel>();
            return generalPolicyRulesModel;
        }

        //Update PolicyRules.
        public virtual bool UpdatePolicyRules(GeneralPolicyRulesModel generalPolicyRulesModel)
        {
            if (IsNull(generalPolicyRulesModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalPolicyRulesModel.GeneralPolicyRulesId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "PolicyRulesID"));


            GeneralPolicyRules generalPolicyRules = generalPolicyRulesModel.FromModelToEntity<GeneralPolicyRules>();

            //Update PolicyRules
            bool isPolicyUpdated = _generalPolicyRulesRepository.Update(generalPolicyRules);
            if (!isPolicyUpdated)
            {
                generalPolicyRulesModel.HasError = true;
                generalPolicyRulesModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isPolicyUpdated;
        }

        //Delete PolicyRules.
        public virtual bool DeletePolicyRules(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GeneralPolicyMasterID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GeneralPolicyRulesId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGeneralPolicyRules @GeneralPolicyRulesId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Policy code is already present or not.
        protected virtual bool IsPolicyCodeAlreadyExist(string policyCode, short generalPolicyMasterId = 0)
         => _generalPolicyMasterRepository.Table.Any(x => x.PolicyName == policyCode && (x.GeneralPolicyMasterId != generalPolicyMasterId || generalPolicyMasterId == 0));
        #endregion
    }
}
