using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGymWorkoutPlanService
    {
        GymWorkoutPlanListModel GetGymWorkoutPlanList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GymWorkoutPlanModel CreateGymWorkoutPlan(GymWorkoutPlanModel model);
        GymWorkoutPlanModel GetGymWorkoutPlan(long gymWorkoutPlanId);
        bool UpdateGymWorkoutPlan(GymWorkoutPlanModel model);
        bool DeleteGymWorkoutPlan(ParameterModel parameterModel);
        GymWorkoutPlanDetailsModel GetWorkoutPlanDetails(long gymWorkoutPlanId);
    }
}
