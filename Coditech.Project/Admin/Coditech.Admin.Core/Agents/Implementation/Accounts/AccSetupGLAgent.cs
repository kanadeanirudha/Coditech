using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;
using Coditech.Resources;
using System.Diagnostics;
namespace Coditech.Admin.Agents
{
    public class AccSetupGLAgent : BaseAgent, IAccSetupGLAgent
    {
        #region Private Variable
        protected readonly ICoditechLogging _coditechLogging;
        private readonly IAccSetupGLClient _accSetupGLClient;
        #endregion

        #region Public Constructor
        public AccSetupGLAgent(ICoditechLogging coditechLogging, IAccSetupGLClient accSetupGLClient)
        {
            _coditechLogging = coditechLogging;
            _accSetupGLClient = GetClient<IAccSetupGLClient>(accSetupGLClient);
        }
        #endregion

        #region Public Methods
        //Get Get AccSetupBalanceSheet.
        public virtual AccSetupGLModel GetAccSetupGLTree(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId)
        {
            AccSetupGLResponse response = _accSetupGLClient.GetAccSetupGLTree(selectedcentreCode, accSetupBalanceSheetTypeId, accSetupBalanceSheetId);
            return response?.AccSetupGLModel;
        }
        //Create CreateAccountSetupGL
        public virtual AccSetupGLModel CreateAccountSetupGL(AccSetupGLModel accSetupGLModel)
        {
            try
            {
                AccSetupGLResponse response = _accSetupGLClient.CreateAccountSetupGL(accSetupGLModel);
                AccSetupGLModel model = response?.AccSetupGLModel;

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
        #endregion
    }
}
