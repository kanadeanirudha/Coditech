using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IAccSetupGLBankClient : IBaseClient
    {
        /// <summary>
        /// Get list of AccSetupGLBank.
        /// </summary>
        /// <returns>AccSetupGLBankListResponse</returns>
        AccSetupGLBankListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create AccSetupGLBank.
        /// </summary>
        /// <param name="AccSetupGLBankModel">AccSetupGLBankModel.</param>
        /// <returns>Returns AccSetupGLBankResponse.</returns>
        AccSetupGLBankResponse CreateAccSetupGLBank(AccSetupGLBankModel body);

        /// <summary>
        /// Get AccSetupGLBank by accSetupGLBankId.
        /// </summary>
        /// <param name="accSetupGLBankId">accSetupGLBankId</param>
        /// <returns>Returns AccSetupGLBankResponse.</returns>
        AccSetupGLBankResponse GetAccSetupGLBank(int accSetupGLBankId);

        /// <summary>
        /// Update AccSetupGLBank.
        /// </summary>
        /// <param name="AccSetupGLBankModel">AccSetupGLBankModel.</param>
        /// <returns>Returns updated AccSetupGLBankResponse</returns>
        AccSetupGLBankResponse UpdateAccSetupGLBank(AccSetupGLBankModel body);

        /// <summary>
        /// Delete AccSetupGLBank.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteAccSetupGLBank(ParameterModel body);
    }
}
