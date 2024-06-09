using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace Coditech.Admin.Controllers
{
    public class GymSalesInvoiceReportController : BaseController
    {
        private IWebHostEnvironment Environment;
        public GymSalesInvoiceReportController(IWebHostEnvironment _environment)
        {
            this.Environment = _environment;
        }

        [HttpGet]
        public virtual IActionResult Index()
        {
            return View("~/Views/Reports/Index.cshtml");
        }

        [HttpGet]
        public IActionResult ViewReport(string reportName)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("InvoiceNumber", typeof(string));
            dataTable.Columns.Add("TransactionDate", typeof(string));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("MobileNumber", typeof(string));
            dataTable.Columns.Add("PlanType", typeof(string));
            dataTable.Columns.Add("MembershipPlanName", typeof(string));
            dataTable.Columns.Add("PaymentMode", typeof(string));
            dataTable.Rows.Add("Inv 1", "2024-06-24", "Anirudha", "Kanade", "985632", "PlanType", "MembershipPlanName","PaymentMode");
            dataTable.Rows.Add("Inv 1", "2024-06-24", "Anirudha", "Kanade", "985632", "PlanType", "MembershipPlanName","PaymentMode");

            Dictionary<string, string> reportParameters = new Dictionary<string, string>();

            Stream stream = GetReport(this.Environment, "Gym", reportName, dataTable, "DataSet1", reportParameters);
            //return File(stream, "application/xls");

            return File(stream, "application/xls", $"{reportName}.xls");
        }

    }
}