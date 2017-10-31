using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dycast_web.Data;
using dycast_web.Models.Entities;
using dycast_web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using dycast_web.Models.ViewModels.DycastViewModels;
using GeoJSON.Net.Feature;

namespace dycast_web.Controllers
{
    [Produces("application/json")]
    [Route("api/Risk")]
    public class RiskController : Controller
    {
        private readonly DycastDbContext _context;

        public RiskController(DycastDbContext context)
        {
            _context = context;
        }

        // GET: api/Risk
        [HttpGet]
        public async Task<FeatureCollection> GetRiskAsync()
        {
            var riskService = new RiskService(_context);
            return await riskService.GetRisk();
        }
    }
}