using System;
using Microsoft.AspNetCore.Mvc;
using dycast_web.Models;
using Microsoft.Extensions.Options;

namespace dycast_web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapboxSettings _mapboxSettings;

        public MapController(IOptions<MapboxSettings> mapboxSettings)
        {
            _mapboxSettings = mapboxSettings.Value;
        }

        public IActionResult Index()
        {
            ViewData["MapboxAccessToken"] = Environment.ExpandEnvironmentVariables(_mapboxSettings.AccessToken);
            return View();
        }
    }
}