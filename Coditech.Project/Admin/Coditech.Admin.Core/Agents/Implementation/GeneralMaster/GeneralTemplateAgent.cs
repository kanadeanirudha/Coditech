using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.Admin.Agents
{
    public class GeneralTemplateAgent : BaseAgent, IGeneralTemplateAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IGeneralTemplateClient _generalTemplateClient;
        #endregion

        #region Public Constructor
        public GeneralTemplateAgent(ICoditechLogging coditechLogging, IGeneralTemplateClient generalTemplateClient)
        {
            _coditechLogging = coditechLogging;
            _generalTemplateClient = GetClient<IGeneralTemplateClient>(generalTemplateClient);
        }
        #endregion

        #region Public Methods
        //Get general template  by general template id.
        public virtual GeneralTemplateViewModel GetTemplate(int generalTemplateId)
        {
            GeneralTemplateResponse response = _generalTemplateClient.GetTemplate(generalTemplateId);
            return response?.GeneralTemplateModel.ToViewModel<GeneralTemplateViewModel>();
        }
        #endregion

        #region protected
        #endregion
    }
}
