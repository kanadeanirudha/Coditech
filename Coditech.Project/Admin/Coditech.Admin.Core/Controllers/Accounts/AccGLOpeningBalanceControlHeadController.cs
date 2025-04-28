using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLOpeningBalanceControlHeadController : BaseController
    {
        private readonly IAccGLOpeningBalanceAgent _accGLOpeningBalanceAgent;

        public AccGLOpeningBalanceControlHeadController(IAccGLOpeningBalanceAgent accGLOpeningBalanceAgent)
        {
            _accGLOpeningBalanceAgent = accGLOpeningBalanceAgent;
        }

        [HttpGet]
        public virtual ActionResult ControlHeadType(short accSetupCategoryId = 0, int accSetupGLId = 0)
        {
            if (!AdminGeneralHelper.IsBalanceSheetAssociated())
            {
                SetNotificationMessage(GetErrorNotificationMessage("Balance Sheet Not Associated."));
                return View("~/Views/Shared/BalanceSheetAssociated.cshtml");
            }
            GeneralFinancialYearModel generalFinancialYearModel = _accGLOpeningBalanceAgent.GetCurrentFinancialYear();
            ACCGLOpeningBalanceViewModel model = new ACCGLOpeningBalanceViewModel();

            if (generalFinancialYearModel?.GeneralFinancialYearId <= 0)
            {
                SetNotificationMessage(GetErrorNotificationMessage("Current Financial Year Not Set For Selected Balance Sheet."));
            }
            else if (accSetupCategoryId > 0)
            {
                model = _accGLOpeningBalanceAgent.GetControlHeadType(accSetupCategoryId);

            }
            model.GeneralFinancialYearModel = generalFinancialYearModel;
            model.AccSetupGLId = accSetupGLId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLOpeningBalance/_ControlHeadTypeList.cshtml", model);
            }
            return View("~/Views/Accounts/AccGLOpeningBalance/ControlHeadTypeList.cshtml", model);
        }

        //Individual Opening Balance
        [HttpGet]
        public virtual ActionResult IndividualOpeningBalance(short userTypeId, short generalFinancialYearId, int accSetupGLId)
        {
            AccGLIndividualOpeningBalanceViewModel accGLIndividualOpeningBalanceViewModel = _accGLOpeningBalanceAgent.GetIndividualOpeningBalance(userTypeId, generalFinancialYearId, accSetupGLId);
            return View("~/Views/Accounts/AccGLOpeningBalance/IndividualOpeningBalance.cshtml", accGLIndividualOpeningBalanceViewModel);
        }

        [HttpPost]
        public virtual ActionResult IndividualOpeningBalance(AccGLIndividualOpeningBalanceViewModel accGLIndividualOpeningBalanceViewModel)
        {
            if (ModelState.IsValid)
            {
                accGLIndividualOpeningBalanceViewModel = _accGLOpeningBalanceAgent.UpdateIndividualOpeningBalance(accGLIndividualOpeningBalanceViewModel);
                if (!accGLIndividualOpeningBalanceViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Saved Successfully."));
                    return RedirectToAction("IndividualOpeningBalance", new { userTypeId = accGLIndividualOpeningBalanceViewModel.UserTypeId, generalFinancialYearId = accGLIndividualOpeningBalanceViewModel.GeneralFinancialYearId, accSetupGLId = accGLIndividualOpeningBalanceViewModel.AccSetupGLId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage("Failed to Save Individual Opening Balance."));
            return View("~/Views/Accounts/AccGLOpeningBalance/IndividualOpeningBalance.cshtml", accGLIndividualOpeningBalanceViewModel);
        }
    }
}