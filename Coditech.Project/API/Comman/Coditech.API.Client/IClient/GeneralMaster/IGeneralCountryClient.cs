using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralCountryClient : IBaseClient
    {
        /// <summary>
        /// Gets list of General Country.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralCountryListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
    }
}
