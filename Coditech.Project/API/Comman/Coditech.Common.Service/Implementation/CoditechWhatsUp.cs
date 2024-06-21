using Coditech.API.Data;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;

using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace Coditech.Common.Service
{
    public class CoditechWhatsUp : ICoditechWhatsUp
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechWhatsUp(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        /// <summary>
        /// Sends WhatsUp, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="phoneNumber">phoneNumber of the recipient.</param>
        /// <param name="WhatsUpText">The body of the WhatsUp.</param>
        public virtual void SendWhatsUpMessage(string centreCode, string whatsUpText, string phoneNumber)
        {
            try
            {
                //Get Whats Up setting details.
                OrganisationCentrewiseWhatsUpSetting whatsUpSettings = new CoditechRepository<OrganisationCentrewiseWhatsUpSetting>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode && x.IsWhatsUpSettingEnabled);

                if (HelperUtility.IsNotNull(whatsUpSettings))
                {
                    GeneralWhatsUpProvider generalWhatsUpProvider = new CoditechRepository<GeneralWhatsUpProvider>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.GeneralWhatsUpProviderId == whatsUpSettings.GeneralWhatsUpProviderId && x.IsActive);

                    if (generalWhatsUpProvider?.ProviderCode == "TWILIO_WhatsUp")
                    {
                        string Twilio_Account_SID = whatsUpSettings.WhatsUpPortalAccountId;
                        string Twilio_Auth_TOKEN = whatsUpSettings.AuthToken;

                        TwilioClient.Init(Twilio_Account_SID, Twilio_Auth_TOKEN);
                        var message = MessageResource.CreateAsync(
                            body: whatsUpText,
                            from: new Twilio.Types.PhoneNumber($"whatsapp:{whatsUpSettings.FromMobileNumber}"),
                            to: new Twilio.Types.PhoneNumber($"whatsapp:{phoneNumber}"));
                    }
                    else
                    {
                        _coditechLogging.LogMessage("WhatsUp sending disabled for this store.", CoditechLoggingEnum.Components.WhatsUpService.ToString(), TraceLevel.Verbose);
                    }
                }
                else
                {
                    _coditechLogging.LogMessage("Please check WhatsUp Settings for .", CoditechLoggingEnum.Components.WhatsUpService.ToString(), TraceLevel.Verbose);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage("WhatsUp sending to customer failed.", CoditechLoggingEnum.Components.WhatsUpService.ToString(), TraceLevel.Error, null, ex);
            }
        }

    }
}


