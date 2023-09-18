using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IAdminSanctionPostAgent
    {
        /// <summary>
        /// Get list of AdminSanctionPost.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AdminSanctionPostListViewModel</returns>
        AdminSanctionPostListViewModel GetAdminSanctionPostList(DataTableViewModel dataTableModel);
        
        /// <summary>
        /// Create AdminSanctionPost.
        /// </summary>
        /// <param name="generalAdminSanctionPostViewModel">AdminSanctionPost View Model.</param>
        /// <returns>Returns created model.</returns>
        AdminSanctionPostViewModel CreateAdminSanctionPost(AdminSanctionPostViewModel generalAdminSanctionPostViewModel);

        /// <summary>
        /// Get AdminSanctionPost by adminSanctionPostId.
        /// </summary>
        /// <param name="adminSanctionPostId">adminSanctionPostId</param>
        /// <returns>Returns AdminSanctionPostViewModel.</returns>
        AdminSanctionPostViewModel GetAdminSanctionPost(int adminSanctionPostId);

        /// <summary>
        /// Update AdminSanctionPost.
        /// </summary>
        /// <param name="generalAdminSanctionPostViewModel">generalAdminSanctionPostViewModel.</param>
        /// <returns>Returns updated AdminSanctionPostViewModel</returns>
        AdminSanctionPostViewModel UpdateAdminSanctionPost(AdminSanctionPostViewModel generalAdminSanctionPostViewModel);

        /// <summary>
        /// Delete AdminSanctionPost.
        /// </summary>
        /// <param name="adminSanctionPostId">adminSanctionPostId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteAdminSanctionPost(string adminSanctionPostIds, out string errorMessage);
    }
}
