using Coditech.API.Data;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
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
        public GeneralCommonService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider, ICoditechEmail coditechEmail, ICoditechSMS coditechSMS, ICoditechWhatsApp coditechWhatsApp) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechEmail = coditechEmail;
            _coditechSMS = coditechSMS;
            _coditechWhatsApp = coditechWhatsApp;
            _coditechApplicationSettingRepository = new CoditechRepository<CoditechApplicationSetting>(_serviceProvider.GetService<Coditech_Entities>());
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

            generalMessagesModel.OTP = HelperUtility.GenerateOTP();

            try
            {
                string emailTemplateCodeEnum = EmailTemplateCodeEnum.SendOTP.ToString();
                GeneralEmailTemplateModel emailTemplateModel = GetEmailTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                if (IsNotNull(emailTemplateModel) && !string.IsNullOrEmpty(emailTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.EmailAddress))
                {
                    string messageText = ReplaceResentOTP(generalMessagesModel, emailTemplateModel.EmailTemplate, generalMessagesModel.OTP);
                    _coditechEmail.SendEmail(generalMessagesModel.CentreCode, generalMessagesModel.EmailAddress, "", emailTemplateModel.Subject, messageText, true);
                }
                GeneralEmailTemplateModel smsTemplateModel = GetSMSTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                if (IsNotNull(smsTemplateModel) && !string.IsNullOrEmpty(smsTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.MobileNumber))
                {
                    string messageText = ReplaceResentOTP(generalMessagesModel, smsTemplateModel.EmailTemplate, generalMessagesModel.OTP);
                    _coditechSMS.SendSMS(generalMessagesModel.CentreCode, messageText, $"{generalMessagesModel.CallingCode}{generalMessagesModel?.MobileNumber}");
                }
                GeneralEmailTemplateModel whatsAppTemplateModel = GetWhatsAppTemplateByCode(generalMessagesModel?.CentreCode, emailTemplateCodeEnum);
                if (IsNotNull(whatsAppTemplateModel) && !string.IsNullOrEmpty(whatsAppTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalMessagesModel?.MobileNumber))
                {
                    string messageText = ReplaceResentOTP(generalMessagesModel, whatsAppTemplateModel.EmailTemplate, generalMessagesModel.OTP);
                    _coditechWhatsApp.SendWhatsAppMessage(generalMessagesModel.CentreCode, messageText, $"{generalMessagesModel.CallingCode}{generalMessagesModel?.MobileNumber}");
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralMessages.ToString(), TraceLevel.Error);
            }
            return generalMessagesModel;
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
