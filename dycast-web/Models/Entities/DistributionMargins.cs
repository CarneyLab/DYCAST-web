namespace dycast_web.Models.Entities
{
    public partial class DistributionMargins
    {
        public int NumberOfCases { get; set; }
        public int CloseInSpaceAndTime { get; set; }
        public double? Probability { get; set; }
        public double? CumulativeProbability { get; set; }
        public int CloseSpace { get; set; }
        public int CloseTime { get; set; }
    }
}
