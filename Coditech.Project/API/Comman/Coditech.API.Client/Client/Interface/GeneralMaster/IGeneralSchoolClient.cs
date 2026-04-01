using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralSchoolClient : IBaseClient
    {
        /// <summary>
        /// Get list of General School.
        /// </summary>
        /// <returns>GeneralSchoolListResponse</returns>
        GeneralSchoolListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create School.
        /// </summary>
        /// <param name="GeneralSchoolModel">GeneralSchoolModel.</param>
        /// <returns>Returns GeneralSchoolResponse.</returns>
        GeneralSchoolResponse CreateSchool(GeneralSchoolModel body);

        /// <summary>
        /// Get School by generalSchoolId.
        /// </summary>
        /// <param name="generalSchoolId">generalSchoolId</param>
        /// <returns>Returns GeneralSchoolResponse.</returns>
        GeneralSchoolResponse GetSchool(short generalSchoolId);

        /// <summary>
        /// Update School.
        /// </summary>
        /// <param name="GeneralSchoolModel">GeneralSchoolModel.</param>
        /// <returns>Returns updated GeneralSchoolResponse</returns>
        GeneralSchoolResponse UpdateSchool(GeneralSchoolModel body);

        /// <summary>
        /// Delete School.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteSchool(ParameterModel body);
    }
}
