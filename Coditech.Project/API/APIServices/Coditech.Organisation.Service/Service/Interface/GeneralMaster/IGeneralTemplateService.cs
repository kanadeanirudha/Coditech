using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralTemplateService
    {
        GeneralTemplateModel GetTemplate(int generalTemplateMasterId);
    }
}
