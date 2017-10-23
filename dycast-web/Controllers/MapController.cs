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
                { -5100397.136, -2474991.085, 10, 2, 33, 3, 0.243175 },
                { -5100397.136, -2475091.085, 10, 2, 33, 3, 0.3 },
                { -5100397.136, -2475191.085, 10, 2, 33, 3, 0.543175 },
                { -5100297.136, -2474891.085, 10, 2, 33, 3, 0.643175 },
                { -5100297.136, -2474991.085, 10, 2, 33, 3, 0.643175 },
                { -5100297.136, -2475091.085, 10, 2, 33, 3, 0.743175 },
                { -5100297.136, -2475191.085, 10, 2, 33, 3, 0.843175 },
                { -5100297.136, -2475291.085, 10, 2, 33, 3, 0.943175 },
                { -5100197.136, -2474791.085, 10, 2, 33, 3, 0.003175 },
                { -5100197.136, -2474891.085, 10, 2, 33, 3, 0.013175 },
                { -5100197.136, -2474991.085, 10, 2, 33, 3, 0.023175 },
                { -5100197.136, -2475091.085, 10, 2, 33, 3, 0.033175 },
                { -5100197.136, -2475191.085, 10, 2, 33, 3, 0.043175 },
                { -5100197.136, -2475291.085, 10, 2, 33, 3, 0.053175 },
                { -5100197.136, -2475391.085, 10, 2, 33, 3, 0.063175 },
                { -5100097.136, -2474691.085, 10, 2, 35, 1, 0.018    },
                { -5100097.136, -2474791.085, 10, 2, 33, 3, 0.073175 },
                { -5100097.136, -2474891.085, 10, 2, 33, 3, 0.093175 },
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