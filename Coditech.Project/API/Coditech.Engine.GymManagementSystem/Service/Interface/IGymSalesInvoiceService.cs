﻿using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGymSalesInvoiceService
    {
        GymMemberSalesInvoiceListModel GymMemberServiceSalesInvoiceList(string selectedCentreCode, DateTime? toDate, DateTime? fromDate, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
    }
}
