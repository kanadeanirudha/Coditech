using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralDesignationMasterService
    {
        GeneralDesignationListModel GetDesignationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralDesignationMasterModel CreateDesignation(GeneralDesignationMasterModel model);
        GeneralDesignationMasterModel GetDesignation(short generalDesignationMasterId);
        bool UpdateDesignation(GeneralDesignationMasterModel model);
        bool DeleteDesignation(ParameterModel parameterModel);
    }
}

