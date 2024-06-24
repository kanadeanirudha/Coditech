//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Coditech.API.Client.Client.Interface.GeneralMaster
//{
//    internal interface IHospitalPatientAppointmentPurposeClient
//    {
//    }
//}
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalPatientAppointmentPurposeClient : IBaseClient
    {
        /// <summary>
        /// Get list of Appointment purpose.
        /// </summary>
        /// <returns>HospitalPatientAppointmentPurposeListResponse</returns>
        HospitalPatientAppointmentPurposeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Appointment purpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeModel">HospitalPatientAppointmentPurposeModel.</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeResponse.</returns>
        HospitalPatientAppointmentPurposeResponse CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body);

        /// <summary>
        /// Get Country by HospitalPatientAppointmentPurposeId.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeId">HospitalPatientAppointmentPurposeId</param>
        /// <returns>Returns HospitalPatientAppointmentPurposeResponse.</returns>
        HospitalPatientAppointmentPurposeResponse GetHospitalPatientAppointmentPurpose(short HospitalPatientAppointmentPurposeId);

        /// <summary>
        /// Update Appointment purpose.
        /// </summary>
        /// <param name="HospitalPatientAppointmentPurposeModel">HospitalPatientAppointmentPurposeModel.</param>
        /// <returns>Returns updated HospitalPatientAppointmentPurposeResponse</returns>
        HospitalPatientAppointmentPurposeResponse UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel body);

        /// <summary>
        /// Delete Appointment purpose.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalPatientAppointmentPurpose(ParameterModel body);
    }
}

