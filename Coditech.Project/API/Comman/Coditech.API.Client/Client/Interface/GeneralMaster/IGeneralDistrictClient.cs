using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralDistrictClient : IBaseClient
    {
        /// <summary>
        /// Get list of General District.
        /// </summary>
        /// <returns>GeneralDistrictListResponse</returns>
        GeneralDistrictListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create District.
        /// </summary>
        /// <param name="GeneralDistrictModel">GeneralDistrictModel.</param>
        /// <returns>Returns GeneralDistrictResponse.</returns>
        GeneralDistrictResponse CreateDistrict(GeneralDistrictModel body);

        /// <summary>
        /// Get District by generalDistrictId.
        /// </summary>
        /// <param name="generalDistrictId">generalDistrictId</param>
        /// <returns>Returns GeneralDistrictResponse.</returns>
        GeneralDistrictResponse GetDistrict(short generalDistrictId);

        /// <summary>
        /// Update District.
        /// </summary>
        /// <param name="GeneralDistrictModel">GeneralDistrictModel.</param>
        /// <returns>Returns updated GeneralDistrictResponse</returns>
        GeneralDistrictResponse UpdateDistrict(GeneralDistrictModel body);

        /// <summary>
        /// Delete District.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteDistrict(ParameterModel body);

        /// <summary>
        /// Get list of General District.
        /// </summary>
        /// <returns>GeneralDistrictListResponse</returns>
        GeneralDistrictListResponse GetDistrictByRegionWise(Int16 generalRegionMasterId);
    }
}
