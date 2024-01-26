using Coditech.Common.API.Model.Response;

namespace Coditech.Common.API.Model.Responses
{
    public class FileUploadListModelResponse : BaseListResponse
    {
        public List<FileUploadResponse> FileUpload { get; set; }
    }
}

