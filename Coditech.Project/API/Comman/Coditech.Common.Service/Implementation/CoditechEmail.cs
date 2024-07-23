using Coditech.API.Data;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;

using MimeKit;

using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;


namespace Coditech.Common.Service
{
    public class CoditechEmail : ICoditechEmail
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        public CoditechEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        /// <summary>
        /// Sends email using SMTP, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="To">Email address of the recipient.</param>
        /// <param name="From">Email address of the sender.</param>
        /// <param name="CC">carbon copy address of the sender.</param>
        /// <param name="BCC">Blind carbon copy email address.</param>
        /// <param name="Subject">The subject line of the email.</param>
        /// <param name="Body">The body of the email.</param>
        /// <param name="IsBodyHtml">Set to True to send this email in HTML format.</param>
        public virtual void SendEmail(string centreCode, string to, string from, string cc, string bcc, string subject, string body, bool isHtmlEmail, string attachedPath = "")
        {
            try
            {
                //Get smtp setting details.
                OrganisationCentrewiseSmtpSetting smtpSettings = new CoditechRepository<OrganisationCentrewiseSmtpSetting>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode);

                //Assign from email address and bcc if smtp details is not null.
                if (HelperUtility.IsNotNull(smtpSettings))
                {
                    //"Disable all emails" flag should only affect the procedure for any particular store. If the call to this method has no centreCode, It should not affect anything.
                    if (IsEmailSendingEnabled(smtpSettings))
                    {
                        string fromEmailAddress = !string.IsNullOrEmpty(from) && IsValidEmail(from) ? from : smtpSettings.FromEmailAddress;
                        string fromAddressPrefix = string.IsNullOrEmpty(smtpSettings.FromDisplayName) ? string.Empty : smtpSettings.FromDisplayName;
                        MimeMessage message = SetEmailMessage(fromEmailAddress, to, subject, body, cc, bcc, isHtmlEmail, fromAddressPrefix, string.IsNullOrEmpty(attachedPath) ? null : new List<string>() { attachedPath });
                        SendSMTPEmail(message, smtpSettings);
                    }
                    else
                    {
                        _coditechLogging.LogMessage("Email sending disabled for this store.", CoditechLoggingEnum.Components.EmailService.ToString(), TraceLevel.Verbose);
                    }
                }
                else
                {
                    _coditechLogging.LogMessage("Please check smtp Settings for .", CoditechLoggingEnum.Components.EmailService.ToString(), TraceLevel.Verbose);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage("Mail sending to customer failed.", CoditechLoggingEnum.Components.EmailService.ToString(), TraceLevel.Error, null, ex);
            }
        }

        /// <summary>
        /// Sends email using SMTP, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="To">Email address of the recipient.</param>
        /// <param name="From">Email address of the sender.</param>
        /// <param name="CC">carbon copy address of the sender.</param>
        /// <param name="BCC">Blind carbon copy email address.</param>
        /// <param name="Subject">The subject line of the email.</param>
        /// <param name="Body">The body of the email.</param>
        public virtual void SendEmail(string centreCode, string to, string from, string cc, string bcc, string subject, string body)
        {
            SendEmail(centreCode, to, from, cc, bcc, subject, body, false, string.Empty);
        }

        /// <summary>
        /// Sends email using SMTP, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="To">Email address of the recipient.</param>
        /// <param name="From">Email address of the sender.</param>
        /// <param name="Subject">The subject line of the email.</param>
        /// <param name="Body">The body of the email.</param>
        /// <param name="IsBodyHtml">Set to True to send this email in HTML format.</param>
        public virtual void SendEmail(string centreCode, string to, string from, string subject, string body, bool isBodyHtml)
        {
            SendEmail(centreCode, to, from, string.Empty, string.Empty, subject, body, isBodyHtml, string.Empty);
        }

        /// <summary>
        /// Sends email using SMTP, Uses default network credentials
        /// </summary>
        /// <param name="centreCode">Current centreCode.</param>
        /// <param name="To">Email address of the recipient.</param>
        /// <param name="From">Email address of the sender.</param>
        /// <param name="Subject">The subject line of the email.</param>
        /// <param name="Body">The body of the email.</param>
        public virtual void SendEmail(string centreCode, string to, string from, string subject, string body)
        {
            SendEmail(centreCode, to, from, string.Empty, string.Empty, subject, body, false, string.Empty);
        }

