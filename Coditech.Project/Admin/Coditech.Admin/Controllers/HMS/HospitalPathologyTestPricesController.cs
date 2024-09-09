using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPathologyTestPricesController : BaseController
    {
        private readonly IHospitalPathologyTestPricesAgent _hospitalPathologyTestPricesAgent;
        private const string createEdit = "~/Views/HMS/HospitalPathologyTestPrices/CreateEdit.cshtml";

        public HospitalPathologyTestPricesController(IHospitalPathologyTestPricesAgent hospitalPathologyTestPricesAgent)
        {
            _hospitalPathologyTestPricesAgent = hospitalPathologyTestPricesAgent;
        }

        public virtual ActionResult List(int hospitalPathologyPriceCategoryEnumId, DataTableViewModel dataTableModel)
        {
            HospitalPathologyTestPricesListViewModel list = _hospitalPathologyTestPricesAgent.GetHospitalPathologyTestPricesList(hospitalPathologyPriceCategoryEnumId,dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPathologyTestPrices/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPathologyTestPrices/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPathologyTestPricesViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalPathologyTestPricesViewModel = _hospitalPathologyTestPricesAgent.CreateHospitalPathologyTestPrices(hospitalPathologyTestPricesViewModel);
                if (!hospitalPathologyTestPricesViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPathologyTestPricesViewModel.ErrorMessage));
            return View(createEdit, hospitalPathologyTestPricesViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalPathologyTestPricesId)
        {
            HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel = _hospitalPathologyTestPricesAgent.GetHospitalPathologyTestPrices(hospitalPathologyTestPricesId);
            return ActionView(createEdit, hospitalPathologyTestPricesViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPathologyTestPricesAgent.UpdateHospitalPathologyTestPrices(hospitalPathologyTestPricesViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPathologyTestPricesViewModel.HospitalPathologyTestPricesId });
            }
            return View(createEdit, hospitalPathologyTestPricesViewModel);
        }

        public virtual ActionResult Delete(string hospitalPathologyTestPricesIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPathologyTestPricesIds))
            {
                status = _hospitalPathologyTestPricesAgent.DeleteHospitalPathologyTestPrices(hospitalPathologyTestPricesIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", CreateActionDataTable());
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", CreateActionDataTable());
        }

        public virtual ActionResult GetPathologyTestNameByPathologyPriceCategory(int hospitalPathologyPriceCategoryEnumId)
        {
            DropdownViewModel hospitalPathologyPriceCategoryDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.PathologyTestNameByPathologyPriceCategory.ToString(),
                DropdownName = "HospitalPathologyTestId",
                Parameter = $"{hospitalPathologyPriceCategoryEnumId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", hospitalPathologyPriceCategoryDropdown);
        }
    }
}
