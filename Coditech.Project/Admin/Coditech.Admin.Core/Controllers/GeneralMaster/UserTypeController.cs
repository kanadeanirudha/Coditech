using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class UserTypeController : BaseController
    {
        IUserTypeAgent _userTypeAgent;
        private const string createEdit = "~/Views/GeneralMaster/UserType/CreateEdit.cshtml";
        public UserTypeController(IUserTypeAgent userTypeAgent)
        {
            _userTypeAgent = userTypeAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            UserTypeListViewModel list = _userTypeAgent.GetUserTypeList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/UserType/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/UserType/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new UserTypeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(UserTypeViewModel userTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                userTypeViewModel = _userTypeAgent.CreateUserType(userTypeViewModel);
                if (!userTypeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(userTypeViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { userTypeId = userTypeViewModel.UserTypeId });
                    }
                    else if (string.Equals(userTypeViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(userTypeViewModel.ErrorMessage));
            return View(createEdit, userTypeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short userTypeId)
        {
            UserTypeViewModel userTypeViewModel = _userTypeAgent.GetUserType(userTypeId);
            return ActionView(createEdit, userTypeViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(UserTypeViewModel userTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_userTypeAgent.UpdateUserType(userTypeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(userTypeViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { userTypeId = userTypeViewModel.UserTypeId });
                }
                else if (string.Equals(userTypeViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, userTypeViewModel);
        }
        //public virtual ActionResult Delete(string userTypeId)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(userTypeId))
        //    {
        //        status = _userTypeAgent.DeleteUserType(userTypeId, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<UserTypeController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<UserTypeController>(x => x.List(null));
        //}
        #region Protected
        #endregion
    }
}