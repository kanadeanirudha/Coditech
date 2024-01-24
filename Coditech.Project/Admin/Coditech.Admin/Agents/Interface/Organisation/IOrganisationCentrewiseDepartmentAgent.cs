﻿using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IOrganisationCentrewiseDepartmentAgent
    {
        /// <summary>
        /// Get list of Organisation Centrewise Department.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>OrganisationCentrewiseDepartmentListViewModel</returns>
        OrganisationCentrewiseDepartmentListViewModel GetOrganisationCentrewiseDepartmentList(DataTableViewModel dataTableModel);
    }
}
