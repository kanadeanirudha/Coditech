using Microsoft.AspNetCore.Http;
namespace Coditech.Common.API.Model
{
    public class UploadMediaModel : BaseModel
    {
        public IFormFile MediaFile { get; set; }
        public string MediaType { get; set; }
        public string MediaFolderName{ get; set; }
        public int MediaFolderMasterId { get; set; }
        public long MediaId { get; set; }
        public string MediaPathUrl { get; set; }     
    }
}
