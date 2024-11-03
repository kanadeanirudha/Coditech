using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGymWorkoutPlanClient : IBaseClient
    {
        /// <summary>
        /// Get list of GymWorkoutPlan.
        /// </summary>
        /// <returns>GymWorkoutPlanListResponse</returns>
        GymWorkoutPlanListResponse List(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create GymWorkoutPlan.
        /// </summary>
        /// <param name="GymWorkoutPlanModel">GymWorkoutPlanModel.</param>
        /// <returns>Returns GymWorkoutPlanResponse.</returns>
        GymWorkoutPlanResponse CreateGymWorkoutPlan(GymWorkoutPlanModel body);

        /// <summary>
        /// Get GymWorkoutPlan by gymWorkoutPlanId.
        /// </summary>
        /// <param name="gymWorkoutPlanId">gymWorkoutPlanId</param>
        /// <returns>Returns GymWorkoutPlanResponse.</returns>
        GymWorkoutPlanResponse GetGymWorkoutPlan(long gymWorkoutPlanId);

        /// <summary>
        /// Update Gym Workout Plan.
        /// </summary>
        /// <param name="GymWorkoutPlanModel">GymWorkoutPlanModel.</param>
        /// <returns>Returns updated GymWorkoutPlanResponse</returns>
        GymWorkoutPlanResponse UpdateGymWorkoutPlan(GymWorkoutPlanModel body);

        /// <summary>
        /// Delete GymWorkoutPlan.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGymWorkoutPlan(ParameterModel body);

        /// <summary>
        /// Get WorkoutPlanDetails by gymWorkoutPlanId.
        /// </summary>
        /// <param name="gymWorkoutPlanId">gymWorkoutPlanId</param>
        /// <returns>Returns WorkoutPlanDetailsResponse.</returns>
        GymWorkoutPlanDetailsResponse GetWorkoutPlanDetails(long gymWorkoutPlanId);

        /// <summary>
        /// Create WorkoutPlanDetails.
        /// </summary>
        /// <param name="GymWorkoutPlanSetModel">GymWorkoutPlanSetModel.</param>
        /// <returns>Returns GymWorkoutPlanSetResponse.</returns>
        GymWorkoutPlanSetResponse AddWorkoutPlanDetails(GymWorkoutPlanSetModel body);

        ////
        ///// <summary>
        ///// Retrieves a gym Workout Plan Detail by its ID and gym Workout Plan ID.
        ///// </summary>
        ///// <param name="gymWorkoutPlanDetailId">The ID of the gym Workout Plan Detail.</param>
        ///// <param name="gymWorkoutPlanId">The ID of the gym Workout Plan.</param>
        ///// <returns>Returns a response containing the gym Workout Plan Detail.</returns>
        //GymWorkoutPlanSetResponse GetWorkoutPlanDetailsPopup(long gymWorkoutPlanId);
    }
}
