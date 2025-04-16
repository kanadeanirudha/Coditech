using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLOpeningBalanceController : BaseController
    {
        private readonly IAccGLOpeningBalanceAgent _accGLOpeningBalanceAgent;

        public AccGLOpeningBalanceController(IAccGLOpeningBalanceAgent accGLOpeningBalanceAgent)
        {
            _accGLOpeningBalanceAgent = accGLOpeningBalanceAgent;
        }

        [HttpGet]
        public virtual ActionResult UpdateNonControlHeadType(short accSetupCategoryId = 0)
        {
            if (!AdminGeneralHelper.IsBalanceSheetAssociated())
            {
                SetNotificationMessage(GetErrorNotificationMessage("Balance Sheet Not Associated."));
                return View("~/Views/Shared/BalanceSheetAssociated.cshtml");
            }
            ACCGLOpeningBalanceListViewModel list = new ACCGLOpeningBalanceListViewModel
            {
                GeneralFinancialYearModel = _accGLOpeningBalanceAgent.GetCurrentFinancialYear()
            };
            if (list.GeneralFinancialYearModel.GeneralFinancialYearId <= 0)
            {
                SetNotificationMessage(GetErrorNotificationMessage("Current Financial Year Not Set For Selected Balance Sheet."));
            }
            else if (accSetupCategoryId > 0)
            {
                list = _accGLOpeningBalanceAgent.GetNonControlHeadType(accSetupCategoryId);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLOpeningBalance/_NonControlHeadTypeList.cshtml", list);
            }
            return View("~/Views/Accounts/AccGLOpeningBalance/NonControlHeadTypeList.cshtml", list);
        }

        [HttpPost]
        public virtual ActionResult UpdateNonControlHeadType(ACCGLOpeningBalanceListViewModel accGLOpeningBalanceListViewModel)
        {
            if (ModelState.IsValid)
            {
                accGLOpeningBalanceListViewModel = _accGLOpeningBalanceAgent.UpdateNonControlHeadType(accGLOpeningBalanceListViewModel);
                if (!accGLOpeningBalanceListViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Saved Successfully."));
                    return RedirectToAction("UpdateNonControlHeadType", new { accSetupCategoryId = accGLOpeningBalanceListViewModel.AccSetupCategoryId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage("Failed to Save Opening Balance."));
            return View("~/Views/Accounts/AccGLOpeningBalance/NonControlHeadTypeList.cshtml", accGLOpeningBalanceListViewModel);
        }
    }
}