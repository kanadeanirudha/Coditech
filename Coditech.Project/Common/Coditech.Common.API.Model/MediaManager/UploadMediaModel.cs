using Microsoft.AspNetCore.Http;
namespace Coditech.Common.API.Model
{
    public class UploadMediaModel : BaseModel
    {
        public IFormFile MediaFile { get; set; }
        public string MediaType { get; set; }
        public string MediaFolderName{ get; set; }
    }
}
