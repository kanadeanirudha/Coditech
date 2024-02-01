
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
    public class GymMembershipPlanService : BaseService, IGymMembershipPlanService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GymMembershipPlan> _gymMembershipPlanRepository;
        public GymMembershipPlanService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymMembershipPlanRepository = new CoditechRepository<GymMembershipPlan>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymMembershipPlanListModel GetGymMembershipPlanList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging shipPlan.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMembershipPlanModel> objStoredProc = new CoditechViewRepository<GymMembershipPlanModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMembershipPlanModel> gymMemberList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMembershipPlanList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GymMembershipPlanListModel listModel = new GymMembershipPlanListModel();

            listModel.GymMembershipPlanList = gymMemberList?.Count > 0 ? gymMemberList : new List<GymMembershipPlanModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get Gym Member Other shipPlan
        public virtual GymMembershipPlanModel GetGymMemberOthershipPlan(int gymMembershipPlanId)
        {
            if (gymMembershipPlanId <= 100)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));

            GymMembershipPlan gymMembershipPlan = _gymMembershipPlanRepository.Table.FirstOrDefault(x => x.GymMembershipPlanId == gymMembershipPlanId);
            GymMembershipPlanModel gymMembershipPlanModel = gymMembershipPlan?.FromEntityToModel<GymMembershipPlanModel>();
            if (IsNotNull(gymMembershipPlanModel))
            {
                GeneralPerson generalPerson = GetGeneralPersonshipPlan(gymMembershipPlanModel.PersonId);
                if (IsNotNull(gymMembershipPlanModel))
                {
                    gymMembershipPlanModel.FirstName = generalPerson.FirstName;
                    gymMembershipPlanModel.LastName = generalPerson.LastName;
                }
            }
            return gymMembershipPlanModel;
        }

        //Update Gym Member Other shipPlan
        public virtual bool UpdateGymMemberOthershipPlan(GymMembershipPlanModel gymMembershipPlanModel)
        {
            if (IsNull(gymMembershipPlanModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymMembershipPlanModel.GymMembershipPlanId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));

            GymMembershipPlan gymMembershipPlan = _gymMembershipPlanRepository.Table.FirstOrDefault(x => x.GymMembershipPlanId == gymMembershipPlanModel.GymMembershipPlanId);

            bool isUpdated = false;
            if (IsNull(gymMembershipPlan))
            {
                return isUpdated;
            }
            gymMembershipPlan.PastInjuries = gymMembershipPlanModel.PastInjuries;
            gymMembershipPlan.MedicalHistory = gymMembershipPlanModel.MedicalHistory;
            gymMembershipPlan.GymGroupEnumId = gymMembershipPlanModel.GymGroupEnumId;
            gymMembershipPlan.SourceEnumId = gymMembershipPlanModel.SourceEnumId;
            gymMembershipPlan.OtherInformation = gymMembershipPlanModel.OtherInformation;

            isUpdated = _gymMembershipPlanRepository.Update(gymMembershipPlan);
            if (!isUpdated)
            {
                gymMembershipPlanModel.HasError = true;
                gymMembershipPlanModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUpdated;
        }

        //Delete Gym Members
        public virtual bool DeleteGymMembers(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymMembershipPlanIds", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymMembers @GymMembershipPlanIds,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Member Follow Up
        public virtual GymMemberFollowUpListModel GymMemberFollowUpList(int gymMembershipPlanId, long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging FollowUp.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMemberFollowUpModel> objStoredProc = new CoditechViewRepository<GymMemberFollowUpModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@GymMembershipPlanId", gymMembershipPlanId, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMemberFollowUpModel> gymMemberList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMemberFollowUpList @GymMembershipPlanId, @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GymMemberFollowUpListModel listModel = new GymMemberFollowUpListModel();

            listModel.GymMemberFollowUpList = gymMemberList?.Count > 0 ? gymMemberList : new List<GymMemberFollowUpModel>();
            listModel.BindPageListModel(pageListModel);

            GeneralPerson generalPerson = GetGeneralPersonshipPlan(personId);
            if (IsNotNull(generalPerson))
            {
                listModel.FirstName = generalPerson.FirstName;
                listModel.LastName = generalPerson.LastName;
            }
            listModel.GymMembershipPlanId = gymMembershipPlanId;
            listModel.PersonId = personId;
            return listModel;
        }

        #endregion
    }
}
