using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace dycast_web.Models.Entities
{
    public partial class EffectsPolysProjected
    {
        public int TileId { get; set; }
        public short? County { get; set; }
        public PostgisGeometry TheGeom { get; set; }
    }
}
