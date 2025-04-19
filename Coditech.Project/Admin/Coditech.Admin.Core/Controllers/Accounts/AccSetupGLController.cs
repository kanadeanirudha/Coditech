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
        [HttpPost]
        public virtual ActionResult AddChild(AccSetupGLModel accSetupGLModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid data. Please check the inputs." });
            }
            //set UserTypeId null if usertypeid is 0
            accSetupGLModel.UserTypeId = accSetupGLModel.UserTypeId == 0 ? (short?)null : accSetupGLModel.UserTypeId;

            // in this we are setting usertypeid 
            accSetupGLModel.UserTypeId = accSetupGLModel.UserTypeId;

            accSetupGLModel = _accSetupGLAgent.AddChild(accSetupGLModel);
            if (accSetupGLModel != null && !accSetupGLModel.HasError)
            {
                string newHtml = $@"<div id='gl-{accSetupGLModel.AccSetupGLId}'style='padding:10px; margin:5px 0; border:1px solid #ccc; border-radius:4px;background-color:#f9f9f9; display:flex; align-items:center;'> <span style='font-weight:bold; color:#333;'> {accSetupGLModel.GLName}</span></div>";
                return Json(new
                {
                    success = true,
                    html = newHtml,
                    message = "Record added successfully!",
                    accSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId,
                    accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId,
                    selectedCentreCode = accSetupGLModel.SelectedCentreCode
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    status = !accSetupGLModel.HasError,
                    message = accSetupGLModel.ErrorMessage,
                    accSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId,
                    accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId,
                    selectedCentreCode = accSetupGLModel.SelectedCentreCode
                });
            }
        }
        public virtual ActionResult Delete(string accSetupGLIds)
        {
            if (string.IsNullOrEmpty(accSetupGLIds))
            {
                return Json(new { success = false, message = "Invalid account ID.", accountId = accSetupGLIds });
            }

            try
            {
                string message;
                bool status = _accSetupGLAgent.DeleteAccountSetupGL(accSetupGLIds, out message);
                if (status)
                {
                    return Json(new { success = true, message = "Record deleted successfully.", accountId = accSetupGLIds });
                }
                else
                {
                    return Json(new { success = false, message = message, accountId = accSetupGLIds });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message, accountId = accSetupGLIds });
            }
        }
        [HttpGet]
        public ActionResult LoadAccountSetupGL(int? accSetupGLId)
        {
            var accSetupGLModel = accSetupGLId.HasValue
                ? _accSetupGLAgent.GetAccountSetupGL(accSetupGLId.Value)
                : new AccSetupGLModel();

            return Json(new
            {
                success = true,
                data = new
                {
                    accSetupGLId = accSetupGLModel.AccSetupGLId,
                    accSetupGLTypeId = accSetupGLModel.AccSetupGLTypeId,
                    glName = accSetupGLModel.GLName,
                    glCode = accSetupGLModel.GLCode,
                    isGroup = accSetupGLModel.IsGroup,
                    bankAccountName = accSetupGLModel.BankAccountName,
                    bankAccountNumber = accSetupGLModel.BankAccountNumber,
                    bankBranchName = accSetupGLModel.BankBranchName,
                    iFSCCode = accSetupGLModel.IFSCCode,
                    accSetupCategoryId = accSetupGLModel.AccSetupCategoryId,
                    accSetupBalancesheetId = accSetupGLModel.AccSetupBalancesheetId,
                    accSetupBalanceSheetTypeId = accSetupGLModel.AccSetupBalanceSheetTypeId,
                    parentAccSetupGLId = accSetupGLModel.ParentAccSetupGLId,
                    selectedCentreCode = accSetupGLModel.SelectedCentreCode,
                    usertypeid = accSetupGLModel.UserTypeId
                }
            });
        }

        [HttpPost]
        public IActionResult RenderChildModel([FromBody] AccSetupGLModel model)
        {
            return PartialView("~/Views/Accounts/AccSetupGL/_ChildModel.cshtml", model);
        }

        [HttpPost]
        public virtual ActionResult SaveAccountSetupGL()
        {
            try
            {
                var accSetupGLModel = new AccSetupGLModel
                {
                    AccSetupGLId = Convert.ToInt32(Request.Form["accSetupGLId"]),
                    AccSetupCategoryId = Convert.ToInt16(Request.Form["accSetupCategoryId"]),
                    GLName = Request.Form["glName"],
                    GLCode = Request.Form["glCode"],
                    AccSetupGLTypeId = byte.TryParse(Request.Form["accSetupGLTypeId"], out byte accSetupGLTypeId) ? accSetupGLTypeId : (byte?)null,
                    IsGroup = bool.TryParse(Request.Form["isGroup"], out bool isGroup) && isGroup,
                    UserTypeId = byte.TryParse(Request.Form["userTypeId"], out byte userTypeId) ? userTypeId : (byte?)null,
                    // Allow nullable for int and byte types
                    AccSetupBalancesheetId = Convert.ToInt32(Request.Form["accSetupBalancesheetId"]),
                    BankAccountName = Request.Form.ContainsKey("bankAccountName") ? Request.Form["bankAccountName"].ToString() : string.Empty,
                    BankAccountNumber = Request.Form.ContainsKey("bankAccountNumber") ? Request.Form["bankAccountNumber"].ToString() : string.Empty,
                    BankBranchName = Request.Form.ContainsKey("bankBranchName") ? Request.Form["bankBranchName"].ToString() : string.Empty,
                    IFSCCode = Request.Form.ContainsKey("iFSCCode") ? Request.Form["iFSCCode"].ToString() : string.Empty
                };
                if (ModelState.IsValid)
                {
                    accSetupGLModel = _accSetupGLAgent.UpdateAccount(accSetupGLModel);
                    return Json(new { success = true, message = "Record Updated successfully!" });
                }
                else
                {
                    return Json(new { success = true, accSetupGLModel = !accSetupGLModel.HasError, message = accSetupGLModel.ErrorMessage, data = accSetupGLModel });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}