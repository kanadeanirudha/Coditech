using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;


namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorVisitingChargesController : BaseController
    {
        private readonly IHospitalDoctorVisitingChargesAgent _hospitalDoctorVisitingChargesAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctorVisitingCharges/CreateEdit.cshtml";

        public HospitalDoctorVisitingChargesController(IHospitalDoctorVisitingChargesAgent hospitalDoctorVisitingChargesAgent)
        {
            _hospitalDoctorVisitingChargesAgent = hospitalDoctorVisitingChargesAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorVisitingChargesListViewModel list = new HospitalDoctorVisitingChargesListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingChargesList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorVisitingCharges/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorVisitingCharges/List.cshtml", list);
        }
        public virtual ActionResult GetHospitalDoctorVisitingChargesByDoctorIdList(DataTableViewModel dataTableModel)
        {
            HospitalDoctorVisitingChargesListViewModel list =new HospitalDoctorVisitingChargesListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedParameter1))
            {
                list = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingChargesByDoctorList(Convert.ToInt32(dataTableModel.SelectedParameter1), dataTableModel);
            }
            list.SelectedParameter1 = dataTableModel.SelectedParameter1;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorVisitingCharges/_HospitalDoctorVisitingChargesByDoctorIdList.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorVisitingCharges/HospitalDoctorVisitingChargesByDoctorIdList.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create(int hospitalDoctorId)
        {
            HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingCharges(0, hospitalDoctorId);
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            DataTableViewModel dataTableModel = new DataTableViewModel();
            if (ModelState.IsValid)
            {
                hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.CreateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel);
                if (!hospitalDoctorVisitingChargesViewModel.HasError)
                {
                    dataTableModel.SelectedParameter1 = Convert.ToString(hospitalDoctorVisitingChargesViewModel.HospitalDoctorId);
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", dataTableModel);
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorVisitingChargesViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalDoctorVisitingChargesId, int hospitalDoctorId)
        {
            HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesId, hospitalDoctorId);
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            DataTableViewModel dataTableModel = new DataTableViewModel();

            if (ModelState.IsValid)
            {
                dataTableModel.SelectedParameter1 = Convert.ToString(hospitalDoctorVisitingChargesViewModel.HospitalDoctorId);

                SetNotificationMessage(_hospitalDoctorVisitingChargesAgent.UpdateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", UpdateActionDataTable(null, 0, dataTableModel));
            }
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        public virtual ActionResult Delete(long hospitaldoctorvisitingchargesId, int hospitalDoctorId)
        {
            string message = string.Empty;
            bool status = false;
            if (hospitaldoctorvisitingchargesId >= 0)
            {
                status = _hospitalDoctorVisitingChargesAgent.DeleteHospitalDoctorVisitingCharges(hospitaldoctorvisitingchargesId, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", new { SelectedParameter1 = hospitalDoctorId });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", new { SelectedParameter1 = hospitalDoctorId });
        }

        public virtual ActionResult Cancel(int hospitalDoctorId)
        {
            return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", new { SelectedParameter1 = hospitalDoctorId });
        }
        #region Protected

        #endregion
    }
}