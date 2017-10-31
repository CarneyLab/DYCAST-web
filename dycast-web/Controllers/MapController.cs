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
    }
}