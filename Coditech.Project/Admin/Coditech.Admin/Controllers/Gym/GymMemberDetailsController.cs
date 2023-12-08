using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            GymMemberDetailsViewModel viewModel = new GymMemberDetailsViewModel()
            {
                GeneralPersonViewModel = new GeneralPersonViewModel()
                {
                    UserType = UserTypeEnum.GymMember.ToString()
                }
            };

            ViewBag.MaritalStatusList = new List<SelectListItem>
            {
             new SelectListItem { Value = "Married", Text = "Married" },
             new SelectListItem { Value = "Single", Text = "Single" },
            };

            ViewBag.BloodGroups = new List<string>
            {   "A+","A-", "B+", "B-", "AB+", "AB-","O+", "O-" };

            BindDropdown(viewModel.GeneralPersonViewModel);
            return View(createEdit, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateMember(GeneralPersonViewModel generalPersonViewModel)
        {
            GymMemberDetailsViewModel viewModel = new GymMemberDetailsViewModel()
            {
                GeneralPersonViewModel = generalPersonViewModel
            };

            string selectedMaritalStatus = generalPersonViewModel.MaritalStatus;

            if (ModelState.IsValid)
            {
                generalPersonViewModel = _userAgent.InsertPersonInformation(generalPersonViewModel);
                if (!generalPersonViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GymMemberDetailsController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalPersonViewModel.ErrorMessage));
            return View(createEdit, viewModel);
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
        protected virtual void BindDropdown(GeneralPersonViewModel generalPersonViewModel)
        {
            generalPersonViewModel.GenderlList = CoditechDropdownHelper.GetGeneralDropdownList("Gender", Convert.ToString(generalPersonViewModel.GenderEnumId));
            generalPersonViewModel.IndentificationList = CoditechDropdownHelper.GetGeneralDropdownList("IndentificationType", Convert.ToString(generalPersonViewModel.IndentificationEnumId));
        }
        #endregion
    }
}