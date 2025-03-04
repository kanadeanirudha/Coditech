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
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class OrganisationCentrewiseJoiningCodeService : IOrganisationCentrewiseJoiningCodeService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechEmail _coditechEmail;
        protected readonly ICoditechSMS _coditechSMS;
        protected readonly ICoditechWhatsApp _coditechWhatsApp;
        private readonly ICoditechRepository<OrganisationCentrewiseJoiningCode> _organisationCentrewiseJoiningCodeRepository;

        public OrganisationCentrewiseJoiningCodeService(ICoditechLogging coditechLogging, ICoditechEmail coditechEmail, ICoditechSMS coditechSMS, ICoditechWhatsApp coditechWhatsApp, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _coditechEmail = coditechEmail;
            _coditechSMS = coditechSMS;
            _coditechWhatsApp = coditechWhatsApp;
            _organisationCentrewiseJoiningCodeRepository = new CoditechRepository<OrganisationCentrewiseJoiningCode>(_serviceProvider.GetService<Coditech_Entities>());
        }
        public virtual OrganisationCentrewiseJoiningCodeListModel GetOrganisationCentrewiseJoiningCodeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            string selectedCentreCode = filters?.Find(x => string.Equals(x.FilterName, FilterKeys.SelectedCentreCode, StringComparison.CurrentCultureIgnoreCase))?.FilterValue;

            filters.RemoveAll(x => x.FilterName == FilterKeys.SelectedCentreCode);


            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<OrganisationCentrewiseJoiningCodeModel> objStoredProc = new CoditechViewRepository<OrganisationCentrewiseJoiningCodeModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@CentreCode", selectedCentreCode, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<OrganisationCentrewiseJoiningCodeModel> OrganisationCentrewiseJoiningCodeList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetOrganisationCentrewiseJoiningCodeList @CentreCode,@WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 5, out pageListModel.TotalRowCount)?.ToList();
            OrganisationCentrewiseJoiningCodeListModel listModel = new OrganisationCentrewiseJoiningCodeListModel();

            listModel.OrganisationCentrewiseJoiningCodeList = OrganisationCentrewiseJoiningCodeList?.Count > 0 ? OrganisationCentrewiseJoiningCodeList : new List<OrganisationCentrewiseJoiningCodeModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Organisation Centre wise Joining Code.
        public virtual OrganisationCentrewiseJoiningCodeModel CreateOrganisationCentrewiseJoiningCode(OrganisationCentrewiseJoiningCodeModel organisationCentrewiseJoiningCodeModel)
        {
            if (IsNull(organisationCentrewiseJoiningCodeModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            List<OrganisationCentrewiseJoiningCode> insertList = new List<OrganisationCentrewiseJoiningCode>();
            for (int i = 1; i <= organisationCentrewiseJoiningCodeModel.Quantity; i++)
            {

                insertList.Add(new OrganisationCentrewiseJoiningCode
                {
                    JoiningCode = GenerateAlphaNumericCode(ApiSettings.JoiningCodeLength),
                    Quantity = 1,
                    CentreCode = organisationCentrewiseJoiningCodeModel.CentreCode
                });
            }
            _organisationCentrewiseJoiningCodeRepository.Insert(insertList);
            return organisationCentrewiseJoiningCodeModel;
        }

        //OrganisationCentrewiseJoiningCodeSend
        public virtual OrganisationCentrewiseJoiningCodeModel OrganisationCentrewiseJoiningCodeSend(string joiningCode, string emailId, string mobileNumber)
        {
            if (string.IsNullOrEmpty(joiningCode))
                throw new CoditechException(ErrorCodes.NullModel, "Joining Code cannot be null or empty.");

           
            string centreCode = _organisationCentrewiseJoiningCodeRepository.Table.Where(x => x.JoiningCode == joiningCode)?.Select(x => x.CentreCode)?.FirstOrDefault();

            if (string.IsNullOrEmpty(centreCode))
                throw new CoditechException(ErrorCodes.InvalidData, "Invalid Joining Code.");

            OrganisationCentrewiseJoiningCodeModel responseModel = new OrganisationCentrewiseJoiningCodeModel()
            {
                JoiningCode = joiningCode,
                CentreCode = centreCode,
                EmailId = emailId,
                MobileNumber = mobileNumber
            };

            try
            {
                string messageBody = $"Your Joining Code is: {joiningCode}";
                // Send Email
                if (!string.IsNullOrEmpty(emailId))
                {
                    try
                    {
                        _coditechEmail.SendEmail(centreCode, emailId, "", "Joining Code", messageBody, true);
                    }
                    catch (Exception ex)
                    {
                        _coditechLogging.LogMessage($"Email sending failed: {ex.Message}", CoditechLoggingEnum.Components.EmailService.ToString(), TraceLevel.Error);
                    }
                }
                // Send SMS
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    try
                    {
                        _coditechSMS.SendSMS(centreCode, messageBody, mobileNumber);
                    }
                    catch (Exception ex)
                    {
                        _coditechLogging.LogMessage($"SMS sending failed: {ex.Message}", CoditechLoggingEnum.Components.SMSService.ToString(), TraceLevel.Error);
                    }
                }

                // Send WhatsApp
                if (!string.IsNullOrEmpty(mobileNumber))
                {
                    try
                    {
                        _coditechWhatsApp.SendWhatsAppMessage(centreCode, messageBody, mobileNumber);
                    }
                    catch (Exception ex)
                    {
                        _coditechLogging.LogMessage($"WhatsApp sending failed: {ex.Message}", CoditechLoggingEnum.Components.WhatsAppService.ToString(), TraceLevel.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.OrganisationCentrewiseJoiningCode.ToString(), TraceLevel.Error);
                throw new CoditechException(ErrorCodes.InvalidData, "Error occurred while sending Joining Code.");
            }

            return responseModel;
        }

        #region Protected Method

        #endregion
    }
}

