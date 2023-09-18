using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAdminSanctionPostClient : IBaseClient
    {
        /// <summary>
        /// Get list of AdminSanctionPost.
        /// </summary>
        /// <returns>AdminSanctionPostListResponse</returns>
        AdminSanctionPostListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create AdminSanctionPost.
        /// </summary>
        /// <param name="AdminSanctionPostModel">AdminSanctionPostModel.</param>
        /// <returns>Returns AdminSanctionPostResponse.</returns>
        AdminSanctionPostResponse CreateAdminSanctionPost(AdminSanctionPostModel body);

        /// <summary>
        /// Get AdminSanctionPost by AdminSanctionPostId.
        /// </summary>
        /// <param name="adminSanctionPostId">adminSanctionPostId</param>
        /// <returns>Returns AdminSanctionPostResponse.</returns>
        AdminSanctionPostResponse GetAdminSanctionPost(int adminSanctionPostId);

        /// <summary>
        /// Update AdminSanctionPost.
        /// </summary>
        /// <param name="AdminSanctionPostModel">AdminSanctionPostModel.</param>
        /// <returns>Returns updated AdminSanctionPostResponse</returns>
        AdminSanctionPostResponse UpdateAdminSanctionPost(AdminSanctionPostModel body);

        /// <summary>
        /// Delete AdminSanctionPost.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteAdminSanctionPost(ParameterModel body);
    }
}
