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

        public virtual GymWorkoutPlanModel GetWorkoutPlanDetails(long gymWorkoutPlanId)
        {
            if (gymWorkoutPlanId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymWorkoutPlanId"));

            GymWorkoutPlan gymWorkoutPlan = _gymWorkoutPlanRepository.Table.FirstOrDefault(x => x.GymWorkoutPlanId == gymWorkoutPlanId);
            GymWorkoutPlanModel gymWorkoutPlanmodel = new GymWorkoutPlanModel();
            gymWorkoutPlanmodel = gymWorkoutPlan?.FromEntityToModel<GymWorkoutPlanModel>();
            List<GymWorkoutPlanDetails> gymWorkoutPlanDetailsList = _gymWorkoutPlanDetailsRepository.Table.Where(x => x.GymWorkoutPlanId == gymWorkoutPlanId)?.ToList();
            if (gymWorkoutPlanDetailsList?.Count > 0)
            {
                List<long> gymWorkoutPlanDetailIds = gymWorkoutPlanDetailsList.Select(y => y.GymWorkoutPlanDetailId).ToList();
                List<GymWorkoutPlanSet> gymWorkoutPlanSetlist = _gymWorkoutPlanSetRepository.Table.Where(x => gymWorkoutPlanDetailIds.Contains(x.GymWorkoutPlanDetailId))?.ToList();
                gymWorkoutPlanmodel.GymWorkoutPlanDetailList = new List<GymWorkoutPlanDetailsModel>();
                foreach (GymWorkoutPlanDetails item in gymWorkoutPlanDetailsList)
                {
                    GymWorkoutPlanDetailsModel gymWorkoutPlanDetailsModel = new GymWorkoutPlanDetailsModel()
                    {
                        GymWorkoutPlanDetailId = item.GymWorkoutPlanDetailId,
                        GymWorkoutPlanId = item.GymWorkoutPlanId,
                        WorkoutName = item.WorkoutName,
                        WorkoutWeek = item.WorkoutWeek,
                        WorkoutDay = item.WorkoutDay,
                    };
                    gymWorkoutPlanDetailsModel.GymWorkoutPlanSetList = new List<GymWorkoutPlanSetModel>();
                    foreach (GymWorkoutPlanSet item2 in gymWorkoutPlanSetlist.Where(x => x.GymWorkoutPlanDetailId == item.GymWorkoutPlanDetailId))
                    {
                        gymWorkoutPlanDetailsModel.GymWorkoutPlanSetList.Add(new GymWorkoutPlanSetModel()
                        {
                            GymWorkoutSetId = item2.GymWorkoutSetId,
                            GymWorkoutPlanDetailId = item2.GymWorkoutPlanDetailId,
                            Weight = item2.Weight,
                            Repetitions = item2.Repetitions,
                            Duration = item2.Duration
                        });
                    }
                    gymWorkoutPlanmodel.GymWorkoutPlanDetailList.Add(gymWorkoutPlanDetailsModel);
                }
            }
            return gymWorkoutPlanmodel;
        }

        //Create Workout Plan Details for set.
        public virtual GymWorkoutPlanDetailsModel AddWorkoutPlanDetails(GymWorkoutPlanDetailsModel gymWorkoutPlanDetailsModel)
        {
            if (IsNull(gymWorkoutPlanDetailsModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            //if (IsWorkoutPlanAlreadyExist(gymWorkoutPlanSetModel.WorkoutPlanName))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Workout Plan"));

            GymWorkoutPlanDetails gymWorkoutPlanDetails = gymWorkoutPlanDetailsModel.FromModelToEntity<GymWorkoutPlanDetails>();

            //Create new Workout plan and return it.
            GymWorkoutPlanDetails gymWorkoutPlanDetailsData = _gymWorkoutPlanDetailsRepository.Insert(gymWorkoutPlanDetails);
            if (gymWorkoutPlanDetailsData?.GymWorkoutPlanDetailId > 0)
            {
                gymWorkoutPlanDetailsModel.GymWorkoutPlanDetailId = gymWorkoutPlanDetailsData.GymWorkoutPlanDetailId;
                InsertGymWorkoutPlanSet(gymWorkoutPlanDetailsModel);
            }
            else
            {
                gymWorkoutPlanDetailsModel.HasError = true;
                gymWorkoutPlanDetailsModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return gymWorkoutPlanDetailsModel;
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

        protected virtual void InsertGymWorkoutPlanSet(GymWorkoutPlanDetailsModel gymWorkoutPlanDetailsModel)
        {
            if (gymWorkoutPlanDetailsModel?.GymWorkoutPlanSetList?.Count > 0)
            {
                List<GymWorkoutPlanSet> workoutPlanInsertList = new List<GymWorkoutPlanSet>();

                foreach (GymWorkoutPlanSetModel item in gymWorkoutPlanDetailsModel.GymWorkoutPlanSetList)
                {
                    workoutPlanInsertList.Add(new GymWorkoutPlanSet()
                    {
                        GymWorkoutPlanDetailId = gymWorkoutPlanDetailsModel.GymWorkoutPlanDetailId,
                        Weight = item.Weight,
                        Repetitions = item.Repetitions,
                        Duration = item.Duration
                    });
                }

                if (workoutPlanInsertList.Count > 0)
                    _gymWorkoutPlanSetRepository.Insert(workoutPlanInsertList);
            }
        }
        #endregion
    }
}
