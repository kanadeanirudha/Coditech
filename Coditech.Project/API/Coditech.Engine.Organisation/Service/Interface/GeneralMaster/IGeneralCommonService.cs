using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IGeneralCommonService
    {
        List<GeneralEnumaratorModel> GetDropdownListByCode(string groupCodes);
    }
}
