using Coditech.API.Data;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;

using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace Coditech.Common.Service
{
    public class CoditechSMS : ICoditechSMS
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechSMS(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        /// <summary>
        /// Sends SMS, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="phoneNumber">phoneNumber of the recipient.</param>
        /// <param name="smsText">The body of the sms.</param>
        public virtual void SendSMS(string centreCode, string smsText, string phoneNumber)
        {
            try
            {
                //Get SMS setting details.
                OrganisationCentrewiseSmsSetting smsSettings = new CoditechRepository<OrganisationCentrewiseSmsSetting>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode && x.IsSMSSettingEnabled);

                if (HelperUtility.IsNotNull(smsSettings))
                {
                    GeneralSmsProvider generalSmsProvider = new CoditechRepository<GeneralSmsProvider>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.GeneralSmsProviderId == smsSettings.GeneralSmsProviderId && x.IsActive);

                    if (generalSmsProvider?.ProviderCode == "TWILIO_SMS")
                    {
                        string Twilio_Account_SID = smsSettings.SmsPortalAccountId;
                        string Twilio_Auth_TOKEN = smsSettings.AuthToken;

                        TwilioClient.Init(Twilio_Account_SID, Twilio_Auth_TOKEN);

                        var message = MessageResource.Create(
                            from: new Twilio.Types.PhoneNumber(smsSettings.FromMobileNumber),
                            body: smsText,
                            to: new Twilio.Types.PhoneNumber(phoneNumber)
                        );
                    }
                    else
                    {
                        _coditechLogging.LogMessage("SMS sending disabled for this store.", CoditechLoggingEnum.Components.SMSService.ToString(), TraceLevel.Verbose);
                    }
                }
                else
                {
                    _coditechLogging.LogMessage("Please check SMS Settings for .", CoditechLoggingEnum.Components.SMSService.ToString(), TraceLevel.Verbose);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage("SMS sending to customer failed.", CoditechLoggingEnum.Components.SMSService.ToString(), TraceLevel.Error, null, ex);
            }
        }

    }
}


