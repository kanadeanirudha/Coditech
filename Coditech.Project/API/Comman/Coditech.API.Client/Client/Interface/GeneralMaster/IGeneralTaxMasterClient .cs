using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralTaxMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of General TaxMaster.
        /// </summary>
        /// <returns>GeneralTaxMasterListResponse</returns>
        GeneralTaxMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create TaxMaster.
        /// </summary>
        /// <param name="GeneralTaxMasterModel">GeneralTaxMasterModel.</param>
        /// <returns>Returns GeneralTaxMasterResponse.</returns>
        GeneralTaxMasterResponse CreateTaxMaster(GeneralTaxMasterModel body);

        /// <summary>
        /// Get TaxMaster by taxMasterId.
        /// </summary>
        /// <param name="taxMasterId">taxMasterId</param>
        /// <returns>Returns GeneralTaxMasterResponse.</returns>
        GeneralTaxMasterResponse GetTaxMaster(short taxMasterId);

        /// <summary>
        /// Update TaxMaster.
        /// </summary>
        /// <param name="GeneralTaxMasterModel">GeneralTaxMasterModel.</param>
        /// <returns>Returns updated GeneralTaxMasterResponse</returns>
        GeneralTaxMasterResponse UpdateTaxMaster(GeneralTaxMasterModel body);

        /// <summary>
        /// Delete TaxMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTaxMaster(ParameterModel body);
    }
}
