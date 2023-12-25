
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;

using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GymMemberDetailsService : IGymMemberDetailsService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GymMemberDetails> _gymMemberDetailsRepository;
        public GymMemberDetailsService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _gymMemberDetailsRepository = new CoditechRepository<GymMemberDetails>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GymMemberDetailsListModel GetGymMemberDetailsList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GymMemberDetailsModel> objStoredProc = new CoditechViewRepository<GymMemberDetailsModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GymMemberDetailsModel> gymMemberList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetGymMemberDetailsList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GymMemberDetailsListModel listModel = new GymMemberDetailsListModel();

            listModel.GymMemberDetailsList = gymMemberList?.Count > 0 ? gymMemberList : new List<GymMemberDetailsModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Get Gym Member Other Details
        public virtual GymMemberDetailsModel GetGymMemberOtherDetails(int gymMemberDetailId)
        {
            if (gymMemberDetailId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            GymMemberDetails gymMemberDetails = _gymMemberDetailsRepository.Table.FirstOrDefault(x => x.GymMemberDetailId == gymMemberDetailId);
            GymMemberDetailsModel gymMemberDetailsModel = gymMemberDetails?.FromEntityToModel<GymMemberDetailsModel>();
            return gymMemberDetailsModel;
        }

        //Update Gym Member Other Details
        public virtual bool UpdateGymMemberOtherDetails(GymMemberDetailsModel gymMemberDetailsModel)
        {
            if (IsNull(gymMemberDetailsModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (gymMemberDetailsModel.GymMemberDetailId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            GymMemberDetails gymMemberDetails = gymMemberDetailsModel.FromModelToEntity<GymMemberDetails>();

            bool isUpdated = _gymMemberDetailsRepository.Update(gymMemberDetails);
            if (!isUpdated)
            {
                gymMemberDetailsModel.HasError = true;
                gymMemberDetailsModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isUpdated;
        }

        //Delete Gym Members
        public virtual bool DeleteGymMembers(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "GymMemberDetailId"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("GymMemberDetailId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteGymMembers @GymMemberDetailId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

    }
}
