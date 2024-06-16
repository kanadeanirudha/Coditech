using Coditech.Common.API.Model.Responses;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCommanDataAgent
    {
        /// <summary>
        /// Upload Images
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        MediaManagerResponse UploadImage(IFormFile file);
    }
}
