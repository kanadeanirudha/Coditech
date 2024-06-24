//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Coditech.Common.API.Model.Responses.GeneralMaster.HospitalPatientAppointmentPurpose
//{
//    internal class HospitalPatientAppointmentPurposeListResponse
//    {
//    }
//}
namespace Coditech.Common.API.Model.Response
{
    public class HospitalPatientAppointmentPurposeListResponse : BaseListResponse
    {
        public List<HospitalPatientAppointmentPurposeModel> HospitalPatientAppointmentPurposeList { get; set; }
    }
}