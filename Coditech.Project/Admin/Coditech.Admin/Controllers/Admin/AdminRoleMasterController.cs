using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AdminRoleMasterController : BaseController
    {
        private readonly IAdminRoleMasterAgent _adminRoleMasterAgent;
        public AdminRoleMasterController(IAdminRoleMasterAgent adminRoleMasterAgent)
        {
            _adminRoleMasterAgent = adminRoleMasterAgent;
        }

        public ActionResult List(DataTableViewModel dataTableViewModel)
        {
            //DataTableViewModel tempDataTable = TempData[AdminConstants.DataTableViewModel] as DataTableViewModel;
            //dataTableViewModel = tempDataTable == null ? dataTableViewModel ?? new DataTableViewModel() : tempDataTable;

            AdminRoleListViewModel list = new AdminRoleListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _adminRoleMasterAgent.GetAdminRoleMasterList(dataTableViewModel);
            }

            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Admin/AdminRoleMaster/_List.cshtml", list);
            }
            return View($"~/Views/Admin/AdminRoleMaster/List.cshtml", list);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    AdminRoleMasterViewModel adminRoleMasterViewModel = new AdminRoleMasterViewModel();
        //    BindDropdown(adminRoleMasterViewModel);
        //    return View("~/Views/Admin/AdminRoleMaster/Create.cshtml", adminRoleMasterViewModel);
        //}

        //[HttpPost]
        //public virtual ActionResult Create(AdminRoleMasterViewModel adminRoleMasterViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        adminRoleMasterViewModel = _adminRoleMasterAgent.CreateAdminRoleMaster(adminRoleMasterViewModel);
        //        if (!adminRoleMasterViewModel.HasError)
        //        {
        //            SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
        //            //TempData[AdminConstants.DataTableViewModel] = CreateActionDataTable(adminRoleMasterViewModel.CentreCode, Convert.ToInt32(adminRoleMasterViewModel.DepartmentId));
        //            return RedirectToAction<AdminRoleMasterController>(x => x.List(null));
        //        }
        //    }
        //    BindDropdown(adminRoleMasterViewModel);
        //    SetNotificationMessage(GetErrorNotificationMessage(adminRoleMasterViewModel.ErrorMessage));
        //    return View("~/Views/Admin/AdminRoleMaster/Create.cshtml", adminRoleMasterViewModel);
        //}

        //[HttpGet]
        //public virtual ActionResult Edit(int adminRoleMasterId)
        //{
        //    AdminRoleMasterViewModel adminRoleMasterViewModel = _adminRoleMasterAgent.GetAdminRoleMaster(adminRoleMasterId);
        //    adminRoleMasterViewModel.SelectedCentreCode = adminRoleMasterViewModel.CentreCode;
        //    adminRoleMasterViewModel.SelectedDepartmentId = Convert.ToString(adminRoleMasterViewModel.DepartmentId);
        //    return ActionView("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleMasterViewModel);
        //}

        //[HttpPost]
        //public virtual ActionResult Edit(AdminRoleMasterViewModel adminRoleMasterViewModel)
        //{
        //    ModelState.Remove("PostType");
        //    ModelState.Remove("DesignationType");
        //    if (ModelState.IsValid)
        //    {
        //        bool status = _adminRoleMasterAgent.UpdateAdminRoleMaster(adminRoleMasterViewModel).HasError;
        //        SetNotificationMessage(status
        //         ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
        //         : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

        //        if (!status)
        //        {
        //            //TempData[AdminConstants.DataTableViewModel] = UpdateActionDataTable(adminRoleMasterViewModel.SelectedCentreCode, Convert.ToInt32(adminRoleMasterViewModel.SelectedDepartmentId));
        //            return RedirectToAction<AdminRoleMasterController>(x => x.List(null));
        //        }
        //    }
        //    SetNotificationMessage(GetErrorNotificationMessage(adminRoleMasterViewModel.ErrorMessage));
        //    return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleMasterViewModel);
        //}

        //public virtual ActionResult Delete(string departmentIds)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(departmentIds))
        //    {
        //        status = _adminRoleMasterAgent.DeleteAdminRoleMaster(departmentIds, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<AdminRoleMasterController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<AdminRoleMasterController>(x => x.List(null));
        //}

        //#region Protected
        //protected virtual void BindDropdown(AdminRoleMasterViewModel adminRoleMasterViewModel)
        //{
        //    List<SelectListItem> postTypeList = new List<SelectListItem>();
        //    postTypeList.Add(new SelectListItem { Text = "Temporary", Value = "Temporary" });
        //    postTypeList.Add(new SelectListItem { Text = "Permanent", Value = "Permanent" });
        //    ViewData["PostType"] = postTypeList;

        //    List<SelectListItem> designationTypeList = new List<SelectListItem>();
        //    designationTypeList.Add(new SelectListItem { Text = "Regular", Value = "Regular" });
        //    designationTypeList.Add(new SelectListItem { Text = "AddOn", Value = "AddOn" });
        //    ViewData["DesignationType"] = designationTypeList;
        //}
        //#endregion
    }
}