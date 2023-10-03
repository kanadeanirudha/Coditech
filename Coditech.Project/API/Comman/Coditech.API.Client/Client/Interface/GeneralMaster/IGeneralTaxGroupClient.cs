using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralTaxGroupClient : IBaseClient
    {
        /// <summary>
        /// Get list of General TaxGroupMaster.
        /// </summary>
        /// <returns>GeneralTaxGroupListResponse</returns>
        GeneralTaxGroupListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create TaxGroupMaster.
        /// </summary>
        /// <param name="GeneralTaxGroupModel">GeneralTaxGroupModel.</param>
        /// <returns>Returns GeneralTaxGroupResponse.</returns>
        GeneralTaxGroupResponse CreateTaxGroupMaster(GeneralTaxGroupModel body);

        /// <summary>
        /// Get TaxGroup by taxGroupMasterId.
        /// </summary>
        /// <param name="taxGroupMasterId">taxGroupMasterId</param>
        /// <returns>Returns GeneralTaxGroupResponse.</returns>
        GeneralTaxGroupResponse GetTaxGroupMaster(short taxGroupMasterId);

        /// <summary>
        /// Update TaxGroupMaster.
        /// </summary>
        /// <param name="GeneralTaxGroupModel">GeneralTaxGroupModel.</param>
        /// <returns>Returns updated GeneralTaxGroupResponse</returns>
        GeneralTaxGroupResponse UpdateTaxGroupMaster(GeneralTaxGroupModel body);

        /// <summary>
        /// Delete TaxGroupMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTaxGroupMaster(ParameterModel body);
    }
}
