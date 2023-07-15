using Coditech.Admin.Agents;
using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Web;

namespace Coditech.Admin.Controllers
{
    public class GeneralDepartmentMasterController : BaseController
    {
        private readonly IGeneralDepartmentAgent _generalDepartmentAgent;
        protected readonly IAuthenticationHelper _authenticationHelper;
        public GeneralDepartmentMasterController(IGeneralDepartmentAgent generalDepartmentAgent, IAuthenticationHelper authenticationHelper)
        {
            _generalDepartmentAgent = generalDepartmentAgent;
            _authenticationHelper = authenticationHelper;
        }

        public ActionResult List(DataTableViewModel dataTableViewModel)
        {
            dataTableViewModel = dataTableViewModel ?? new DataTableViewModel();
            GeneralDepartmentListViewModel list = _generalDepartmentAgent.GetDepartmentList(dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralDepartmentMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralDepartmentMaster/List.cshtml", list);
        }

        #region Private

        #endregion
    }
}