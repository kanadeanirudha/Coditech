using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
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
        public virtual ActionResult Edit(long personId)
        {
            GymCreateEditMemberViewModel gymCreateEditMemberViewModel = _gymMemberDetailsAgent.GetMemberDetails(personId);
            return ActionView(createEdit, gymCreateEditMemberViewModel);
        }

        //[HttpPost]
        //public virtual ActionResult Edit(GeneralGymMemberDetailsViewModel gymMemberDetailsViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SetNotificationMessage(_gymMemberDetailsAgent.UpdateGymMemberDetails(gymMemberDetailsViewModel).HasError
        //        ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
        //        return RedirectToAction("Edit", new { gymMemberDetailsId = gymMemberDetailsViewModel.GymMemberDetailsId });
        //    }
        //    return View(createEdit, gymMemberDetailsViewModel);
        //}

        public virtual ActionResult Delete(string gymMemberDetailIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gymMemberDetailIds))
            {
                status = _gymMemberDetailsAgent.DeleteMembers(gymMemberDetailIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
        }

        #region Protected
        #endregion
    }
}