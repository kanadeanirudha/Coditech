﻿namespace Coditech.Common.API.Model
{
    public class HospitalPatientAppointmentListModel : BaseListModel
    {
        public List<HospitalPatientAppointmentModel> HospitalPatientAppointmentList { get; set; }
        public HospitalPatientAppointmentListModel()
        {
            HospitalPatientAppointmentList = new List<HospitalPatientAppointmentModel>();
        }
        public int MedicalSpecilizationEnumId { get; set; }
        //public string MedicalSpecilization { get; set; }
        public string SelectedCentreCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
