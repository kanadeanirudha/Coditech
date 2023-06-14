using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralDepartmentMasterService : IGeneralDepartmentMasterService
    {
        private readonly IMapper Mapper;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly GeneralDepartmentMasterDBContext _departmentMasterDBContext;
        public GeneralDepartmentMasterService(IMapper mapper, ICoditechLogging coditechLogging, GeneralDepartmentMasterDBContext departmentMasterDBContext)
        {
            Mapper = mapper;
            _departmentMasterDBContext = departmentMasterDBContext;
            _coditechLogging = coditechLogging;
        }

        public virtual GeneralDepartmentMasterModel Insert(GeneralDepartmentMasterModel generalDepartmentMasterModel)
        {
            _coditechLogging.LogMessage("Execution started.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);

            if (IsNull(generalDepartmentMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, "Model can not ne null");

            GeneralDepartmentMaster generalDepartmentMaster = Mapper.Map<GeneralDepartmentMaster>(generalDepartmentMasterModel);

            _departmentMasterDBContext.Add(generalDepartmentMaster);
            int a = _departmentMasterDBContext.SaveChanges();

            _coditechLogging.LogMessage("Execution done.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);
            return generalDepartmentMasterModel;
        }

        public virtual GeneralDepartmentMasterModel Get(short deneralDepartmentMasterId)
        {
            GeneralDepartmentMaster generalDepartmentMaster = _departmentMasterDBContext.GeneralDepartmentMaster.Find(deneralDepartmentMasterId);
            return Mapper.Map<GeneralDepartmentMasterModel>(generalDepartmentMaster);
        }

        public virtual GeneralDepartmentMasterModel Update(GeneralDepartmentMasterModel generalDepartmentMasterModel)
        {
            _coditechLogging.LogMessage("Execution started.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);

            if (IsNull(generalDepartmentMasterModel))
                throw new CoditechException(ErrorCodes.NullModel, "Model can not ne null");

            GeneralDepartmentMaster generalDepartmentMaster = Mapper.Map<GeneralDepartmentMaster>(generalDepartmentMasterModel);

            _departmentMasterDBContext.Update(generalDepartmentMaster);
            _departmentMasterDBContext.SaveChanges();

            _coditechLogging.LogMessage("Execution done.", CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Info);
            return generalDepartmentMasterModel;
        }
    }
}
