﻿using Coditech.Admin.Helpers;
using Coditech.Admin.Utilities;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Newtonsoft.Json;
using System.Diagnostics;
namespace Coditech.Admin.Agents
{
    public class AccSetupGLAgent : BaseAgent, IAccSetupGLAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupGLClient _accSetupGLClient;
        private readonly IGeneralFinancialYearClient _generalFinancialYearClient;
        #endregion

        #region Public Constructor
        public AccSetupGLAgent(ICoditechLogging coditechLogging, IAccSetupGLClient accSetupGLClient, IGeneralFinancialYearClient generalFinancialYearClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupGLClient = GetClient<IAccSetupGLClient>(accSetupGLClient);
            _generalFinancialYearClient = GetClient<IGeneralFinancialYearClient>(generalFinancialYearClient);
        }
        #endregion

        #region Public Methods
        //Get Get AccSetupBalanceSheet.
        public virtual AccSetupGLModel GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {

            AccSetupGLResponse response = _accSetupGLClient.GetAccSetupGLTree(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);
            response.AccSetupGLModel.CurrencySymbol = SessionHelper.GetDataFromSession<AccPrequisiteModel>(AdminConstants.AccountPrerequisiteSession)?.CurrencySymbol;
            return response?.AccSetupGLModel;
        }
        public virtual GeneralFinancialYearModel GetCurrentFinancialYear()
        {
            int accSetupBalanceSheetId = AdminGeneralHelper.GetSelectedBalanceSheetId();
            GeneralFinancialYearResponse financialyearresponse = _generalFinancialYearClient.GetCurrentFinancialYear(accSetupBalanceSheetId);
            return financialyearresponse?.GeneralFinancialYearModel.ToViewModel<GeneralFinancialYearModel>();
        }
        //Create CreateAccountSetupGL
        public virtual AccSetupGLModel CreateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            try
            {
                AccSetupGLResponse response = _accSetupGLClient.CreateAccountSetupGL(accSetupGLModel);
                AccSetupGLModel model = response?.AccSetupGLModel;
                if (!model.HasError)
                {
                    RemoveInSession(AdminConstants.AccountPrerequisiteSession);
                }

                // Return the model directly without converting to ViewModel
                return model ?? new AccSetupGLModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        accSetupGLModel.ErrorMessage = ex.ErrorMessage;
                        break;
                    default:
                        accSetupGLModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                        break;
                }
                return accSetupGLModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                accSetupGLModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                return accSetupGLModel;
            }
        }
        // Get AccountSetupGL by AccountSetupGLId.
        public virtual AccSetupGLModel GetAccountSetupGL(int accSetupGLId)
        {
            AccSetupGLResponse response = _accSetupGLClient.GetAccountSetupGL(accSetupGLId);
            return response?.AccSetupGLModel ?? new AccSetupGLModel();
        }

        //Update Account.
        public virtual AccSetupGLModel UpdateAccount(AccSetupGLModel accSetupGLModel)
        {
            try
            {
                AccSetupGLResponse response = _accSetupGLClient.UpdateAccount(accSetupGLModel);

                return response?.AccSetupGLModel ?? new AccSetupGLModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        accSetupGLModel.ErrorMessage = ex.ErrorMessage;
                        break;
                    default:
                        accSetupGLModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                        break;
                }
                return accSetupGLModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                accSetupGLModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                return accSetupGLModel;
            }
        }

        //Update AccountSetupGL.
        public virtual AccSetupGLModel UpdateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            try
            {
                AccSetupGLResponse response = _accSetupGLClient.UpdateAccountSetupGL(accSetupGLModel);

                return response?.AccSetupGLModel ?? new AccSetupGLModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                switch (ex.ErrorCode)
                {
                    case ErrorCodes.AlreadyExist:
                        accSetupGLModel.ErrorMessage = ex.ErrorMessage;
                        break;
                    default:
                        accSetupGLModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
                        break;
                }
                return accSetupGLModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                accSetupGLModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                return accSetupGLModel;
            }
        }
        //AddChild .
        public virtual AccSetupGLModel AddChild(AccSetupGLModel accSetupGLModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(accSetupGLModel.BankModelData))
                {
                    if (!accSetupGLModel.BankModelData.Trim().StartsWith("["))
                    {
                        // Wrap it in an array if it's not already an array
                        accSetupGLModel.BankModelData = "[" + accSetupGLModel.BankModelData + "]";
                    }

                    accSetupGLModel.AccSetupGLBankList = JsonConvert.DeserializeObject<List<AccSetupGLBankModel>>(accSetupGLModel.BankModelData);
                }

                AccSetupGLResponse response = _accSetupGLClient.AddChild(accSetupGLModel);

                if (response?.HasError == true)
                {
                    accSetupGLModel.HasError = true;
                    accSetupGLModel.ErrorMessage = response.ErrorMessage;
                    return accSetupGLModel;
                }

                return response?.AccSetupGLModel ?? new AccSetupGLModel();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                accSetupGLModel.HasError = true;
                accSetupGLModel.ErrorMessage = ex.ErrorMessage;
                return accSetupGLModel;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                accSetupGLModel.HasError = true;
                accSetupGLModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
                return accSetupGLModel;
            }
        }

        //Delete AccountSetupGL.
        public virtual bool DeleteAccountSetupGL(string accSetupGLId, out string errorMessage)
        {
            errorMessage = GeneralResources.ErrorFailedToDelete;

            try
            {
                _coditechLogging.LogMessage("Agent method execution started.", CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Info);
                TrueFalseResponse trueFalseResponse = _accSetupGLClient.DeleteAccountSetupGL(new ParameterModel { Ids = accSetupGLId });

                if (trueFalseResponse.HasError)
                {
                    errorMessage = trueFalseResponse.ErrorMessage;  // ✅ Pass the correct error message
                    return false;
                }

                return trueFalseResponse.IsSuccess;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Warning);
                errorMessage = ex.Message;  // ✅ Pass correct message back
                return false;
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccountSetupGL.ToString(), TraceLevel.Error);
                errorMessage = GeneralResources.ErrorFailedToDelete;
                return false;
            }
        }
        #endregion
    }
}
