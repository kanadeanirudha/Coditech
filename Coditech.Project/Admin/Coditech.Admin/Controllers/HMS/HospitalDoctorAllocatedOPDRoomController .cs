using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
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
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalDoctorAllocatedOPDRoomViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalDoctorAllocatedOPDRoomViewModel = _hospitalDoctorAllocatedOPDRoomAgent.CreateHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomViewModel);
                if (!hospitalDoctorAllocatedOPDRoomViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorAllocatedOPDRoomViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorAllocatedOPDRoomViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalDoctorAllocatedOPDRoomId)
        {
            HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel = _hospitalDoctorAllocatedOPDRoomAgent.GetHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomId);
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
                return RedirectToAction("Edit", new { hospitalDoctorAllocatedOPDRoomId = hospitalDoctorAllocatedOPDRoomViewModel.HospitalDoctorAllocatedOPDRoomId });
            }
            return View(createEdit, hospitalDoctorAllocatedOPDRoomViewModel);
        }

        public virtual ActionResult Delete(string hospitalDoctorAllocatedOPDRoomIdIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalDoctorAllocatedOPDRoomIdIds))
            {
                status = _hospitalDoctorAllocatedOPDRoomAgent.DeleteHospitalDoctorAllocatedOPDRoom(hospitalDoctorAllocatedOPDRoomIdIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalDoctorAllocatedOPDRoomController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalDoctorAllocatedOPDRoomController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}