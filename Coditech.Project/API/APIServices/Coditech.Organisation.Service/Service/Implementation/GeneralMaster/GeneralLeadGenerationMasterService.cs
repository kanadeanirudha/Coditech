﻿using Coditech.API.Data;
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
    public class GeneralLeadGenerationMasterService : IGeneralLeadGenerationMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralLeadGenerationMaster> _generalLeadGenerationMasterRepository;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        public GeneralLeadGenerationMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalLeadGenerationMasterRepository = new CoditechRepository<GeneralLeadGenerationMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralLeadGenerationListModel GetLeadGenerationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;
            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralLeadGenerationModel> objStoredProc = new CoditechViewRepository<GeneralLeadGenerationModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralLeadGenerationModel> LeadGenerationList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetLeadGenerationList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
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

            if (IsLeadGenerationAlreadyExist(generalLeadGenerationModel))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "First Name"));
            
            string errorMessage = string.Empty;
            if (!ValidateUserwiseLeadGeneration(generalLeadGenerationModel, out errorMessage))
            {
                throw new CoditechException(ErrorCodes.AlreadyExist, errorMessage);
            }
            GeneralLeadGenerationMaster generalLeadGenerationMaster = generalLeadGenerationModel.FromModelToEntity<GeneralLeadGenerationMaster>();
            generalLeadGenerationMaster.FirstName = generalLeadGenerationMaster.FirstName.ToFirstLetterCapital();
            generalLeadGenerationMaster.LastName = generalLeadGenerationMaster.LastName.ToFirstLetterCapital();
            generalLeadGenerationMaster.MiddleName = generalLeadGenerationMaster.MiddleName.ToFirstLetterCapital();
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

        protected virtual bool ValidateUserwiseLeadGeneration(GeneralLeadGenerationModel generalLeadGenerationModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            string userNameBasedOn = new CoditechRepository<OrganisationCentrewiseUserNameRegistration>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.CentreCode == generalLeadGenerationModel.CentreCode && x.UserType.ToLower() == generalLeadGenerationModel.UserTypeCode.ToLower())?.Select(y => y.UserNameBasedOn)?.FirstOrDefault();
            if (string.IsNullOrEmpty(userNameBasedOn))
            {
                errorMessage = "Organisation Centrewise UserName Registration not set";
                return false;
            }
            else if (userNameBasedOn == UserNameRegistrationTypeEnum.MobileNumber.ToString() && string.IsNullOrEmpty(generalLeadGenerationModel.MobileNumber))
            {
                errorMessage = "Mobile Number is required";
                return false;
            }
            else if (userNameBasedOn == UserNameRegistrationTypeEnum.EmailId.ToString() && string.IsNullOrEmpty(generalLeadGenerationModel.EmailId))
            {
                errorMessage = "EmailId is required";
                return false;
            }
            else if (userNameBasedOn == UserNameRegistrationTypeEnum.MobileNumber.ToString())
            {
                if (_userMasterRepository.Table.Any(x => x.UserName == generalLeadGenerationModel.MobileNumber && x.UserType.ToLower() == generalLeadGenerationModel.UserTypeCode.ToLower()))
                {
                    errorMessage = "Mobile Number is already exist.";
                    return false;
                }
            }
            else if (userNameBasedOn == UserNameRegistrationTypeEnum.EmailId.ToString())
            {
                if (_userMasterRepository.Table.Any(x => x.UserName == generalLeadGenerationModel.EmailId && x.UserType.ToLower() == generalLeadGenerationModel.UserTypeCode.ToLower()))
                {
                    errorMessage = "EmailId is already exist.";
                    return false;
                }
            }
            return true;
        }

        //Get LeadGeneration by LeadGeneration id.
        public virtual GeneralLeadGenerationModel GetLeadGeneration(long LeadGenerationId)
        {
            if (LeadGenerationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LeadGenerationID"));

            //Get the LeadGeneration Details based on id.
            GeneralLeadGenerationMaster generalLeadGenerationMaster = _generalLeadGenerationMasterRepository.Table.FirstOrDefault(x => x.GeneralLeadGenerationMasterId == LeadGenerationId);
            GeneralLeadGenerationModel generalLeadGenerationModel = generalLeadGenerationMaster?.FromEntityToModel<GeneralLeadGenerationModel>();
            return generalLeadGenerationModel;
        }

        //Update LeadGeneration.
        public virtual bool UpdateLeadGeneration(GeneralLeadGenerationModel generalLeadGenerationModel)
        {
            if (IsNull(generalLeadGenerationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalLeadGenerationModel.GeneralLeadGenerationMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "LeadGenerationID"));

            if (IsLeadGenerationAlreadyExist(generalLeadGenerationModel))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "FirstName"));

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
        protected virtual bool IsLeadGenerationAlreadyExist(GeneralLeadGenerationModel generalLeadGenerationModel)
         => _generalLeadGenerationMasterRepository.Table.Any(x => x.FirstName == generalLeadGenerationModel.FirstName && x.LastName == generalLeadGenerationModel.LastName && x.MobileNumber == generalLeadGenerationModel.MobileNumber && (x.GeneralLeadGenerationMasterId != generalLeadGenerationModel.GeneralLeadGenerationMasterId || generalLeadGenerationModel.GeneralLeadGenerationMasterId == 0));
        #endregion
    }
}
