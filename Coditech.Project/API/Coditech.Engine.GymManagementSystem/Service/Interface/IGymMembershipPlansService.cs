using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGymMembershipPlanService
    {
        GymMembershipPlanListModel GetGymMembershipPlanList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GymMembershipPlanModel GetGymMemberOthershipPlan(int gymMemberDetailId);
        bool UpdateGymMemberOthershipPlan(GymMembershipPlanModel model);
        bool DeleteGymMembers(ParameterModel parameterModel);

        GymMemberFollowUpListModel GymMemberFollowUpList(int gymMemberDetailId, long personId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);

    }
}
