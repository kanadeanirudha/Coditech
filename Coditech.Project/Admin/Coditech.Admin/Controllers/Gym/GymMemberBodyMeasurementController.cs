using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMemberBodyMeasurementController : BaseController
    {
        private readonly IGymMemberBodyMeasurementAgent _gymMemberBodyMeasurementAgent;
        private const string createEdit = "~/Views/Gym/GymMemberBodyMeasurement/CreateEdit.cshtml";

        public GymMemberBodyMeasurementController(IGymMemberBodyMeasurementAgent gymMemberBodyMeasurementAgent)
        {
            _gymMemberBodyMeasurementAgent = gymMemberBodyMeasurementAgent;
        }

        [HttpGet]
        public virtual ActionResult GetBodyMeasurementTypeListByMemberId(int gymMemberDetailId, long personId)
        {
            GymMemberBodyMeasurementListViewModel list = _gymMemberBodyMeasurementAgent.GetBodyMeasurementTypeListByMemberId(gymMemberDetailId, personId, 3);
            return ActionView("~/Views/Gym/GymMemberBodyMeasurement/GymMemberBodyMeasurement.cshtml", list);
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GymMemberBodyMeasurementListViewModel list = _gymMemberBodyMeasurementAgent.GetMemberBodyMeasurementList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberBodyMeasurement/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberBodyMeasurement/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GymMemberBodyMeasurementViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GymMemberBodyMeasurementViewModel gymMemberBodyMeasurementViewModel)
        {
            if (ModelState.IsValid)
            {
                gymMemberBodyMeasurementViewModel = _gymMemberBodyMeasurementAgent.CreateMemberBodyMeasurement(gymMemberBodyMeasurementViewModel);
                if (!gymMemberBodyMeasurementViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMemberBodyMeasurementController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymMemberBodyMeasurementViewModel.ErrorMessage));
            return View(createEdit, gymMemberBodyMeasurementViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long gymMemberBodyMeasurementId)
        {
            GymMemberBodyMeasurementViewModel gymMemberBodyMeasurementViewModel = _gymMemberBodyMeasurementAgent.GetMemberBodyMeasurement(gymMemberBodyMeasurementId);
            return ActionView(createEdit, gymMemberBodyMeasurementViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GymMemberBodyMeasurementViewModel gymMemberBodyMeasurementViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMemberBodyMeasurementAgent.UpdateMemberBodyMeasurement(gymMemberBodyMeasurementViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { gymMemberBodyMeasurementId = gymMemberBodyMeasurementViewModel.GymMemberBodyMeasurementId });
            }
            return View(createEdit, gymMemberBodyMeasurementViewModel);
        }

        public virtual ActionResult Delete(string MemberBodyMeasurementIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(MemberBodyMeasurementIds))
            {
                status = _gymMemberBodyMeasurementAgent.DeleteMemberBodyMeasurement(MemberBodyMeasurementIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GymMemberBodyMeasurementController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GymMemberBodyMeasurementController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}