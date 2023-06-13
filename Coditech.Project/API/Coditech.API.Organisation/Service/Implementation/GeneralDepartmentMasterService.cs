using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Service
{
    public class GeneralDepartmentMasterService : IGeneralDepartmentMasterService
    {
        private readonly IMapper Mapper;
        private readonly GeneralDepartmentMasterDBContext _departmentMasterDBContext;
        public GeneralDepartmentMasterService(IMapper mapper,GeneralDepartmentMasterDBContext departmentMasterDBContext)
        {
            Mapper = mapper;
            _departmentMasterDBContext = departmentMasterDBContext;
        }

        public virtual GeneralDepartmentMasterModel Get(short deneralDepartmentMasterId)
        {
            GeneralDepartmentMaster generalDepartmentMaster = _departmentMasterDBContext.GeneralDepartmentMaster.Find(deneralDepartmentMasterId);
            return Mapper.Map<GeneralDepartmentMasterModel>(generalDepartmentMaster);
        }

        public virtual GeneralDepartmentMasterModel Update(GeneralDepartmentMasterModel model)
        {
            GeneralDepartmentMasterModel DepartmentMasterModel = new GeneralDepartmentMasterModel();
            return DepartmentMasterModel;
        }
    }
}
