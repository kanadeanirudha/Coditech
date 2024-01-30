using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralRunningNumbersController : BaseController
    {
        private readonly IGeneralRunningNumbersAgent _generalRunningNumbersAgent;
       // private const string createEdit = "~/Views/GeneralMaster/GeneralRunningNumbers/CreateEdit.cshtml";
        public GeneralRunningNumbersController(IGeneralRunningNumbersAgent generalRunningNumbersAgent)
        {
            _generalRunningNumbersAgent = generalRunningNumbersAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            GeneralRunningNumbersListViewModel list = new GeneralRunningNumbersListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _generalRunningNumbersAgent.GetRunningNumbersList(dataTableViewModel);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralRunningNumbers/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralRunningNumbers/List.cshtml", list);
        }

       
    }
}