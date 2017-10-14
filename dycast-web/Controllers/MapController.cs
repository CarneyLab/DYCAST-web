using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

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
            var model = new FeatureCollection();

            for (var i = 10; i-- > 0;)
            {
                var point = new Point(
                    new Position(41.940403 + i, -87.637596 + i));

                var featureProperties = new Dictionary<string, object>
            {
                { "title", "Point " + i.ToString() },
                { "description", "This is point number " + i.ToString() },
                { "pValue", (float)i/100 },
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