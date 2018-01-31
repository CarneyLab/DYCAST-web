using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;

namespace dycast_web.Models.ViewModels.DycastViewModels
{
    public class RiskPointViewModel
    {
        public DateTime RiskDate { get; set; }
        public double LatOriginal { get; set; }
        public double LongOriginal { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int NumCases { get; set; }
        public int ClosePairs { get; set; }
        public int CloseSpace { get; set; }
        public int CloseTime { get; set; }
        public double? PValue { get; set; }


        public Feature AsGeoJsonFeature()
        {
            var point = new Point(
                new Position(Long, Lat));

            var featureProperties = new Dictionary<string, object>
                {
                    { "date", RiskDate.ToString("yyyy-MM-dd") },
                    { "pValue", PValue },
                    { "latOriginal", LatOriginal },
                    { "longOriginal", LongOriginal },
                    { "lat", Lat },
                    { "long", Long },
                    //{ "numCases", NumCases },
                    //{ "closePairs", ClosePairs },
                    //{ "closeSpace", CloseSpace },
                    //{ "closeTime", CloseTime },
                };

            return new Feature(point, featureProperties);
        }
    }
}
