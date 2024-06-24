//namespace Coditech.Engine.Admin.ViewModel.GeneralMaster.HospitalPatientAppointmentPurpose
//{
//    public class HospitalPatientAppointmentPurposeListViewModel
//    {
//    }
//}
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentPurposeListViewModel : BaseViewModel
    {
        public List<HospitalPatientAppointmentPurposeViewModel> HospitalPatientAppointmentPurposeList { get; set; }
        public HospitalPatientAppointmentPurposeListViewModel()
        {
            HospitalPatientAppointmentPurposeList = new List<HospitalPatientAppointmentPurposeViewModel>();
        }
    }
}