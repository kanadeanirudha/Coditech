using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralPersonFollowUpService
    {
        GeneralPersonFollowUpListModel GetPersonFollowUpList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralPersonFollowUpModel CreatePersonFollowUp(GeneralPersonFollowUpModel model);
        GeneralPersonFollowUpModel GetPersonFollowUp(long generalPersonFollowUpId);
        bool UpdatePersonFollowUp(GeneralPersonFollowUpModel model);
        bool DeletePersonFollowUp(ParameterModel parameterModel);
    }
}

