using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymWorkoutPlanController : BaseController
    {
        private readonly IGymWorkoutPlanAgent _gymWorkoutPlanAgent;
        private readonly IGymWorkoutPlanAgent _gymWorkoutPlanDetailsAgent;
        private readonly IGymWorkoutPlanAgent _gymWorkoutPlanSetAgent;

        private const string createEditGymWorkoutPlan = "~/Views/Gym/GymWorkoutPlan/GymWorkoutPlan.cshtml";

        public GymWorkoutPlanController(IGymWorkoutPlanAgent gymWorkoutPlanAgent, IGymWorkoutPlanAgent gymWorkoutPlanDetailsAgent, IGymWorkoutPlanAgent gymWorkoutPlanSetAgent)
        {
            _gymWorkoutPlanAgent = gymWorkoutPlanAgent;
            _gymWorkoutPlanDetailsAgent = gymWorkoutPlanDetailsAgent;
            _gymWorkoutPlanSetAgent = gymWorkoutPlanSetAgent;
        }

        #region GymWorkoutPlan

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            GymWorkoutPlanListViewModel list = new GymWorkoutPlanListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _gymWorkoutPlanAgent.GetGymWorkoutPlanList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymWorkoutPlan/_List.cshtml", list);
            }
            return View($"~/Views/Gym/GymWorkoutPlan/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreateGymWorkoutPlan()
        {
            return View(createEditGymWorkoutPlan, new GymWorkoutPlanViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateGymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                gymWorkoutPlanViewModel = _gymWorkoutPlanAgent.CreateGymWorkoutPlan(gymWorkoutPlanViewModel);
                if (!gymWorkoutPlanViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    //return RedirectToAction("List", CreateActionDataTable());
                    return RedirectToAction("List", new { selectedCentreCode = gymWorkoutPlanViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymWorkoutPlanViewModel.ErrorMessage));
            return View(createEditGymWorkoutPlan, gymWorkoutPlanViewModel);
        }

        //UpdateGymWorkoutPlan
        [HttpGet]
        public virtual ActionResult UpdateGymWorkoutPlanDetails(long gymWorkoutPlanId)
        {
            GymWorkoutPlanViewModel gymWorkoutPlanViewModel = _gymWorkoutPlanAgent.GetGymWorkoutPlan(gymWorkoutPlanId);
            return ActionView(createEditGymWorkoutPlan, gymWorkoutPlanViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateGymWorkoutPlanDetails(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymWorkoutPlanAgent.UpdateGymWorkoutPlan(gymWorkoutPlanViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GymWorkoutPlan", new { gymWorkoutPlanId = gymWorkoutPlanViewModel.GymWorkoutPlanId });
            }
            return View(createEditGymWorkoutPlan, gymWorkoutPlanViewModel);
        }

        [HttpGet]
        public virtual ActionResult GymWorkoutPlan(long gymWorkoutPlanId)
        {
            GymWorkoutPlanViewModel gymWorkoutPlanViewModel = _gymWorkoutPlanAgent.GetGymWorkoutPlan(gymWorkoutPlanId);
            return View("~/Views/Gym/GymWorkoutPlan/GymWorkoutPlan.cshtml", gymWorkoutPlanViewModel);
        }

        [HttpPost]
        public virtual ActionResult GymWorkoutPlan(GymWorkoutPlanViewModel gymWorkoutPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_gymWorkoutPlanAgent.UpdateGymWorkoutPlan(gymWorkoutPlanViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GymWorkoutPlan", new { gymWorkoutPlanId = gymWorkoutPlanViewModel.GymWorkoutPlanId });
            }
            return View("~/Views/Gym/GymWorkoutPlan/GymWorkoutPlan.cshtml", gymWorkoutPlanViewModel);
        }

        #region GymWorkoutPlanDetails
        //GymWorkoutPlanDetails
        [HttpGet]
        public virtual ActionResult GetWorkoutPlanDetails(long gymWorkoutPlanId)
        {
            GymWorkoutPlanViewModel gymWorkoutPlanViewModel = _gymWorkoutPlanAgent.GetWorkoutPlanDetails(gymWorkoutPlanId);
            return View("~/Views/Gym/GymWorkoutPlan/WorkoutPlanDetails.cshtml", gymWorkoutPlanViewModel);
        }

        # region Add Workout Plan Details
        [HttpGet]
        public virtual ActionResult AddWorkoutPlanDetails(long gymWorkoutPlanId, short numberOfWeeks, byte numberOfDaysPerWeek)
        {

            GymWorkoutPlanDetailsViewModel gymWorkoutPlanDetailsViewModel = new GymWorkoutPlanDetailsViewModel()
            {
                GymWorkoutPlanId = gymWorkoutPlanId,
                WorkoutWeek = numberOfWeeks,
                WorkoutDay = numberOfDaysPerWeek

            };
            return PartialView("~/Views/Gym/GymWorkoutPlan/_WorkoutPlanDetailsPopUp.cshtml", gymWorkoutPlanDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult AddWorkoutPlanDetails(GymWorkoutPlanDetailsViewModel gymWorkoutPlanDetailsViewModel)
        {
            if (ModelState.IsValid)
            {

                gymWorkoutPlanDetailsViewModel = _gymWorkoutPlanSetAgent.AddWorkoutPlanDetails(gymWorkoutPlanDetailsViewModel);
                if (!gymWorkoutPlanDetailsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(gymWorkoutPlanDetailsViewModel.ErrorMessage));
            return RedirectToAction("GetWorkoutPlanDetails", new { gymWorkoutPlanId = gymWorkoutPlanDetailsViewModel.GymWorkoutPlanId });
        }
       
        #endregion

        public virtual ActionResult Delete(string gymWorkoutPlanIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;

            if (!string.IsNullOrEmpty(gymWorkoutPlanIds))
            {
                status = _gymWorkoutPlanAgent.DeleteGymWorkoutPlan(gymWorkoutPlanIds, out message);

                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
        }
        #endregion GymWorkoutPlan

        public virtual ActionResult Cancel(string selectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = selectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }
        #endregion
    }
}