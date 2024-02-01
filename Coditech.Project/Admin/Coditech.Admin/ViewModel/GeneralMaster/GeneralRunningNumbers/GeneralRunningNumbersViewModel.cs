﻿using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GeneralRunningNumbersViewModel : BaseViewModel
    {
        public long GeneralRunningNumberId { get; set; }
        public string Description { get; set; }
        public int KeyFieldEnum { get; set; }
        public int GeneralFinancialYearId { get; set; }
        public string CentreCode { get; set; }
        public string DisplayFormat { get; set; }
        public bool IsSequenceReset { get; set; }
        public string SeparatorChar { get; set; }
        public string Prefix { get; set; }
        public bool IsBackDated { get; set; }
        public string BackDatedPrefix { get; set; }
        public long StartSequence { get; set; }
        public long CurrentSequnce { get; set; }
        public bool IsRowLock { get; set; }
        public bool IsActive { get; set; }
    }
}
