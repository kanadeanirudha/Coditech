using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class PaymentGatewaysController : BaseController
    {
        private readonly IPaymentGatewaysAgent _paymentGatewaysAgent;
        private const string createEdit = "~/Views/Payment/PaymentGateways/CreateEdit.cshtml";
        public PaymentGatewaysController(IPaymentGatewaysAgent paymentGatewaysAgent)
        {
            _paymentGatewaysAgent = paymentGatewaysAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            PaymentGatewaysListViewModel list = _paymentGatewaysAgent.GetPaymentGatewaysList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Payment/PaymentGateways/_List.cshtml", list);
            }
            return View($"~/Views/Payment/PaymentGateways/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new PaymentGatewaysViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(PaymentGatewaysViewModel paymentGatewaysViewModel)
        {
            if (ModelState.IsValid)
            {
                paymentGatewaysViewModel = _paymentGatewaysAgent.CreatePaymentGateways(paymentGatewaysViewModel);
                if (!paymentGatewaysViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(paymentGatewaysViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { paymentGatewayId = paymentGatewaysViewModel.PaymentGatewayId });
                    }
                    else if (string.Equals(paymentGatewaysViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList, CreateActionDataTable());
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(paymentGatewaysViewModel.ErrorMessage));
            return View(createEdit, paymentGatewaysViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(byte paymentGatewayId)
        {
            PaymentGatewaysViewModel paymentGatewaysViewModel = _paymentGatewaysAgent.GetPaymentGateways(paymentGatewayId);
            return ActionView(createEdit, paymentGatewaysViewModel);
        }
        [HttpPost]
        public virtual ActionResult Edit(PaymentGatewaysViewModel paymentGatewaysViewModel)
        {
            if (ModelState.IsValid)
            {
                paymentGatewaysViewModel = _paymentGatewaysAgent.UpdatePaymentGateways(paymentGatewaysViewModel);
                SetNotificationMessage(paymentGatewaysViewModel.HasError
                ? GetErrorNotificationMessage(paymentGatewaysViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(paymentGatewaysViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { paymentGatewayId = paymentGatewaysViewModel.PaymentGatewayId });
                }
                else if (string.Equals(paymentGatewaysViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList, CreateActionDataTable());
                }
            }
            return View(createEdit, paymentGatewaysViewModel);
        }
        public virtual ActionResult Delete(string paymentGatewayIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(paymentGatewayIds))
            {
                status = _paymentGatewaysAgent.DeletePaymentGateways(paymentGatewayIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<PaymentGatewaysController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<PaymentGatewaysController>(x => x.List(null));
        }
        #region Protected

        #endregion
    }
}
