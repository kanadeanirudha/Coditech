using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralTrainerMasterController : BaseController
    {
        private readonly IGeneralTrainerAgent _generalTrainerAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTrainerMaster/CreateEdit.cshtml";

        public GeneralTrainerMasterController(IGeneralTrainerAgent generalTrainerAgent)
        {
            _generalTrainerAgent = generalTrainerAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralTrainerListViewModel list = new GeneralTrainerListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _generalTrainerAgent.GetTrainerList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, true, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralTrainerMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralTrainerMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            GeneralTrainerViewModel generalTrainerViewModel = new GeneralTrainerViewModel();
            return View(createEdit, generalTrainerViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralTrainerViewModel generalTrainerViewModel)
        {
            if (ModelState.IsValid)
            {
                generalTrainerViewModel = _generalTrainerAgent.CreateTrainer(generalTrainerViewModel);
                if (!generalTrainerViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = generalTrainerViewModel.SelectedCentreCode, SelectedDepartmentId = Convert.ToInt16(generalTrainerViewModel.SelectedDepartmentId) });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalTrainerViewModel.ErrorMessage));
            return View(createEdit, generalTrainerViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long generalTrainerId)
        {
            GeneralTrainerViewModel generalTrainerViewModel = _generalTrainerAgent.GetTrainer(generalTrainerId);
            return ActionView(createEdit, generalTrainerViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralTrainerViewModel generalTrainerViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalTrainerAgent.UpdateTrainer(generalTrainerViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalTrainerId = generalTrainerViewModel.GeneralTrainerMasterId });
            }
            return View(createEdit, generalTrainerViewModel);
        }

        public virtual ActionResult Delete(string generalTrainerIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalTrainerIds))
            {
                status = _generalTrainerAgent.DeleteTrainer(generalTrainerIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", CreateActionDataTable());
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", CreateActionDataTable());
        }

        public virtual ActionResult GetEmployeeList(string selectedCentreCode, string selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.UnAssociatedTrainerEmployeeList.ToString(),
                DropdownName = "EmployeeId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }
    }
}
