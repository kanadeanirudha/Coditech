using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientTypeController : BaseController
    {
        private readonly IHospitalPatientTypeAgent _hospitalPatientTypeAgent;
        private const string createEditPatientType = "~/Views/HMS/HospitalPatientType/CreateEdit.cshtml";

        public HospitalPatientTypeController(IHospitalPatientTypeAgent hospitalPatientTypeAgent)
        {
            _hospitalPatientTypeAgent = hospitalPatientTypeAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            HospitalPatientTypeListViewModel list = _hospitalPatientTypeAgent.GetHospitalPatientTypeList(dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPatientType/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPatientType/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEditPatientType, new HospitalPatientTypeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPatientTypeViewModel hospitalPatientTypeViewModel)
        {       
            if (ModelState.IsValid)
            {
                hospitalPatientTypeViewModel = _hospitalPatientTypeAgent.CreateHospitalPatientType(hospitalPatientTypeViewModel);
                if (!hospitalPatientTypeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPatientTypeViewModel.ErrorMessage));
            return View(createEditPatientType, hospitalPatientTypeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(byte hospitalPatientTypeId)
        {
            HospitalPatientTypeViewModel hospitalPatientTypeViewModel = _hospitalPatientTypeAgent.GetHospitalPatientType(hospitalPatientTypeId);
            return ActionView(createEditPatientType, hospitalPatientTypeViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPatientTypeViewModel hospitalPatientTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPatientTypeAgent.UpdateHospitalPatientType(hospitalPatientTypeViewModel).HasError
               ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
               : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPatientTypeId = hospitalPatientTypeViewModel.HospitalPatientTypeId });
            }
            return View(createEditPatientType, hospitalPatientTypeViewModel);
        }
      
        public virtual ActionResult Delete(string hospitalPatientTypeIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPatientTypeIds))
            {
                status = _hospitalPatientTypeAgent.DeleteHospitalPatientType(hospitalPatientTypeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPatientTypeController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPatientTypeController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}