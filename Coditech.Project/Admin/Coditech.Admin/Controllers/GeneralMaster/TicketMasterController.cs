using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class TicketMasterController : BaseController
    {
        private readonly ITicketMasterAgent _ticketMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/TicketMaster/CreateEditTicketMaster.cshtml";

        public TicketMasterController(ITicketMasterAgent ticketMasterAgent)
        {
            _ticketMasterAgent = ticketMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            TicketMasterListViewModel list = new TicketMasterListViewModel();           
            list = _ticketMasterAgent.GetTicketMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/TicketMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/TicketMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new TicketMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(TicketMasterViewModel ticketMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                ticketMasterViewModel = _ticketMasterAgent.CreateTicket(ticketMasterViewModel);
                if (!ticketMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(ticketMasterViewModel.ErrorMessage));
            return View(createEdit, ticketMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long ticketMasterId)
        {
            long userMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.UserMasterId ?? 0;
            TicketMasterViewModel ticketMasterViewModel = _ticketMasterAgent.GetTicket(ticketMasterId, userMasterId);
            return ActionView(createEdit, ticketMasterViewModel);
        }
        
        [HttpPost]
        public virtual ActionResult Edit(TicketMasterViewModel ticketMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_ticketMasterAgent.UpdateTicket(ticketMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { ticketMasterId = ticketMasterViewModel.TicketMasterId });
            }
            return View(createEdit, ticketMasterViewModel);
        }

        public virtual ActionResult Delete(string ticketMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(ticketMasterIds))
            {
                status = _ticketMasterAgent.DeleteTicket(ticketMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<TicketMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<TicketMasterController>(x => x.List(null));
        }

        public virtual ActionResult Cancel()
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel();
            return RedirectToAction("List", dataTableViewModel);
        }
        #region Protected

        #endregion
    }
}