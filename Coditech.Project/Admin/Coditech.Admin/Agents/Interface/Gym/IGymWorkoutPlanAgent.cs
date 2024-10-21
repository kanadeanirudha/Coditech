using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGymWorkoutPlanAgent
    {
        /// <summary>
        /// Get list of GymWorkoutPlan.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymWorkoutPlanListViewModel</returns>
        GymWorkoutPlanListViewModel GetGymWorkoutPlanList(DataTableViewModel dataTableModel, string listType = null);


        /// <summary>
        /// Create GymWorkoutPlan.
        /// </summary>
        /// <param name="gymWorkoutPlanViewModel">Gym Workout Plan View Model.</param>
        /// <returns>Returns created model.</returns>
        GymWorkoutPlanViewModel CreateGymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanViewModel);

        /// <summary>
        /// Get GymWorkoutPlan Details by gymWorkoutPlanId.
        /// </summary>
        /// <param name="gymWorkoutPlanId">gymWorkoutPlanId</param>
        /// <returns>Returns GymWorkoutPlanViewModel.</returns>
        GymWorkoutPlanViewModel GetGymWorkoutPlanDetails(long gymWorkoutPlanId);

        /// <summary>
        /// Update Gym Workout Plan Details.
        /// </summary>
        /// <param name="gymWorkoutPlanViewModel">gymWorkoutPlanViewModel.</param>
        /// <returns>Returns updated GymWorkoutPlanViewModel</returns>
        GymWorkoutPlanViewModel UpdateGymWorkoutPlanDetails(GymWorkoutPlanViewModel gymWorkoutPlanViewModel);


        /// <summary>
        /// Get GymWorkoutPlan by gymWorkoutPlanId.
        /// </summary>
        /// <param name="gymWorkoutPlanId">gymWorkoutPlanId</param>
        /// <returns>Returns GymWorkoutPlanResponse.</returns>
        GymWorkoutPlanViewModel GetGymWorkoutPlan(long gymWorkoutPlanId);

        /// <summary>
        /// Update Gym Workout Plan
        /// </summary>
        /// <param name="gymWorkoutPlanModel">GymWorkoutPlanModel.</param>
        /// <returns>Returns updated GymWorkoutPlanViewModel</returns>
        GymWorkoutPlanViewModel UpdateGymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanModel);


        /// <summary>
        /// Delete Gym Workout Plan.
        /// </summary>
        /// <param name="gymWorkoutPlanIds">gymWorkoutPlanIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteGymWorkoutPlan(string gymWorkoutPlanIds, out string errorMessage);

    }
}
