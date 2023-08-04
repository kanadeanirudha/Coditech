using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneraTaxMasterClient : IBaseClient
    {
        /// <summary>
        /// Gets list of General TaxMaster.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralTaxMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create general TaxMaster.
        /// </summary>
        /// <param name="generalTaxMasterViewModel">General TaxMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralTaxMasterResponse CreateTaxMaster(GeneralTaxMasterModel body);

        /// <summary>
        /// Get general TaxMaster by generalTaxMaster id.
        /// </summary>
        /// <param name="generalTaxMasterId">GeneralTaxMaster id to get generalTaxMaster details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralTaxMasterResponse GetTaxMaster(int generalTaxMasterId);

        /// <summary>
        /// Update generalTaxMaster.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralTaxMasterResponse UpdateTaxMaster(GeneralTaxMasterModel body);

        /// <summary>
        /// Delete generalTaxMaster.
        /// </summary>
        /// <param name="body">GeneralTaxMaster Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        TrueFalseResponse DeleteTaxMaster(ParameterModel body);
    }
}
