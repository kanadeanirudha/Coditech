//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Coditech.Common.API.Model.GeneralMaster.HospitalPatientAppointmentPurpose
//{
//    internal class HospitalPatientAppointmentPurposeListModel
//    {
//    }
//}
namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentPurposeListModel : BaseListModel
    {
        public List<HospitalPatientAppointmentPurposeModel> HospitalPatientAppointmentPurposeList { get; set; }
        public HospitalPatientAppointmentPurposeListModel()
        {
            HospitalPatientAppointmentPurposeList = new List<HospitalPatientAppointmentPurposeModel>();
        }

    }
}