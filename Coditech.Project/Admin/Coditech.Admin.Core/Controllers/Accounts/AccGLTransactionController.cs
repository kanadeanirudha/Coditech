using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLTransactionController : BaseController
    {
        private readonly IAccGLTransactionAgent _accGLTransactionAgent;
        private readonly IGeneralCommonAgent _generalCommonAgent;
        private const string createEdit = "~/Views/Accounts/AccGLTransaction/CreateEdit.cshtml";

        public AccGLTransactionController(IAccGLTransactionAgent accGLTransactionAgent, IGeneralCommonAgent generalCommonAgent)
        {
            _accGLTransactionAgent = accGLTransactionAgent;
            _generalCommonAgent = generalCommonAgent;

        }


        [HttpGet]
        public virtual ActionResult GetFinancialYearListByCentreCode(string selectedCentreCode)
        {
            DropdownViewModel financialYearDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.FinancialYear.ToString(),
                DropdownName = "GeneralFinancialYearId",
                Parameter = $"{selectedCentreCode}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", financialYearDropdown);
        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            if (!_generalCommonAgent.GetAccountPrequisite())
                return IscheckAccPrequisiteStatified();
            return View(createEdit, new AccGLTransactionViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(AccGLTransactionViewModel accGLTransactionViewModel)
        {
            if (ModelState.IsValid)
            {
                accGLTransactionViewModel = _accGLTransactionAgent.CreateGLTransaction(accGLTransactionViewModel);
                if (!accGLTransactionViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<AccGLTransactionController>(x => x.Create(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accGLTransactionViewModel.ErrorMessage));
            return View(createEdit, accGLTransactionViewModel);
        }


        public virtual ActionResult Cancel()
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { };
            return RedirectToAction("Create", dataTableViewModel);
        }
        #region AutoSearch
        public virtual JsonResult GetAccounts(string term, int accountId, string personType, string transactionTypeCode, int balanceSheet)
        {

            var data = GetAccSetupGLAccountList(term, accountId, personType, transactionTypeCode, balanceSheet);
            return Json(data);
        }
        public virtual JsonResult GetPersonsByUserType(string term, int userTypeId, int balanceSheet)
        {
            var data = GetPersons(term, userTypeId, balanceSheet);
            return Json(data);
        }

        [HttpPost]
        public virtual IActionResult GetPersons(string SearchKeyWord, int userTypeId, int balanceSheet)
        {
            GeneralFinancialYearModel generalFinancialYearModel = _accGLTransactionAgent.GetCurrentFinancialYear();
            ACCGLOpeningBalanceViewModel model = new ACCGLOpeningBalanceViewModel();
            model.GeneralFinancialYearModel = generalFinancialYearModel;

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            var transactions = _accGLTransactionAgent.GetPersons(SearchKeyWord, userTypeId, balanceSheet);

            // Extract only AccSetupGLList and store it in SuggestionsAccSetupGLList
            var suggestionsAccSetupGLList = transactions
                .SelectMany(t => t.Personlist ?? new List<AccGLIndividualOpeningBalanceModel>()) // Flatten the list
                .Select(pl => new AccGLIndividualOpeningBalanceModel
                {
                    AccSetupGLId = pl.AccSetupGLId,
                    PersonName = pl.PersonName.Trim(), // Clean up extra spaces/tabs
                    PersonId = pl.PersonId,
                    UserTypeId = pl.UserTypeId,
                })
                .ToList();

            return Json(suggestionsAccSetupGLList); // Instead of wrapping it inside another object

        }

        [HttpPost]
        public virtual IActionResult GetAccSetupGLAccountList(string SearchKeyWord, int accountId, string personType, string transactionTypeCode, int balanceSheet)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            var transactions = _accGLTransactionAgent.GetAccSetupGLAccountList(SearchKeyWord, accountId, personType, transactionTypeCode, balanceSheet);

            // Extract only AccSetupGLList and store it in SuggestionsAccSetupGLList
            var suggestionsAccSetupGLList = transactions
                .SelectMany(t => t.AccSetupGLList ?? new List<AccSetupGLModel>())
                .Where(gl => !gl.IsGroup || (gl.IsGroup && gl.UserTypeId != null)) // Flatten the list
                .Select(gl => new AccSetupGLModel
                {
                    AccSetupGLId = gl.AccSetupGLId,
                    GLName = gl.GLName?.Trim(), // Clean up extra spaces/tabs
                    GLCode = gl.GLCode?.Trim(),
                    AccSetupGLTypeId = gl.AccSetupGLTypeId,
                    ParentAccSetupGLId = gl.ParentAccSetupGLId,
                    CategoryCode = gl.CategoryCode,
                    UserTypeId = gl.UserTypeId,
                })
                .ToList();

            return Json(suggestionsAccSetupGLList); // Instead of wrapping it inside another object

        }

        #endregion


        #region Protected

        #endregion
    }
}