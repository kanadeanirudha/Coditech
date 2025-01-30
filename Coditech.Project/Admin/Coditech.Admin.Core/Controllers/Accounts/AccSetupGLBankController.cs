using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class AccSetupGLBankController : BaseController
    {
        private readonly IAccSetupGLBankAgent _accSetupGLBankAgent;
        private const string createEdit = "~/Views/Accounts/AccSetupGLBank/CreateEdit.cshtml";

        public AccSetupGLBankController(IAccSetupGLBankAgent accSetupGLBankAgent)
        {
            _accSetupGLBankAgent = accSetupGLBankAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccSetupGLBankListViewModel list = _accSetupGLBankAgent.GetAccSetupGLBankList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccSetupGLBank/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccSetupGLBank/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new AccSetupGLBankViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AccSetupGLBankViewModel accSetupGLBankViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupGLBankViewModel = _accSetupGLBankAgent.CreateAccSetupGLBank(accSetupGLBankViewModel);
                if (!accSetupGLBankViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accSetupGLBankViewModel.ErrorMessage));
            return View(createEdit, accSetupGLBankViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int accSetupGLBankId)
        {
            AccSetupGLBankViewModel accSetupGLBankViewModel = _accSetupGLBankAgent.GetAccSetupGLBank(accSetupGLBankId);
            return ActionView(createEdit, accSetupGLBankViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccSetupGLBankViewModel accSetupGLBankViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupGLBankViewModel = _accSetupGLBankAgent.UpdateAccSetupGLBank(accSetupGLBankViewModel);
                SetNotificationMessage(accSetupGLBankViewModel.HasError
                ? GetErrorNotificationMessage(accSetupGLBankViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { accSetupGLBankId = accSetupGLBankViewModel.AccSetupGLBankId });
            }
            return View(createEdit, accSetupGLBankViewModel);
        }
        public virtual ActionResult Delete(string accSetupGLBankIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(accSetupGLBankIds))
            {
                status = _accSetupGLBankAgent.DeleteAccSetupGLBank(accSetupGLBankIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<AccSetupGLBankController>(x => x.List(null));
        }
    }
}
