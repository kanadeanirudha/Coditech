using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
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
                    return RedirectToAction("List", new { hospitalDoctorId = hospitalDoctorsViewModel.HospitalDoctorId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorsViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorsViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalDoctorId)
        {
            HospitalDoctorsViewModel hospitalDoctorsViewModel = _hospitalDoctorsAgent.GetHospitalDoctors(hospitalDoctorId);
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
                return RedirectToAction("Edit", new { hospitalDoctorId = hospitalDoctorsViewModel.HospitalDoctorId });
            }
            return View(createEdit, hospitalDoctorsViewModel);
        }

        //public virtual ActionResult Delete(string hospitalDoctorIds)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(hospitalDoctorIds))
        //    {
        //        status = _hospitalDoctorsAgent.DeleteHospitalDoctors(hospitalDoctorIds, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<HospitalDoctorsController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<HospitalDoctorsController>(x => x.List(null));
        //}

        #region Protected

        #endregion
    }
}