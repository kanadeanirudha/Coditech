using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralEmailTemplateService : IGeneralEmailTemplateService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralEmailTemplate> _generalEmailTemplateRepository;
        public GeneralEmailTemplateService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalEmailTemplateRepository = new CoditechRepository<GeneralEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralEmailTemplateListModel GetEmailTemplateList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralEmailTemplateModel> objStoredProc = new CoditechViewRepository<GeneralEmailTemplateModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralEmailTemplateModel> EmailTemplateList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGeneralEmailTemplateList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralEmailTemplateListModel listModel = new GeneralEmailTemplateListModel();

            listModel.GeneralEmailTemplateList = EmailTemplateList?.Count > 0 ? EmailTemplateList : new List<GeneralEmailTemplateModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create EmailTemplate.
        public virtual GeneralEmailTemplateModel CreateEmailTemplate(GeneralEmailTemplateModel generalEmailTemplateModel)
        {
            if (IsNull(generalEmailTemplateModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsEmailTemplateCodeAlreadyExist(generalEmailTemplateModel.EmailTemplateCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmailTemplate Code"));

            GeneralEmailTemplate generalEmailTemplate = generalEmailTemplateModel.FromModelToEntity<GeneralEmailTemplate>();

            //Create new EmailTemplate and return it.
            GeneralEmailTemplate emailTemplateData = _generalEmailTemplateRepository.Insert(generalEmailTemplate);
            if (emailTemplateData?.GeneralEmailTemplateId > 0)
            {
                generalEmailTemplateModel.GeneralEmailTemplateId = emailTemplateData.GeneralEmailTemplateId;
            }
            else
            {
                generalEmailTemplateModel.HasError = true;
                generalEmailTemplateModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalEmailTemplateModel;
        }

        //Get EmailTemplate by EmailTemplate id.
        public virtual GeneralEmailTemplateModel GetEmailTemplate(short emailTemplateId)
        {
            if (emailTemplateId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmailTemplateID"));

            //Get the EmailTemplate Details based on id.
            GeneralEmailTemplate generalEmailTemplate = _generalEmailTemplateRepository.Table.FirstOrDefault(x => x.GeneralEmailTemplateId == emailTemplateId);
            GeneralEmailTemplateModel generalEmailTemplateModel = generalEmailTemplate?.FromEntityToModel<GeneralEmailTemplateModel>();
            return generalEmailTemplateModel;
        }

        //Update EmailTemplate.
        public virtual bool UpdateEmailTemplate(GeneralEmailTemplateModel generalEmailTemplateModel)
        {
            if (IsNull(generalEmailTemplateModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalEmailTemplateModel.GeneralEmailTemplateId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmailTemplateID"));

            if (IsEmailTemplateCodeAlreadyExist(generalEmailTemplateModel.EmailTemplateCode, generalEmailTemplateModel.GeneralEmailTemplateId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "EmailTemplate Code"));

            GeneralEmailTemplate generalEmailTemplate = generalEmailTemplateModel.FromModelToEntity<GeneralEmailTemplate>();

            //Update EmailTemplate
            bool isEmailTemplateUpdated = _generalEmailTemplateRepository.Update(generalEmailTemplate);
            if (!isEmailTemplateUpdated)
            {
                generalEmailTemplateModel.HasError = true;
                generalEmailTemplateModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isEmailTemplateUpdated;
        }

        //Delete EmailTemplate.
        public virtual bool DeleteEmailTemplate(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "EmailTemplateID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("EmailTemplateId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteEmailTemplate @EmailTemplateId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if EmailTemplate code is already present or not.
        protected virtual bool IsEmailTemplateCodeAlreadyExist(string emailTemplateName, short generalEmailTemplateId = 0)
         => _generalEmailTemplateRepository.Table.Any(x => x.EmailTemplateName == emailTemplateName && (x.GeneralEmailTemplateId != generalEmailTemplateId || generalEmailTemplateId == 0));
        #endregion
    }
}
