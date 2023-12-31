﻿using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using System.Collections.Specialized;

namespace Coditech.API.Organisation.Service.Interface.Organisation
{
    public interface IOrganisationCentreMasterService
    {
        OrganisationCentreListModel GetOrganisationCentreList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        OrganisationCentreModel CreateOrganisationCentre(OrganisationCentreModel model);
        OrganisationCentreModel GetOrganisationCentre(short organisationCentreMasterId);
        bool UpdateOrganisationCentre(OrganisationCentreModel model);
        bool DeleteOrganisationCentre(ParameterModel parameterModel);
        OrganisationCentrePrintingFormatModel GetPrintingFormat(short organisationCentreMasterId);
        bool UpdatePrintingFormat(OrganisationCentrePrintingFormatModel model);
        OrganisationCentrewiseGSTCredentialModel GetCentrewiseGSTSetup(short organisationCentreMasterId);
        bool UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel model);
    }
}

