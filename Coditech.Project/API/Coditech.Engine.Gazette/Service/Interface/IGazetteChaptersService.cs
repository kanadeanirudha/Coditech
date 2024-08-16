using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGazetteChaptersService
    {
        GazetteChaptersListModel GetGazetteChaptersList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GazetteChaptersModel CreateGazetteChapters(GazetteChaptersModel model);
        GazetteChaptersModel GetGazetteChapters(int gazetteChaptersId);
        bool UpdateGazetteChapters(GazetteChaptersModel model);
        bool DeleteGazetteChapters(ParameterModel parameterModel);
        GazetteChaptersListModel GetGazetteChaptersByDistrictWise(int generalDistrictMasterId);
    }
}
