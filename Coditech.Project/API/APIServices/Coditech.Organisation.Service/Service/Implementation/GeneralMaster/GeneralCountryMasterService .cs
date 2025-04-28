
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;
using Coditech.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Specialized;
using System.Data;

using static Coditech.Common.Helper.HelperUtility;
namespace Coditech.API.Service
{
    public class GeneralCountryMasterService : IGeneralCountryMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralCountryMaster> _generalCountryMasterRepository;
        public GeneralCountryMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalCountryMasterRepository = new CoditechRepository<GeneralCountryMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralCountryListModel GetCountryList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralCountryModel> objStoredProc = new CoditechViewRepository<GeneralCountryModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCountryModel> CountryList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetCountryList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralCountryListModel listModel = new GeneralCountryListModel();

            listModel.GeneralCountryList = CountryList?.Count > 0 ? CountryList : new List<GeneralCountryModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }
        //Create Country.
        public virtual GeneralCountryModel CreateCountry(GeneralCountryModel generalCountryModel)
        {
            if (IsNull(generalCountryModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);

            if (IsCountryCodeAlreadyExist(generalCountryModel.CountryName, generalCountryModel.GeneralCountryMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Country Name"));

            if (IsCallingCodeAlreadyExist(generalCountryModel.CallingCode, generalCountryModel.GeneralCountryMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Calling Code"));


            GeneralCountryMaster generalCountryMaster = generalCountryModel.FromModelToEntity<GeneralCountryMaster>();

            //Create new Country and return it.
            GeneralCountryMaster countryData = _generalCountryMasterRepository.Insert(generalCountryMaster);
            if (countryData?.GeneralCountryMasterId > 0)
            {
                generalCountryModel.GeneralCountryMasterId = countryData.GeneralCountryMasterId;
            }
            else
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCountryModel;
        }

        //Get Country by Country id.
        public virtual GeneralCountryModel GetCountry(short countryId)
        {
            if (countryId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            //Get the Country Details based on id.
            GeneralCountryMaster generalCountryMaster = _generalCountryMasterRepository.Table.FirstOrDefault(x => x.GeneralCountryMasterId == countryId);
            GeneralCountryModel generalCountryModel = generalCountryMaster?.FromEntityToModel<GeneralCountryModel>();
            return generalCountryModel;
        }

        //Update Country.
        public virtual bool UpdateCountry(GeneralCountryModel generalCountryModel)
        {
            if (IsNull(generalCountryModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCountryModel.GeneralCountryMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            if (IsCountryCodeAlreadyExist(generalCountryModel.CountryName, generalCountryModel.GeneralCountryMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Country Name"));

            if (IsCallingCodeAlreadyExist(generalCountryModel.CallingCode, generalCountryModel.GeneralCountryMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "Calling Code"));


            GeneralCountryMaster generalCountryMaster = generalCountryModel.FromModelToEntity<GeneralCountryMaster>();

            //Update Country
            bool isCountryUpdated = _generalCountryMasterRepository.Update(generalCountryMaster);
            if (!isCountryUpdated)
            {
                generalCountryModel.HasError = true;
                generalCountryModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isCountryUpdated;
        }

        //Delete Country.
        public virtual bool DeleteCountry(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CountryID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("CountryId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteCountry @CountryId,  @Status OUT", 1, out status);

            return status == 1 ? true : false;
        }

        #region Protected Method
        //Check if Country code is already present or not.
        protected virtual bool IsCountryCodeAlreadyExist(string countryName, short generalCountryMasterId = 0)
         => _generalCountryMasterRepository.Table.Any(x => x.CountryName == countryName && (x.GeneralCountryMasterId != generalCountryMasterId || generalCountryMasterId == 0));
        protected virtual bool IsCallingCodeAlreadyExist(string callingCode, short generalCountryMasterId = 0)
         => _generalCountryMasterRepository.Table.Any(x => x.CallingCode == callingCode && (x.GeneralCountryMasterId != generalCountryMasterId || generalCountryMasterId == 0));
        #endregion
    }
}
