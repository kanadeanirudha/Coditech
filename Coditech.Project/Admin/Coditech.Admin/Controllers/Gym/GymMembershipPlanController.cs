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
        public ActionResult CreateMember()
        {
            GymCreateEditMemberViewModel viewModel = new GymCreateEditMemberViewModel();
            return View(createEdit, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateMember(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                gymCreateEditMemberViewModel = _gymMembershipPlanAgent.CreateMembershipPlan(gymCreateEditMemberViewModel);
                if (!gymCreateEditMemberViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymCreateEditMemberViewModel.ErrorMessage));
            return View(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateMemberPersonalshipPlan(int gymMemberDetailId, long personId)
        {
            GymCreateEditMemberViewModel gymCreateEditMemberViewModel = _gymMembershipPlanAgent.GetMemberPersonalshipPlan(personId);
            gymCreateEditMemberViewModel.GymMemberDetailId = gymMemberDetailId;
            return ActionView(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateMemberPersonalshipPlan(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMembershipPlanAgent.UpdateMemberPersonalshipPlan(gymCreateEditMemberViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateMemberPersonalshipPlan", new { gymMemberDetailId = gymCreateEditMemberViewModel.GymMemberDetailId, personId = gymCreateEditMemberViewModel.PersonId });
            }
            return View(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult MemberOthershipPlan(int gymMemberDetailId)
        {
            GymMembershipPlanViewModel gymMembershipPlanViewModel = _gymMembershipPlanAgent.GetGymMemberOthershipPlan(gymMemberDetailId);
            return View("~/Views/Gym/GymMembershipPlan/UpdateGymMemberOthershipPlan.cshtml", gymMembershipPlanViewModel);
        }

        [HttpPost]
        public virtual ActionResult MemberOthershipPlan(GymMembershipPlanViewModel gymMembershipPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMembershipPlanAgent.UpdateGymMemberOthershipPlan(gymMembershipPlanViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("MemberOthershipPlan", new { gymMemberDetailId = gymMembershipPlanViewModel.GymMemberDetailId, personId = gymMembershipPlanViewModel.PersonId });
            }
            return View("~/Views/Gym/GymMembershipPlan/UpdateGymMemberOthershipPlan.cshtml", gymMembershipPlanViewModel);
        }
        public virtual ActionResult Delete(string gymMemberDetailIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gymMemberDetailIds))
            {
                status = _gymMembershipPlanAgent.DeleteGymMembers(gymMemberDetailIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GymMembershipPlanController>(x => x.List(null));
        }
        #endregion

        #region MemberFollowUp
        public ActionResult MemberFollowUpList(int gymMemberDetailId, long personId, DataTableViewModel dataTableModel)
        {
            GymMemberFollowUpListViewModel list = _gymMembershipPlanAgent.GymMemberFollowUpList(gymMemberDetailId, personId, dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMembershipPlan/_GymMemberFollowUpList.cshtml", list);
            }
            return View($"~/Views/Gym/GymMembershipPlan/GymMemberFollowUpList.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult GetMemberFollowUp(int gymMemberDetailId, long gymMemberFollowUpId)
        {
            GymMemberFollowUpViewModel model = new GymMemberFollowUpViewModel()
            {
                GymMemberDetailId = gymMemberDetailId,
                GymMemberFollowUpId = gymMemberFollowUpId
            };
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMembershipPlan/_CreateEditMemberFollowUp.cshtml", model);
            }
            return View($"~/Views/Gym/GymMembershipPlan/_CreateEditMemberFollowUp.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreatEditMemberFollowUp(GymMemberFollowUpViewModel gymMemberFollowUpViewModel)
        {
            if (ModelState.IsValid)
            {
                return PartialView("~/Views/Gym/GymMembershipPlan/_CreateEditMemberFollowUp.cshtml", gymMemberFollowUpViewModel);
            }
            return PartialView($"~/Views/Gym/GymMembershipPlan/_CreateEditMemberFollowUp.cshtml", gymMemberFollowUpViewModel);
        }
        #endregion

        #region Protected
        #endregion
    }
}