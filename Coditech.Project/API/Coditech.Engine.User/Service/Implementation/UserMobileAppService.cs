using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Common.Service;
using Coditech.Resources;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Service
{
    public class UserMobileAppService : BaseService, IUserMobileAppService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<UserMaster> _userMasterRepository;
        private readonly ICoditechRepository<GymMemberDetails> _gymMemberDetailsRepository;
        public UserMobileAppService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _userMasterRepository = new CoditechRepository<UserMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _gymMemberDetailsRepository = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual UserMobileAppModel Login(UserLoginModel userLoginModel)
        {
            if (IsNull(userLoginModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            userLoginModel.Password = MD5Hash(userLoginModel.Password);
            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserName == userLoginModel.UserName && x.Password == userLoginModel.Password);

            if (IsNull(userMasterData))
                throw new CoditechException(ErrorCodes.NotFound, null);
            else if (!userMasterData.IsActive)
                throw new CoditechException(ErrorCodes.ContactAdministrator, null);

            UserMobileAppModel userModel = userMasterData?.FromEntityToModel<UserMobileAppModel>();
            GeneralPersonModel generalPersonModel = GetGeneralPersonDetails(userModel.EntityId);
            userModel.PersonTitle = generalPersonModel.PersonTitle;
            userModel.DateOfBirth = generalPersonModel.DateOfBirth;
            userModel.Gender = GetEnumDisplayTextByEnumId(generalPersonModel.GenderEnumId);
            userModel.PhoneNumber = generalPersonModel.PhoneNumber;
            userModel.MobileNumber = generalPersonModel.MobileNumber;
            userModel.EmergencyContact = generalPersonModel.EmergencyContact;
            userModel.MaritalStatus = generalPersonModel.MaritalStatus;
            userModel.BirthMark = generalPersonModel.BirthMark;
            userModel.GeneralOccupationMasterId = generalPersonModel.GeneralOccupationMasterId;
            userModel.AnniversaryDate = generalPersonModel.AnniversaryDate;

            GymMemberDetails gymMemberDetails = _gymMemberDetailsRepository.Table.FirstOrDefault(x => x.GymMemberDetailId == userModel.EntityId);
            if (IsNotNull(gymMemberDetails))
            {
                userModel.PastInjuries = gymMemberDetails.PastInjuries;
                userModel.MedicalHistory = gymMemberDetails.MedicalHistory;
                userModel.OtherInformation = gymMemberDetails.OtherInformation;
            }
            return userModel;
        }

        //Change Password.
        public virtual ChangePasswordModel ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (IsNull(changePasswordModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            UserMaster userMasterData = _userMasterRepository.Table.FirstOrDefault(x => x.UserMasterId == changePasswordModel.UserMasterId);
            if (IsNotNull(userMasterData) && userMasterData.Password == MD5Hash(changePasswordModel.CurrentPassword))
            {
                userMasterData.Password = MD5Hash(changePasswordModel.NewPassword);
                _userMasterRepository.Update(userMasterData);
            }
            else
            {
                changePasswordModel.HasError = true;
                changePasswordModel.ErrorMessage = "Current Password DoesNot Match ";
            }
            return changePasswordModel;
        }

    }
}
