using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorsController : BaseController
    {
        private readonly IHospitalDoctorsAgent _hospitalDoctorsAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctors/CreateEdit.cshtml";

        public HospitalDoctorsController(IHospitalDoctorsAgent hospitalDoctorsAgent)
        {
            _hospitalDoctorsAgent = hospitalDoctorsAgent;
        }

        public virtual ActionResult List(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, DataTableViewModel dataTableModel)
        {
            HospitalDoctorsListViewModel list = new HospitalDoctorsListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorsAgent.GetHospitalDoctorsList(selectedCentreCode, selectedDepartmentId, true, dataTableModel);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctors/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctors/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalDoctorsViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalDoctorsViewModel = _hospitalDoctorsAgent.CreateHospitalDoctors(hospitalDoctorsViewModel);
                if (!hospitalDoctorsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = hospitalDoctorsViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorsViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorsViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int doctorId)
        {
            HospitalDoctorsViewModel hospitalDoctorsViewModel = _hospitalDoctorsAgent.GetHospitalDoctors(doctorId);
            return ActionView(createEdit, hospitalDoctorsViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorsAgent.UpdateHospitalDoctors(hospitalDoctorsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { doctorId = hospitalDoctorsViewModel.HospitalDoctorId });
            }
            return View(createEdit, hospitalDoctorsViewModel);
        }

        public virtual ActionResult Delete(string hospitalDoctorIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalDoctorIds))
            {
                status = _hospitalDoctorsAgent.DeleteHospitalDoctors(hospitalDoctorIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", CreateActionDataTable());
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", CreateActionDataTable());
        }

        public virtual ActionResult GetOrganisationCentrewiseBuildingRoomByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuildingRooms.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingRoomId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetEmployeeList(string selectedCentreCode, short selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.UnAssociatedEmployeeList.ToString(),
                DropdownName = "EmployeeId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentId",
                Parameter = centreCode,
                ChangeEvent = "GetEmployeeList()"
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetOrganisationCentrewiseBuildingByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuilding.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingMasterId",
                Parameter = centreCode,
                ChangeEvent = ""
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }
        #region Protected

        #endregion
    }
}
