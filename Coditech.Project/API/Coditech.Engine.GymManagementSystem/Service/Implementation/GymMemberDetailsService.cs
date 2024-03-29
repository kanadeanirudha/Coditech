﻿
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
    public class GymMemberDetailsService : BaseService, IGymMemberDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GymMemberDetails> _gymMemberDetailsRepository;
        private readonly ICoditechRepository<GymMemberFollowUp> _gymMemberFollowUpRepository;
        public GymMemberDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymMemberDetailsRepository = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberFollowUpRepository = new CoditechRepository<GymMemberFollowUp>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymMemberDetailsListModel GetGymMemberDetailsList(string SelectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMemberDetailsModel> objStoredProc = new CoditechViewRepository<GymMemberDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", SelectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMemberDetailsModel> gymMemberList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMemberDetailsList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GymMemberDetailsListModel listModel = new GymMemberDetailsListModel();

            listModel.GymMemberDetailsList = gymMemberList?.Count > 0 ? gymMemberList : new List<GymMemberDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get Gym Member Other Details
        public virtual GymMemberDetailsModel GetGymMemberOtherDetails(int gymMemberDetailId)
        {
            if (gymMemberDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            GymMemberDetails gymMemberDetails = _gymMemberDetailsRepository.Table.FirstOrDefault(x => x.GymMemberDetailId == gymMemberDetailId);
            GymMemberDetailsModel gymMemberDetailsModel = gymMemberDetails?.FromEntityToModel<GymMemberDetailsModel>();
            if (IsNotNull(gymMemberDetailsModel))
            {
                GeneralPerson generalPerson = GetGeneralPersonDetails(gymMemberDetailsModel.PersonId);
                if (IsNotNull(gymMemberDetailsModel))
                {
                    gymMemberDetailsModel.FirstName = generalPerson.FirstName;
                    gymMemberDetailsModel.LastName = generalPerson.LastName;
                }
            }
            return gymMemberDetailsModel;
        }

        //Update Gym Member Other Details
        public virtual bool UpdateGymMemberOtherDetails(GymMemberDetailsModel gymMemberDetailsModel)
        {
            if (IsNull(gymMemberDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymMemberDetailsModel.GymMemberDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            GymMemberDetails gymMemberDetails = _gymMemberDetailsRepository.Table.FirstOrDefault(x => x.GymMemberDetailId == gymMemberDetailsModel.GymMemberDetailId);

            bool isUpdated = false;
            if (IsNull(gymMemberDetails))
            {
                return isUpdated;
            }
            gymMemberDetails.PastInjuries = gymMemberDetailsModel.PastInjuries;
            gymMemberDetails.MedicalHistory = gymMemberDetailsModel.MedicalHistory;
            gymMemberDetails.GymGroupEnumId = gymMemberDetailsModel.GymGroupEnumId;
            gymMemberDetails.SourceEnumId = gymMemberDetailsModel.SourceEnumId;
            gymMemberDetails.OtherInformation = gymMemberDetailsModel.OtherInformation;
            gymMemberDetails.IsActive = gymMemberDetailsModel.IsActive;

            isUpdated = _gymMemberDetailsRepository.Update(gymMemberDetails);
            if (!isUpdated)
            {
                gymMemberDetailsModel.HasError = true;
                gymMemberDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUpdated;
        }

        //Delete Gym Members
        public virtual bool DeleteGymMembers(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymMemberDetailIds", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymMembers @GymMemberDetailIds,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Member Follow Up
        public virtual GymMemberFollowUpListModel GymMemberFollowUpList(int gymMemberDetailId, long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging FollowUp.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMemberFollowUpModel> objStoredProc = new CoditechViewRepository<GymMemberFollowUpModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@GymMemberDetailId", gymMemberDetailId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMemberFollowUpModel> gymMemberList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMemberFollowUpList @GymMemberDetailId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GymMemberFollowUpListModel listModel = new GymMemberFollowUpListModel();

            listModel.GymMemberFollowUpList = gymMemberList?.Count > 0 ? gymMemberList : new List<GymMemberFollowUpModel>();
            listModel.BindPageListModel(pageListModel);

            GeneralPerson generalPerson = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPerson))
            {
                listModel.FirstName = generalPerson.FirstName;
                listModel.LastName = generalPerson.LastName;
            }
            listModel.GymMemberDetailId = gymMemberDetailId;
            listModel.PersonId = personId;
            return listModel;
        }

        public virtual GymMemberFollowUpModel GetGymMemberFollowUp(long gymMemberFollowUpId)
        {
            if (gymMemberFollowUpId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "gymMemberFollowUpId"));

            GymMemberFollowUp gymMemberFollowUp = _gymMemberFollowUpRepository.Table.FirstOrDefault(x => x.GymMemberFollowUpId == gymMemberFollowUpId);
            GymMemberFollowUpModel gymMemberFollowUpModel = gymMemberFollowUp?.FromEntityToModel<GymMemberFollowUpModel>();
            return gymMemberFollowUpModel;
        }

        public virtual bool InserUpdateGymMemberFollowUp(GymMemberFollowUpModel gymMemberFollowUpModel)
        {
            if (IsNull(gymMemberFollowUpModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            gymMemberFollowUpModel.ReminderDate = gymMemberFollowUpModel.IsSetReminder ? gymMemberFollowUpModel.ReminderDate : null;
            bool isGymMemberFollowUpUpdated = false;
            GymMemberFollowUp gymMemberFollowUp = gymMemberFollowUpModel.FromModelToEntity<GymMemberFollowUp>();

            if (gymMemberFollowUpModel.GymMemberFollowUpId > 0)
            {
                isGymMemberFollowUpUpdated = _gymMemberFollowUpRepository.Update(gymMemberFollowUp);
            }
            else
            {
                GymMemberFollowUp gymMemberFollowUpData = _gymMemberFollowUpRepository.Insert(gymMemberFollowUp);
                isGymMemberFollowUpUpdated = gymMemberFollowUpData?.GymMemberFollowUpId > 0 ? true : false;
            }

            if (!isGymMemberFollowUpUpdated)
            {
                gymMemberFollowUpModel.HasError = true;
                gymMemberFollowUpModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isGymMemberFollowUpUpdated;
        }

        public virtual bool DeleteGymMemberFollowUp(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberFollowUpID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymMemberFollowUpId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymMemberFollowUp @GymMemberFollowUpId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
        #endregion

        #region Gym Member Attendance


        #endregion
    }
}
