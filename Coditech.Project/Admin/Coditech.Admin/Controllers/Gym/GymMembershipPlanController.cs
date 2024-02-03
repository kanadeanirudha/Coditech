using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMembershipPlanController : BaseController
    {
        private readonly IGymMembershipPlanAgent _gymMembershipPlanAgent;
        private const string createEdit = "~/Views/Gym/GymMembershipPlan/CreateEdit.cshtml";

        public GymMembershipPlanController(IGymMembershipPlanAgent gymMembershipPlanAgent)
        {
            _gymMembershipPlanAgent = gymMembershipPlanAgent;
        }

        #region GymMembershipPlan
        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GymMembershipPlanListViewModel list = _gymMembershipPlanAgent.GetGymMembershipPlanList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMembershipPlan/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymMembershipPlan/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult CreateGymMembershipPlan()
        {
            GymMembershipPlanViewModel viewModel = new GymMembershipPlanViewModel();
            return View(createEdit, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateGymMembershipPlan(GymMembershipPlanViewModel gymMembershipPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                gymMembershipPlanViewModel = _gymMembershipPlanAgent.CreateGymMembershipPlan(gymMembershipPlanViewModel);
                if (!gymMembershipPlanViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymMembershipPlanViewModel.ErrorMessage));
            return View(createEdit, gymMembershipPlanViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateGymMembershipPlan(int gymMembershipPlanId)
        {
            GymMembershipPlanViewModel gymMembershipPlanViewModel = _gymMembershipPlanAgent.GetGymMembershipPlan(gymMembershipPlanId);
            return ActionView(createEdit, gymMembershipPlanViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateGymMembershipPlan(GymMembershipPlanViewModel gymMembershipPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMembershipPlanAgent.UpdateGymMembershipPlan(gymMembershipPlanViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateGymMembershipPlan", new { gymMembershipPlanId = gymMembershipPlanViewModel.GymMembershipPlanId});
            }
            return View(createEdit, gymMembershipPlanViewModel);
        }

        public virtual ActionResult Delete(string gymMembershipPlanIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gymMembershipPlanIds))
            {
                status = _gymMembershipPlanAgent.DeleteGymMembershipPlan(gymMembershipPlanIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
        }
        #endregion
    }
}