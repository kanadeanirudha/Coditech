using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralTrainerMasterService
    {
        GeneralTrainerListModel GetTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralTrainerModel CreateTrainer(GeneralTrainerModel model);
        GeneralTrainerModel GetTrainer(long generalTrainerId);
        bool UpdateTrainer(GeneralTrainerModel model);
        bool DeleteTrainer(ParameterModel parameterModel);
        GeneralTraineeAssociatedToTrainerListModel GetAssociatedTrainerList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, long entityId, string userType, long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralTraineeAssociatedToTrainerModel InsertAssociatedTrainer(GeneralTraineeAssociatedToTrainerModel model);
        GeneralTraineeAssociatedToTrainerModel GetAssociatedTrainer(long generalTraineeAssociatedToTrainerId);
        bool UpdateAssociatedTrainer(GeneralTraineeAssociatedToTrainerModel model);
        bool DeleteAssociatedTrainer(ParameterModel parameterModel);
    }
}
