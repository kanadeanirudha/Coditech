using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalRegistrationFeeController : BaseController
    {
        private readonly IHospitalRegistrationFeeAgent _hospitalRegistrationFeeAgent;
        private const string createEdit = "~/Views/HMS/HospitalRegistrationFee/CreateEdit.cshtml";

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
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalRegistrationFeeViewModel()
            {
                FromDate = DateTime.Now
            });
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalRegistrationFeeViewModel = _hospitalRegistrationFeeAgent.CreateRegistrationFee(hospitalRegistrationFeeViewModel);
                if (!hospitalRegistrationFeeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = hospitalRegistrationFeeViewModel.SelectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalRegistrationFeeViewModel.ErrorMessage));
            return View(createEdit, hospitalRegistrationFeeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalRegistrationFeeId)
        {
            HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel = _hospitalRegistrationFeeAgent.GetRegistrationFee(hospitalRegistrationFeeId);
            return ActionView(createEdit, hospitalRegistrationFeeViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalRegistrationFeeViewModel hospitalRegistrationFeeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalRegistrationFeeAgent.UpdateRegistrationFee(hospitalRegistrationFeeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalRegistrationFeeId = hospitalRegistrationFeeViewModel.HospitalRegistrationFeeId });
            }
            return View(createEdit, hospitalRegistrationFeeViewModel);
        }

        public virtual ActionResult Delete(string hospitalRegistrationFeeIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalRegistrationFeeIds))
            {
                status = _hospitalRegistrationFeeAgent.DeleteRegistrationFee(hospitalRegistrationFeeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalRegistrationFeeController>(x => x.List(null));
        }

        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }
        #endregion 
    }
}