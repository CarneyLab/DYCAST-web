using System;

namespace dycast_web.Models.Entities
{
    public partial class RiskTableList
    {
        public int TableId { get; set; }
        public string Tablename { get; set; }
        public DateTime? DateGenerated { get; set; }
        public int? MonteCarloId { get; set; }
    }
}
