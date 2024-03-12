using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Enum;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMemberDetailsController : BaseController
    {
        private readonly IGymMemberDetailsAgent _gymMemberDetailsAgent;
        private GeneralPersonAttendanceDetailsViewModel gymMemberDetailsViewModel;
        private readonly IGymMemberBodyMeasurementAgent _gymMemberBodyMeasurementAgent;
        private const string createEditGymMember = "~/Views/Gym/GymMemberDetails/CreateEditGymMember.cshtml";

        public GymMemberDetailsController(IGymMemberDetailsAgent gymMemberDetailsAgent, IGymMemberBodyMeasurementAgent gymMemberBodyMeasurementAgent)
        {
            _gymMemberDetailsAgent = gymMemberDetailsAgent;
            _gymMemberBodyMeasurementAgent = gymMemberBodyMeasurementAgent;
        }

        #region GymMemberDetails
        public ActionResult List(DataTableViewModel dataTableViewModel)
        {
            GymMemberDetailsListViewModel list = new GymMemberDetailsListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _gymMemberDetailsAgent.GetGymMemberDetailsList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/List.cshtml", list);
        }

        public ActionResult ActiveMemberList(DataTableViewModel dataTableModel)
        {
            GymMemberDetailsListViewModel list = _gymMemberDetailsAgent.GetGymMemberDetailsList(dataTableModel, "Active");
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/List.cshtml", list);
        }

        public ActionResult InActiveMemberList(DataTableViewModel dataTableModel)
        {
            GymMemberDetailsListViewModel list = _gymMemberDetailsAgent.GetGymMemberDetailsList(dataTableModel, "InActive");
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
            return View(createEditGymMember, viewModel);
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
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymCreateEditMemberViewModel.ErrorMessage));
            return View(createEditGymMember, gymCreateEditMemberViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateMemberPersonalDetails(int gymMemberDetailId, long personId)
        {
            GymCreateEditMemberViewModel gymCreateEditMemberViewModel = _gymMemberDetailsAgent.GetMemberPersonalDetails(personId);
            gymCreateEditMemberViewModel.GymMemberDetailId = gymMemberDetailId;
            return ActionView(createEditGymMember, gymCreateEditMemberViewModel);
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
            return View(createEditGymMember, gymCreateEditMemberViewModel);
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
                return RedirectToAction("MemberOtherDetails", new { gymMemberDetailId = gymMemberDetailsViewModel.GymMemberDetailId });
            }
            return View("~/Views/Gym/GymMemberDetails/UpdateGymMemberOtherDetails.cshtml", gymMemberDetailsViewModel);
        }
        public virtual ActionResult Delete(string gymMemberDetailIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;

            if (!string.IsNullOrEmpty(gymMemberDetailIds))
            {
                status = _gymMemberDetailsAgent.DeleteGymMembers(gymMemberDetailIds, out message);

                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));

                // Redirect to the List action with the selectedCentreCode
                return RedirectToAction("List", new { selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));

            // Redirect to the List action with the selectedCentreCode
            return RedirectToAction("List", new { selectedCentreCode });
        }


        [HttpGet]
        public virtual ActionResult CreateEditGymMemberAddress(int gymMemberDetailId, long personId)
        {
            GeneralPersonAddressListViewModel model = new GeneralPersonAddressListViewModel()
            {
                GymMemberDetailId = gymMemberDetailId,
                PersonId = personId
            };
            return ActionView("~/Views/Gym/GymMemberDetails/CreateEditGymMemberAddress.cshtml", model);
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
        public virtual ActionResult GetMemberFollowUp(int gymMemberDetailId, long gymMemberFollowUpId, long personId)
        {
            GymMemberFollowUpViewModel gymMemberDetailsViewModel = null;
            if (gymMemberFollowUpId > 0)
            {
                gymMemberDetailsViewModel = _gymMemberDetailsAgent.GetMemberFollowUp(gymMemberFollowUpId);
                gymMemberDetailsViewModel.GymMemberDetailId = gymMemberDetailId;
                gymMemberDetailsViewModel.PersonId = personId;
            }
            else
            {
                gymMemberDetailsViewModel = new GymMemberFollowUpViewModel()
                {
                    GymMemberDetailId = gymMemberDetailId,
                    GymMemberFollowUpId = gymMemberFollowUpId
                };
            }
            return PartialView($"~/Views/Gym/GymMemberDetails/_CreateEditMemberFollowUp.cshtml", gymMemberDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreatEditMemberFollowUp(GymMemberFollowUpViewModel gymMemberFollowUpViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMemberDetailsAgent.InserUpdateGymMemberFollowUp(gymMemberFollowUpViewModel).HasError
                ? gymMemberFollowUpViewModel.GymMemberFollowUpId > 0 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage) : GetErrorNotificationMessage(GeneralResources.ErrorFailedToCreate)
                : gymMemberFollowUpViewModel.GymMemberFollowUpId > 0 ? GetSuccessNotificationMessage(GeneralResources.UpdateMessage) : GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
            }
            return RedirectToAction("MemberFollowUpList", new { gymMemberDetailId = gymMemberFollowUpViewModel.GymMemberDetailId, personId = gymMemberFollowUpViewModel.PersonId });
        }

        public virtual ActionResult DeleteGymMemberFollowUp(string gymMemberFollowUpIdIds, int gymMemberDetailId, long personId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(gymMemberFollowUpIdIds))
            {
                status = _gymMemberDetailsAgent.DeleteGymMemberFollowUp(gymMemberFollowUpIdIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("MemberFollowUpList", new { gymMemberDetailId = gymMemberDetailId, personId = personId });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("MemberFollowUpList", new { gymMemberDetailId = gymMemberDetailId, personId = personId });
        }
        #endregion

        #region Gym Member Attendance
        public ActionResult MemberAttendanceDetails(int gymMemberDetailId, long personId, DataTableViewModel dataTableModel)
        {
            GeneralPersonAttendanceDetailsListViewModel list = _gymMemberDetailsAgent.GeneralPersonAttendanceDetailsList(gymMemberDetailId, personId, UserTypeEnum.GymMember.ToString(), dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/GeneralPersonAttendanceDetails/_GeneralPersonAttendanceDetailsList.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/GeneralPersonAttendanceDetails/GeneralPersonAttendanceDetailsList.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult GetGeneralPersonAttendanceDetails(int gymMemberDetailId, long generalPersonAttendanceDetailId)
        {
            GeneralPersonAttendanceDetailsViewModel gymMemberDetailsViewModel = null;
            if (generalPersonAttendanceDetailId > 0)
            {
                gymMemberDetailsViewModel = _gymMemberDetailsAgent.GetGeneralPersonAttendanceDetails(generalPersonAttendanceDetailId);
                gymMemberDetailsViewModel.GymMemberDetailId = gymMemberDetailId;

            }
            else
            {
                gymMemberDetailsViewModel = new GeneralPersonAttendanceDetailsViewModel()
                {
                    GymMemberDetailId = gymMemberDetailId,
                    GeneralPersonAttendanceDetailId = generalPersonAttendanceDetailId
                };
            }
            GymMemberDetailsViewModel gymMemberDetailModel = _gymMemberDetailsAgent.GetGymMemberOtherDetails(gymMemberDetailId);

            // Now, set the Firstname property in the ViewModel
            if (gymMemberDetailModel != null)
            {
                gymMemberDetailsViewModel.FirstName = gymMemberDetailModel.FirstName;
                gymMemberDetailsViewModel.LastName = gymMemberDetailModel.LastName;
            }

            return PartialView($"~/Views/Gym/GymMemberDetails/GeneralPersonAttendanceDetails/_CreateEditGeneralPersonAttendanceDetails.cshtml", gymMemberDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreatEditGeneralPersonAttendanceDetails(GeneralPersonAttendanceDetailsViewModel generalPersonAttendanceDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymMemberDetailsAgent.InserUpdateGeneralPersonAttendanceDetails(generalPersonAttendanceDetailsViewModel).HasError
                ? generalPersonAttendanceDetailsViewModel.GeneralPersonAttendanceDetailId > 0 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage) : GetErrorNotificationMessage(GeneralResources.ErrorFailedToCreate)
                : generalPersonAttendanceDetailsViewModel.GeneralPersonAttendanceDetailId > 0 ? GetSuccessNotificationMessage(GeneralResources.UpdateMessage) : GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
            }
            return RedirectToAction("MemberAttendanceDetails", new { gymMemberDetailId = generalPersonAttendanceDetailsViewModel.GymMemberDetailId, personId = generalPersonAttendanceDetailsViewModel.PersonId });
        }

        public virtual ActionResult DeleteGeneralPersonAttendanceDetails(string generalPersonAttendanceDetailIdIds, int gymMemberDetailId, long personId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalPersonAttendanceDetailIdIds))
            {
                status = _gymMemberDetailsAgent.DeleteGeneralPersonAttendanceDetails(generalPersonAttendanceDetailIdIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("GeneralPersonAttendanceDetailsList", new { gymMemberDetailId = gymMemberDetailId, personId = personId });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("GeneralPersonAttendanceDetailsList", new { gymMemberDetailId = gymMemberDetailId, personId = personId });
        }
        #endregion

        #region GymMemberBodyMeasurement
        [HttpGet]
        public virtual ActionResult GetBodyMeasurementTypeListByMemberId(int gymMemberDetailId, long personId)
        {
            GymMemberBodyMeasurementListViewModel list = _gymMemberBodyMeasurementAgent.GetBodyMeasurementTypeListByMemberId(gymMemberDetailId, personId, 4);
            return ActionView("~/Views/Gym/GymMemberDetails/GymMemberBodyMeasurement/GymMemberBodyMeasurement.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetGymMemberBodyMeasurement(GymMemberBodyMeasurementViewModel gymMemberBodyMeasurementViewModel)
        {
            gymMemberBodyMeasurementViewModel.CreatedDate = DateTime.Now.ToShortDateString();
            return PartialView("~/Views/Gym/GymMemberDetails/GymMemberBodyMeasurement/_GymMemberBodyMeasurementPopUp.cshtml", gymMemberBodyMeasurementViewModel);
        }

        [HttpPost]
        public virtual ActionResult AddGymMemberBodyMeasurement(GymMemberBodyMeasurementViewModel gymMemberBodyMeasurementViewModel)
        {
            if (ModelState.IsValid)
            {
                gymMemberBodyMeasurementViewModel = _gymMemberBodyMeasurementAgent.CreateMemberBodyMeasurement(gymMemberBodyMeasurementViewModel);
                if (!gymMemberBodyMeasurementViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("GetBodyMeasurementTypeListByMemberId", new { gymMemberDetailId = gymMemberBodyMeasurementViewModel.GymMemberDetailId, personId = gymMemberBodyMeasurementViewModel.PersonId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymMemberBodyMeasurementViewModel.ErrorMessage));
            return PartialView("~/Views/Gym/GymMemberDetails/_GymMemberBodyMeasurementPopUp.cshtml", gymMemberBodyMeasurementViewModel);
        }
        #endregion

        #region GymMemberMembershipPlan
        [HttpGet]
        public virtual ActionResult GetGymMemberMembershipPlan(int gymMemberDetailId, long personId)
        {
            GymMemberMembershipPlanListViewModel gymMemberMembershipPlanListViewModel = _gymMemberDetailsAgent.GetGymMemberMembershipPlan(gymMemberDetailId, personId);
            return View($"~/Views/Gym/GymMemberDetails/GymMemberMembershipPlan/GymMemberMembershipPlan.cshtml", gymMemberMembershipPlanListViewModel);
        }

        [HttpGet]
        public virtual ActionResult AssociateGymMemberMembershipPlan(int gymMemberDetailId, long personId)
        {
            GymMemberDetailsViewModel gymMemberDetailsViewModel = _gymMemberDetailsAgent.GetGymMemberOtherDetails(gymMemberDetailId);
            GymMemberMembershipPlanViewModel gymMemberMembershipPlanViewModel = new GymMemberMembershipPlanViewModel()
            {
                CentreCode = gymMemberDetailsViewModel.CentreCode,
                FirstName = gymMemberDetailsViewModel.FirstName,
                LastName = gymMemberDetailsViewModel.LastName,
                GymMemberDetailId = gymMemberDetailId,
                PersonId = personId
            };
            return View($"~/Views/Gym/GymMemberDetails/GymMemberMembershipPlan/AssociateGymMemberMembershipPlan.cshtml", gymMemberMembershipPlanViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AssociateGymMemberMembershipPlan(GymMemberMembershipPlanViewModel gymMemberMembershipPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                gymMemberMembershipPlanViewModel = _gymMemberDetailsAgent.AssociateGymMemberMembershipPlan(gymMemberMembershipPlanViewModel);
                if (!gymMemberMembershipPlanViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("GetGymMemberMembershipPlan", new { gymMemberDetailId = gymMemberMembershipPlanViewModel.GymMemberDetailId, personId = gymMemberMembershipPlanViewModel.PersonId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymMemberMembershipPlanViewModel.ErrorMessage));
            return View($"~/Views/Gym/GymMemberDetails/GymMemberMembershipPlan/AssociateGymMemberMembershipPlan.cshtml", gymMemberMembershipPlanViewModel);
        }

        [HttpGet]
        public ActionResult MemberPaymentHistoryList(int gymMemberDetailId, DataTableViewModel dataTableModel)
        {
            GymMemberMembershipPlanListViewModel list = _gymMemberDetailsAgent.GymMemberPaymentHistoryList(gymMemberDetailId, dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymMemberDetails/_GymMemberPaymentHistory.cshtml", list);
            }
            return View($"~/Views/Gym/GymMemberDetails/GymMemberPaymentHistory.cshtml", list);
        }
        #endregion
    }
}
