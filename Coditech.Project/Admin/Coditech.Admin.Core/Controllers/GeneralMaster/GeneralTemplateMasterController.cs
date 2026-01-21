using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralTemplateMasterController : BaseController
    {
        private readonly IGeneralTemplateAgent _generalTemplateAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralTemplate/CreateEdit.cshtml";

        public GeneralTemplateMasterController(IGeneralTemplateAgent generalTemplateAgent)
        {
            _generalTemplateAgent = generalTemplateAgent;
        }
     
        [HttpGet]
        public virtual ActionResult Edit(int generalTemplateId)
        {
            GeneralTemplateViewModel generalTemplateViewModel = _generalTemplateAgent.GetTemplate(generalTemplateId);
            return ActionView(createEdit, generalTemplateViewModel);
        }    
    }
}
