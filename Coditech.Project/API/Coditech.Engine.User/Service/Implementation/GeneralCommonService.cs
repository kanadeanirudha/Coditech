using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Coditech.Common.Service;

using System.Data;

namespace Coditech.API.Service
{
    public class GeneralCommonService : BaseService, IGeneralCommonService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCommonService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
        }

        #region Public
        public virtual List<GeneralEnumaratorModel> GetDropdownListByCode(string groupCodes)
        {
            List<GeneralEnumaratorModel> moduleList = base.BindEnumarator();
            List<string> groupCodeList = groupCodes.Split(',')?.ToList();
            return moduleList?.Where(x=> groupCodeList.Contains(x.EnumGroupCode))?.ToList();
        }

        #endregion
    }
}
