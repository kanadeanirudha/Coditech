
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
        private readonly ICoditechRepository<GymMemberMembershipPlan> _gymMemberMembershipPlanRepository;
        public GymMembershipPlanService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymMembershipPlanRepository = new CoditechRepository<GymMembershipPlan>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberMembershipPlanRepository = new CoditechRepository<GymMemberMembershipPlan>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymMembershipPlanListModel GetGymMembershipPlanList(string SelectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging shipPlan.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMembershipPlanModel> objStoredProc = new CoditechViewRepository<GymMembershipPlanModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", SelectedCentreCode, ParameterDirection.Input, DbType.String); 
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMembershipPlanModel> gymMembershipPlanList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMembershipPlanList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GymMembershipPlanListModel listModel = new GymMembershipPlanListModel();

            listModel.GymMembershipPlanList = gymMembershipPlanList?.Count > 0 ? gymMembershipPlanList : new List<GymMembershipPlanModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }


        //Create Gym Membership Plan.
        public virtual GymMembershipPlanModel CreateGymMembershipPlan(GymMembershipPlanModel gymMembershipPlanModel)
        {
            if (IsNull(gymMembershipPlanModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            GymMembershipPlan gymMembershipPlan = gymMembershipPlanModel.FromModelToEntity<GymMembershipPlan>();

            //Create new Country and return it.
            GymMembershipPlan gymMembershipPlanData = _gymMembershipPlanRepository.Insert(gymMembershipPlan);
            if (gymMembershipPlanData?.GymMembershipPlanId > 0)
            {
                gymMembershipPlanModel.GymMembershipPlanId = gymMembershipPlanData.GymMembershipPlanId;
            }
            else
            {
                gymMembershipPlanModel.HasError = true;
                gymMembershipPlanModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gymMembershipPlanModel;
        }

        //Get Gym Membership Plan
        public virtual GymMembershipPlanModel GetGymMembershipPlan(int gymMembershipPlanId)
        {
            if (gymMembershipPlanId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));

            GymMembershipPlan gymMembershipPlan = _gymMembershipPlanRepository.Table.FirstOrDefault(x => x.GymMembershipPlanId == gymMembershipPlanId);
            GymMembershipPlanModel gymMembershipPlanModel = gymMembershipPlan?.FromEntityToModel<GymMembershipPlanModel>();
            gymMembershipPlanModel.IsEditable = !_gymMemberMembershipPlanRepository.Table.Any(x=>x.GymMembershipPlanId == gymMembershipPlanModel.GymMembershipPlanId);
            return gymMembershipPlanModel;
        }

        //Update Gym Membership Plan
        public virtual bool UpdateGymMembershipPlan(GymMembershipPlanModel gymMembershipPlanModel)
        {
            if (IsNull(gymMembershipPlanModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymMembershipPlanModel.GymMembershipPlanId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));


            GymMembershipPlan gymMembershipPlan = gymMembershipPlanModel.FromModelToEntity<GymMembershipPlan>();

            bool isUpdated = _gymMembershipPlanRepository.Update(gymMembershipPlan);
            if (!isUpdated)
            {
                gymMembershipPlanModel.HasError = true;
                gymMembershipPlanModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUpdated;
        }

        //Delete Gym Members
        public virtual bool DeleteGymMembershipPlan(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMembershipPlanId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymMembershipPlanIds", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymMembershipPlan @GymMembershipPlanIds,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }
    }
}
