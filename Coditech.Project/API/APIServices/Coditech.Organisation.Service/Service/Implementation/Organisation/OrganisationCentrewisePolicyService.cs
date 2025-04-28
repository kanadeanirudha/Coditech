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
    public class OrganisationCentrewisePolicyService : IOrganisationCentrewisePolicyService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralPolicyDetails> _organisationCentrewisePolicyRepository;
        private readonly ICoditechRepository<GeneralPolicyRules> _organisationCentrewisePolicyRulesRepository;
        public OrganisationCentrewisePolicyService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentrewisePolicyRepository = new CoditechRepository<GeneralPolicyDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewisePolicyRulesRepository = new CoditechRepository<GeneralPolicyRules>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual GeneralPolicyDetailsListModel GetOrganisationCentrewisePolicyList(string centreCode,FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralPolicyDetailsModel> objStoredProc = new CoditechViewRepository<GeneralPolicyDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", centreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralPolicyDetailsModel> organisationCentrewisePolicyList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentrewisePolicyList @CentreCode,@RowsCount OUT", 1, out pageListModel.TotalRowCount)?.ToList();
            GeneralPolicyDetailsListModel listModel = new GeneralPolicyDetailsListModel();

            listModel.GeneralPolicyDetailsList = organisationCentrewisePolicyList?.Count > 0 ? organisationCentrewisePolicyList : new List<GeneralPolicyDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get Centrewise Policy Details by Centrewise Policy Details id.
        public virtual GeneralPolicyDetailsModel GetCentrewisePolicyDetails(string centreCode, short generalPolicyRulesId)
        {
            if (generalPolicyRulesId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "generalPolicyRulesId"));
            GeneralPolicyDetailsModel generalPolicyDetailsModel = new GeneralPolicyDetailsModel();
            GeneralPolicyRules generalPolicyRules = _organisationCentrewisePolicyRulesRepository.Table.Where(x => x.GeneralPolicyRulesId == generalPolicyRulesId)?.FirstOrDefault();
            if (IsNotNull(generalPolicyRules))
            {
                generalPolicyDetailsModel.GeneralPolicyRulesId = generalPolicyRulesId;
                generalPolicyDetailsModel.PolicyCode = generalPolicyRules.PolicyCode;
                generalPolicyDetailsModel.PolicyQuestionDescription = generalPolicyRules.PolicyQuestionDescription;
            }
            if (!string.IsNullOrEmpty(centreCode))
            {
                generalPolicyDetailsModel.CentreCode = centreCode;
            }
            GeneralPolicyDetails generalPolicyDetails = _organisationCentrewisePolicyRepository.Table.Where(x => x.GeneralPolicyRulesId == generalPolicyRulesId && x.CentreCode == centreCode)?.FirstOrDefault();
            if (generalPolicyDetails?.GeneralPolicyDetailId > 0)
            {
                generalPolicyDetailsModel.GeneralPolicyDetailId = generalPolicyDetails.GeneralPolicyDetailId;
                generalPolicyDetailsModel.PolicyAnswered = generalPolicyDetails.PolicyAnswered;
                generalPolicyDetailsModel.ApplicableFromDate = generalPolicyDetails.ApplicableFromDate;
                generalPolicyDetailsModel.ApplicableUptoDate = generalPolicyDetails.ApplicableUptoDate;
            }
            return generalPolicyDetailsModel;
        }

        //Update Centrewise Policy.
        public virtual bool CentrewisePolicyDetails(GeneralPolicyDetailsModel organisationCentrewisePolicyModel)
        {
            if (IsNull(organisationCentrewisePolicyModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (string.IsNullOrEmpty(organisationCentrewisePolicyModel.CentreCode))
                throw new CoditechException(ErrorCodes.NotFound, string.Format(GeneralResources.ErrorCodeExists, "CentreCode"));

            bool isAssociateUnAssociateCentrewisePolicy = false;
            GeneralPolicyDetails generalPolicyDetails = organisationCentrewisePolicyModel.FromModelToEntity<GeneralPolicyDetails>();
            if (organisationCentrewisePolicyModel.GeneralPolicyDetailId > 0)
            {
                isAssociateUnAssociateCentrewisePolicy = _organisationCentrewisePolicyRepository.Update(generalPolicyDetails);
            }
            else
            {
                //generalPolicyDetails.ActiveFlag = true;
                isAssociateUnAssociateCentrewisePolicy = _organisationCentrewisePolicyRepository.Insert(generalPolicyDetails).GeneralPolicyDetailId > 0;
            }
            if (!isAssociateUnAssociateCentrewisePolicy)
            {
                organisationCentrewisePolicyModel.HasError = true;
                organisationCentrewisePolicyModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isAssociateUnAssociateCentrewisePolicy;
        }

        //Delete CentrewisePolicy
        public virtual bool DeleteCentrewisePolicy(ParameterModel parameterModel)
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
        //Check if Centre code is already present or not.
        protected virtual bool IsCentreCodeAlreadyExist(string centreCode, short generalPolicyDetailId = 0)
         => _organisationCentrewisePolicyRepository.Table.Any(x => x.CentreCode == centreCode && (x.GeneralPolicyDetailId != generalPolicyDetailId || generalPolicyDetailId == 0));
        #endregion
    }
}

