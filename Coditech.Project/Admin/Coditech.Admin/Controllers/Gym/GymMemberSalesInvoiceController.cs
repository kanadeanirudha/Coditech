using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Enum;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GymMemberSalesInvoiceController : BaseController
    {
        private readonly IGymSalesInvoiceAgent _gymSaleInvoiceAgent;

        public GymMemberSalesInvoiceController(IGymSalesInvoiceAgent gymSaleInvoiceAgent)
        {
            _gymSaleInvoiceAgent = gymSaleInvoiceAgent;
        }

        #region GymMemberSaleInvoice

        [HttpGet]
        public ActionResult SaleInvoiceList(DataTableViewModel dataTableModel, DateTime? toDate = null, DateTime? fromDate = null)
        {

            GymMemberSalesInvoiceListViewModel list = new GymMemberSalesInvoiceListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _gymSaleInvoiceAgent.GymMemberServiceSalesInvoiceList(toDate, fromDate, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymSalesInvoice/_List.cshtml", list);
            }

            return View($"~/Views/Gym/GymSalesInvoice/List.cshtml", list);
        }
        #endregion
    }
}
