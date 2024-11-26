using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class DBTMNewRegistrationController : BaseController
    {
        private readonly IDBTMNewRegistrationAgent _dBTMNewRegistrationAgent;

        public DBTMNewRegistrationController(IDBTMNewRegistrationAgent dBTMNewRegistrationAgent)
        {
            _dBTMNewRegistrationAgent = dBTMNewRegistrationAgent;
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult NewRegistration()
        {
            TempData["FormSizeClass"] = "col-lg-8";
            return View("~/Views/DBTM/DBTMNewRegistration/DBTMNewRegistration.cshtml", new DBTMNewRegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual ActionResult NewRegistration(DBTMNewRegistrationViewModel dBTMNewRegistrationViewModel)
        {
            if (ModelState.IsValid)
            {
                dBTMNewRegistrationViewModel = _dBTMNewRegistrationAgent.DBTMNewRegistration(dBTMNewRegistrationViewModel);
                if (!dBTMNewRegistrationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage("Your Registration successfully."));
                    return RedirectToAction("Login");
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(dBTMNewRegistrationViewModel.ErrorMessage));
            return View("~/Views/DBTM/DBTMNewRegistration/DBTMNewRegistration.cshtml", dBTMNewRegistrationViewModel);
        }
    }
}