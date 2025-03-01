using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Coditech.API.Data.EntityLogging;
namespace Coditech.Admin.Controllers
{
    public class OrganisationCentrewiseJoiningCodeController : BaseController
    {
        private readonly IOrganisationCentrewiseJoiningCodeAgent _oganisationCentrewiseJoiningCodeAgent;
        private const string createEdit = "~/Views/Organisation/OrganisationCentrewiseJoiningCode/CreateEdit.cshtml";
        public OrganisationCentrewiseJoiningCodeController(IOrganisationCentrewiseJoiningCodeAgent oganisationCentrewiseJoiningCodeAgent)
        {
            _oganisationCentrewiseJoiningCodeAgent = oganisationCentrewiseJoiningCodeAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            OrganisationCentrewiseJoiningCodeListViewModel list = new OrganisationCentrewiseJoiningCodeListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _oganisationCentrewiseJoiningCodeAgent.GetOrganisationCentrewiseJoiningCodeList(dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Organisation/OrganisationCentrewiseJoiningCode/_List.cshtml", list);
            }
            return View($"~/Views/Organisation/OrganisationCentrewiseJoiningCode/List.cshtml", list);
        }
       
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new OrganisationCentrewiseJoiningCodeViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel)
        {
            if (ModelState.IsValid)
            {
                organisationCentrewiseJoiningCodeViewModel = _oganisationCentrewiseJoiningCodeAgent.CreateOrganisationCentrewiseJoiningCode(organisationCentrewiseJoiningCodeViewModel);
                if (!organisationCentrewiseJoiningCodeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new { SelectedCentreCode = organisationCentrewiseJoiningCodeViewModel.CentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(organisationCentrewiseJoiningCodeViewModel.ErrorMessage));
            return View(createEdit, organisationCentrewiseJoiningCodeViewModel);
        }

        [HttpGet]
        public virtual ActionResult GetOrganisationCentrewiseJoiningCodeSend(string joiningCode)
        {
            OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel = new OrganisationCentrewiseJoiningCodeViewModel()
            {
                JoiningCode = joiningCode
            };
            return PartialView("~/Views/Organisation/OrganisationCentrewiseJoiningCode/_SendDetailsPopUp.cshtml", organisationCentrewiseJoiningCodeViewModel);
        }



        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetOrganisationCentrewiseJoiningCodeSend(string joiningCode, string sendOTPOn, string mobileNumber = null, string callingCode = null, string emailId = null)
        {
           
            OrganisationCentrewiseJoiningCodeViewModel organisationCentrewiseJoiningCodeViewModel = new()
            {

                MobileNumber = sendOTPOn.ToLower() == "mobile" ? mobileNumber : null,
                EmailId = sendOTPOn.ToLower() == "email" ? emailId : null,
                JoiningCode = joiningCode
            };

            organisationCentrewiseJoiningCodeViewModel.MobileNumber = sendOTPOn.ToLower() == "whatsapp" ? $"{callingCode}{mobileNumber}" : mobileNumber;

            organisationCentrewiseJoiningCodeViewModel = _oganisationCentrewiseJoiningCodeAgent.OrganisationCentrewiseJoiningCodeSend(organisationCentrewiseJoiningCodeViewModel.JoiningCode, organisationCentrewiseJoiningCodeViewModel.EmailId, organisationCentrewiseJoiningCodeViewModel.MobileNumber);
            if (!string.IsNullOrEmpty(organisationCentrewiseJoiningCodeViewModel?.CentreCode))
            {
                SetNotificationMessage(GetSuccessNotificationMessage("Your Joining Code has been sent successfully."));
                return Json(new { success = true, message = "Your Joining Code has been sent successfully.", centreCode = organisationCentrewiseJoiningCodeViewModel.CentreCode });
               
            }
            SetNotificationMessage(GetErrorNotificationMessage("Failed to Send Joining Code."));
            return Json(new { success = false, message = "Failed to Send Joining Code.", centreCode = organisationCentrewiseJoiningCodeViewModel.CentreCode });

        }

    }
}



