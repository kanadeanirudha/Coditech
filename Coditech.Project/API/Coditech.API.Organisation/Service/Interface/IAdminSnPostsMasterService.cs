using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAdminSnPostsMasterService
    {
        AdminSnPostsListModel GetAdminSnPostsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminSnPostsModel CreateAdminSnPosts(AdminSnPostsModel model);
        AdminSnPostsModel GetAdminSnPosts(short adminSactionPostId); 
        bool UpdateAdminSnPosts(AdminSnPostsModel model);
        bool DeleteAdminSnPosts(ParameterModel parameterModel);
    }
}
