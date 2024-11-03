using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class GymWorkoutPlanService : BaseService, IGymWorkoutPlanService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GymWorkoutPlan> _gymWorkoutPlanRepository;
        private readonly ICoditechRepository<GymWorkoutPlanSet> _gymWorkoutPlanSetRepository;
        private readonly ICoditechRepository<GymWorkoutPlanDetails> _gymWorkoutPlanDetailsRepository;

        public GymWorkoutPlanService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymWorkoutPlanRepository = new CoditechRepository<GymWorkoutPlan>(_serviceProvider.GetService<Coditech_Entities>());
            _gymWorkoutPlanDetailsRepository = new CoditechRepository<GymWorkoutPlanDetails>(_serviceProvider.GetService<Coditech_Entities>());
            _gymWorkoutPlanSetRepository = new CoditechRepository<GymWorkoutPlanSet>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymWorkoutPlanListModel GetGymWorkoutPlanList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {

            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);

            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymWorkoutPlanModel> objStoredProc = new CoditechViewRepository<GymWorkoutPlanModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymWorkoutPlanModel> gymWorkoutPlanList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymWorkoutPlanList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            GymWorkoutPlanListModel listModel = new GymWorkoutPlanListModel();

            listModel.GymWorkoutPlanList = gymWorkoutPlanList?.Count > 0 ? gymWorkoutPlanList : new List<GymWorkoutPlanModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Gym Workout Plan.
        public virtual GymWorkoutPlanModel CreateGymWorkoutPlan(GymWorkoutPlanModel gymWorkoutPlanModel)
        {
            if (IsNull(gymWorkoutPlanModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsWorkoutPlanAlreadyExist(gymWorkoutPlanModel.WorkoutPlanName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Workout Plan"));

            GymWorkoutPlan gymWorkoutPlan = gymWorkoutPlanModel.FromModelToEntity<GymWorkoutPlan>();

            //Create new Workout plan and return it.
            GymWorkoutPlan gymWorkoutPlanData = _gymWorkoutPlanRepository.Insert(gymWorkoutPlan);
            if (gymWorkoutPlanData?.GymWorkoutPlanId > 0)
            {
                gymWorkoutPlanModel.GymWorkoutPlanId = gymWorkoutPlanData.GymWorkoutPlanId;
            }
            else
            {
                gymWorkoutPlanModel.HasError = true;
                gymWorkoutPlanModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gymWorkoutPlanModel;
        }

        //Get Gym Workout Plan
        public virtual GymWorkoutPlanModel GetGymWorkoutPlan(long gymWorkoutPlanId)
        {
            if (gymWorkoutPlanId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymWorkoutPlanId"));

            GymWorkoutPlan gymWorkoutPlan = _gymWorkoutPlanRepository.Table.FirstOrDefault(x => x.GymWorkoutPlanId == gymWorkoutPlanId);
            GymWorkoutPlanModel gymWorkoutPlanModel = gymWorkoutPlan?.FromEntityToModel<GymWorkoutPlanModel>();
            return gymWorkoutPlanModel;
        }

        //Update Gym Workout Plan
        public virtual bool UpdateGymWorkoutPlan(GymWorkoutPlanModel gymWorkoutPlanModel)
        {
            if (IsNull(gymWorkoutPlanModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymWorkoutPlanModel.GymWorkoutPlanId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymWorkoutPlanId"));

            GymWorkoutPlan gymWorkoutPlan = _gymWorkoutPlanRepository.Table.FirstOrDefault(x => x.GymWorkoutPlanId == gymWorkoutPlanModel.GymWorkoutPlanId);

            bool isUpdated = false;
            if (IsNull(gymWorkoutPlan))
            {
                return isUpdated;
            }
            gymWorkoutPlan.WorkoutPlanName = gymWorkoutPlanModel.WorkoutPlanName;
            gymWorkoutPlan.Target = gymWorkoutPlanModel.Target;

            isUpdated = _gymWorkoutPlanRepository.Update(gymWorkoutPlan);
            if (!isUpdated)
            {
                gymWorkoutPlanModel.HasError = true;
                gymWorkoutPlanModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUpdated;
        }

        public virtual GymWorkoutPlanDetailsModel GetWorkoutPlanDetails(long gymWorkoutPlanId)
        {
            if (gymWorkoutPlanId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymWorkoutPlanId"));

            GymWorkoutPlan gymWorkoutPlan = _gymWorkoutPlanRepository.Table.FirstOrDefault(x => x.GymWorkoutPlanId == gymWorkoutPlanId);
            GymWorkoutPlanDetailsModel gymWorkoutPlanDetailsModel = new GymWorkoutPlanDetailsModel() ;

            if (IsNotNull(gymWorkoutPlan))
            {
                gymWorkoutPlanDetailsModel.WorkoutName = gymWorkoutPlan.WorkoutPlanName;
       
            }
            gymWorkoutPlanDetailsModel.GymWorkoutPlanModel = gymWorkoutPlan?.FromEntityToModel<GymWorkoutPlanModel>();
            return gymWorkoutPlanDetailsModel; 
        }


        //Create Workout Plan Details for set.
        public virtual GymWorkoutPlanSetModel AddWorkoutPlanDetails(GymWorkoutPlanSetModel gymWorkoutPlanSetModel)
        {
            if (IsNull(gymWorkoutPlanSetModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsWorkoutPlanAlreadyExist(gymWorkoutPlanSetModel.WorkoutPlanName))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Workout Plan"));

            GymWorkoutPlanSet gymWorkoutPlanSet = gymWorkoutPlanSetModel.FromModelToEntity<GymWorkoutPlanSet>();

            //Create new Workout plan and return it.
            GymWorkoutPlanSet gymWorkoutPlanSetData = _gymWorkoutPlanSetRepository.Insert(gymWorkoutPlanSet);
            if (gymWorkoutPlanSetData?.GymWorkoutPlanDetailId > 0)
            {
                gymWorkoutPlanSetModel.GymWorkoutPlanDetailId = gymWorkoutPlanSetData.GymWorkoutPlanDetailId;
                InsertUpdateGymWorkoutPlan(gymWorkoutPlanSetModel);
            }
            else
            {
                gymWorkoutPlanSetModel.HasError = true;
                gymWorkoutPlanSetModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gymWorkoutPlanSetModel;
        }
      

        //Delete Gym Workout Plan
        public virtual bool DeleteGymWorkoutPlan(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymWorkoutPlanId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymWorkoutPlanIds", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymWorkoutPlan @GymWorkoutPlanIds,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method

        //Check if Workout Plan is already present or not.
        protected virtual bool IsWorkoutPlanAlreadyExist(string workoutPlanName, long gymWorkoutPlanId = 0)

        => _gymWorkoutPlanRepository.Table.Any(x => x.WorkoutPlanName == workoutPlanName && (x.GymWorkoutPlanId != gymWorkoutPlanId || gymWorkoutPlanId == 0));


       
        protected virtual void InsertUpdateGymWorkoutPlan(GymWorkoutPlanSetModel gymWorkoutPlanSetModel)
        {
            if (gymWorkoutPlanSetModel?.GymWorkoutPlanDetailsList?.Count > 0)
            {
                List<GymWorkoutPlanSet> workoutPlanInsertList = new List<GymWorkoutPlanSet>();
                List<GymWorkoutPlanSet> workoutPlanUpdateList = new List<GymWorkoutPlanSet>();

                foreach (GymWorkoutPlanDetailsModel item in gymWorkoutPlanSetModel.GymWorkoutPlanDetailsList)
                {
                    GymWorkoutPlanSet workoutPlanSet = item.FromModelToEntity<GymWorkoutPlanSet>();
                    workoutPlanSet.GymWorkoutPlanDetailId = gymWorkoutPlanSetModel.GymWorkoutPlanDetailId;

                    if (item.GymWorkoutPlanDetailId > 0)
                        workoutPlanUpdateList.Add(workoutPlanSet);
                    else
                        workoutPlanInsertList.Add(workoutPlanSet);
                }

                if (workoutPlanInsertList.Count > 0)
                    _gymWorkoutPlanSetRepository.Insert(workoutPlanInsertList);

                if (workoutPlanUpdateList.Count > 0)
                    _gymWorkoutPlanSetRepository.BatchUpdate(workoutPlanUpdateList);
            }
        }

        #endregion


    }
}
