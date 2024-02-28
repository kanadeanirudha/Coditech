using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseBuildingRoomsController : BaseController
    {
        private readonly IOrganisationCentrewiseBuildingRoomsAgent _organisationCentrewiseBuildingRoomsAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewiseBuildingRooms/CreateEdit.cshtml";
        public OrganisationCentrewiseBuildingRoomsController(IOrganisationCentrewiseBuildingRoomsAgent organisationCentrewiseBuildingRoomsAgent)
        {
            _organisationCentrewiseBuildingRoomsAgent = organisationCentrewiseBuildingRoomsAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            OrganisationCentrewiseBuildingRoomsListViewModel list = new OrganisationCentrewiseBuildingRoomsListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && !string.IsNullOrEmpty(dataTableViewModel.SelectedParameter1))
            {
                list = _organisationCentrewiseBuildingRoomsAgent.GetOrganisationCentrewiseBuildingRoomsList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseBuildingRooms/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseBuildingRooms/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentrewiseBuildingRoomsViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrewiseBuildingRoomsViewModel = _organisationCentrewiseBuildingRoomsAgent.CreateOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomsViewModel);
                if (!organisationCentrewiseBuildingRoomsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = organisationCentrewiseBuildingRoomsViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseBuildingRoomsViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseBuildingRoomsViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short organisationCentrewiseBuildingRoomId)
        {
            OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel = _organisationCentrewiseBuildingRoomsAgent.GetOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomId);
            return ActionView(createEdit, organisationCentrewiseBuildingRoomsViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentrewiseBuildingRoomsViewModel organisationCentrewiseBuildingRoomsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentrewiseBuildingRoomsAgent.UpdateOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                return RedirectToAction("Edit", new { organisationCentrewiseBuildingRoomId = organisationCentrewiseBuildingRoomsViewModel.OrganisationCentrewiseBuildingRoomId });
            }
            return View(createEdit, organisationCentrewiseBuildingRoomsViewModel);
        }

        public virtual ActionResult Delete(string organisationCentrewiseBuildingRoomIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentrewiseBuildingRoomIds))
            {
                status = _organisationCentrewiseBuildingRoomsAgent.DeleteOrganisationCentrewiseBuildingRooms(organisationCentrewiseBuildingRoomIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentrewiseBuildingRoomsController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentrewiseBuildingRoomsController>(x => x.List(null));
        }

        public virtual ActionResult GetOrganisationCentrewiseBuildingByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuilding.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingMasterId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }
        #region Protected

        #endregion
    }
}