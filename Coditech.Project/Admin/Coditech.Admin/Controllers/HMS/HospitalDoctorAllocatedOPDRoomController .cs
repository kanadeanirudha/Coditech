using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorAllocatedOPDRoomController : BaseController
    {
        private readonly IHospitalDoctorAllocatedOPDRoomAgent _hospitalDoctorAllocatedOPDRoomAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctorAllocatedOPDRoom/CreateEdit.cshtml";

        public HospitalDoctorAllocatedOPDRoomController(IHospitalDoctorAllocatedOPDRoomAgent hospitalDoctorAllocatedOPDRoomAgent)
        {
            _hospitalDoctorAllocatedOPDRoomAgent = hospitalDoctorAllocatedOPDRoomAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorAllocatedOPDRoomListViewModel list = new HospitalDoctorAllocatedOPDRoomListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorAllocatedOPDRoomAgent.GetHospitalDoctorAllocatedOPDRoomList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorAllocatedOPDRoom/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorAllocatedOPDRoom/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId)
        {
            HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel = _hospitalDoctorAllocatedOPDRoomAgent.GetHospitalDoctorAllocatedOPDRoom(hospitalDoctorId, hospitalDoctorAllocatedOPDRoomId);
            return ActionView(createEdit, hospitalDoctorAllocatedOPDRoomViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorAllocatedOPDRoomAgent.UpdateHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("List", CreateActionDataTable(hospitalDoctorAllocatedOPDRoomViewModel.SelectedCentreCode, Convert.ToInt16(hospitalDoctorAllocatedOPDRoomViewModel.SelectedDepartmentId)));
            }
            return View(createEdit, hospitalDoctorAllocatedOPDRoomViewModel);
        }

        public virtual ActionResult GetOrganisationCentrewiseBuildingRooms(string organisationCentrewiseBuildingMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuildingRooms.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingRoomId",
                Parameter = organisationCentrewiseBuildingMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }

        #region Protected

        #endregion
    }
}