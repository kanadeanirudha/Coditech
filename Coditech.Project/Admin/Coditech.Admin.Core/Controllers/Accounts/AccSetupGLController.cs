using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class AccSetupGLController : BaseController
    {
        private readonly IAccSetupGLAgent _accSetupGLAgent;
        public AccSetupGLController(IAccSetupGLAgent accSetupGLAgent)
        {
            _accSetupGLAgent = accSetupGLAgent;
        }
        [HttpGet]
        public virtual ActionResult GetAccSetupGL(string selectedcentreCode = null, byte accSetupBalanceSheetTypeId = 0, int accSetupBalanceSheetId = 0)
        {
            AccSetupGLModel accSetupGLModel = new AccSetupGLModel();
            if (accSetupBalanceSheetId > 0)
            {
                accSetupGLModel = _accSetupGLAgent.GetAccSetupGLTree(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);
                accSetupGLModel.SelectedCentreCode = selectedcentreCode;
                accSetupGLModel.AccSetupBalanceSheetTypeId = accSetupBalanceSheetTypeId;
                accSetupGLModel.AccSetupBalancesheetId = accSetupBalanceSheetId;
            }
            return View($"~/Views/Accounts/AccSetupGL/AccSetupGL.cshtml", accSetupGLModel);
        }

        [HttpGet]
        public virtual ActionResult GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {
            AccSetupGLModel accSetupGLModel = _accSetupGLAgent.GetAccSetupGLTree(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);

            if (accSetupGLModel.ActionMode == ActionModeEnum.Create.ToString())
                return PartialView("~/Views/Accounts/AccSetupGL/_CreateAccountSetupGL.cshtml", accSetupGLModel);
            else
                return PartialView("~/Views/Accounts/AccSetupGL/_UpdateAccountSetupGL.cshtml", accSetupGLModel);
        }

        public virtual ActionResult GetAccSetupBalanceSheetByCentreCodeAndTypeId(string selectedcentreCode, byte accSetupBalanceSheetTypeId)
        {
            DropdownViewModel accSetupBalanceSheetByCentreCodeDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.AccSetupBalanceSheet.ToString(),
                DropdownName = "AccSetupBalanceSheetId",
                Parameter = $"{selectedcentreCode}~{accSetupBalanceSheetTypeId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", accSetupBalanceSheetByCentreCodeDropdown);
        }

        [HttpPost]
        public virtual ActionResult CreateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            if (ModelState.IsValid)
            {
                accSetupGLModel = _accSetupGLAgent.CreateAccountSetupGL(accSetupGLModel);
                if (!accSetupGLModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("GetAccSetupGL", new { selectedcentreCode = accSetupGLModel.SelectedCentreCode, accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId, accSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId });

                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accSetupGLModel.ErrorMessage));
            return PartialView("~/Views/Accounts/AccSetupGL/_CreateAccountSetupGL.cshtml", accSetupGLModel);
        }
       
        [HttpPost]
        public virtual ActionResult UpdateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            if (ModelState.IsValid)
            {
                accSetupGLModel = _accSetupGLAgent.UpdateAccountSetupGL(accSetupGLModel);
                SetNotificationMessage(accSetupGLModel.HasError
                ? GetErrorNotificationMessage(accSetupGLModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetAccSetupGL", new { selectedcentreCode = accSetupGLModel.SelectedCentreCode, accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId, accSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId });
            }
            return RedirectToAction("GetAccSetupGL", new { selectedcentreCode = accSetupGLModel.SelectedCentreCode, accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId, accSetupBalanceSheetId = accSetupGLModel.AccSetupBalancesheetId });

        }
    }
}
