using System;

namespace dycast_web.Models.Entities
{
    public partial class Risk
    {
        public DateTime RiskDate { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int? NumberOfCases { get; set; }
        public int? ClosePairs { get; set; }
        public int? CloseSpace { get; set; }
        public int? CloseTime { get; set; }
        public double? CumulativeProbability { get; set; }
    }
}
