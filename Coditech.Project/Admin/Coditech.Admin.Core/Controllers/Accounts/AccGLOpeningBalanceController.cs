using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLOpeningBalanceController : BaseController
    {
        private readonly IAccGLOpeningBalanceAgent _accGLOpeningBalanceAgent;
        private readonly IGeneralCommonAgent _generalCommonAgent;

        public AccGLOpeningBalanceController(IAccGLOpeningBalanceAgent accGLOpeningBalanceAgent , IGeneralCommonAgent generalCommonAgent)
        {
            _accGLOpeningBalanceAgent = accGLOpeningBalanceAgent;
            _generalCommonAgent = generalCommonAgent;
        }

        [HttpGet]
        public virtual ActionResult UpdateNonControlHeadType(short accSetupCategoryId = 0)
        {
            if (!_generalCommonAgent.GetAccountPrequisite())
                return IscheckAccPrequisiteStatified();
            GeneralFinancialYearModel generalFinancialYearModel = _accGLOpeningBalanceAgent.GetCurrentFinancialYear();
            ACCGLOpeningBalanceListViewModel model = new ACCGLOpeningBalanceListViewModel();

            if (generalFinancialYearModel?.GeneralFinancialYearId <= 0)
            {
                SetNotificationMessage(GetErrorNotificationMessage("Current Financial Year Not Set For Selected Balance Sheet."));
            }
            else if (accSetupCategoryId > 0)
            {
                model = _accGLOpeningBalanceAgent.GetNonControlHeadType(accSetupCategoryId);

            }
            model.GeneralFinancialYearModel = generalFinancialYearModel;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLOpeningBalance/_NonControlHeadTypeList.cshtml", model);
            }
            return View("~/Views/Accounts/AccGLOpeningBalance/NonControlHeadTypeList.cshtml", model);
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