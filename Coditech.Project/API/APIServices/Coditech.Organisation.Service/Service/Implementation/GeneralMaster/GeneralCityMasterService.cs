﻿using Coditech.API.Data;
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
    public class GeneralCityMasterService : IGeneralCityMasterService
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ICoditechLogging _coditechLogging;
        private readonly ICoditechRepository<GeneralCityMaster> _generalCityMasterRepository;
        private readonly ICoditechRepository<GeneralRegionMaster> _generalRegionMasterRepository;
        public GeneralCityMasterService(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
            _generalCityMasterRepository = new CoditechRepository<GeneralCityMaster>(_serviceProvider.GetService<Coditech_Entities>());
            _generalRegionMasterRepository = new CoditechRepository<GeneralRegionMaster>(_serviceProvider.GetService<Coditech_Entities>());
        }

        public virtual GeneralCityListModel GetCityList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength)
        {
            //Bind the Filter, sorts & Paging details.
            PageListModel pageListModel = new PageListModel(filters, sorts, pagingStart, pagingLength);
            CoditechViewRepository<GeneralCityModel> objStoredProc = new CoditechViewRepository<GeneralCityModel>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("@WhereClause", pageListModel?.SPWhereClause, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@PageNo", pageListModel.PagingStart, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Rows", pageListModel.PagingLength, ParameterDirection.Input, DbType.Int32);
            objStoredProc.SetParameter("@Order_BY", pageListModel.OrderBy, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("@RowsCount", pageListModel.TotalRowCount, ParameterDirection.Output, DbType.Int32);
            List<GeneralCityModel> cityList = objStoredProc.ExecuteStoredProcedureList("Coditech_GetCityList @WhereClause,@Rows,@PageNo,@Order_BY,@RowsCount OUT", 4, out pageListModel.TotalRowCount)?.ToList();
            GeneralCityListModel listModel = new GeneralCityListModel();

            listModel.GeneralCityList = cityList?.Count > 0 ? cityList : new List<GeneralCityModel>();
            listModel.BindPageListModel(pageListModel);
            return listModel;
        }

        //Create City.
        public virtual GeneralCityModel CreateCity(GeneralCityModel generalCityModel)
        {
            if (IsNull(generalCityModel))
                throw new CoditechException(ErrorCodes.NullModel, GeneralResources.ModelNotNull);
            if (IsCityNameAlreadyExist(generalCityModel.CityName))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "City Name"));
            GeneralCityMaster generalCityMaster = generalCityModel.FromModelToEntity<GeneralCityMaster>();

            //Create new City and return it.
            GeneralCityMaster cityData = _generalCityMasterRepository.Insert(generalCityMaster);
            if (cityData?.GeneralCityMasterId > 0)
            {
                generalCityModel.GeneralCityMasterId = cityData.GeneralCityMasterId;
            }
            else
            {
                generalCityModel.HasError = true;
                generalCityModel.ErrorMessage = GeneralResources.ErrorFailedToCreate;
            }
            return generalCityModel;
        }

        //Get City by City id.
        public virtual GeneralCityModel GetCity(int cityId)
        {
            if (cityId <= 0)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            //Get the City Details based on id.
            GeneralCityMaster cityData = _generalCityMasterRepository.Table.FirstOrDefault(x => x.GeneralCityMasterId == cityId);
            GeneralCityModel generalCityModel = cityData.FromEntityToModel<GeneralCityModel>();
            if (IsNotNull(generalCityModel))
            {
                generalCityModel.GeneralCountryMasterId = _generalRegionMasterRepository.Table.FirstOrDefault(x => x.GeneralRegionMasterId == generalCityModel.GeneralRegionMasterId).GeneralCountryMasterId;
            }
            return generalCityModel;
        }

        //Update City.
        public virtual bool UpdateCity(GeneralCityModel generalCityModel)
        {
            if (IsNull(generalCityModel))
                throw new CoditechException(ErrorCodes.InvalidData, GeneralResources.ModelNotNull);

            if (generalCityModel.GeneralCityMasterId < 1)
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            if (IsCityNameAlreadyExist(generalCityModel.CityName, generalCityModel.GeneralCityMasterId))
                throw new CoditechException(ErrorCodes.AlreadyExist, string.Format(GeneralResources.ErrorCodeExists, "City Name"));

            GeneralCityMaster generalCityMaster = generalCityModel.FromModelToEntity<GeneralCityMaster>();

            //Update City
            bool isCityUpdated = _generalCityMasterRepository.Update(generalCityMaster);
            if (!isCityUpdated)
            {
                generalCityModel.HasError = true;
                generalCityModel.ErrorMessage = GeneralResources.UpdateErrorMessage;
            }
            return isCityUpdated;
        }

        //Delete City.
        public virtual bool DeleteCity(ParameterModel parameterModel)
        {
            if (IsNull(parameterModel) || string.IsNullOrEmpty(parameterModel.Ids))
                throw new CoditechException(ErrorCodes.IdLessThanOne, string.Format(GeneralResources.ErrorIdLessThanOne, "CityID"));

            CoditechViewRepository<View_ReturnBoolean> objStoredProc = new CoditechViewRepository<View_ReturnBoolean>(_serviceProvider.GetService<Coditech_Entities>());
            objStoredProc.SetParameter("CityId", parameterModel.Ids, ParameterDirection.Input, DbType.String);
            objStoredProc.SetParameter("Status", null, ParameterDirection.Output, DbType.Int32);
            int status = 0;
            objStoredProc.ExecuteStoredProcedureList("Coditech_DeleteCity @CityId,  @Status OUT", 1, out status);
            return status == 1 ? true : false;
        }

        //Get city list.
        public virtual GeneralCityListModel GetCityByRegionWise(int generalRegionMasterId)
        {
            string regionName = _generalRegionMasterRepository.Table.FirstOrDefault(x => x.GeneralRegionMasterId == generalRegionMasterId)?.RegionName;

            GeneralCityListModel list = new GeneralCityListModel();
            list.GeneralCityList = (from a in _generalCityMasterRepository.Table
                                    where (a.GeneralRegionMasterId == generalRegionMasterId)
                                    select new GeneralCityModel()
                                    {
                                        GeneralCityMasterId = a.GeneralCityMasterId,
                                        CityName = a.CityName,
                                        RegionName = regionName,
                                    })?.ToList();
            return list;
        }

        //Get all city list.
        public virtual GeneralCityListModel GetAllCities()
        {
            GeneralCityListModel list = new GeneralCityListModel();
            list.GeneralCityList = (from a in _generalCityMasterRepository.Table 
                                    join b in _generalRegionMasterRepository.Table 
                                    on a.GeneralRegionMasterId equals b.GeneralRegionMasterId
                                    select new GeneralCityModel()
                                    {
                                        GeneralCityMasterId = a.GeneralCityMasterId,
                                        CityName = $"{a.CityName}({b.RegionName})",
                                    })?.ToList();
            return list;
        }

        #region Protected Method
        //Check if City code is already present or not.
        protected virtual bool IsCityNameAlreadyExist(string cityName, int generalCityMasterId = 0)
         => _generalCityMasterRepository.Table.Any(x => x.CityName == cityName && (x.GeneralCityMasterId != generalCityMasterId || generalCityMasterId == 0));
        #endregion
    }
}
