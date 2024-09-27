using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralTrainerClient : IBaseClient
    {
        /// <summary>
        /// Get list of Trainer.
        /// </summary>
        /// <returns>GeneralTrainerListResponse</returns>
        GeneralTrainerListResponse List(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Trainer.
        /// </summary>
        /// <param name="GeneralTrainerModel">GeneralTrainerModel.</param>
        /// <returns>Returns GeneralTrainerResponse.</returns>
        GeneralTrainerResponse CreateTrainer(GeneralTrainerModel body);

        /// <summary>
        /// Get Trainer by generalTrainerId.
        /// </summary>
        /// <param name="generalTrainerId">generalTrainerId</param>
        /// <returns>Returns GeneralTrainerResponse.</returns>
        GeneralTrainerResponse GetTrainer(long generalTrainerId);

        /// <summary>
        /// Update Trainer.
        /// </summary>
        /// <param name="GeneralTrainerModel">GeneralTrainerModel.</param>
        /// <returns>Returns updated GeneralTrainerResponse</returns>
        GeneralTrainerResponse UpdateTrainer(GeneralTrainerModel model);

        /// <summary>
        /// Delete Trainer.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTrainer(ParameterModel body);
    }
}
