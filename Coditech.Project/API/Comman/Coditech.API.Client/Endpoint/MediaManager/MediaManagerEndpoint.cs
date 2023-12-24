using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class MediaManagerEndpoint : BaseEndpoint
    {
        public string UploadMediaAsync() =>
                   $"{CoditechAdminSettings.CoditechMediaManagerApiRootUri}/MediaManager/UploadMedia";
    }
}
