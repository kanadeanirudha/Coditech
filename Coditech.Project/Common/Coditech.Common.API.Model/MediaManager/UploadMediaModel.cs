using Microsoft.AspNetCore.Http;
namespace Coditech.Common.API.Model
{
    public class UploadMediaModel : BaseModel
    {
        public List<IFormFile> MediaFileList { get; set; }
    }
}
