﻿namespace Coditech.Admin.ViewModel
{
    public class LineChartViewModel
    {
        public string LineChartId { get; set; }
        public string XValues { get; set; }
        public string YValues { get; set; }
        public string XAxisLabel { get; set; }
        public string YAxisLabel { get; set; }
        public string Title { get; set; } = string.Empty;
        public string BackgroundColor { get; set; }
    }
}