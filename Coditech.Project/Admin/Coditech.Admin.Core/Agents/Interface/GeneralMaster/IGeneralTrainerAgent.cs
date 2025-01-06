using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralTrainerAgent
    {
        /// <summary>
        /// Get list of Trainer.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralTrainerListViewModel</returns>
        GeneralTrainerListViewModel GetTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Trainer.
        /// </summary>
        /// <param name="generalTrainerViewModel">General Trainer View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralTrainerViewModel CreateTrainer(GeneralTrainerViewModel generalTrainerViewModel);


        /// <summary>
        /// Get Trainer by generalTrainerId.
        /// </summary>
        /// <param name="generalTrainerId">generalTrainerId</param>
        /// <returns>Returns GeneralTrainerViewModel.</returns>
        GeneralTrainerViewModel GetTrainer(long generalTrainerId);

        /// <summary>
        /// Update Trainer.
        /// </summary>
        /// <param name="generalTrainerViewModel">generalTrainerViewModel.</param>
        /// <returns>Returns updated GeneralTrainerViewModel</returns>
        GeneralTrainerViewModel UpdateTrainer(GeneralTrainerViewModel generalTrainerViewModel);

        /// <summary>
        /// Delete Trainer.
        /// </summary>
        /// <param name="generalTrainerIds">generalTrainerIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTrainer(string generalTrainerIds, out string errorMessage);
    }
}
