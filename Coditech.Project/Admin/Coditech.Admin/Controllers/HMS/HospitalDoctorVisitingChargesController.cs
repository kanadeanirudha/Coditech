using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
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
            HospitalDoctorVisitingChargesListViewModel list = new HospitalDoctorVisitingChargesListViewModel();
            if (dataTableModel.HospitalDoctorId > 0)
            {
                list = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingChargesByDoctorList(dataTableModel.HospitalDoctorId, dataTableModel);
            }
            list.HospitalDoctorId = dataTableModel.HospitalDoctorId;

            return View($"~/Views/HMS/HospitalDoctorVisitingCharges/HospitalDoctorVisitingChargesByDoctorIdList.cshtml", list);

        }

        [HttpGet]
        public virtual ActionResult Create(int hospitalDoctorId)
        {
            return View(createEdit, new HospitalDoctorVisitingChargesViewModel() { HospitalDoctorId = hospitalDoctorId });
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
                    dataTableModel.HospitalDoctorId = hospitalDoctorVisitingChargesViewModel.HospitalDoctorId;
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    //return View($"~/Views/HMS/HospitalDoctorVisitingCharges/HospitalDoctorVisitingChargesByDoctorIdList.cshtml", listModel);
                    return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", dataTableModel);
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorVisitingChargesViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalDoctorVisitingChargesId,int hospitalDoctorId)
        {
            HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesId,hospitalDoctorId);
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
			DataTableViewModel dataTableModel = new DataTableViewModel();

			if (ModelState.IsValid)
            {
				dataTableModel.HospitalDoctorId = hospitalDoctorVisitingChargesViewModel.HospitalDoctorId;

				SetNotificationMessage(_hospitalDoctorVisitingChargesAgent.UpdateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetHospitalDoctorVisitingChargesByDoctorIdList", dataTableModel);
            }
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        public virtual ActionResult Delete(long hospitaldoctorvisitingchargesId)
        {
            string message = string.Empty;
            bool status = false;
            if (hospitaldoctorvisitingchargesId >= 0)
            {
                status = _hospitalDoctorVisitingChargesAgent.DeleteHospitalDoctorVisitingCharges(hospitaldoctorvisitingchargesId, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalDoctorVisitingChargesController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalDoctorVisitingChargesController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}