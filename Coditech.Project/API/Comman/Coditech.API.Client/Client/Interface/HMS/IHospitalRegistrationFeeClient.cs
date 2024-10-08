﻿using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalRegistrationFeeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Registration Fee.
        /// </summary>
        /// <returns>HospitalRegistrationFeeListResponse</returns>
        HospitalRegistrationFeeListResponse List(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Registration Fee.
        /// </summary>
        /// <param name="HospitalRegistrationFeeModel">HospitalRegistrationFeeModel.</param>
        /// <returns>Returns HospitalRegistrationFeeResponse.</returns>
        HospitalRegistrationFeeResponse CreateRegistrationFee(HospitalRegistrationFeeModel body);

        /// <summary>
        /// Get RegistrationFee by hospitalRegistrationFeeId.
        /// </summary>
        /// <param name="hospitalRegistrationFeeId">hospitalRegistrationFeeId</param>
        /// <returns>Returns HospitalRegistrationFeeResponse.</returns>
        HospitalRegistrationFeeResponse GetRegistrationFee(int hospitalRegistrationFeeId);

        /// <summary>
        /// Update HospitalRegistrationFee.
        /// </summary>
        /// <param name="HospitalRegistrationFeeModel">HospitalRegistrationFeeModel.</param>
        /// <returns>Returns updated HospitalRegistrationFeeResponse</returns>
        HospitalRegistrationFeeResponse UpdateRegistrationFee(HospitalRegistrationFeeModel body);

        /// <summary>
        /// Delete Registration Fee.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteRegistrationFee(ParameterModel body);
    }
}
