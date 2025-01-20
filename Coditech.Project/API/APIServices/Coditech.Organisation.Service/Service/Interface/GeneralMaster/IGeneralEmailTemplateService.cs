using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralEmailTemplateService
    {
        GeneralEmailTemplateListModel GetEmailTemplateList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralEmailTemplateModel CreateEmailTemplate(GeneralEmailTemplateModel model);
        GeneralEmailTemplateModel GetEmailTemplate(short generalEmailTemplateMasterId);
        bool UpdateEmailTemplate(GeneralEmailTemplateModel model);
        bool DeleteEmailTemplate(ParameterModel parameterModel);
    }
}
