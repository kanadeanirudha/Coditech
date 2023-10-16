using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Coditech.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class OrganisationCentreMasterController : BaseController
    {
       // RARIndiaEntities db = new RARIndiaEntities();
        private readonly IOrganisationCentreAgent _organisationCentreAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentre/CreateEdit.cshtml";
        private const string OrganisationCentrePrintingFormat = "~/Views/Organisation/OrganisationCentre/OrganisationCentrePrintingFormat.cshtml";
        private readonly string organisationCentrePrintingFormatViewModel;

        public OrganisationCentreMasterController(IOrganisationCentreAgent organisationCentreAgent)
        {
            _organisationCentreAgent = organisationCentreAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            OrganisationCentreListViewModel list = _organisationCentreAgent.GetOrganisationCentreList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentre/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentre/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new OrganisationCentreViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentreViewModel = _organisationCentreAgent.CreateOrganisationCentre(organisationCentreViewModel);
                if (!organisationCentreViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentreViewModel.ErrorMessage));
            return View(createEdit, organisationCentreViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short organisationCentreId)
        {
            OrganisationCentreViewModel organisationCentreViewModel = _organisationCentreAgent.GetOrganisationCentre(organisationCentreId);
            return ActionView(createEdit, organisationCentreViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(OrganisationCentreViewModel organisationCentreViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_organisationCentreAgent.UpdateOrganisationCentre(organisationCentreViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { organisationCentreId = organisationCentreViewModel.OrganisationCentreMasterId });
            }
            return View(createEdit, organisationCentreViewModel);
        }

        public virtual ActionResult Delete(string organisationCentreIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(organisationCentreIds))
            {
                status = _organisationCentreAgent.DeleteOrganisationCentre(organisationCentreIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
        }

        //Get: Organisation Centre Printing Format.
        [HttpGet]
        public ActionResult PrintingFormat()
        {
            return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        }

        //Post: Organisation Centre Printing Format .
        [HttpPost]
        public virtual ActionResult PrintingFormat(OrganisationCentrePrintingFormatViewModel organisationCentrePrintingFormatViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrePrintingFormatViewModel = _organisationCentreAgent.GetPrintingFormat(organisationCentrePrintingFormatViewModel);
                if (!organisationCentrePrintingFormatViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // TempData[RARIndiaConstant.DataTableModel] = CreateActionDataTable();
                    return RedirectToAction<OrganisationCentreMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrePrintingFormatViewModel.ErrorMessage));
            return View(OrganisationCentrePrintingFormat, organisationCentrePrintingFormatViewModel);
        }

        //#region
        ////Logo :Printing Format
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        //}

        //[HttpPost]
        //public ActionResult Index(tbl_data d, HttpPostedFileBase imgfile)
        //{
        //    tbl_data di = new tbl_data();
        //    string path = uploadimage(imgfile);
        //    if (path.Equals("-1"))
        //    {

        //    }
        //    else
        //    {
        //        di.Logo = path;
        //        db.SaveChanges();
        //    }
        //    return View(OrganisationCentrePrintingFormat, new OrganisationCentrePrintingFormatViewModel());
        //}
        //public string uploadimage(HttpPostedFileBase imgfile)
        //{
        //    Random r = new Random();
        //    string path = "-1";
        //    int random = r.Next();
        //    if (imgfile != null && imgfile.ContentLength > 0)
        //    {
        //        string extension = Path.GetExtension(imgfile.FileName);
        //        if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
        //        {
        //            try
        //            {
        //                path = Path.Combine(Server.MapPath("~/Images/organisationCentrePrintingFormat"), random + Path.GetFileName(imgfile.FileName));
        //                imgfile.SaveAs(path);
        //                path = "~/Images/organisationCentrePrintingFormat/" + random + Path.GetFileName(imgfile.FileName);
        //                ViewBag.Message = "File uploaded successfully";
        //            }
        //            catch (Exception ex)
        //            {
        //                path = "-1";
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('Please select a file'); </script>");
        //        path = "-1";
        //    }
        //    return path;
        //}

        // #endregion
    }
    //public class tbl_data
    //{
    //    internal string Logo;
    //}
}