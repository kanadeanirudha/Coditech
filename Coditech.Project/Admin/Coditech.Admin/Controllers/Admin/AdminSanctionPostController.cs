using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AdminSanctionPostController : BaseController
    {
        private readonly IAdminSanctionPostAgent _adminSanctionPostAgent;
        private const string createEdit = "~/Views/Admin/AdminSanctionPost/CreateEdit.cshtml";

        public AdminSanctionPostController(IAdminSanctionPostAgent adminSanctionPostAgent)
        {
            _adminSanctionPostAgent = adminSanctionPostAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            AdminSanctionPostListViewModel list = new AdminSanctionPostListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _adminSanctionPostAgent.GetAdminSanctionPostList(dataTableModel);
            }

            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Admin/AdminSanctionPost/_List.cshtml", list);
            }
            return View($"~/Views/Admin/AdminSanctionPost/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new AdminSanctionPostViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            if (ModelState.IsValid)
            {
                adminSanctionPostViewModel = _adminSanctionPostAgent.CreateAdminSanctionPost(adminSanctionPostViewModel);
                if (!adminSanctionPostViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<AdminSanctionPostController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(adminSanctionPostViewModel.ErrorMessage));
            return View(createEdit, adminSanctionPostViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int adminSanctionPostId)
        {
            AdminSanctionPostViewModel adminSanctionPostViewModel = _adminSanctionPostAgent.GetAdminSanctionPost(adminSanctionPostId);
            return ActionView(createEdit, adminSanctionPostViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_adminSanctionPostAgent.UpdateAdminSanctionPost(adminSanctionPostViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { adminSanctionPostId = adminSanctionPostViewModel.AdminSanctionPostId });
            }
            return View(createEdit, adminSanctionPostViewModel);
        }

        public virtual ActionResult Delete(string departmentIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(departmentIds))
            {
                status = _adminSanctionPostAgent.DeleteAdminSanctionPost(departmentIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<AdminSanctionPostController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<AdminSanctionPostController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}