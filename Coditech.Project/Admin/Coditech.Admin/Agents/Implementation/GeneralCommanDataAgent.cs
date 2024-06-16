using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Logger;

namespace Coditech.Admin.Agents
{
    public class GeneralCommanDataAgent : BaseAgent, IGeneralCommanDataAgent
    {
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IMediaManagerClient _mediaManagerClient;
        public GeneralCommanDataAgent(ICoditechLogging coditechLogging, IMediaManagerClient mediaManagerClient) 
        {
            _coditechLogging = coditechLogging;
            _mediaManagerClient = GetClient<IMediaManagerClient>(mediaManagerClient);
        }

        /// <summary>
        /// Upload Images
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public MediaManagerResponse UploadImage(IFormFile file)
        {
            UploadMediaModel uploadMediaModel = new UploadMediaModel();
            uploadMediaModel.MediaFile = file;
            return _mediaManagerClient.UploadMedia(uploadMediaModel);
        }

    }
}
