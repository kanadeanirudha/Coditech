using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralEmailTemplateAgent
    {

        /// <summary>
        /// Get list of General EmailTemplate.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralEmailTemplateListViewModel</returns>
        GeneralEmailTemplateListViewModel GetEmailTemplateList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Country.
        /// </summary>
        /// <param name="generalEmailTemplateViewModel">General EmailTemplate View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralEmailTemplateViewModel CreateEmailTemplate(GeneralEmailTemplateViewModel generalEmailTemplateViewModel);

        /// <summary>
        /// Get Email by generalEmailTemplateId.
        /// </summary>
        /// <param name="generalEmailTemplateId">generalEmailTemplateId</param>
        /// <returns>Returns GeneralEmailTemplateViewModel.</returns>
        GeneralEmailTemplateViewModel GetEmailTemplate(short generalEmailTemplateId);

        /// <summary>
        /// Update Email.
        /// </summary>
        /// <param name="generalEmailTemplateViewModel">generalEmailTemplateViewModel.</param>
        /// <returns>Returns updated GeneralEmailTemplateViewModel</returns>
        GeneralEmailTemplateViewModel UpdateEmailTemplate(GeneralEmailTemplateViewModel generalEmailTemplateViewModel);

        /// <summary>
        /// Delete Email.
        /// </summary>
        /// <param name="generalEmailTemplateId">generalEmailTemplateId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteEmailTemplate(string generalEmailTemplateId, out string errorMessage);
        GeneralEmailTemplateListResponse GetEmailTemplateList();
    }
}
