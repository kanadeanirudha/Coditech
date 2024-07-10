using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientRegistrationController : BaseController
    {
        private readonly IHospitalPatientRegistrationAgent _hospitalPatientRegistrationAgent;
        private const string createEditPatientRegistration = "~/Views/HMS/HospitalPatientRegistration/CreateEditPatientRegistration.cshtml";

        public HospitalPatientRegistrationController(IHospitalPatientRegistrationAgent hospitalPatientRegistrationAgent)
        {
            _hospitalPatientRegistrationAgent = hospitalPatientRegistrationAgent;
        }
        #region Patient Registration
        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            HospitalPatientRegistrationListViewModel list = new HospitalPatientRegistrationListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _hospitalPatientRegistrationAgent.GetHospitalPatientRegistrationList(dataTableViewModel.SelectedCentreCode, dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPatientRegistration/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPatientRegistration/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreatePatientRegistration()
        {
            return View(createEditPatientRegistration, new HospitalPatientRegistrationCreateEditViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreatePatientRegistration(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel, string selectedCentreCode)
        {
            if (ModelState.IsValid)
            {
                hospitalPatientRegistrationCreateEditViewModel = _hospitalPatientRegistrationAgent.CreatePatientRegistration(hospitalPatientRegistrationCreateEditViewModel);
                if (!hospitalPatientRegistrationCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // Redirect to the List action with selectedCentreCode and selectedDepartmentId
                    return RedirectToAction("List", new { selectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPatientRegistrationCreateEditViewModel.ErrorMessage));
            return View(createEditPatientRegistration, hospitalPatientRegistrationCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdatePatientPersonalDetails(long hospitalPatientRegistrationId, long personId)
        {
            HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel = _hospitalPatientRegistrationAgent.GetPatientRegistrationPersonalDetails(hospitalPatientRegistrationId, personId);
            return ActionView(createEditPatientRegistration, hospitalPatientRegistrationCreateEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdatePatientPersonalDetails(HospitalPatientRegistrationCreateEditViewModel hospitalPatientRegistrationCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPatientRegistrationAgent.UpdatePatientRegistrationPersonalDetails(hospitalPatientRegistrationCreateEditViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdatePatientPersonalDetails", new { hospitalPatientRegistrationId = hospitalPatientRegistrationCreateEditViewModel.HospitalPatientRegistrationId, personId = hospitalPatientRegistrationCreateEditViewModel.PersonId });
            }
            return View(createEditPatientRegistration, hospitalPatientRegistrationCreateEditViewModel);
        }

        public virtual ActionResult Delete(string hospitalPatientRegistrationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPatientRegistrationIds))
            {
                status = _hospitalPatientRegistrationAgent.DeletePatientRegistration(hospitalPatientRegistrationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPatientRegistrationController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPatientRegistrationController>(x => x.List(null));
        }


        #endregion HospitalPatientRegistration

        #region Hospital Patient Registration Address
        [HttpGet]
        public virtual ActionResult CreateEditPatientRegistrationAddress(long hospitalPatientRegistrationId, long personId)
        {
            GeneralPersonAddressListViewModel model = new GeneralPersonAddressListViewModel()
            {
                EntityId = hospitalPatientRegistrationId,
                PersonId = personId,
                EntityType = UserTypeEnum.Patient.ToString()
            };
            return ActionView("~/Views/HMS/HospitalPatientRegistration/CreateEditPatientRegistrationAddress.cshtml", model);
        }

        #endregion Hospital Patient Registration Address Address
    }
}