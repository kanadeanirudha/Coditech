using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCommanDataController : BaseController
    {
        //Get Departments By CentreCode
        public ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public ActionResult GetRegionListByCountryId(string generalCountryMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.Region.ToString(),
                DropdownName = "GeneralRegionMasterId",
                Parameter = generalCountryMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }
    }
}
