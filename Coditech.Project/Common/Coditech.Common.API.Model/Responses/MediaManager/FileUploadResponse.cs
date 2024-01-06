namespace Coditech.Common.API.Model.Responses
{
    public class FileUploadResponse : BaseResponse
    {
        public int StatusCode { get; set; }
        public string FileName { get; set; }
        public string MediaId { get; set; }
        public string ImagePath { get; set; }
        public bool IsDocumentRemove { get; set; }
    }
}

