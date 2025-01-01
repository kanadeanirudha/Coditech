using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GeneralCommonAgent : BaseAgent, IGeneralCommonAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IMediaManagerClient _mediaManagerClient;
        private readonly IGeneralCommonClient _generalCommonClient;
        #endregion

        #region Public Constructor
        public GeneralCommonAgent(ICoditechLogging coditechLogging, IMediaManagerClient mediaManagerClient, IGeneralCommonClient generalCommonClient)
        {
            _coditechLogging = coditechLogging;
            _mediaManagerClient = GetClient<IMediaManagerClient>(mediaManagerClient);
            _generalCommonClient = GetClient<IGeneralCommonClient>(generalCommonClient);
        }
        #endregion

        #region Public Methods
        //Send OTP.
        public virtual GeneralMessagesViewModel SendOTP(GeneralMessagesViewModel generalMessagesViewModel)
        {
            try
            {
                GeneralMessagesResponse response = _generalCommonClient.SendOTP(generalMessagesViewModel.ToModel<GeneralMessagesModel>());
                GeneralMessagesModel generalMessagesModel = response?.GeneralMessagesModel;
                return IsNotNull(generalMessagesModel) ? generalMessagesModel.ToViewModel<GeneralMessagesViewModel>() : new GeneralMessagesViewModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        return (GeneralMessagesViewModel)GetViewModelWithErrorMessage(generalMessagesViewModel, ex.ErrorMessage);
                    default:
                        return (GeneralMessagesViewModel)GetViewModelWithErrorMessage(generalMessagesViewModel, GeneralResources.UpdateErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Error);
                return (GeneralMessagesViewModel)GetViewModelWithErrorMessage(generalMessagesViewModel, GeneralResources.UpdateErrorMessage);
            }
        }

        //Media Manager 
        public MediaManagerResponse UploadImage(IFormFile file)
        {
            MediaModel uploadMediaModel = new MediaModel();
            uploadMediaModel.MediaFile = file;
            return _mediaManagerClient.UploadMedia(0, "PersonImages", 0, uploadMediaModel);
        }

        //CoditechApplicationSetting
        public virtual CoditechApplicationSettingListViewModel GetCoditechApplicationSettingList(string applicationCodes)
        {
            CoditechApplicationSettingListResponse response = _generalCommonClient.GetCoditechApplicationSettingList(applicationCodes);

            CoditechApplicationSettingListModel applicationSettingList = new CoditechApplicationSettingListModel { CoditechApplicationSettingList = response?.CoditechApplicationSettingList };
            CoditechApplicationSettingListViewModel listViewModel = new CoditechApplicationSettingListViewModel();
            listViewModel.CoditechApplicationSettingList = applicationSettingList?.CoditechApplicationSettingList?.ToViewModel<CoditechApplicationSettingViewModel>().ToList();
            return listViewModel;
        }
        #endregion
    }
}
