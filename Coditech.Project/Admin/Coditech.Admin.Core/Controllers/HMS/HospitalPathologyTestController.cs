using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPathologyTestController : BaseController
    {
        private readonly IHospitalPathologyTestAgent _hospitalPathologyTestAgent;
        private const string createEdit = "~/Views/HMS/HospitalPathologyTest/CreateEdit.cshtml";

        public HospitalPathologyTestController(IHospitalPathologyTestAgent hospitalPathologyTestAgent)
        {
            _hospitalPathologyTestAgent = hospitalPathologyTestAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalPathologyTestListViewModel list = _hospitalPathologyTestAgent.GetHospitalPathologyTestList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPathologyTest/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPathologyTest/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPathologyTestViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPathologyTestViewModel hospitalPathologyTestViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalPathologyTestViewModel = _hospitalPathologyTestAgent.CreateHospitalPathologyTest(hospitalPathologyTestViewModel);
                if (!hospitalPathologyTestViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPathologyTestViewModel.ErrorMessage));
            return View(createEdit, hospitalPathologyTestViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalPathologyTestId)
        {
            HospitalPathologyTestViewModel hospitalPathologyTestViewModel = _hospitalPathologyTestAgent.GetHospitalPathologyTest(hospitalPathologyTestId);
            return ActionView(createEdit, hospitalPathologyTestViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPathologyTestViewModel hospitalPathologyTestViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPathologyTestAgent.UpdateHospitalPathologyTest(hospitalPathologyTestViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPathologyTestViewModel.HospitalPathologyTestId });
            }
            return View(createEdit, hospitalPathologyTestViewModel);
        }

        public virtual ActionResult Delete(string hospitalPathologyTestIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPathologyTestIds))
            {
                status = _hospitalPathologyTestAgent.DeleteHospitalPathologyTest(hospitalPathologyTestIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPathologyTestController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPathologyTestController>(x => x.List(null));
        }

    }
}
