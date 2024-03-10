using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorsController : BaseController
    {
        private readonly IHospitalDoctorsAgent _hospitalDoctorsAgent;
        private const string create = "~/Views/HMS/HospitalDoctors/Create.cshtml";

        public HospitalDoctorsController(IHospitalDoctorsAgent hospitalDoctorsAgent)
        {
            _hospitalDoctorsAgent = hospitalDoctorsAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorsListViewModel list = new HospitalDoctorsListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorsAgent.GetHospitalDoctorsList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, true, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctors/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctors/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            List<GeneralEnumaratorModel> weekDays = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList?.Where(x => x.EnumGroupCode == DropdownTypeEnum.WeekDays.ToString())?.ToList();
            HospitalDoctorsViewModel hospitalDoctorsViewModel = new HospitalDoctorsViewModel()
            {
                AllWeekDays = weekDays
            };
            return View(create, hospitalDoctorsViewModel);
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
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = hospitalDoctorsViewModel.SelectedCentreCode, SelectedDepartmentId = Convert.ToInt16(hospitalDoctorsViewModel.SelectedDepartmentId) });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorsViewModel.ErrorMessage));
            return View(create, hospitalDoctorsViewModel);
        }

        [HttpGet]
        public virtual ActionResult GetHospitalDoctors(int hospitalDoctorId, long employeeId)
        {
            HospitalDoctorEditViewModel hospitalDoctorEditViewModel = _hospitalDoctorsAgent.GetEmployeeDetails(hospitalDoctorId, employeeId);
            return ActionView("~/Views/HMS/HospitalDoctors/Edit.cshtml", hospitalDoctorEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult GetHospitalDoctors(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorsAgent.UpdateHospitalDoctors(hospitalDoctorsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetHospitalDoctors", new { hospitalDoctorId = hospitalDoctorsViewModel.HospitalDoctorId });
            }
            return View("~/Views/HMS/HospitalDoctors/Edit.cshtml", hospitalDoctorsViewModel);
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

        public virtual ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentId",
                Parameter = centreCode,
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
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }


        public virtual ActionResult GetOrganisationCentrewiseRoomByBuildingId(string buildingMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuildingRooms.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingRoomId",
                Parameter = buildingMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetEmployeeList(string selectedCentreCode, string selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.UnAssociatedEmployeeList.ToString(),
                DropdownName = "EmployeeId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        #region Protected

        #endregion
    }
}
