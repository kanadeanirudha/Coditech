
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Collections.Specialized;
using System.Data;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class AccGLSetupNarrationService : IAccGLSetupNarrationService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<AccGLSetupNarration> _accGLSetupNarrationRepository;
        public AccGLSetupNarrationService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _accGLSetupNarrationRepository = new CoditechRepository<AccGLSetupNarration>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual AccGLSetupNarrationListModel GetNarrationList(string selectedCentreCode)
        {
            AccGLSetupNarrationListModel listModel = new AccGLSetupNarrationListModel();

            List<AccGLSetupNarration> list = _accGLSetupNarrationRepository.Table.Where(x => x.CentreCode == selectedCentreCode)?.ToList();
            listModel.AccGLSetupNarrationList = new List<AccGLSetupNarrationModel>();
            listModel.AccGLSetupNarrationList.Add(new AccGLSetupNarrationModel()
            {
                NarrationType = "Test Mode",
                IsSystemGenerated = false,
                AccGLSetupNarrationId = list?.Count() > 0 ? Convert.ToByte(list.FirstOrDefault(x => !x.IsSystemGenerated)?.AccGLSetupNarrationId) : (byte)0
            });
            listModel.AccGLSetupNarrationList.Add(new AccGLSetupNarrationModel()
            {
                NarrationType = "Live Mode",
                IsSystemGenerated = true,
                AccGLSetupNarrationId = list?.Count() > 0 ? Convert.ToByte(list.FirstOrDefault(x => x.IsSystemGenerated)?.AccGLSetupNarrationId) : (byte)0
            });
            return listModel;
        }
        //Create Narration.
        public virtual AccGLSetupNarrationModel CreateNarration(AccGLSetupNarrationModel accGLSetupNarrationModel)
        {
            accGLSetupNarrationModel.CentreCode = accGLSetupNarrationModel.CentreCode;
            accGLSetupNarrationModel.IsSystemGenerated = accGLSetupNarrationModel.IsSystemGenerated;

                if (IsNull(accGLSetupNarrationModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
           

            //if (IsNarrationIDAlreadyExist(AccGLSetupNarration.NarrationType))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Narration Type"));

            AccGLSetupNarration accGLSetupNarration = accGLSetupNarrationModel.FromModelToEntity<AccGLSetupNarration>();

            //Create new Narration and return it.
            AccGLSetupNarration narrationData = _accGLSetupNarrationRepository.Insert(accGLSetupNarration);
            if (narrationData?.AccGLSetupNarrationId > 0)
            {
                accGLSetupNarrationModel.AccGLSetupNarrationId = narrationData.AccGLSetupNarrationId;
            }
            else
            {
                accGLSetupNarrationModel.HasError = true;
                accGLSetupNarrationModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return accGLSetupNarrationModel;
        }

        //Get Narration by Narration id.
        public virtual AccGLSetupNarrationModel GetNarration(int accGLSetupNarrationId)
        {
            if (accGLSetupNarrationId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NarrationID"));

            //Get the Narration Details based on id.
            AccGLSetupNarration accGLSetupNarration = _accGLSetupNarrationRepository.Table.FirstOrDefault(x => x.AccGLSetupNarrationId == accGLSetupNarrationId);
            AccGLSetupNarrationModel accGLSetupNarrationModel = accGLSetupNarration?.FromEntityToModel<AccGLSetupNarrationModel>();
            return accGLSetupNarrationModel;
        }
        //Update NArration.
        public virtual bool UpdateNarration(AccGLSetupNarrationModel accGLSetupNarrationModel)
        {
            if (IsNull(accGLSetupNarrationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (accGLSetupNarrationModel.AccGLSetupNarrationId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "NarrationID"));

            //if (IsNarrationIDAlreadyExist(accGLSetupNarrationModel.NarrationType, accGLSetupNarrationModel.AccGLSetupNarrationId))
            //    throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Narration Type"));

            AccGLSetupNarration accGLSetupNarration = accGLSetupNarrationModel.FromModelToEntity<AccGLSetupNarration>();

            //Update Narration
            bool isNarrationUpdated = _accGLSetupNarrationRepository.Update(accGLSetupNarration);
            if (!isNarrationUpdated)
            {
                accGLSetupNarrationModel.HasError = true;
                accGLSetupNarrationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isNarrationUpdated;
        }

        //Delete Narration.
        

        #region Protected Method
        //Check if Narration ID is already present or not.
        //protected virtual bool IsNarrationIDAlreadyExist(int AccGLSetupNarrationId, int accGLSetupNarrationId = 0)
        // => _accGLSetupNarrationRepository.Table.Any(x => x.AccGLSetupNarrationId == accGLSetupNarrationId && (x.AccGLSetupNarrationId != accGLSetupNarrationId || accGLSetupNarrationId == 0));
        #endregion
    }
}
