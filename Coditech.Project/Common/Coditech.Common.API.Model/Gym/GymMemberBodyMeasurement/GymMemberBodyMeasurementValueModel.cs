﻿namespace Coditech.Common.API.Model
{
    public class GymMemberBodyMeasurementValueModel : BaseModel
    {
        public long GymMemberBodyMeasurementId { get; set; }

        public string BodyMeasurementValue { get; set; }

        public string BodyMeasurementType { get; set; }
        public string MeasurementUnitShortCode { get; set; }
        public string MeasurementUnitDisplayName { get; set; }
        public DateTime BodyMeasurementDate { get; set; }
    }
}
