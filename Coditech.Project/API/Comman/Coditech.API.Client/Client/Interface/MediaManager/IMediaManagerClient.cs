using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IMediaManagerClient : IBaseClient
    {
        /// <summary>
        /// Upload Media
        /// </summary>
        /// <returns>UploadMediaModel</returns>
        MediaManagerResponse UploadMedia(UploadMediaModel model);
    }
}
