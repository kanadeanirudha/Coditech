﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class PaymentGatewayDetailsController : BaseController
    {
        private readonly IPaymentGatewayDetailsAgent _paymentGatewayDetailsAgent;
        private const string createEdit = "~/Views/Payment/PaymentGatewayDetails/CreateEdit.cshtml";
        public PaymentGatewayDetailsController(IPaymentGatewayDetailsAgent paymentGatewayDetailsAgent)
        {
            _paymentGatewayDetailsAgent = paymentGatewayDetailsAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            PaymentGatewayDetailsListViewModel list = new PaymentGatewayDetailsListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && !string.IsNullOrEmpty(dataTableModel.SelectedParameter1))
            {
                list = _paymentGatewayDetailsAgent.GetPaymentGatewayDetailsList(dataTableModel, Convert.ToByte(dataTableModel.SelectedParameter1));
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedParameter1 = dataTableModel.SelectedParameter1;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Payment/PaymentGatewayDetails/_List.cshtml", list);
            }
            return View($"~/Views/Payment/PaymentGatewayDetails/List.cshtml", list);
        }
        [HttpGet]
        public virtual ActionResult Create(string centreCode, byte paymentGatewayId, bool isLiveMode, string paymentCode)
        {
            PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel = new PaymentGatewayDetailsViewModel()
            {
                CentreCode = centreCode,
                PaymentGatewayId = paymentGatewayId,
                IsLiveMode = isLiveMode,
                PaymentCode = paymentCode
            };
            if (paymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
            }
            else
            {
                return View(createEdit, paymentGatewayDetailsViewModel);
            }
        }

        [HttpPost]
        public virtual ActionResult Create(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel)
        {
            if (paymentGatewayDetailsViewModel.PaymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                if (string.IsNullOrEmpty(paymentGatewayDetailsViewModel.GatewayUsername))
                {
                    SetNotificationMessage(GetErrorNotificationMessage("Please Entre Key Id."));
                    return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
                }
                if (string.IsNullOrEmpty(paymentGatewayDetailsViewModel.GatewayUsername))
                {
                    SetNotificationMessage(GetErrorNotificationMessage("Please Entre Secret Key."));
                    return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
                }
            }
            if (ModelState.IsValid)
            {
                paymentGatewayDetailsViewModel = _paymentGatewayDetailsAgent.CreatePaymentGatewayDetails(paymentGatewayDetailsViewModel);
                if (!paymentGatewayDetailsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = paymentGatewayDetailsViewModel.CentreCode, SelectedParameter1 = Convert.ToString(paymentGatewayDetailsViewModel.PaymentGatewayId) });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(paymentGatewayDetailsViewModel.ErrorMessage));

            if (paymentGatewayDetailsViewModel.PaymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
            }
            else
            {
                return View(createEdit, paymentGatewayDetailsViewModel);
            }
        }
        [HttpGet]
        public virtual ActionResult Edit(int paymentGatewayDetailId)
        {
            PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel = _paymentGatewayDetailsAgent.GetPaymentGatewayDetails(paymentGatewayDetailId);
            if (paymentGatewayDetailsViewModel.PaymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
            }
            else
            {
                return View(createEdit, paymentGatewayDetailsViewModel);
            }
        }


        [HttpPost]
        public virtual ActionResult Edit(PaymentGatewayDetailsViewModel paymentGatewayDetailsViewModel)
        {
            if (paymentGatewayDetailsViewModel.PaymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                if (string.IsNullOrEmpty(paymentGatewayDetailsViewModel.GatewayUsername))
                {
                    SetNotificationMessage(GetErrorNotificationMessage("Please Entre Key Id."));
                    return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
                }
                if (string.IsNullOrEmpty(paymentGatewayDetailsViewModel.GatewayUsername))
                {
                    SetNotificationMessage(GetErrorNotificationMessage("Please Entre Secret Key."));
                    return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
                }
            }
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_paymentGatewayDetailsAgent.UpdatePaymentGatewayDetails(paymentGatewayDetailsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { paymentGatewayDetailId = paymentGatewayDetailsViewModel.PaymentGatewayDetailId });
            }
            if (paymentGatewayDetailsViewModel.PaymentCode == PaymentCodeEnum.razorpay.ToString())
            {
                return View("~/Views/Payment/PaymentGatewayDetails/CreateEditRazorpay.cshtml", paymentGatewayDetailsViewModel);
            }
            else
            {
                return View(createEdit, paymentGatewayDetailsViewModel);
            }
        }
        public virtual ActionResult Delete(string paymentGatewayDetailIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(paymentGatewayDetailIds))
            {
                status = _paymentGatewayDetailsAgent.DeletePaymentGatewayDetails(paymentGatewayDetailIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", CreateActionDataTable());
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", CreateActionDataTable());
        }
        public virtual ActionResult Cancel(string SelectedCentreCode, string SelectedParameter1)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedParameter1 = SelectedParameter1 };
            return RedirectToAction("List", dataTableViewModel);
        }

    }
}
