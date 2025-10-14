using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Coditech.Admin.Controllers
{
    public class EmployeeMasterController : BaseController
    {
        private readonly IEmployeeMasterAgent _employeeMasterAgent;
        private readonly IGeneralCommonAgent _generalCommonAgent;
        private readonly IUserAgent _userAgent;
        private const string createEditEmployee = "~/Views/EmployeeMaster/CreateEditEmployee.cshtml";
        private readonly IEmployeeServiceAgent _employeeServiceAgent;
        private const string createEditEmployeeService = "~/Views/EmployeeMaster/EmployeeService/UpdateEmployeeServiceDetails.cshtml";

        public EmployeeMasterController(IEmployeeMasterAgent employeeMasterAgent, IGeneralCommonAgent generalCommonAgent, IEmployeeServiceAgent employeeServiceAgent, IUserAgent userAgent)
        {
            _employeeMasterAgent = employeeMasterAgent;
            _employeeServiceAgent = employeeServiceAgent;
            _generalCommonAgent = generalCommonAgent;
            _userAgent = userAgent;
        }

        #region Employee
        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            EmployeeMasterListViewModel list = new EmployeeMasterListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _employeeMasterAgent.GetEmployeeMasterList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/_List.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreateEmployee()
        {
            return View(createEditEmployee, new EmployeeCreateEditViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel, string selectedCentreCode, int selectedDepartmentId)
        {
            if (ModelState.IsValid)
            {
                employeeCreateEditViewModel = _employeeMasterAgent.CreateEmployee(employeeCreateEditViewModel);
                if (!employeeCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // Redirect to the List action with selectedCentreCode and selectedDepartmentId
                    return RedirectToAction("UpdateEmployeePersonalDetails", new { employeeId = employeeCreateEditViewModel.EntityId, personId = employeeCreateEditViewModel.PersonId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeCreateEditViewModel.ErrorMessage));
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateEmployeePersonalDetails(long employeeId, long personId)
        {
            EmployeeCreateEditViewModel employeeCreateEditViewModel = _employeeMasterAgent.GetEmployeePersonalDetails(employeeId, personId);
            return ActionView(createEditEmployee, employeeCreateEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeePersonalDetails(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeMasterAgent.UpdateEmployeePersonalDetails(employeeCreateEditViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(employeeCreateEditViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("UpdateEmployeePersonalDetails", new { employeeId = employeeCreateEditViewModel.EmployeeId, personId = employeeCreateEditViewModel.PersonId });
                }
                else if (string.Equals(employeeCreateEditViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList, new { selectedCentreCode = employeeCreateEditViewModel.SelectedCentreCode, selectedDepartmentId = employeeCreateEditViewModel.SelectedDepartmentId });
                }
            }
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        public virtual ActionResult Delete(string employeeIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(employeeIds))
            {
                status = _employeeMasterAgent.DeleteEmployee(employeeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<EmployeeMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<EmployeeMasterController>(x => x.List(null));
        }


        #endregion Employee

        #region Employee Other Detail
        [HttpGet]
        public virtual ActionResult GetEmployeeOtherDetail(long employeeId, long personId)
        {
            EmployeeMasterViewModel employeeMasterViewModel = _employeeMasterAgent.GetEmployeeOtherDetail(employeeId);
            return View("~/Views/EmployeeMaster/UpdateEmployeeeDetails.cshtml", employeeMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeeOtherDetail(EmployeeMasterViewModel employeeMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeMasterAgent.UpdateEmployeeOtherDetail(employeeMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetEmployeeOtherDetail", new { employeeId = employeeMasterViewModel.EmployeeId, personId = employeeMasterViewModel.PersonId });
            }
            return View("~/Views/EmployeeMaster/UpdateEmployeeeDetails.cshtml", employeeMasterViewModel);
        }
        #endregion Employee Other Detail

        #region Employee Address
        //[HttpGet]
        //public virtual ActionResult CreateEditEmployeeAddress(long employeeId, long personId)
        //{
        //    if (TempData["GeneralPersonAddressViewModel"] is string serializedModel)
        //    {
        //        TempData.Keep();
        //        GeneralPersonAddressListViewModel model = _userAgent.GetGeneralPersonAddresses(personId);
        //        GeneralPersonAddressViewModel viewModel = JsonConvert.DeserializeObject<GeneralPersonAddressViewModel>(serializedModel);
        //        model.GeneralPersonAddressList = new List<GeneralPersonAddressViewModel> { viewModel };
        //        model.PersonId = viewModel.PersonId;
        //        model.EntityType = viewModel.EntityType;
        //        model.EntityId = viewModel.EntityId;
        //        model.GeneralPersonAddressList = new List<GeneralPersonAddressViewModel> { viewModel };
        //        return ActionView("~/Views/EmployeeMaster/CreateEditEmployeeAddress.cshtml", model);

        //    }
        //    else
        //    {
        //        GeneralPersonAddressListViewModel model = new GeneralPersonAddressListViewModel()
        //        {
        //            EntityId = employeeId,
        //            PersonId = personId,
        //            EntityType = UserTypeEnum.Employee.ToString()

        //        };
        //        return ActionView("~/Views/EmployeeMaster/CreateEditEmployeeAddress.cshtml", model);

        //    }
        //}

        //[HttpPost]
        //public virtual ActionResult CreateEditGeneralPersonalAddress(GeneralPersonAddressViewModel generalPersonAddressViewModel)
        //{
        //    if (!generalPersonAddressViewModel.AddressData.IsNullOrEmpty())
        //    {
        //        BindAddressToPostalCodeViewModel bindAddressToPostalCodeViewModel = new BindAddressToPostalCodeViewModel();
        //        bindAddressToPostalCodeViewModel = JsonConvert.DeserializeObject<BindAddressToPostalCodeViewModel>(generalPersonAddressViewModel.AddressData);

        //        BindAddressToPostalCodeViewModel listModel = _generalCommonAgent.ValidateAddress(bindAddressToPostalCodeViewModel);
        //        generalPersonAddressViewModel.BindAddressToPostalCodeList = listModel.BindAddressToPostalCodeList;
        //        if (generalPersonAddressViewModel.BindAddressToPostalCodeList.Count > 0)
        //        {
        //            generalPersonAddressViewModel.AddressLine1 = bindAddressToPostalCodeViewModel.Name;
        //            generalPersonAddressViewModel.GeneralCityMasterId = generalPersonAddressViewModel.BindAddressToPostalCodeList[0].SelectedCityId;
        //            generalPersonAddressViewModel.GeneralRegionMasterId = generalPersonAddressViewModel.BindAddressToPostalCodeList[0].SelectedRegionId;
        //            TempData["GeneralPersonAddressViewModel"] = JsonConvert.SerializeObject(generalPersonAddressViewModel);
        //            return RedirectToAction("CreateEditEmployeeAddress", "EmployeeMaster", new { employeeId = generalPersonAddressViewModel.EntityId, personId = generalPersonAddressViewModel.PersonId });
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        SetNotificationMessage(_userAgent.InsertUpdateGeneralPersonAddress(generalPersonAddressViewModel).HasError
        //        ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
        //    }

        //    return RedirectToAction("CreateEditEmployeeAddress", "EmployeeMaster", new { employeeId = generalPersonAddressViewModel.EntityId, personId = generalPersonAddressViewModel.PersonId });
        //}
        [HttpGet]
        public virtual ActionResult CreateEditEmployeeAddress(long employeeId, long personId)
        {
            GeneralPersonAddressListViewModel model = new GeneralPersonAddressListViewModel()
            {
                EntityId = employeeId,
                PersonId = personId,
                EntityType = UserTypeEnum.Employee.ToString()
            };
            return ActionView("~/Views/EmployeeMaster/CreateEditEmployeeAddress.cshtml", model);
        }

        [HttpPost]
        public virtual ActionResult CreateEditGeneralPersonalAddress(GeneralPersonAddressViewModel generalPersonAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_userAgent.InsertUpdateGeneralPersonAddress(generalPersonAddressViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
            }
            return RedirectToAction("CreateEditEmployeeAddress", "EmployeeMaster", new { employeeId = generalPersonAddressViewModel.EntityId, personId = generalPersonAddressViewModel.PersonId });
        }
        #endregion Employee Address

        #region Employee Service
        public virtual ActionResult EmployeeServiceList(long employeeId, long personId, DataTableViewModel dataTableViewModel)
        {
            EmployeeServiceListViewModel list = _employeeServiceAgent.GetEmployeeServiceList(employeeId, personId, dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/EmployeeService/_EmployeeServiceList.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/EmployeeService/EmployeeServiceList.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetEmployeeService(long employeeId, long personId, long employeeServiceId)
        {
            EmployeeServiceViewModel employeeServiceViewModel = _employeeServiceAgent.GetEmployeeService(employeeId, personId, employeeServiceId);
            return View(createEditEmployeeService, employeeServiceViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeServiceViewModel = _employeeServiceAgent.CreateEmployeeService(employeeServiceViewModel);
                if (!employeeServiceViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(employeeServiceViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("EmployeeServiceList", CreateActionDataTable());
                    }
                    else if (string.Equals(employeeServiceViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeServiceViewModel.ErrorMessage));
            return View(createEditEmployeeService, employeeServiceViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeServiceAgent.UpdateEmployeeService(employeeServiceViewModel).HasError
               ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
              : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("EmployeeServiceList", new { employeeId = employeeServiceViewModel.EmployeeId, personId = employeeServiceViewModel.PersonId });
            }
            return View(createEditEmployeeService, employeeServiceViewModel);
        }
        #endregion Employee Service

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }
    }
}