using Coditech.Common.API.Model.Response;

namespace Coditech.API.Client
{
    public interface IAccSetupCategoryClient : IBaseClient
    {
        AccSetupCategoryListResponse GetAccSetupCategory();
    }
}
