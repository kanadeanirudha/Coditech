using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalRegistrationFeeController : BaseController
    {
        private readonly IHospitalRegistrationFeeAgent _hospitalRegistrationFeeAgent;
        private const string createEditRegistrationFee = "~/Views/HMS/HospitalRegistrationFee/CreateEditRegistrationFee.cshtml";

        public HospitalRegistrationFeeController(IHospitalRegistrationFeeAgent hospitalRegistrationFeeAgent)
        {
            _hospitalRegistrationFeeAgent = hospitalRegistrationFeeAgent;
        }
        #region Registration Fee
        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            HospitalRegistrationFeeListViewModel list = new HospitalRegistrationFeeListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _hospitalRegistrationFeeAgent.GetHospitalRegistrationFeeList(dataTableViewModel.SelectedCentreCode, dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalRegistrationFee/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalRegistrationFee/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreateRegistrationFee()
        {
            return View(createEditRegistrationFee, new HospitalRegistrationFeeCreateEditViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateRegistrationFee(HospitalRegistrationFeeCreateEditViewModel hospitalRegistrationFeeCreateEditViewModel, string selectedCentreCode)
        {
            if (ModelState.IsValid)
            {
                hospitalRegistrationFeeCreateEditViewModel = _hospitalRegistrationFeeAgent.CreateRegistrationFee(hospitalRegistrationFeeCreateEditViewModel);
                if (!hospitalRegistrationFeeCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // Redirect to the List action with selectedCentreCode and selectedDepartmentId
                    return RedirectToAction("List", new { selectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalRegistrationFeeCreateEditViewModel.ErrorMessage));
            return View(createEditRegistrationFee, hospitalRegistrationFeeCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateRegistrationFee(int hospitalRegistrationFeeId, long personId)
        {
            HospitalRegistrationFeeCreateEditViewModel hospitalRegistrationFeeCreateEditViewModel = _hospitalRegistrationFeeAgent.GetRegistrationFee(hospitalRegistrationFeeId, personId);
            return ActionView(createEditRegistrationFee, hospitalRegistrationFeeCreateEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateRegistrationFee(HospitalRegistrationFeeCreateEditViewModel hospitalRegistrationFeeCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalRegistrationFeeAgent.UpdateRegistrationFee(hospitalRegistrationFeeCreateEditViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateRegistrationFee", new { hospitalRegistrationFeeId = hospitalRegistrationFeeCreateEditViewModel.HospitalRegistrationFeeId, personId = hospitalRegistrationFeeCreateEditViewModel.PersonId });
            }
            return View(createEditRegistrationFee, hospitalRegistrationFeeCreateEditViewModel);
        }

        public virtual ActionResult Delete(string hospitalRegistrationFeeIds, string SelectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalRegistrationFeeIds))
            {
                status = _hospitalRegistrationFeeAgent.DeleteRegistrationFee(hospitalRegistrationFeeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalRegistrationFeeController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalRegistrationFeeController>(x => x.List(null));
        }

        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode};
            return RedirectToAction("List", dataTableViewModel);
        }
        #endregion HospitalRegistrationFee


    }
}