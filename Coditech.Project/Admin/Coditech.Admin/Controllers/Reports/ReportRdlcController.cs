using AspNetCore.Reporting;

using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace Coditech.Admin.Controllers
{
    public class ReportRdlcController : BaseController
    {
        private IWebHostEnvironment Environment;
        public ReportRdlcController(IWebHostEnvironment _environment)
        {
            this.Environment = _environment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        public virtual IActionResult Index()
        {
            return View("~/Views/Reports/Index.cshtml");
        }

        [HttpGet]
        public IActionResult ViewReport(string reportName)
        {
            string mimeType = "";
            int pageIndex = 1;
            var _reportPath = $"{this.Environment.WebRootPath}\\Reports\\{reportName}.rdlc";
            LocalReport localReport = new LocalReport(_reportPath);

            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("InvoiceNumber", typeof(string));
            dataTable.Columns.Add("TransactionDate", typeof(string));
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("MobileNumber", typeof(string));
            dataTable.Columns.Add("PlanType", typeof(string));
            dataTable.Columns.Add("MembershipPlanName", typeof(string));
            dataTable.Rows.Add("Inv 1","2024-06-24","Anirudha","Kanade","985632", "PlanType", "MembershipPlanName");
            dataTable.Rows.Add("Inv 1", "2024-06-24", "Anirudha", "Kanade", "985632", "PlanType", "MembershipPlanName");
            localReport.AddDataSource("DataSet1", dataTable);

            Dictionary<string, string> reportParameters = new Dictionary<string, string>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = localReport.Execute(RenderType.Pdf, pageIndex, reportParameters, mimeType);
            byte[] file = result.MainStream;

            Stream stream = new MemoryStream(file);
            return File(stream, "application/pdf");
            //return File(stream, "application/pdf", $"{reportName}.pdf");
        }
    }
}