using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientAppointmentController : BaseController
    {
        private readonly IHospitalPatientAppointmentAgent _hospitalPatientAppointmentAgent;
        private const string createEdit = "~/Views/HMS/HospitalPatientAppointment/CreateEdit.cshtml";

        public HospitalPatientAppointmentController(IHospitalPatientAppointmentAgent hospitalPatientAppointmentAgent)
        {
            _hospitalPatientAppointmentAgent = hospitalPatientAppointmentAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            HospitalPatientAppointmentListViewModel list = new HospitalPatientAppointmentListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _hospitalPatientAppointmentAgent.GetHospitalPatientAppointmentList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPatientAppointment/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPatientAppointment/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPatientAppointmentViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel, string selectedCentreCode)
        {
            if (ModelState.IsValid)
            {
                hospitalPatientAppointmentViewModel = _hospitalPatientAppointmentAgent.CreateHospitalPatientAppointment(hospitalPatientAppointmentViewModel);
                if (!hospitalPatientAppointmentViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new { selectedCentreCode = hospitalPatientAppointmentViewModel.SelectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPatientAppointmentViewModel.ErrorMessage));
            return View(createEdit, hospitalPatientAppointmentViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalPatientAppointmentId)
        {
            HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel = _hospitalPatientAppointmentAgent.GetHospitalPatientAppointment(hospitalPatientAppointmentId);
            return ActionView(createEdit, hospitalPatientAppointmentViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPatientAppointmentAgent.UpdateHospitalPatientAppointment(hospitalPatientAppointmentViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPatientAppointmentId = hospitalPatientAppointmentViewModel.HospitalPatientAppointmentId });
            }
            return View(createEdit, hospitalPatientAppointmentViewModel);
        }

        public virtual ActionResult Delete(string hospitalPatientAppointmentIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPatientAppointmentIds))
            {
                status = _hospitalPatientAppointmentAgent.DeleteHospitalPatientAppointment(hospitalPatientAppointmentIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPatientAppointmentController>(x => x.List(null));
        }

        public virtual ActionResult GetDoctorsByCentreCodeAndSpecialization(string selectedCentreCode, int medicalSpecializationEnumId)
        {
            DropdownViewModel medicalSpecializationDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.HospitalDoctorsListBySpecialization.ToString(),
                DropdownName = "HospitalDoctorId",
                Parameter = $"{selectedCentreCode}~{medicalSpecializationEnumId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", medicalSpecializationDropdown);
        }


        //Get HospitalPatientsList By CentreCode
        public ActionResult GetHospitalPatientsListByCentreCode(string selectedCentreCode)
        {
            DropdownViewModel centreDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseHospitalPatientsList.ToString(),
                DropdownName = "HospitalPatientRegistrationId",
                Parameter = selectedCentreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", centreDropdown);
        }

        public virtual ActionResult GetTimeSlotByDoctorsAndAppointmentDate(int hospitalDoctorId, DateTime appointmentDate)
        {
            DropdownViewModel timeSlotList = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.TimeSlotByDoctorsListAndAppointmentDate.ToString(),
                DropdownName = "RequestedTimeSlot",
                Parameter = $"{hospitalDoctorId}~{appointmentDate}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", timeSlotList);
        }
        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }

        #region Protected
        #endregion
    }
}
