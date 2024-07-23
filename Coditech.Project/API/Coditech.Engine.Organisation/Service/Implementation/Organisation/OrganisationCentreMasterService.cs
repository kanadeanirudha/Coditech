using Coditech.API.Data;
using Coditech.API.Organisation.Service.Interface.Organisation;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Model;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class OrganisationCentreMasterService : BaseService, IOrganisationCentreMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository;
        private readonly ICoditechRepository<OrganisationCentrePrintingFormat> _organisationCentrePrintingFormatRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseGSTCredential> _organisationCentrewiseGSTCredentialRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseSmtpSetting> _organisationCentrewiseSmtpSettingRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseSmsSetting> _organisationCentrewiseSmsSettingRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseEmailTemplate> _organisationCentrewiseEmailTemplateRepository;
        private readonly ICoditechRepository<GeneralEmailTemplate> _generalEmailTemplateRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseUserNameRegistration> _organisationCentrewiseUserNameRegistrationRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseWhatsAppSetting> _organisationCentrewiseWhatsAppSettingRepository;
        public OrganisationCentreMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrePrintingFormatRepository = new CoditechRepository<OrganisationCentrePrintingFormat>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseGSTCredentialRepository = new CoditechRepository<OrganisationCentrewiseGSTCredential>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseSmtpSettingRepository = new CoditechRepository<OrganisationCentrewiseSmtpSetting>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseSmsSettingRepository = new CoditechRepository<OrganisationCentrewiseSmsSetting>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseEmailTemplateRepository = new CoditechRepository<OrganisationCentrewiseEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>());
            _generalEmailTemplateRepository = new CoditechRepository<GeneralEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseUserNameRegistrationRepository = new CoditechRepository<OrganisationCentrewiseUserNameRegistration>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseWhatsAppSettingRepository = new CoditechRepository<OrganisationCentrewiseWhatsAppSetting>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentreModel> objStoredProc = new CoditechViewRepository<OrganisationCentreModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentreModel> organisationCentreList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentreList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentreListModel listModel = new OrganisationCentreListModel();

            listModel.OrganisationCentreList = organisationCentreList?.Count > 0 ? organisationCentreList : new List<OrganisationCentreModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create Organisation Centre.
        public virtual OrganisationCentreModel CreateOrganisationCentre(OrganisationCentreModel organisationCentreModel)
        {
            if (IsNull(organisationCentreModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCentreCodeAlreadyExist(organisationCentreModel.CentreCode))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            OrganisationCentreMaster organisationCentreMaster = organisationCentreModel.FromModelToEntity<OrganisationCentreMaster>();

            //Create new Organisation Centre and return it.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Insert(organisationCentreMaster);
            if (organisationData?.OrganisationCentreMasterId > 0)
            {
                organisationCentreModel.OrganisationCentreMasterId = organisationData.OrganisationCentreMasterId;
            }
            else
            {
                organisationCentreModel.HasError = true;
                organisationCentreModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return organisationCentreModel;
        }

        //Get Organisation Centre by organisationCentreMasterId.
        public virtual OrganisationCentreModel GetOrganisationCentre(short organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            //Get the  Organisation Details based on id.
            OrganisationCentreMaster organisationData = _organisationCentreMasterRepository.Table.FirstOrDefault(x => x.OrganisationCentreMasterId == organisationCentreId);
            OrganisationCentreModel organisationCentreModel = organisationData.FromEntityToModel<OrganisationCentreModel>();
            return IsNotNull(organisationData) ? organisationCentreModel : new OrganisationCentreModel();
        }

        //Update  Organisation Centre.
        public virtual bool UpdateOrganisationCentre(OrganisationCentreModel organisationCentreModel)
        {
            if (IsNull(organisationCentreModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentreModel.OrganisationCentreMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            if (IsCentreCodeAlreadyExist(organisationCentreModel.CentreCode, organisationCentreModel.OrganisationCentreMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            OrganisationCentreMaster organisationCentreMaster = organisationCentreModel.FromModelToEntity<OrganisationCentreMaster>();

            //Update Organisation Centre
            bool isOrganisationCentreUpdated = _organisationCentreMasterRepository.Update(organisationCentreMaster);
            if (!isOrganisationCentreUpdated)
            {
                organisationCentreModel.HasError = true;
                organisationCentreModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentreUpdated;
        }

        //Delete Organisation Centre.
        public virtual bool DeleteOrganisationCentre(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("OrganisationCentreId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteOrganisationCentre @organisationCentreId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        //Get Organisation Centre Printing Format by organisationCentreMasterId.
        public virtual OrganisationCentrePrintingFormatModel GetPrintingFormat(short organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            //Get the  Organisation Details based on id.

            OrganisationCentrePrintingFormat organisationCentrePrintingData = _organisationCentrePrintingFormatRepository.Table.FirstOrDefault(x => x.OrganisationCentreMasterId == organisationCentreId);
            OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel = IsNull(organisationCentrePrintingData) ? new OrganisationCentrePrintingFormatModel() : organisationCentrePrintingData.FromEntityToModel<OrganisationCentrePrintingFormatModel>();
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            organisationCentrePrintingFormatModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrePrintingFormatModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrePrintingFormatModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrePrintingFormatModel;
        }

        //Update  Organisation Centre Printing Format.
        public virtual bool UpdatePrintingFormat(OrganisationCentrePrintingFormatModel organisationCentrePrintingFormatModel)
        {
            if (IsNull(organisationCentrePrintingFormatModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrePrintingFormatModel.OrganisationCentreMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreMasterId"));

            if (IsCentreCodeAlreadyExist(organisationCentrePrintingFormatModel.CentreCode, organisationCentrePrintingFormatModel.OrganisationCentreMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            bool isOrganisationCentrePrintingFormatUpdated = false;
            OrganisationCentrePrintingFormat organisationCentrePrintingFormat = organisationCentrePrintingFormatModel.FromModelToEntity<OrganisationCentrePrintingFormat>();

            if (organisationCentrePrintingFormatModel.OrganisationCentrePrintingFormatId > 0)
                isOrganisationCentrePrintingFormatUpdated = _organisationCentrePrintingFormatRepository.Update(organisationCentrePrintingFormat);
            else
            {
                organisationCentrePrintingFormat = _organisationCentrePrintingFormatRepository.Insert(organisationCentrePrintingFormat);
                isOrganisationCentrePrintingFormatUpdated = organisationCentrePrintingFormat.OrganisationCentrePrintingFormatId > 0;
            }

            if (!isOrganisationCentrePrintingFormatUpdated)
            {
                organisationCentrePrintingFormatModel.HasError = true;
                organisationCentrePrintingFormatModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrePrintingFormatUpdated;
        }

        //Get Organisation Centrewise GST Credential by organisationCentreMasterId.
        public virtual OrganisationCentrewiseGSTCredentialModel GetCentrewiseGSTSetup(short organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            //Get the  Organisation Details based on id.
            OrganisationCentrewiseGSTCredential organisationCentrewiseGSTCredentialData = _organisationCentrewiseGSTCredentialRepository.Table.FirstOrDefault(x => x.OrganisationCentreMasterId == organisationCentreId);
            OrganisationCentrewiseGSTCredentialModel organisationCentrewiseGSTCredentialModel = IsNull(organisationCentrewiseGSTCredentialData) ? new OrganisationCentrewiseGSTCredentialModel() : organisationCentrewiseGSTCredentialData.FromEntityToModel<OrganisationCentrewiseGSTCredentialModel>();
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            organisationCentrewiseGSTCredentialModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseGSTCredentialModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseGSTCredentialModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseGSTCredentialModel;
        }

        //Update  Organisation Centrewise GST Credential .
        public virtual bool UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel organisationCentrewiseGSTCredentialModel)
        {
            if (IsNull(organisationCentrewiseGSTCredentialModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseGSTCredentialModel.OrganisationCentreMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreMasterId"));

            if (IsCentreCodeAlreadyExist(organisationCentrewiseGSTCredentialModel.CentreCode, organisationCentrewiseGSTCredentialModel.OrganisationCentreMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            bool isOrganisationCentrewiseGSTCredentialUpdated = false;
            OrganisationCentrewiseGSTCredential organisationCentrewiseGSTCredential = organisationCentrewiseGSTCredentialModel.FromModelToEntity<OrganisationCentrewiseGSTCredential>();

            if (organisationCentrewiseGSTCredentialModel.OrganisationCentrewiseGSTCredentialId > 0)
                isOrganisationCentrewiseGSTCredentialUpdated = _organisationCentrewiseGSTCredentialRepository.Update(organisationCentrewiseGSTCredential);
            else
            {
                organisationCentrewiseGSTCredential = _organisationCentrewiseGSTCredentialRepository.Insert(organisationCentrewiseGSTCredential);
                isOrganisationCentrewiseGSTCredentialUpdated = organisationCentrewiseGSTCredential.OrganisationCentrewiseGSTCredentialId > 0;
            }

            if (!isOrganisationCentrewiseGSTCredentialUpdated)
            {
                organisationCentrewiseGSTCredentialModel.HasError = true;
                organisationCentrewiseGSTCredentialModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseGSTCredentialUpdated;
        }

        //Get Organisation Centrewise Smtp Setting by organisationCentreMasterId.
        public virtual OrganisationCentrewiseSmtpSettingModel GetCentrewiseSmtpSetup(short organisationCentreId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the  Organisation Details based on id.

            OrganisationCentrewiseSmtpSetting organisationCentrewiseSmtpSettingData = _organisationCentrewiseSmtpSettingRepository.Table.FirstOrDefault(x => x.CentreCode == organisationCentreModel.CentreCode);
            OrganisationCentrewiseSmtpSettingModel organisationCentrewiseSmtpSettingModel = IsNull(organisationCentrewiseSmtpSettingData) ? new OrganisationCentrewiseSmtpSettingModel() : organisationCentrewiseSmtpSettingData.FromEntityToModel<OrganisationCentrewiseSmtpSettingModel>();

            organisationCentrewiseSmtpSettingModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseSmtpSettingModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseSmtpSettingModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseSmtpSettingModel;
        }

        //Update  Organisation Centrewise Smtp Setting .
        public virtual bool UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingModel organisationCentrewiseSmtpSettingModel)
        {
            if (IsNull(organisationCentrewiseSmtpSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            bool isOrganisationCentrewiseSmtpSettingUpdated = false;
            OrganisationCentrewiseSmtpSetting organisationCentrewiseSmtpSetting = organisationCentrewiseSmtpSettingModel.FromModelToEntity<OrganisationCentrewiseSmtpSetting>();

            if (organisationCentrewiseSmtpSettingModel.OrganisationCentrewiseSmtpSettingId > 0)
                isOrganisationCentrewiseSmtpSettingUpdated = _organisationCentrewiseSmtpSettingRepository.Update(organisationCentrewiseSmtpSetting);
            else
            {
                organisationCentrewiseSmtpSetting = _organisationCentrewiseSmtpSettingRepository.Insert(organisationCentrewiseSmtpSetting);
                isOrganisationCentrewiseSmtpSettingUpdated = organisationCentrewiseSmtpSetting.OrganisationCentrewiseSmtpSettingId > 0;
            }

            if (!isOrganisationCentrewiseSmtpSettingUpdated)
            {
                organisationCentrewiseSmtpSettingModel.HasError = true;
                organisationCentrewiseSmtpSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseSmtpSettingUpdated;
        }

        //Get Organisation Centrewise Sms Setting by organisationCentreMasterId.
        public virtual OrganisationCentrewiseSmsSettingModel GetCentrewiseSmsSetup(short organisationCentreId, byte generalSmsProviderId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the Organisation Details based on id.

            OrganisationCentrewiseSmsSetting organisationCentrewiseSmsSettingData = generalSmsProviderId > 0 ? _organisationCentrewiseSmsSettingRepository.Table.FirstOrDefault(x => x.CentreCode == organisationCentreModel.CentreCode && x.GeneralSmsProviderId == generalSmsProviderId) : null;
            OrganisationCentrewiseSmsSettingModel organisationCentrewiseSmsSettingModel = IsNull(organisationCentrewiseSmsSettingData) ? new OrganisationCentrewiseSmsSettingModel() : organisationCentrewiseSmsSettingData.FromEntityToModel<OrganisationCentrewiseSmsSettingModel>();

            organisationCentrewiseSmsSettingModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseSmsSettingModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseSmsSettingModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseSmsSettingModel;
        }

        //Update  Organisation Centrewise Sms Setting .
        public virtual bool UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingModel organisationCentrewiseSmsSettingModel)
        {
            if (IsNull(organisationCentrewiseSmsSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseSmsSettingModel.OrganisationCentreMasterId == 0)
                throw new CoditechException(ErrorCodes.InvalidData, "OrganisationCentreId cannot be zero");

            bool isOrganisationCentrewiseSmsSettingUpdated = false;
            OrganisationCentrewiseSmsSetting organisationCentrewiseSmsSetting = organisationCentrewiseSmsSettingModel.FromModelToEntity<OrganisationCentrewiseSmsSetting>();

            if (organisationCentrewiseSmsSettingModel.OrganisationCentrewiseSmsSettingId > 0)
                isOrganisationCentrewiseSmsSettingUpdated = _organisationCentrewiseSmsSettingRepository.Update(organisationCentrewiseSmsSetting);
            else
            {
                organisationCentrewiseSmsSetting = _organisationCentrewiseSmsSettingRepository.Insert(organisationCentrewiseSmsSetting);
                isOrganisationCentrewiseSmsSettingUpdated = organisationCentrewiseSmsSetting.OrganisationCentrewiseSmsSettingId > 0;
                organisationCentrewiseSmsSettingModel.OrganisationCentrewiseSmsSettingId = organisationCentrewiseSmsSetting.OrganisationCentrewiseSmsSettingId;
            }

            if (!isOrganisationCentrewiseSmsSettingUpdated)
            {
                organisationCentrewiseSmsSettingModel.HasError = true;
                organisationCentrewiseSmsSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseSmsSettingUpdated;
        }

        //Get Organisation Centrewise WhatsApp Setting by organisationCentreMasterId.
        public virtual OrganisationCentrewiseWhatsAppSettingModel GetCentrewiseWhatsAppSetup(short organisationCentreId, byte generalWhatsAppProviderId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the Organisation Details based on id.

            OrganisationCentrewiseWhatsAppSetting organisationCentrewiseWhatsAppSettingData = generalWhatsAppProviderId > 0 ? _organisationCentrewiseWhatsAppSettingRepository.Table.FirstOrDefault(x => x.CentreCode == organisationCentreModel.CentreCode && x.GeneralWhatsAppProviderId == generalWhatsAppProviderId) : null;
            OrganisationCentrewiseWhatsAppSettingModel organisationCentrewiseWhatsAppSettingModel = IsNull(organisationCentrewiseWhatsAppSettingData) ? new OrganisationCentrewiseWhatsAppSettingModel() : organisationCentrewiseWhatsAppSettingData.FromEntityToModel<OrganisationCentrewiseWhatsAppSettingModel>();

            
            organisationCentrewiseWhatsAppSettingModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseWhatsAppSettingModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseWhatsAppSettingModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseWhatsAppSettingModel;
        }

        //Update  Organisation Centrewise WhatsApp Setting .
        public virtual bool UpdateCentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingModel organisationCentrewiseWhatsAppSettingModel)
        {
            if (IsNull(organisationCentrewiseWhatsAppSettingModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (organisationCentrewiseWhatsAppSettingModel.OrganisationCentreMasterId == 0)
                throw new CoditechException(ErrorCodes.InvalidData, "OrganisationCentreId cannot be zero");

            bool isOrganisationCentrewiseWhatsAppSettingUpdated = false;
            OrganisationCentrewiseWhatsAppSetting organisationCentrewiseWhatsAppSetting = organisationCentrewiseWhatsAppSettingModel.FromModelToEntity<OrganisationCentrewiseWhatsAppSetting>();

            if (organisationCentrewiseWhatsAppSettingModel.OrganisationCentrewiseWhatsAppSettingId > 0)
                isOrganisationCentrewiseWhatsAppSettingUpdated = _organisationCentrewiseWhatsAppSettingRepository.Update(organisationCentrewiseWhatsAppSetting);
            else
            {
                organisationCentrewiseWhatsAppSetting = _organisationCentrewiseWhatsAppSettingRepository.Insert(organisationCentrewiseWhatsAppSetting);
                isOrganisationCentrewiseWhatsAppSettingUpdated = organisationCentrewiseWhatsAppSetting.OrganisationCentrewiseWhatsAppSettingId> 0;
                organisationCentrewiseWhatsAppSettingModel.OrganisationCentrewiseWhatsAppSettingId = organisationCentrewiseWhatsAppSetting.OrganisationCentrewiseWhatsAppSettingId;
            }

            if (!isOrganisationCentrewiseWhatsAppSettingUpdated)
            {
                organisationCentrewiseWhatsAppSettingModel.HasError = true;
                organisationCentrewiseWhatsAppSettingModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseWhatsAppSettingUpdated;
        }


        //Get Organisation Centrewise Email Template by organisationCentreMasterId.
        public virtual OrganisationCentrewiseEmailTemplateModel GetCentrewiseEmailTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the  Organisation Details based on id.
            OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = new OrganisationCentrewiseEmailTemplateModel();
            if (!string.IsNullOrEmpty(emailTemplateCode))
            {
                OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplateData = _organisationCentrewiseEmailTemplateRepository.Table.Where(x => x.CentreCode == organisationCentreModel.CentreCode && x.EmailTemplateCode == emailTemplateCode)?.FirstOrDefault();
                if (IsNotNull(organisationCentrewiseEmailTemplateData))
                {
                    organisationCentrewiseEmailTemplateModel = IsNull(organisationCentrewiseEmailTemplateData) ? new OrganisationCentrewiseEmailTemplateModel() : organisationCentrewiseEmailTemplateData.FromEntityToModel<OrganisationCentrewiseEmailTemplateModel>();
                }
                else
                {
                    GeneralEmailTemplate generalEmailTemplate = _generalEmailTemplateRepository.Table.Where(x => x.EmailTemplateCode == emailTemplateCode && x.IsActive)?.FirstOrDefault();
                    if (IsNotNull(generalEmailTemplate))
                    {
                        organisationCentrewiseEmailTemplateModel.EmailTemplateCode = generalEmailTemplate.EmailTemplateCode;
                        organisationCentrewiseEmailTemplateModel.Subject = generalEmailTemplate.Subject;
                        organisationCentrewiseEmailTemplateModel.EmailTemplate = generalEmailTemplate.EmailTemplate;
                    }
                }
            }
            organisationCentrewiseEmailTemplateModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseEmailTemplateModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseEmailTemplateModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseEmailTemplateModel;
        }

        //Update  Organisation Centrewise Smtp Setting .
        public virtual bool UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel)
        {
            if (IsNull(organisationCentrewiseEmailTemplateModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            bool isOrganisationCentrewiseEmailTemplateUpdated = false;
            OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplate = organisationCentrewiseEmailTemplateModel.FromModelToEntity<OrganisationCentrewiseEmailTemplate>();

            if (organisationCentrewiseEmailTemplateModel.OrganisationCentrewiseEmailTemplateId > 0)
                isOrganisationCentrewiseEmailTemplateUpdated = _organisationCentrewiseEmailTemplateRepository.Update(organisationCentrewiseEmailTemplate);
            else
            {
                organisationCentrewiseEmailTemplate = _organisationCentrewiseEmailTemplateRepository.Insert(organisationCentrewiseEmailTemplate);
                isOrganisationCentrewiseEmailTemplateUpdated = organisationCentrewiseEmailTemplate.OrganisationCentrewiseEmailTemplateId > 0;
            }

            if (!isOrganisationCentrewiseEmailTemplateUpdated)
            {
                organisationCentrewiseEmailTemplateModel.HasError = true;
                organisationCentrewiseEmailTemplateModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseEmailTemplateUpdated;
        }

        //Get Organisation Centrewise SMS Template by organisationCentreMasterId.
        public virtual OrganisationCentrewiseEmailTemplateModel GetCentrewiseSMSTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the  Organisation Details based on id.
            OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = new OrganisationCentrewiseEmailTemplateModel();
            if (!string.IsNullOrEmpty(emailTemplateCode))
            {
                OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplateData = _organisationCentrewiseEmailTemplateRepository.Table.Where(x => x.CentreCode == organisationCentreModel.CentreCode && x.EmailTemplateCode == emailTemplateCode)?.FirstOrDefault();
                if (IsNotNull(organisationCentrewiseEmailTemplateData))
                {
                    organisationCentrewiseEmailTemplateModel = IsNull(organisationCentrewiseEmailTemplateData) ? new OrganisationCentrewiseEmailTemplateModel() : organisationCentrewiseEmailTemplateData.FromEntityToModel<OrganisationCentrewiseEmailTemplateModel>();
                }
                else
                {
                    GeneralEmailTemplate generalEmailTemplate = _generalEmailTemplateRepository.Table.Where(x => x.EmailTemplateCode == emailTemplateCode && x.IsActive)?.FirstOrDefault();
                    if (IsNotNull(generalEmailTemplate))
                    {
                        organisationCentrewiseEmailTemplateModel.EmailTemplateCode = generalEmailTemplate.EmailTemplateCode;
                        organisationCentrewiseEmailTemplateModel.Subject = generalEmailTemplate.Subject;
                        organisationCentrewiseEmailTemplateModel.EmailTemplate = generalEmailTemplate.EmailTemplate;
                    }
                }
            }
            organisationCentrewiseEmailTemplateModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseEmailTemplateModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseEmailTemplateModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseEmailTemplateModel;
        }

        //Update  Organisation Centrewise SMS Template .
        public virtual bool UpdateCentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel)
        {
           
            if (IsNull(organisationCentrewiseEmailTemplateModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            bool isOrganisationCentrewiseEmailTemplateUpdated = false;
            OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplate = organisationCentrewiseEmailTemplateModel.FromModelToEntity<OrganisationCentrewiseEmailTemplate>();

            if (organisationCentrewiseEmailTemplateModel.OrganisationCentrewiseEmailTemplateId > 0)
                isOrganisationCentrewiseEmailTemplateUpdated = _organisationCentrewiseEmailTemplateRepository.Update(organisationCentrewiseEmailTemplate);
            else
            {
                organisationCentrewiseEmailTemplate = _organisationCentrewiseEmailTemplateRepository.Insert(organisationCentrewiseEmailTemplate);
                isOrganisationCentrewiseEmailTemplateUpdated = organisationCentrewiseEmailTemplate.OrganisationCentrewiseEmailTemplateId > 0;
            }

            if (!isOrganisationCentrewiseEmailTemplateUpdated)
            {
                organisationCentrewiseEmailTemplateModel.HasError = true;
                organisationCentrewiseEmailTemplateModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseEmailTemplateUpdated;
        }

        //Get Organisation Centrewise WhatsApp Template by organisationCentreMasterId.
        public virtual OrganisationCentrewiseEmailTemplateModel GetCentrewiseWhatsAppTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));

            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the  Organisation Details based on id.
            OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel = new OrganisationCentrewiseEmailTemplateModel();
            if (!string.IsNullOrEmpty(emailTemplateCode))
            {
                OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplateData = _organisationCentrewiseEmailTemplateRepository.Table.Where(x => x.CentreCode == organisationCentreModel.CentreCode && x.EmailTemplateCode == emailTemplateCode)?.FirstOrDefault();
                if (IsNotNull(organisationCentrewiseEmailTemplateData))
                {
                    organisationCentrewiseEmailTemplateModel = IsNull(organisationCentrewiseEmailTemplateData) ? new OrganisationCentrewiseEmailTemplateModel() : organisationCentrewiseEmailTemplateData.FromEntityToModel<OrganisationCentrewiseEmailTemplateModel>();
                }
                else
                {
                    GeneralEmailTemplate generalEmailTemplate = _generalEmailTemplateRepository.Table.Where(x => x.EmailTemplateCode == emailTemplateCode && x.IsActive)?.FirstOrDefault();
                    if (IsNotNull(generalEmailTemplate))
                    {
                        organisationCentrewiseEmailTemplateModel.EmailTemplateCode = generalEmailTemplate.EmailTemplateCode;
                        organisationCentrewiseEmailTemplateModel.Subject = generalEmailTemplate.Subject;
                        organisationCentrewiseEmailTemplateModel.EmailTemplate = generalEmailTemplate.EmailTemplate;
                    }
                }
            }
            organisationCentrewiseEmailTemplateModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseEmailTemplateModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseEmailTemplateModel.OrganisationCentreMasterId = organisationCentreId;
            return organisationCentrewiseEmailTemplateModel;
        }

        //Update  Organisation Centrewise WhatsApp Template .
        public virtual bool UpdateCentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateModel organisationCentrewiseEmailTemplateModel)
        {
            if (IsNull(organisationCentrewiseEmailTemplateModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            bool isOrganisationCentrewiseEmailTemplateUpdated = false;
            OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplate = organisationCentrewiseEmailTemplateModel.FromModelToEntity<OrganisationCentrewiseEmailTemplate>();

            if (organisationCentrewiseEmailTemplateModel.OrganisationCentrewiseEmailTemplateId > 0)
                isOrganisationCentrewiseEmailTemplateUpdated = _organisationCentrewiseEmailTemplateRepository.Update(organisationCentrewiseEmailTemplate);
            else
            {
                organisationCentrewiseEmailTemplate = _organisationCentrewiseEmailTemplateRepository.Insert(organisationCentrewiseEmailTemplate);
                isOrganisationCentrewiseEmailTemplateUpdated = organisationCentrewiseEmailTemplate.OrganisationCentrewiseEmailTemplateId > 0;
            }

            if (!isOrganisationCentrewiseEmailTemplateUpdated)
            {
                organisationCentrewiseEmailTemplateModel.HasError = true;
                organisationCentrewiseEmailTemplateModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseEmailTemplateUpdated;
        }

        //Get Organisation Centrewise UserName Registration by organisationCentreMasterId.
        public virtual OrganisationCentrewiseUserNameRegistrationModel GetCentrewiseUserName(short organisationCentreId, short organisationCentrewiseUserNameRegistrationId)
        {
            if (organisationCentreId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "organisationCentreId"));
            OrganisationCentreModel organisationCentreModel = GetOrganisationCentre(organisationCentreId);
            //Get the  Organisation Details based on id.
            OrganisationCentrewiseUserNameRegistrationModel organisationCentrewiseUserNameRegistrationModel = new OrganisationCentrewiseUserNameRegistrationModel();

            if (organisationCentrewiseUserNameRegistrationId > 0)
            {
                OrganisationCentrewiseUserNameRegistration organisationCentrewiseUserNameRegistrationData = _organisationCentrewiseUserNameRegistrationRepository.Table.FirstOrDefault(x => x.CentreCode == organisationCentreModel.CentreCode);
                organisationCentrewiseUserNameRegistrationModel = IsNull(organisationCentrewiseUserNameRegistrationData) ? new OrganisationCentrewiseUserNameRegistrationModel() : organisationCentrewiseUserNameRegistrationData.FromEntityToModel<OrganisationCentrewiseUserNameRegistrationModel>();
            }
            organisationCentrewiseUserNameRegistrationModel.CentreCode = organisationCentreModel.CentreCode;
            organisationCentrewiseUserNameRegistrationModel.CentreName = organisationCentreModel.CentreName;
            organisationCentrewiseUserNameRegistrationModel.OrganisationCentreMasterId = organisationCentreId;
            organisationCentrewiseUserNameRegistrationModel.CentrewiseUserNameRegistrationList = _organisationCentrewiseUserNameRegistrationRepository.Table.Where(x => x.CentreCode == organisationCentreModel.CentreCode)?.FromEntityToModel<List<OrganisationCentrewiseUserNameRegistrationModel>>()?.ToList();
            return organisationCentrewiseUserNameRegistrationModel;
        }

        //Update  Organisation Centrewise UserName Registration .
        public virtual bool UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationModel organisationCentrewiseUserNameRegistrationModel)
        {
            if (IsNull(organisationCentrewiseUserNameRegistrationModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (IsCentrewiseUserNameAlreadyExist(organisationCentrewiseUserNameRegistrationModel.CentreCode, organisationCentrewiseUserNameRegistrationModel.UserType, organisationCentrewiseUserNameRegistrationModel.OrganisationCentrewiseUserNameRegistrationId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Centre Code"));

            bool isOrganisationCentrewiseUserNameUpdated = false;
            OrganisationCentrewiseUserNameRegistration organisationCentrewiseUserNameRegistration = organisationCentrewiseUserNameRegistrationModel.FromModelToEntity<OrganisationCentrewiseUserNameRegistration>();

            if (organisationCentrewiseUserNameRegistrationModel.OrganisationCentrewiseUserNameRegistrationId > 0)
                isOrganisationCentrewiseUserNameUpdated = _organisationCentrewiseUserNameRegistrationRepository.Update(organisationCentrewiseUserNameRegistration);
            else
            {
                organisationCentrewiseUserNameRegistration = _organisationCentrewiseUserNameRegistrationRepository.Insert(organisationCentrewiseUserNameRegistration);
                isOrganisationCentrewiseUserNameUpdated = organisationCentrewiseUserNameRegistration.OrganisationCentrewiseUserNameRegistrationId > 0;
            }

            if (!isOrganisationCentrewiseUserNameUpdated)
            {
                organisationCentrewiseUserNameRegistrationModel.HasError = true;
                organisationCentrewiseUserNameRegistrationModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isOrganisationCentrewiseUserNameUpdated;
        }

        #region Protected Method
        //Check if Centre code is already present or not.
        protected virtual bool IsCentreCodeAlreadyExist(string centreCode, short organisationCentreMasterId = 0)
         => _organisationCentreMasterRepository.Table.Any(x => x.CentreCode == centreCode && (x.OrganisationCentreMasterId != organisationCentreMasterId || organisationCentreMasterId == 0));

        protected virtual bool IsCentrewiseUserNameAlreadyExist(string centreCode, string userType, short organisationCentrewiseUserNameRegistrationId = 0)
        => _organisationCentrewiseUserNameRegistrationRepository.Table.Any(x => x.CentreCode == centreCode && x.UserType == userType && (x.OrganisationCentrewiseUserNameRegistrationId != organisationCentrewiseUserNameRegistrationId || organisationCentrewiseUserNameRegistrationId == 0));

        #endregion
    }
}

