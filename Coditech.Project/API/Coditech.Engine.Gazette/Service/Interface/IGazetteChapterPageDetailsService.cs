using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGazetteChaptersPageDetailService
    {
        GazetteChaptersPageDetailListModel GetGazetteChaptersPageDetailList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GazetteChaptersPageDetailModel CreateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel model);
        GazetteChaptersPageDetailModel GetGazetteChaptersPageDetail(int gazetteChaptersPageDetailId);
        bool UpdateGazetteChaptersPageDetail(GazetteChaptersPageDetailModel model);
        bool DeleteGazetteChaptersPageDetail(ParameterModel parameterModel);
    }
}
