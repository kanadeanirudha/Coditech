using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralTemplateService : IGeneralTemplateService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralTemplateMaster> _generalTemplateRepository;
        private readonly ICoditechRepository<GeneralTemplateHeaderConfiguration> _generalTemplateHeaderConfigurationRepository;
        public GeneralTemplateService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalTemplateRepository = new CoditechRepository<GeneralTemplateMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalTemplateHeaderConfigurationRepository = new CoditechRepository<GeneralTemplateHeaderConfiguration>(_serviceProvider.GetService<Coditech_Entities>());
        }

        //Get Template by Template id.
        public virtual GeneralTemplateModel GetTemplate(int generalTemplateId)
        {
            if (generalTemplateId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "generalTemplateId"));
            //Get the Template Details based on id.
            GeneralTemplateMaster generalTemplate = _generalTemplateRepository.Table.FirstOrDefault(x => x.GeneralTemplateMasterId == generalTemplateId);
            if (IsNull(generalTemplate))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);          
            GeneralTemplateModel generalTemplateModel = generalTemplate.FromEntityToModel<GeneralTemplateModel>();
            List<GeneralTemplateHeaderConfiguration> headers = _generalTemplateHeaderConfigurationRepository.Table.Where(x => x.TemplateCode == generalTemplate.TemplateCode).OrderBy(x => x.OrderBy).ToList();
            generalTemplateModel.HeaderConfigurationList = headers.Select(x => new GeneralTemplateHeaderConfigurationModel
            {
                GeneralTemplateHeaderConfigurationId = x.GeneralTemplateHeaderConfigurationId,
                TemplateCode = x.TemplateCode,
                HeaderName = x.HeaderName,
                HeaderType = x.HeaderType,
                CentreCode = x.CentreCode,
                OrderBy = x.OrderBy
            }).ToList();
            return generalTemplateModel;
        }

        #region Protected Method
        #endregion
    }
}
