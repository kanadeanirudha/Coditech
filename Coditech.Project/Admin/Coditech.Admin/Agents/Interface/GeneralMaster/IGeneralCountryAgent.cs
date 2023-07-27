using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCountryAgent
    {
        GeneralCountryListViewModel GetCountryList(DataTableViewModel dataTableModel);
    }
}
