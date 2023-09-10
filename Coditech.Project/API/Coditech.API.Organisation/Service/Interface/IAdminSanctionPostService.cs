using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAdminSanctionPostService
    {
        AdminSanctionPostListModel GetAdminSanctionPostList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminSanctionPostModel CreateAdminSanctionPost(AdminSanctionPostModel model);
        AdminSanctionPostModel GetAdminSanctionPost(int adminSanctionPostId);
        bool UpdateAdminSanctionPost(AdminSanctionPostModel model);
        bool DeleteAdminSanctionPost(ParameterModel parameterModel);
    }
}
