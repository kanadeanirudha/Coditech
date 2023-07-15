using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralDepartmentAgent
    {
         GeneralDepartmentListViewModel GetDepartmentList(DataTableViewModel dataTableModel);
    }
}
