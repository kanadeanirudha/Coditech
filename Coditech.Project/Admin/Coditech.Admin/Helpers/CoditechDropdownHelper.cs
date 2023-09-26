using Coditech.Admin.Agents;
using Coditech.Admin.ViewModel;
using Coditech.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Coditech.Common.Logger.CoditechLoggingEnum;

namespace Coditech.DropdownHelper
{
    public static class CoditechDropdownHelper
    {
        private static List<SelectListItem> dropdownList;

        public static DropdownViewModel GeneralDropdownList(DropdownViewModel dropdownViewModel)
        {
          
            if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                GeneralCityListViewModel list = new GeneralCityAgent().GetCityList(new DataTableModel() { PageSize = int.MaxValue });
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select City-------" });
                foreach (var item in list.GeneralCityList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = string.Concat(item.CityName, " (", item.RegionName, ")"),
                        Value = Convert.ToString(item.GeneralCityMasterId),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCityMasterId)
                    });
                }
            }
           
            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
        }
    }
}