//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Coditech.Common.API.Model.Responses.GeneralMaster.HospitalPatientAppointmentPurpose
//{
//    internal class HospitalPatientAppointmentPurposeResponse
//    {
//    }
//}
namespace Coditech.Common.API.Model.Responses
{
    public class HospitalPatientAppointmentPurposeResponse : BaseResponse
    {
        public HospitalPatientAppointmentPurposeModel hospitalPatientAppointmentPurposeModel { get; set; }
    }
}