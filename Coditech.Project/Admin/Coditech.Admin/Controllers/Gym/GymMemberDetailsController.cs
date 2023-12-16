using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMemberDetailsController : BaseController
    {
        private readonly IUserAgent _userAgent;
        //private readonly IGeneralGymMemberDetailsAgent _gymMemberDetailsAgent;
        private const string createEdit = "~/Views/Gym/GymMemberDetails/CreateEdit.cshtml";

        public GymMemberDetailsController(/*IGeneralGymMemberDetailsAgent gymMemberDetailsAgent*/ IUserAgent userAgent)
        {
            //_gymMemberDetailsAgent = gymMemberDetailsAgent;
            _userAgent = userAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GymMemberDetailsListViewModel list = new GymMemberDetailsListViewModel();// _gymMemberDetailsAgent.GetGymMemberDetailsList(dataTableModel);
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
                GeneralPersonViewModel generalPersonViewModel = _userAgent.InsertPersonInformation(gymCreateEditMemberViewModel.ToModel<GeneralPersonViewModel>());
                if (!generalPersonViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
                }
                else
                {
                    gymCreateEditMemberViewModel.ErrorMessage = generalPersonViewModel.ErrorMessage;
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymCreateEditMemberViewModel.ErrorMessage));
            return View(createEdit, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short gymMemberDetailsId)
        {
            GymMemberDetailsViewModel gymMemberDetailsViewModel = new GymMemberDetailsViewModel();// _gymMemberDetailsAgent.GetGymMemberDetails(gymMemberDetailsId);
            return ActionView(createEdit, gymMemberDetailsViewModel);
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

        //public virtual ActionResult Delete(string countryIds)
        //{
        //    string message = string.Empty;
        //    bool status = false;
        //    if (!string.IsNullOrEmpty(countryIds))
        //    {
        //        status = _gymMemberDetailsAgent.DeleteGymMemberDetails(countryIds, out message);
        //        SetNotificationMessage(!status
        //        ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
        //        return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
        //    }

        //    SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
        //    return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
        //}

        #region Protected
        #endregion
    }
}