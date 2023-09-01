﻿using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IOrganisationCentreMasterService
    {
        OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentreModel CreateOrganisationCentre(OrganisationCentreModel model);
        OrganisationCentreModel GetOrganisationCentre(int organisationCentreMasterId);
        bool UpdateOrganisationCentre(OrganisationCentreModel model);
        bool DeleteOrganisationCentre(ParameterModel parameterModel);
    }
} 
