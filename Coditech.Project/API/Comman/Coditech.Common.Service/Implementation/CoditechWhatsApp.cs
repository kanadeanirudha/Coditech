using Coditech.API.Data;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace Coditech.Common.Service
{
    public class CoditechWhatsApp : ICoditechWhatsApp
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechWhatsApp(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        /// <summary>
        /// Sends WhatsApp, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="phoneNumber">phoneNumber of the recipient.</param>
        /// <param name="WhatsAppText">The body of the WhatsApp.</param>
        public virtual void SendWhatsAppMessage(string centreCode, string WhatsAppText, string phoneNumber)
        {
            try
            {
                //Get Whats Up setting details.
                OrganisationCentrewiseWhatsAppSetting WhatsAppSettings = new CoditechRepository<OrganisationCentrewiseWhatsAppSetting>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode && x.IsWhatsAppSettingEnabled);

                if (HelperUtility.IsNotNull(WhatsAppSettings))
                {
                    GeneralWhatsAppProvider generalWhatsAppProvider = new CoditechRepository<GeneralWhatsAppProvider>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.GeneralWhatsAppProviderId == WhatsAppSettings.GeneralWhatsAppProviderId && x.IsActive);

                    if (generalWhatsAppProvider?.ProviderCode == "TWILIO_WhatsApp")
                    {
                        string Twilio_Account_SID = WhatsAppSettings.WhatsAppPortalAccountId;
                        string Twilio_Auth_TOKEN = WhatsAppSettings.AuthToken;

                        TwilioClient.Init(Twilio_Account_SID, Twilio_Auth_TOKEN);

                        var message = MessageResource.Create(
                            body: WhatsAppText,
                            from: new Twilio.Types.PhoneNumber($"whatsapp:{WhatsAppSettings.FromMobileNumber}"),
                             to: new Twilio.Types.PhoneNumber($"whatsapp:{phoneNumber}"));
                    }
                    else
                    {
                        _coditechLogging.LogMessage("WhatsApp sending disabled for this store.", CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Verbose);
                    }
                }
                else
                {
                    _coditechLogging.LogMessage("Please check WhatsApp Settings for .", CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Verbose);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage("WhatsApp sending to customer failed.", CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Error, ex);
            }
        }

    }
}


