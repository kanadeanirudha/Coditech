using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Service
{
    public interface IMediaManagerService
    {
        UploadMediaModel UploadMedia(UploadMediaModel model);
        Task<MediaManagerResponse> UploadServerFiles(IEnumerable<IFormFile> files, HttpRequest request);
    }
}
