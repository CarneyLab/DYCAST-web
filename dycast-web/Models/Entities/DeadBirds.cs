using System;
using System.Collections.Generic;

namespace dycast_web.Models.Entities
{
    public partial class DeadBirds
    {
        public int BirdId { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}
