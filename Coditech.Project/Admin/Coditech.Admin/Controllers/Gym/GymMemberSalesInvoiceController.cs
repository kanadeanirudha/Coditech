using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;

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
        public ActionResult SaleInvoiceList(DataTableViewModel dataTableModel)
        {
            GymMemberSalesInvoiceListViewModel list = new GymMemberSalesInvoiceListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _gymSaleInvoiceAgent.GymMemberServiceSalesInvoiceList(dataTableModel);
            }
            else
            {
                list.FromDate = Convert.ToDateTime(DateTime.Now.AddMonths(-1));
                list.ToDate = Convert.ToDateTime(DateTime.Now);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Gym/GymSalesInvoice/_SaleInvoiceList.cshtml", list);
            }

            return View($"~/Views/Gym/GymSalesInvoice/SaleInvoiceList.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetSalesInvoiceDetails(long salesInvoiceMasterId)
        {
            SalesInvoicePrintModel salesInvoicePrintModel = _gymSaleInvoiceAgent.GetSalesInvoiceDetails(salesInvoiceMasterId);
            return View($"~/Views/Gym/GymSalesInvoice/SalesInvoicePrint.cshtml", salesInvoicePrintModel);
        }
        #endregion
    }
}
