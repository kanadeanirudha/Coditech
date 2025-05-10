using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralCommonService : BaseService, IGeneralCommonService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechEmail _coditechEmail;
        protected readonly ICoditechSMS _coditechSMS;
        protected readonly ICoditechWhatsApp _coditechWhatsApp;
        private readonly ICoditechRepository<CoditechApplicationSetting> _coditechApplicationSettingRepository;
        private readonly ICoditechRepository<AccSetupBalanceSheet> _accSetupBalanceSheetRepository;
        private readonly ICoditechRepository<OrganisationCentrewiseAccountSetup> _organisationCentrewiseAccountSetupRepository;
        private readonly ICoditechRepository<GeneralCurrencyMaster> _generalCurrencyMasterRepository;
        private readonly ICoditechRepository<GeneralFinancialYear> _generalFinancialYearMasterRepository;
        private readonly ICoditechRepository<AccSetupGLBalanceSheet> _accSetupGLBalanceSheetRepository;
        public GeneralCommonService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, ICoditechEmail coditechEmail, ICoditechSMS coditechSMS, ICoditechWhatsApp coditechWhatsApp) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechEmail = coditechEmail;
            _coditechSMS = coditechSMS;
            _coditechWhatsApp = coditechWhatsApp;
            _coditechApplicationSettingRepository = new CoditechRepository<CoditechApplicationSetting>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupBalanceSheetRepository = new CoditechRepository<AccSetupBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
            _organisationCentrewiseAccountSetupRepository = new CoditechRepository<OrganisationCentrewiseAccountSetup>(_serviceProvider.GetService<Coditech_Entities>());
            _generalCurrencyMasterRepository = new CoditechRepository<GeneralCurrencyMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalFinancialYearMasterRepository = new CoditechRepository<GeneralFinancialYear>(_serviceProvider.GetService<Coditech_Entities>());
            _accSetupGLBalanceSheetRepository = new CoditechRepository<AccSetupGLBalanceSheet>(_serviceProvider.GetService<Coditech_Entities>());
        }

        #region Public
        public virtual List<GeneralEnumaratorModel> GetDropdownListByCode(string groupCodes)
        {
            List<GeneralEnumaratorModel> moduleList = base.BindEnumarator();
            List<string> groupCodeList = groupCodes.Split(',')?.ToList();
            return moduleList?.Where(x => groupCodeList.Contains(x.EnumGroupCode))?.ToList();
        }

        public virtual CoditechApplicationSettingListModel GetCoditechApplicationSettingList(string applicationCodes)
        {
            CoditechApplicationSettingListModel listModel = new CoditechApplicationSettingListModel();
            listModel.CoditechApplicationSettingList = new List<CoditechApplicationSettingModel>();
            if (!string.IsNullOrEmpty(applicationCodes))
            {
                List<string> applicationCodeList = applicationCodes.Split(",").ToList();
                List<CoditechApplicationSetting> coditechApplicationSettingList = _coditechApplicationSettingRepository.Table.Where(x => applicationCodeList.Contains(x.ApplicationCode))?.ToList();
                foreach (CoditechApplicationSetting item in coditechApplicationSettingList)
                {
                    listModel.CoditechApplicationSettingList.Add(item.FromEntityToModel<CoditechApplicationSettingModel>());
                }
            }
            return listModel;
        }

        public virtual string GetDomainAPIKey(string requestKey)
        {
            string databaseApiDomainRequestKey = _coditechApplicationSettingRepository.Table.Where(x => x.ApplicationCode == "ApiDomainRequestKey")?.Select(y => y.ApplicationValue1)?.FirstOrDefault();
            if (!string.IsNullOrEmpty(requestKey) && !string.IsNullOrEmpty(databaseApiDomainRequestKey) && requestKey == databaseApiDomainRequestKey)
            {
                return $"Basic {HelperUtility.EncodeBase64($"{ApiSettings.CoditechApiDomainName}|{ApiSettings.CoditechApiDomainKey}")}";
            }
            return string.Empty;
        }

        //Send OTP.
        public virtual GeneralMessagesModel SendOTP(GeneralMessagesModel generalMessagesModel)
        {

            string token = HelperUtility.GenerateNumericCode();

            try
            {
                string emailTemplateCodeEnum = EmailTemplateCodeEnum.SendOTP.ToString();
                if (generalMessagesModel.IsSendOnEmail)
                {
                    GeneralEmailTemplateModel emailTemplateModel = GetEmailTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                    if (IsNotNull(emailTemplateModel) && !string.IsNullOrEmpty(emailTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.EmailAddress))
                    {
                        string messageText = ReplaceResentOTP(generalMessagesModel, emailTemplateModel.EmailTemplate, token);
                        _coditechEmail.SendEmail(generalMessagesModel.CentreCode, generalMessagesModel.EmailAddress, "", emailTemplateModel.Subject, messageText, true);
                        generalMessagesModel.OTP = token;
                    }
                }
                else if (generalMessagesModel.IsSendOnMobile)
                {
                    GeneralEmailTemplateModel smsTemplateModel = GetSMSTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                    if (IsNotNull(smsTemplateModel) && !string.IsNullOrEmpty(smsTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.MobileNumber))
                    {
                        string messageText = ReplaceResentOTP(generalMessagesModel, smsTemplateModel.EmailTemplate, token);
                        _coditechSMS.SendSMS(generalMessagesModel.CentreCode, messageText, $"{generalMessagesModel.CallingCode}{generalMessagesModel?.MobileNumber}");
                        generalMessagesModel.OTP = token;
                    }
                }
                else if (generalMessagesModel.IsSendOnWhatsapp)
                {
                    GeneralEmailTemplateModel whatsAppTemplateModel = GetWhatsAppTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                    if (IsNotNull(whatsAppTemplateModel) && !string.IsNullOrEmpty(whatsAppTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.MobileNumber))
                    {
                        string messageText = ReplaceResentOTP(generalMessagesModel, whatsAppTemplateModel.EmailTemplate, token);
                        _coditechWhatsApp.SendWhatsAppMessage(generalMessagesModel.CentreCode, messageText, $"{generalMessagesModel.CallingCode}{generalMessagesModel?.MobileNumber}");
                        generalMessagesModel.OTP = token;
                    }
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Error);
            }
            return generalMessagesModel;
        }
        public virtual AccPrequisiteModel GetAccountPrequisite(int balanceSheetId)
        {
            if (balanceSheetId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "BalanceId"));

            AccPrequisiteModel accPrequisiteModel = new AccPrequisiteModel();
            //Get CC from bs
            string centreCode = _accSetupBalanceSheetRepository.Table.Where(x => x.AccSetupBalanceSheetId == balanceSheetId).Select(x => x.CentreCode).FirstOrDefault();
            int generalCurrencyMasterId = _organisationCentrewiseAccountSetupRepository.Table.Where(x => x.CentreCode == centreCode).Select(x => x.GeneralCurrencyMasterId).FirstOrDefault();
            if (generalCurrencyMasterId > 0)
            {
                accPrequisiteModel.IsCurrencyAssociated = true;
                accPrequisiteModel.CurrencySymbol = _generalCurrencyMasterRepository.Table.Where(x => x.GeneralCurrencyMasterId == generalCurrencyMasterId).FirstOrDefault().CurrencySymbol;
                accPrequisiteModel.IsAssociated = true;
            }
            else
            {
                accPrequisiteModel.IsAssociated = false;
            }

            GeneralFinancialYear generalFinancialYear = _generalFinancialYearMasterRepository.Table.Where(x => x.CentreCode == centreCode && x.IsCurrentFinancialYear)?.FirstOrDefault();
            if (IsNotNull(generalFinancialYear))
            {
                accPrequisiteModel.IsFinacialYearAssociated = true;
                accPrequisiteModel.IsAssociated = true;
            }
            else
            {
                accPrequisiteModel.IsAssociated = false;
            }
            AccSetupGLBalanceSheet accSetupGLBalanceSheet = _accSetupGLBalanceSheetRepository.Table.Where(x => x.AccSetupBalanceSheetId == balanceSheetId)?.FirstOrDefault();

            if (IsNotNull(accSetupGLBalanceSheet))
            {
                accPrequisiteModel.IsAccGLBalanceSheetAssociated = true;
                accPrequisiteModel.IsAssociated = true;
            }
            else
            {
                accPrequisiteModel.IsAssociated = false;
            }
            return accPrequisiteModel;
        }
        #endregion

        #region Protected Method
        protected virtual string ReplaceResentOTP(GeneralMessagesModel generalMessagesModel, string emailTemplate, string resetPassToken)
        {
            string messageText = emailTemplate;
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.OTP, resetPassToken, messageText);
            return ReplaceEmailTemplateFooter(generalMessagesModel.CentreCode, messageText);
        }
        #endregion
    }
}
