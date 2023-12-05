using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class AdminSanctionPostController : BaseController
    {
        private readonly IAdminSanctionPostAgent _adminSanctionPostAgent;
        public AdminSanctionPostController(IAdminSanctionPostAgent adminSanctionPostAgent)
        {
            _adminSanctionPostAgent = adminSanctionPostAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            //DataTableViewModel tempDataTable = TempData[AdminConstants.DataTableViewModel] as DataTableViewModel;
            //dataTableViewModel = tempDataTable == null ? dataTableViewModel ?? new DataTableViewModel() : tempDataTable;

            AdminSanctionPostListViewModel list = new AdminSanctionPostListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _adminSanctionPostAgent.GetAdminSanctionPostList(dataTableViewModel);
            }

            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Admin/AdminSanctionPost/_List.cshtml", list);
            }
            return View($"~/Views/Admin/AdminSanctionPost/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            AdminSanctionPostViewModel adminSanctionPostViewModel = new AdminSanctionPostViewModel();
            BindDropdown(adminSanctionPostViewModel);
            return View("~/Views/Admin/AdminSanctionPost/Create.cshtml", adminSanctionPostViewModel);
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
                    //TempData[AdminConstants.DataTableViewModel] = CreateActionDataTable(adminSanctionPostViewModel.CentreCode, Convert.ToInt32(adminSanctionPostViewModel.DepartmentId));
                    return RedirectToAction<AdminSanctionPostController>(x => x.List(null));
                }
            }
            BindDropdown(adminSanctionPostViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(adminSanctionPostViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminSanctionPost/Create.cshtml", adminSanctionPostViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int adminSanctionPostId)
        {
            AdminSanctionPostViewModel adminSanctionPostViewModel = _adminSanctionPostAgent.GetAdminSanctionPost(adminSanctionPostId);
            adminSanctionPostViewModel.SelectedCentreCode = adminSanctionPostViewModel.CentreCode;
            adminSanctionPostViewModel.SelectedDepartmentId = Convert.ToString(adminSanctionPostViewModel.DepartmentId);
            return ActionView("~/Views/Admin/AdminSanctionPost/Edit.cshtml", adminSanctionPostViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            ModelState.Remove("PostType");
            ModelState.Remove("DesignationType");
            if (ModelState.IsValid)
            {
                bool status = _adminSanctionPostAgent.UpdateAdminSanctionPost(adminSanctionPostViewModel).HasError;
                SetNotificationMessage(status
                 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                 : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    //TempData[AdminConstants.DataTableViewModel] = UpdateActionDataTable(adminSanctionPostViewModel.SelectedCentreCode, Convert.ToInt32(adminSanctionPostViewModel.SelectedDepartmentId));
                    return RedirectToAction<AdminSanctionPostController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(adminSanctionPostViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminSanctionPost/Edit.cshtml", adminSanctionPostViewModel);
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
        protected virtual void BindDropdown(AdminSanctionPostViewModel adminSanctionPostViewModel)
        {
            List<SelectListItem> postTypeList = new List<SelectListItem>();
            postTypeList.Add(new SelectListItem { Text = AdminConstants.Temporary, Value = AdminConstants.Temporary });
            postTypeList.Add(new SelectListItem { Text = AdminConstants.Permanent, Value =  AdminConstants.Permanent });
            ViewData["PostType"] = postTypeList;

            List<SelectListItem> designationTypeList = new List<SelectListItem>();
            designationTypeList.Add(new SelectListItem { Text =  AdminConstants.Regular, Value = AdminConstants.Regular });
            designationTypeList.Add(new SelectListItem { Text = AdminConstants.Addon, Value = AdminConstants.Addon });
            ViewData["DesignationType"] = designationTypeList;
        }
        #endregion
    }
}