using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace dycast_web.Models.Entities
{
    public partial class TmpClusterPerPointSelection
    {
        public int BirdId { get; set; }
        public DateTime? ReportDate { get; set; }
        public PostgisGeometry Location { get; set; }
    }
}
