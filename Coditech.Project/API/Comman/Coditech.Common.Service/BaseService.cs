using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text.RegularExpressions;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.Common.Service
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly ICoditechEmail _coditechEmail;
        public BaseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
            _coditechEmail = new CoditechEmail(serviceProvider);
        }
        protected virtual EmployeeDesignationMaster GetDesignationDetails(short designationId)
        {
            EmployeeDesignationMaster employeeDesignationMaster = new CoditechRepository<EmployeeDesignationMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(designationId);
            return employeeDesignationMaster;
        }

        protected virtual GeneralDepartmentMaster GetDepartmentDetails(short departmentId)
        {
            GeneralDepartmentMaster generalDepartmentMaster = new CoditechRepository<GeneralDepartmentMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(departmentId);
            return generalDepartmentMaster;
        }

        protected virtual OrganisationCentreMaster GetOrganisationCentreDetails(string centreCode)
        {
            OrganisationCentreMaster organisationCentreMaster = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.CentreCode == centreCode);
            return organisationCentreMaster;
        }

        protected virtual List<UserAccessibleCentreModel> OrganisationCentreList()
        {
            List<OrganisationCentreMaster> centreList = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.ToList();
            List<UserAccessibleCentreModel> organisationCentreList = new List<UserAccessibleCentreModel>();
            foreach (OrganisationCentreMaster item in centreList)
            {
                organisationCentreList.Add(new UserAccessibleCentreModel()
                {
                    CentreCode = item.CentreCode,
                    CentreName = item.CentreName,
                    ScopeIdentity = "Centre"
                });
            }

            return organisationCentreList;
        }

        protected virtual List<UserModuleMaster> GetAllActiveModuleList()
        {
            List<UserModuleMaster> userAllModuleList = new CoditechRepository<UserModuleMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.ModuleActiveFlag == true)?.OrderBy(y => y.ModuleSeqNumber)?.ToList();
            return userAllModuleList;
        }

        protected virtual List<UserMainMenuMaster> GetAllActiveMenuList(string moduleCode = null)
        {
            List<UserMainMenuMaster> userAllMenuList = new CoditechRepository<UserMainMenuMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.IsEnable == true && (x.ModuleCode == moduleCode || moduleCode == null))?.OrderBy(y => y.MenuDisplaySeqNo)?.ToList();
            return userAllMenuList;
        }

        protected virtual List<GeneralSystemGlobleSettingModel> GetSystemGlobleSettingList(string featureName = null)
        {
            List<GeneralSystemGlobleSettingMaster> settingList = new CoditechRepository<GeneralSystemGlobleSettingMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.FeatureName == featureName || featureName == null)?.ToList();
            List<GeneralSystemGlobleSettingModel> list = new List<GeneralSystemGlobleSettingModel>();
            foreach (GeneralSystemGlobleSettingMaster item in settingList)
            {
                list.Add(new GeneralSystemGlobleSettingModel()
                {
                    GeneralSystemGlobleSettingMasterId = item.GeneralSystemGlobleSettingMasterId,
                    FeatureName = item.FeatureName,
                    FeatureValue = item.FeatureValue
                });
            }
            return list;
        }

        protected virtual GeneralPersonModel GetGeneralPersonDetails(long personId)
        {
            GeneralPerson generalPerson = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>()).GetById(personId);
            return generalPerson.FromEntityToModel<GeneralPersonModel>();
        }


        protected virtual GeneralPersonModel GetGeneralPersonDetailsByEntityType(long entityId, string entityType)
        {
            long personId = 0;
            string centreCode = string.Empty;
            string personCode = string.Empty;
            short generalDepartmentMasterId = 0;
            bool isEntityActive = false;
            if (entityType == UserTypeEnum.Employee.ToString())
            {
                EmployeeMaster employeeMaster = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.FirstOrDefault(x => x.EmployeeId == entityId);
                if (IsNotNull(employeeMaster))
                {
                    personId = employeeMaster.PersonId;
                    centreCode = employeeMaster.CentreCode;
                    personCode = employeeMaster.PersonCode;
                    isEntityActive = employeeMaster.IsActive;
                    generalDepartmentMasterId = employeeMaster.GeneralDepartmentMasterId;
                }
            }
            return BindGeneralPersonInformation(personId, centreCode, personCode, generalDepartmentMasterId, isEntityActive);
        }

        protected virtual GeneralPersonModel BindGeneralPersonInformation(long personId, string centreCode, string personCode, short generalDepartmentMasterId, bool isEntityActive)
        {
            GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(personId);
            if (IsNotNull(generalPersonModel))
            {
                generalPersonModel.SelectedCentreCode = centreCode;
                generalPersonModel.SelectedDepartmentId = Convert.ToString(generalDepartmentMasterId);
                generalPersonModel.PersonCode = personCode;
                generalPersonModel.IsEntityActive = isEntityActive;
            }
            return generalPersonModel;
        }

        protected virtual string GetEnumCodeByEnumId(int generalEnumaratorId)
        {
            if (generalEnumaratorId == 0)
                return string.Empty;

            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(generalEnumaratorId);
            return generalEnumaratorMaster.EnumName;
        }

        protected virtual string GetEnumDisplayTextByEnumId(int generalEnumaratorId)
        {
            if (generalEnumaratorId == 0)
                return string.Empty;

            GeneralEnumaratorMaster generalEnumaratorMaster = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).GetById(generalEnumaratorId);
            return generalEnumaratorMaster.EnumDisplayText;
        }

        protected virtual int GetEnumIdByEnumCode(string enumCode, string groupCode)
        {
            int? generalEnumaratorGroupId = new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.EnumGroupCode == groupCode)?.Select(y => y.GeneralEnumaratorGroupId)?.FirstOrDefault();
            if (generalEnumaratorGroupId > 0)
            {
                int? generalEnumaratorId = new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.EnumName == enumCode && x.GeneralEnumaratorGroupId == generalEnumaratorGroupId)?.Select(y => y.GeneralEnumaratorId)?.FirstOrDefault();
                return generalEnumaratorId ?? 0;
            }
            return 0;
        }

        protected virtual string GenerateRegistrationCode(string enumCode, string centreCode)
        {
            string registrationCode = string.Empty;
            int generalEnumaratorId = GetEnumIdByEnumCode(enumCode, GeneralEnumaratorGroupCodeEnum.GeneralRunningNumberFor.ToString());
            CoditechRepository<GeneralRunningNumbers> _generalRunningNumbersRepository = new CoditechRepository<GeneralRunningNumbers>(_serviceProvider.GetService<Coditech_Entities>());
            GeneralRunningNumbers generalRunningNumbers = _generalRunningNumbersRepository.Table.Where(x => x.KeyFieldEnumId == generalEnumaratorId && x.IsActive && !x.IsRowLock && x.CentreCode == centreCode)?.FirstOrDefault();
            if (generalRunningNumbers != null)
            {
                DateTime dateTime = DateTime.Now;
                generalRunningNumbers.CurrentSequnce = (generalRunningNumbers.CurrentSequnce + 1);
                registrationCode = generalRunningNumbers.DisplayFormat?.ToLower();
                registrationCode = registrationCode.Replace("<centrecode>", centreCode);
                registrationCode = registrationCode.Replace("<separator>", generalRunningNumbers.Separator);
                registrationCode = registrationCode.Replace("<prefix>", generalRunningNumbers.Prefix);
                registrationCode = registrationCode.Replace("<yyyy>", dateTime.Year.ToString());
                registrationCode = registrationCode.Replace("<yy>", dateTime.Year.ToString().Substring(2));
                registrationCode = registrationCode.Replace("<mm>", dateTime.Month.ToString());
                registrationCode = registrationCode.Replace("<dd>", dateTime.Day.ToString());
                registrationCode = registrationCode.Replace("<hh>", dateTime.Hour.ToString());
                registrationCode = registrationCode.Replace("<min>", dateTime.Minute.ToString());
                registrationCode = registrationCode.Replace("<sec>", dateTime.Second.ToString());
                registrationCode = registrationCode.Replace("<currentsequence>", (generalRunningNumbers.CurrentSequnce).ToString());
                _generalRunningNumbersRepository.Update(generalRunningNumbers);
            }
            return registrationCode;
        }

        protected virtual InventoryGeneralItemLineDetailsModel GetInventoryGeneralItemLineDetails(long inventoryGeneralItemLineId)
        {
            CoditechRepository<InventoryGeneralItemMaster> _inventoryGeneralItemMasterRepository = new CoditechRepository<InventoryGeneralItemMaster>(_serviceProvider.GetService<Coditech_Entities>());
            CoditechRepository<InventoryGeneralItemLine> _inventoryGeneralItemLineRepository = new CoditechRepository<InventoryGeneralItemLine>(_serviceProvider.GetService<Coditech_Entities>());

            InventoryGeneralItemLineDetailsModel itemLineDetails = null;
            itemLineDetails = (from a in _inventoryGeneralItemMasterRepository.Table
                               join b in _inventoryGeneralItemLineRepository.Table
                               on a.InventoryGeneralItemMasterId equals b.InventoryGeneralItemMasterId
                               where (b.InventoryGeneralItemLineId == inventoryGeneralItemLineId)
                               select new InventoryGeneralItemLineDetailsModel()
                               {
                                   InventoryGeneralItemMasterId = a.InventoryGeneralItemMasterId,
                                   InventoryGeneralItemLineId = b.InventoryGeneralItemLineId,
                                   ItemNumber = a.ItemNumber,
                                   ItemDescription = a.ItemDescription,
                                   GeneralTaxGroupMasterId = a.GeneralTaxGroupMasterId,
                                   HSNSACCode = a.HSNSACCode,
                                   ItemName = b.ItemName,
                                   SKU = b.SKU,

                               })?.FirstOrDefault();
            return itemLineDetails;
        }
        protected virtual decimal ItemLineTaxAmount(byte generalTaxGroupMasterId, decimal amount)
        {
            CoditechRepository<GeneralTaxGroupMasterDetails> _generalTaxGroupMasterDetailsRepository = new CoditechRepository<GeneralTaxGroupMasterDetails>(_serviceProvider.GetService<Coditech_Entities>());
            CoditechRepository<GeneralTaxMaster> _generalTaxMasterRepository = new CoditechRepository<GeneralTaxMaster>(_serviceProvider.GetService<Coditech_Entities>());
            decimal? taxInPercentage = (from a in _generalTaxGroupMasterDetailsRepository.Table
                                        join b in _generalTaxMasterRepository.Table
                                        on a.GeneralTaxMasterId equals b.GeneralTaxMasterId
                                        where (a.GeneralTaxGroupMasterId == generalTaxGroupMasterId)
                                        select b.TaxRate).Sum();

            return Convert.ToDecimal(amount * (taxInPercentage / 100));
        }

        protected virtual void ActiveInActiveUserLogin(bool flag, long entityId, string userType)
        {
            CoditechRepository<UserMaster> _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            UserMaster userMaster = _userMasterRepository.Table.Where(x => x.EntityId == entityId && x.UserType == userType)?.FirstOrDefault();
            if (userMaster != null && userMaster.IsActive != flag)
            {
                userMaster.IsActive = flag;
                _userMasterRepository.Update(userMaster);
            }
        }

        protected virtual int GetOrganisationCentreMasterIdByCentreCode(string centreCode)
        {
            CoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            int organisationCentreMasterId = _organisationCentreMasterRepository.Table.Where(x => x.CentreCode == centreCode).Select(y => y.OrganisationCentreMasterId).FirstOrDefault();
            return organisationCentreMasterId;
        }

        protected virtual string GetOrganisationCentreCodeByOrganisationCentreMasterId(int organisationCentreMasterId)
        {
            CoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            string CentreCode = _organisationCentreMasterRepository.Table.Where(x => x.OrganisationCentreMasterId == organisationCentreMasterId).Select(y => y.CentreCode).FirstOrDefault();
            return CentreCode;
        }

        protected virtual string GetOrganisationCentreNameByCentreCode(string centreCode)
        {
            CoditechRepository<OrganisationCentreMaster> _organisationCentreMasterRepository = new CoditechRepository<OrganisationCentreMaster>(_serviceProvider.GetService<Coditech_Entities>());
            string centreName = _organisationCentreMasterRepository.Table.Where(x => x.CentreCode == centreCode).Select(y => y.CentreName).FirstOrDefault();
            return centreName;
        }

        protected virtual GeneralEmailTemplateModel GetEmailTemplateByCode(string centreCode, string emailTemplateByCode)
        {
            return GetGeneralEmailTemplateByCode(centreCode, emailTemplateByCode, false, false);
        }

        protected virtual GeneralEmailTemplateModel GetSMSTemplateByCode(string centreCode, string emailTemplateByCode)
        {
            return GetGeneralEmailTemplateByCode(centreCode, emailTemplateByCode, true, false);
        }

        protected virtual GeneralEmailTemplateModel GetWhatsAppTemplateByCode(string centreCode, string emailTemplateByCode)
        {
            return GetGeneralEmailTemplateByCode(centreCode, emailTemplateByCode, false, true);
        }

        protected virtual GeneralEmailTemplateModel GetGeneralEmailTemplateByCode(string centreCode, string emailTemplateByCode, bool isSmsTemplate, bool isWhatsAppTemplate)
        {
            GeneralEmailTemplateModel emailTemplateModel = new GeneralEmailTemplateModel();
            if (!string.IsNullOrEmpty(centreCode))
            {
                OrganisationCentrewiseEmailTemplate organisationCentrewiseEmailTemplate = new CoditechRepository<OrganisationCentrewiseEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.CentreCode == centreCode && x.EmailTemplateCode == emailTemplateByCode && x.IsActive && x.IsSmsTemplate == isSmsTemplate && x.IsWhatsAppTemplate == isWhatsAppTemplate)?.FirstOrDefault();
                if (IsNotNull(organisationCentrewiseEmailTemplate))
                {
                    emailTemplateModel.EmailTemplateCode = organisationCentrewiseEmailTemplate.EmailTemplateCode;
                    emailTemplateModel.EmailTemplate = organisationCentrewiseEmailTemplate.EmailTemplate;
                    emailTemplateModel.Subject = organisationCentrewiseEmailTemplate.Subject;
                    emailTemplateModel.IsSmsTemplate = organisationCentrewiseEmailTemplate.IsSmsTemplate;
                    emailTemplateModel.IsWhatsAppTemplate = organisationCentrewiseEmailTemplate.IsWhatsAppTemplate;
                }
                else
                {
                    BindEmailTemplateModel(emailTemplateByCode, isSmsTemplate, emailTemplateModel);
                }
            }
            else
            {
                BindEmailTemplateModel(emailTemplateByCode, isSmsTemplate, emailTemplateModel);
            }
            return emailTemplateModel;
        }

        protected virtual void BindEmailTemplateModel(string emailTemplateByCode, bool isSmsTemplate, GeneralEmailTemplateModel emailTemplateModel)
        {
            GeneralEmailTemplate generalEmailTemplate = new CoditechRepository<GeneralEmailTemplate>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.EmailTemplateCode == emailTemplateByCode && x.IsActive && x.IsSmsTemplate == isSmsTemplate)?.FirstOrDefault();
            if (IsNotNull(generalEmailTemplate))
            {
                emailTemplateModel.EmailTemplateCode = generalEmailTemplate.EmailTemplateCode;
                emailTemplateModel.EmailTemplate = generalEmailTemplate.EmailTemplate;
                emailTemplateModel.Subject = generalEmailTemplate.Subject;
                emailTemplateModel.IsSmsTemplate = generalEmailTemplate.IsSmsTemplate;
                emailTemplateModel.IsWhatsAppTemplate = generalEmailTemplate.IsWhatsAppTemplate;
            }
        }

        protected virtual List<GeneralEnumaratorModel> BindEnumarator()
        {
            List<GeneralEnumaratorModel> generalEnumaratorList = new List<GeneralEnumaratorModel>();
            generalEnumaratorList = (from generalEnumarator in new CoditechRepository<GeneralEnumaratorMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table
                                     join generalEnumaratorGroup in new CoditechRepository<GeneralEnumaratorGroup>(_serviceProvider.GetService<Coditech_Entities>()).Table on generalEnumarator.GeneralEnumaratorGroupId equals generalEnumaratorGroup.GeneralEnumaratorGroupId
                                     where generalEnumarator.IsActive
                                     select new GeneralEnumaratorModel
                                     {
                                         GeneralEnumaratorGroupId = generalEnumaratorGroup.GeneralEnumaratorGroupId,
                                         EnumGroupCode = generalEnumaratorGroup.EnumGroupCode,
                                         GeneralEnumaratorId = generalEnumarator.GeneralEnumaratorId,
                                         EnumName = generalEnumarator.EnumName,
                                         EnumDisplayText = generalEnumarator.EnumDisplayText,
                                         EnumValue = generalEnumarator.EnumValue,
                                         SequenceNumber = generalEnumarator.SequenceNumber,
                                     })?.ToList();
            return generalEnumaratorList;
        }

        protected virtual string GetMediaUrl()
        {
            string url = new CoditechRepository<MediaConfiguration>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.IsActive)?.FirstOrDefault().URL;
            return url;
        }

        //Method to replace token with message text.
        public static string ReplaceTokenWithMessageText(string key, string replaceValue, string resourceText)
        {
            Regex rgx = new Regex(key, RegexOptions.IgnoreCase);
            return rgx.Replace(resourceText, string.IsNullOrEmpty(replaceValue) ? string.Empty : replaceValue);
        }

        //Get Image Path
        protected virtual string GetImagePath(long mediaId)
        {
            string imagePath = string.Empty;
            if (mediaId > 0)
            {
                MediaDetail mediaDetail = new CoditechRepository<MediaDetail>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.MediaId == mediaId)?.FirstOrDefault();
                if (mediaDetail != null)
                {
                    imagePath = $"{GetMediaUrl()}{mediaDetail.Path}";
                }
            }
            return imagePath;
        }

        protected virtual List<CoditechApplicationSetting> CoditechApplicationSetting()
        {
            return new CoditechRepository<CoditechApplicationSetting>(_serviceProvider.GetService<Coditech_Entities>())?.Table?.ToList();
        }

        protected virtual string ReplaceEmailTemplateFooter(string centreCode, string messageText)
        {
            if (!string.IsNullOrEmpty(centreCode))
            {
                OrganisationCentreMaster organisationCentreMaster = GetOrganisationCentreDetails(centreCode);
                string city = new CoditechRepository<GeneralCityMaster>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.GeneralCityMasterId == organisationCentreMaster.GeneralCityMasterId).FirstOrDefault().CityName;
                string centreAddress = $"{organisationCentreMaster.CentreAddress}<br>{city}-{organisationCentreMaster.Pincode}";
                messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.CentreName, organisationCentreMaster.CentreName, messageText);
                messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.CentreAddress, centreAddress, messageText);
                messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.CentreContactNumber, organisationCentreMaster.PhoneNumberOffice, messageText);
            }
            return messageText;
        }

        protected virtual GeneralPerson InsertGeneralPersonData(GeneralPersonModel generalPersonModel)
        {
            generalPersonModel.FirstName = generalPersonModel.FirstName.ToFirstLetterCapital();
            generalPersonModel.LastName = generalPersonModel.LastName.ToFirstLetterCapital();
            generalPersonModel.MiddleName = generalPersonModel.MiddleName.ToFirstLetterCapital();
            GeneralPerson generalPerson = generalPersonModel.FromModelToEntity<GeneralPerson>();

            // Create new Person and return it.
            GeneralPerson personData = new CoditechRepository<GeneralPerson>(_serviceProvider.GetService<Coditech_Entities>()).Insert(generalPerson);
            return personData;
        }

        protected virtual long InsertEmployee(GeneralPersonModel generalPersonModel, List<GeneralSystemGlobleSettingModel> settingMasterList, bool isActive)
        {
            generalPersonModel.PersonCode = GenerateRegistrationCode(GeneralRunningNumberForEnum.EmployeeRegistration.ToString(), generalPersonModel.SelectedCentreCode);
            EmployeeMaster employeeMaster = new EmployeeMaster()
            {
                PersonId = generalPersonModel.PersonId,
                PersonCode = generalPersonModel.PersonCode,
                UserType = generalPersonModel.UserType,
                CentreCode = generalPersonModel.SelectedCentreCode,
                GeneralDepartmentMasterId = Convert.ToInt16(generalPersonModel.SelectedDepartmentId),
                EmployeeDesignationMasterId = Convert.ToInt16(generalPersonModel.EmployeeDesignationMasterId),
                IsActive = isActive
            };
            employeeMaster = new CoditechRepository<EmployeeMaster>(_serviceProvider.GetService<Coditech_Entities>()).Insert(employeeMaster);
            //Check Is Employee need to Login
            if (employeeMaster?.EmployeeId > 0)
            {
                generalPersonModel.EntityId = employeeMaster.EmployeeId;
                EmployeeService employeeService = new EmployeeService()
                {
                    EmployeeId = employeeMaster.EmployeeId,
                    EmployeeCode = generalPersonModel.PersonCode,
                    IsCurrentPosition = true,
                    EmployeeDesignationMasterId = generalPersonModel.EmployeeDesignationMasterId,
                    JoiningDate = DateTime.Now,
                    EmployeeStageEnumId = GetEnumIdByEnumCode("Joining", GeneralEnumaratorGroupCodeEnum.EmployeeStage.ToString())
                };
                new CoditechRepository<EmployeeService>(_serviceProvider.GetService<Coditech_Entities>()).Insert(employeeService);
                if (settingMasterList?.FirstOrDefault(x => x.FeatureName.Equals(GeneralSystemGlobleSettingEnum.IsEmployeeLogin.ToString(), StringComparison.InvariantCultureIgnoreCase)).FeatureValue == "1")
                {
                    InsertUserMasterDetails(generalPersonModel, employeeMaster.EmployeeId, isActive);
                    try
                    {
                        GeneralEmailTemplateModel emailTemplateModel = GetEmailTemplateByCode(employeeMaster.CentreCode, EmailTemplateCodeEnum.EmployeeRegistration.ToString());
                        if (IsNotNull(emailTemplateModel) && !string.IsNullOrEmpty(emailTemplateModel?.EmailTemplateCode) && !string.IsNullOrEmpty(generalPersonModel?.EmailId))
                        {
                            string subject = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.CentreName, generalPersonModel.CentreName, emailTemplateModel.Subject);
                            string messageText = ReplaceEmployeeEmailTemplate(generalPersonModel, emailTemplateModel.EmailTemplate);
                            _coditechEmail.SendEmail(employeeMaster.CentreCode, generalPersonModel.EmailId, "", subject, messageText, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                    }
                }
            }
            return employeeMaster.EmployeeId;
        }

        protected virtual void InsertUserMasterDetails(GeneralPersonModel generalPersonModel, long entityId, bool isActive)
        {
            string userNameBasedOn = new CoditechRepository<OrganisationCentrewiseUserNameRegistration>(_serviceProvider.GetService<Coditech_Entities>()).Table.Where(x => x.CentreCode == generalPersonModel.SelectedCentreCode && x.UserType.ToLower() == generalPersonModel.UserType.ToLower())?.Select(y => y.UserNameBasedOn)?.FirstOrDefault();
            UserMaster userMaster = generalPersonModel.FromModelToEntity<UserMaster>();
            userMaster.EntityId = entityId;
            userMaster.IsAcceptedTermsAndConditions = true;
            userMaster.IsActive = isActive;
            userMaster.Password = MD5Hash(userMaster.Password);
            if (string.IsNullOrEmpty(userNameBasedOn))
            {
                userMaster.UserName = generalPersonModel.PersonCode;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.EmailId.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.EmailId;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.MobileNumber.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.MobileNumber;
            }
            else if (userNameBasedOn.Equals(UserNameRegistrationTypeEnum.PersonCode.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                userMaster.UserName = generalPersonModel.PersonCode;
            }
            generalPersonModel.UserName = userMaster.UserName;
            generalPersonModel.EntityId = entityId;
            userMaster = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>()).Insert(userMaster);
        }

        protected virtual string ReplaceEmployeeEmailTemplate(GeneralPersonModel generalPersonModel, string emailTemplate)
        {
            List<CoditechApplicationSetting> coditechApplicationSettingList = CoditechApplicationSetting();
            string centreUrl = coditechApplicationSettingList.First(x => x.ApplicationCode == "CoditechAdminRootUri")?.ApplicationValue1;
            string messageText = emailTemplate;
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.FirstName, generalPersonModel.FirstName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.LastName, generalPersonModel.LastName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.PersonCode, generalPersonModel.PersonCode, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.Designation, GetDesignationDetails(generalPersonModel.EmployeeDesignationMasterId).Description, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.DepartmentName, GetDepartmentDetails(Convert.ToInt16(generalPersonModel.SelectedDepartmentId)).DepartmentName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.CentreUrl, centreUrl, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.EmployeeUsername, generalPersonModel.UserName, messageText);
            messageText = ReplaceTokenWithMessageText(EmailTemplateTokenConstant.TemporaryPassword, generalPersonModel.Password, messageText);
            return ReplaceEmailTemplateFooter(generalPersonModel.SelectedCentreCode, messageText);
        }
    }
}