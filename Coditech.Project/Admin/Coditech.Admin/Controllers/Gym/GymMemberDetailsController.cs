﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMemberDetailsController : BaseController
    {
        private readonly IGymMemberDetailsAgent _gymMemberDetailsAgent;
        private const string createEdit = "~/Views/Gym/GymMemberDetails/CreateEdit.cshtml";

        public GymMemberDetailsController(IGymMemberDetailsAgent gymMemberDetailsAgent)
        {
            _gymMemberDetailsAgent = gymMemberDetailsAgent;
        }

        #region GymMemberDetails
        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GymMemberDetailsListViewModel list = _gymMemberDetailsAgent.GetGymMemberDetailsList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/List.cshtml", list);
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
                gymCreateEditMemberViewModel = _gymMemberDetailsAgent.CreateMemberDetails(gymCreateEditMemberViewModel);
                if (!gymCreateEditMemberViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymCreateEditMemberViewModel.ErrorMessage));
            return View(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateMemberPersonalDetails(int gymMemberDetailId, long personId)
        {
            GymCreateEditMemberViewModel gymCreateEditMemberViewModel = _gymMemberDetailsAgent.GetMemberPersonalDetails(personId);
            gymCreateEditMemberViewModel.GymMemberDetailId = gymMemberDetailId;
            return ActionView(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateMemberPersonalDetails(GymCreateEditMemberViewModel gymCreateEditMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMemberDetailsAgent.UpdateMemberPersonalDetails(gymCreateEditMemberViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateMemberPersonalDetails", new { gymMemberDetailId = gymCreateEditMemberViewModel.GymMemberDetailId, personId = gymCreateEditMemberViewModel.PersonId });
            }
            return View(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult MemberOtherDetails(int gymMemberDetailId)
        {
            GymMemberDetailsViewModel gymMemberDetailsViewModel = _gymMemberDetailsAgent.GetGymMemberOtherDetails(gymMemberDetailId);
            return View("~/Views/Gym/GymMemberDetails/UpdateGymMemberOtherDetails.cshtml", gymMemberDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult MemberOtherDetails(GymMemberDetailsViewModel gymMemberDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMemberDetailsAgent.UpdateGymMemberOtherDetails(gymMemberDetailsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("MemberOtherDetails", new { gymMemberDetailId = gymMemberDetailsViewModel.GymMemberDetailId, personId = gymMemberDetailsViewModel.PersonId });
            }
            return View("~/Views/Gym/GymMemberDetails/UpdateGymMemberOtherDetails.cshtml", gymMemberDetailsViewModel);
        }
        public virtual ActionResult Delete(string gymMemberDetailIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gymMemberDetailIds))
            {
                status = _gymMemberDetailsAgent.DeleteGymMembers(gymMemberDetailIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
        }
        #endregion

        #region MemberFollowUp
        public ActionResult MemberFollowUpList(int gymMemberDetailId, long personId, DataTableViewModel dataTableModel)
        {
            GymMemberFollowUpListViewModel list = _gymMemberDetailsAgent.GymMemberFollowUpList(gymMemberDetailId, personId, dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_GymMemberFollowUpList.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/GymMemberFollowUpList.cshtml", list);
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
                return PartialView("~/Views/Gym/GymMemberDetails/_CreateEditMemberFollowUp.cshtml", model);
            }
            return View($"~/Views/Gym/GymMemberDetails/_CreateEditMemberFollowUp.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreatEditMemberFollowUp(GymMemberFollowUpViewModel gymMemberFollowUpViewModel)
        {
            if (ModelState.IsValid)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_CreateEditMemberFollowUp.cshtml", gymMemberFollowUpViewModel);
            }
            return PartialView($"~/Views/Gym/GymMemberDetails/_CreateEditMemberFollowUp.cshtml", gymMemberFollowUpViewModel);
        }
        #endregion

        #region Protected
        #endregion
    }
}