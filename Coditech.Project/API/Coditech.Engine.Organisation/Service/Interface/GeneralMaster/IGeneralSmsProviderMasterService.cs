using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralSmsProviderMasterService
    {
        GeneralSmsProviderListModel GetSmsProviderList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralSmsProviderModel CreateSmsProvider(GeneralSmsProviderModel model);
        GeneralSmsProviderModel GetSmsProvider(short SmsProviderId);
        bool UpdateSmsProvider(GeneralSmsProviderModel model);
        bool DeleteSmsProvider(ParameterModel parametermodel); 
    }
}
