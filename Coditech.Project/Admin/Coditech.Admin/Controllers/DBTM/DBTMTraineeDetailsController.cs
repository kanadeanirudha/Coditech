using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class DBTMTraineeDetailsController : BaseController
    {
        private readonly IDBTMTraineeDetailsAgent _dBTMTraineeDetailsAgent;
        private const string createEditTraineeDetails = "~/Views/DBTM/DBTMTraineeDetails/DBTMTraineeDetails.cshtml";

        public DBTMTraineeDetailsController(IDBTMTraineeDetailsAgent dBTMTraineeDetailsAgent)
        {
            _dBTMTraineeDetailsAgent = dBTMTraineeDetailsAgent;
        }

        #region DBTMTraineeDetails

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            DBTMTraineeDetailsListViewModel list = new DBTMTraineeDetailsListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _dBTMTraineeDetailsAgent.GetDBTMTraineeDetailsList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/DBTM/DBTMTraineeDetails/_List.cshtml", list);
            }
            return View($"~/Views/DBTM/DBTMTraineeDetails/List.cshtml", list);
        }

        public ActionResult ActiveMemberList(DataTableViewModel dataTableViewModel)
        {
            DBTMTraineeDetailsListViewModel list = new DBTMTraineeDetailsListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _dBTMTraineeDetailsAgent.GetDBTMTraineeDetailsList(dataTableViewModel, "Active");
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.ListType = "Active";
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/DBTM/DBTMTraineeDetails/_List.cshtml", list);
            }
            return View($"~/Views/DBTM/DBTMTraineeDetails/List.cshtml", list);
        }

        public ActionResult InActiveMemberList(DataTableViewModel dataTableViewModel)
        {
            DBTMTraineeDetailsListViewModel list = new DBTMTraineeDetailsListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _dBTMTraineeDetailsAgent.GetDBTMTraineeDetailsList(dataTableViewModel, "InActive");
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.ListType = "InActive";
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/DBTM/DBTMTraineeDetails/_List.cshtml", list);
            }
            return View($"~/Views/DBTM/DBTMTraineeDetails/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult CreateDBTMTrainee()
        {
            DBTMTraineeDetailsCreateEditViewModel viewModel = new DBTMTraineeDetailsCreateEditViewModel();
            return View(createEditTraineeDetails, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateDBTMTrainee(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                dBTMTraineeDetailsCreateEditViewModel = _dBTMTraineeDetailsAgent.CreateDBTMTraineeDetails(dBTMTraineeDetailsCreateEditViewModel);
                if (!dBTMTraineeDetailsCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new { selectedCentreCode = dBTMTraineeDetailsCreateEditViewModel.SelectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(dBTMTraineeDetailsCreateEditViewModel.ErrorMessage));
            return View(createEditTraineeDetails, dBTMTraineeDetailsCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateDBTMTraineePersonalDetails(long dBTMTraineeDetailId, long personId)
        {
            DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel = _dBTMTraineeDetailsAgent.GetDBTMTraineePersonalDetails(dBTMTraineeDetailId, personId);
            return ActionView(createEditTraineeDetails, dBTMTraineeDetailsCreateEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateDBTMTraineePersonalDetails(DBTMTraineeDetailsCreateEditViewModel dBTMTraineeDetailsCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_dBTMTraineeDetailsAgent.UpdateDBTMTraineePersonalDetails(dBTMTraineeDetailsCreateEditViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateDBTMTraineePersonalDetails", new { dBTMTraineeDetailId = dBTMTraineeDetailsCreateEditViewModel.DBTMTraineeDetailId, personId = dBTMTraineeDetailsCreateEditViewModel.PersonId });
            }
            return View(createEditTraineeDetails, dBTMTraineeDetailsCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult MemberOtherDetails(long dBTMTraineeDetailId)
        {
            DBTMTraineeDetailsViewModel dBTMTraineeDetailsViewModel = _dBTMTraineeDetailsAgent.GetDBTMTraineeOtherDetails(dBTMTraineeDetailId);
            return View("~/Views/DBTM/DBTMTraineeDetails/UpdateDBTMTraineeOtherDetails.cshtml", dBTMTraineeDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult MemberOtherDetails(DBTMTraineeDetailsViewModel dBTMTraineeDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_dBTMTraineeDetailsAgent.UpdateDBTMTraineeOtherDetails(dBTMTraineeDetailsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("MemberOtherDetails", new { dBTMTraineeDetailId = dBTMTraineeDetailsViewModel.DBTMTraineeDetailId });
            }
            return View("~/Views/DBTM/DBTMTraineeDetails/UpdateDBTMTraineeOtherDetails.cshtml", dBTMTraineeDetailsViewModel);
        }

        public virtual ActionResult Delete(string dBTMTraineeDetailIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;

            if (!string.IsNullOrEmpty(dBTMTraineeDetailIds))
            {
                status = _dBTMTraineeDetailsAgent.DeleteDBTMTraineeDetails(dBTMTraineeDetailIds, out message);

                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
        }
        #endregion DBTMTraineeDetails

        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }
    }
}