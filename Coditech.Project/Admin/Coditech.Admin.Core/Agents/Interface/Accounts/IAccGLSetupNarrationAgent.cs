using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IAccGLSetupNarrationAgent
    {
        /// <summary>
        /// Get list of Narration.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>AccGLSetupNarrationListViewModel</returns>
        AccGLSetupNarrationListViewModel GetNarrationList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Narration
        /// </summary>
        /// <param name="accGLSetupNarrationViewModel">narration View Model.</param>
        /// <returns>Returns created model.</returns>
        AccGLSetupNarrationViewModel CreateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel);

        /// <summary>
        /// Get Narration by accGLSetupNarrationId.
        /// </summary>
        /// <param name="accGLSetupNarrationId">accGLSetupNarrationId</param>
        /// <returns>Returns AccGLSetupNarrationViewModel.</returns>
        AccGLSetupNarrationViewModel GetNarration(int accGLSetupNarrationId);

        /// <summary>
        /// Update Narration.
        /// </summary>
        /// <param name="accGLSetupNarrationViewModel">accGLSetupNarrationViewModel.</param>
        /// <returns>Returns updated AccGLSetupNarrationViewModel</returns>
        AccGLSetupNarrationViewModel UpdateNarration(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel);
        //AccGLSetupNarrationListResponse GetNarrationList();
    }
}
