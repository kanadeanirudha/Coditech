using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IAccSetupGLTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of AccSetupGL Type.
        /// </summary>
        /// <returns>AccSetupGLTypeListResponse</returns>
        AccSetupGLTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

    }
}
