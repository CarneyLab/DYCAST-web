using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using DotSpatial.Projections;
using System.Diagnostics;

namespace dycast_web.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult GetRisk()
        {
            var projectionFrom = ProjectionInfo.FromEpsgCode(3857);
            var projectionTo = ProjectionInfo.FromEpsgCode(4326);

            var data = new double[,] {
                { -5238718.136,-2619612.085,10,1,28,1,0.0484 },
                { -5238718.136,-2619712.085,10,1,28,1,0.0484 },
                { -5238718.136,-2619912.085,10,1,30,1,0.5168 },
                { -5238618.136,-2619312.085,10,1,28,1,0.0484 },
                { -5238618.136,-2619412.085,10,1,28,1,0.0484 },
                { -5238618.136,-2619512.085,10,1,28,1,0.0484 },
                { -5238618.136,-2619612.085,10,1,28,1,0.0484 },
                { -5238618.136,-2619712.085,10,1,28,1,0.0484 },
                { -5238618.136,-2619812.085,11,1,36,1,0.0634 },
                { -5238618.136,-2619912.085,11,1,36,1,0.0634 },
                { -5238618.136,-2620012.085,11,1,36,1,0.0634 },
                { -5238518.136,-2619412.085,10,1,28,1,0.0484 },
                { -5238518.136,-2619512.085,10,1,28,1,0.0484 },
                { -5238518.136,-2619612.085,10,1,28,1,0.0484 },
                { -5238518.136,-2619712.085,10,1,28,1,0.0484 },
                { -5238518.136,-2619812.085,10,1,28,1,0.0484 },
                { -5238518.136,-2619912.085,10,1,28,1,0.0484 },
                { -5238418.136,-2619412.085,10,1,28,1,0.0484 },
            };

            var model = new FeatureCollection();

            for (var i = data.GetLength(0); i-- > 0;)
            {

                var pointsArray = new double[2];
                pointsArray[0] = data[i, 0];
                pointsArray[1] = data[i, 1];

                double[] z = { 0 };

                Reproject.ReprojectPoints(pointsArray, z, projectionFrom, projectionTo, 0, 1);

                var point = new Point(
                    new Position(pointsArray[1], pointsArray[0]));


                var featureProperties = new Dictionary<string, object>
            {
                { "title", "Point " + i.ToString() },
                { "description", "This is point number " + i.ToString() },
                { "pValue", data[i,6] },
            };

                var feature = new Feature(point, featureProperties);

                model.Features.Add(feature);
            }


            var serializedFeature = JsonConvert.SerializeObject(model, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });

            return Content(serializedFeature, "application/json");
        }
    }
}