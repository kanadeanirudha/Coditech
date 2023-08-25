using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralDesignationMasterService
    {
        GeneralDesignationListModel GetDesignationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralDesignationModel CreateDesignation(GeneralDesignationModel model);
        GeneralDesignationModel GetDesignation(short generalDesignationMasterId);
        bool UpdateDesignation(GeneralDesignationModel model);
        bool DeleteDesignation(ParameterModel parameterModel);
    }
}
