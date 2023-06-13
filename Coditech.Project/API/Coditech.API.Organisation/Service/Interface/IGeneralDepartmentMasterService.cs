using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IGeneralDepartmentMasterService
    {
        GeneralDepartmentMasterModel Get(short deneralDepartmentMasterId);
        GeneralDepartmentMasterModel Update(GeneralDepartmentMasterModel model);
    }
}
