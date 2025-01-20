using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralWhatsAppProviderMasterService
    {
        GeneralWhatsAppProviderListModel GetWhatsAppProviderList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralWhatsAppProviderModel CreateWhatsAppProvider(GeneralWhatsAppProviderModel model);
        GeneralWhatsAppProviderModel GetWhatsAppProvider(short WhatsAppProviderId);
        bool UpdateWhatsAppProvider(GeneralWhatsAppProviderModel model);
        bool DeleteWhatsAppProvider(ParameterModel parametermodel);
    }
}