        // This method is responsible to send emails using MailKit SMTPClient.
        protected virtual string SendSMTPEmail(MimeMessage message, OrganisationCentrewiseSmtpSetting smtpSettings)
        {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    if (!(Convert.ToBoolean(smtpSettings.IsEnableSsl)))
                    {
                        client.CheckCertificateRevocation = false;
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    }

                    if (Convert.ToBoolean(smtpSettings.IsEnableSsl))
                        client.Connect(smtpSettings.ServerName, smtpSettings.Port, (bool)smtpSettings.IsEnableSsl);
                    else
                        client.Connect(smtpSettings.ServerName, smtpSettings.Port);

                    //Note: only needed if the SMTP server requires authentication
                    if (!string.IsNullOrEmpty(smtpSettings.UserName) && !string.IsNullOrEmpty(smtpSettings.Password))
                        client.Authenticate(smtpSettings.UserName, smtpSettings.Password);

                    client.Send(message);
                    client.Disconnect(true);
                    return "SUCCESS";
                }
        }

        protected virtual MimeMessage SetEmailMessage(string from, string to, string subject, string body, string cc, string bcc, bool isHtmlEmail, string fromAddressPrefix, List<string> attachments)
        {
            char[] validSeperators = new char[] { ',', ':', ';' };
            string fromEmail = string.Empty;
            foreach (char seperator in validSeperators)
            {
                if (!string.IsNullOrEmpty(from) && from.Contains(seperator.ToString()))
                {
                    fromEmail = from.Split(seperator)[0];
                    break;
                }
            }
            from = !string.IsNullOrEmpty(fromEmail) ? fromEmail : from;
            bcc = bcc?.TrimEnd(',');
            cc = cc?.TrimEnd(',');

            //Email email = new Email(from, to, subject, body, cc, bcc, isHtmlEmail, fromAddressPrefix);

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromAddressPrefix, from));
            if (!string.IsNullOrEmpty(to))
            {
                message.To.AddRange(GetEmailList(to));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                message.Cc.AddRange(GetEmailList(cc));
            }
            if (!string.IsNullOrEmpty(bcc))
            {
                if (Regex.IsMatch(bcc, "^((\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*)\\s*[,]{0,1}\\s*)+$", RegexOptions.IgnoreCase))
                {
                    message.Bcc.AddRange(GetEmailList(bcc));
                }
            }
            message.Subject = subject;
            var builder = new BodyBuilder();

            if (isHtmlEmail)
                builder.HtmlBody = body;
            else
                builder.TextBody = body;

            if (attachments?.Count > 0)
            {
                foreach (string attach in attachments)
                    builder.Attachments.Add(attach);
            }

            message.Body = builder.ToMessageBody();


            return message;
        }

        protected virtual InternetAddressList GetEmailList(string address)
        {
            InternetAddressList addressList = new InternetAddressList();
            List<string> emailAddresses = address?.Split(',')?.Distinct()?.ToList();
            foreach (string email in emailAddresses)
            {
                if (!string.IsNullOrEmpty(email))
                    addressList.Add(new MailboxAddress(Encoding.UTF8, null, email));
            }
            return addressList;
        }
        /// <summary>
        /// Checks whether the email sending for this centre is enabled or not.
        /// </summary>
        /// <param name="smtpSettings"></param>
        /// <returns></returns>
        protected virtual bool IsEmailSendingEnabled(OrganisationCentrewiseSmtpSetting smtpSettings)
        {
            return !smtpSettings.DisableAllEmails;
        }

        //Checks for valid email address
        protected virtual bool IsValidEmail(string from)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,63}){1,3})$");
            bool isValid = false;
            try
            {
                isValid = regex.Match(from).Success;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage("Email Address is not valid.", CoditechLoggingEnum.Components.EmailService.ToString(), TraceLevel.Error, null, ex);
            }
            return isValid;
        }
    }
}


