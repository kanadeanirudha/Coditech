using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class TicketMasterReplyController : BaseController
    {
        private readonly ITicketMasterAgent _ticketMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/TicketMaster/CreateEditTicketMaster.cshtml";

        public TicketMasterReplyController(ITicketMasterAgent ticketMasterAgent)
        {
            _ticketMasterAgent = ticketMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            TicketMasterListViewModel list = new TicketMasterListViewModel();
            list = _ticketMasterAgent.GetTicketMasterList(dataTableModel, 0);
            list.IsTicketReplied = true;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/TicketMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/TicketMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Edit(long ticketMasterId)
        {
            long userMasterId = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.UserMasterId ?? 0;
            TicketMasterViewModel ticketMasterViewModel = _ticketMasterAgent.GetTicket(ticketMasterId, userMasterId);
            ticketMasterViewModel.IsTicketReplied = true;
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

        public virtual ActionResult Cancel()
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel();
            return RedirectToAction("List", dataTableViewModel);
        }
        #region Protected

        #endregion
    }
}