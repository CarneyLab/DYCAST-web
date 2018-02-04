using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dycast_web.Data;
using dycast_web.Services;
using GeoJSON.Net.Feature;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace dycast_web.Controllers
{
    [Produces("application/json")]
    [Route("api/Risk")]
    public class RiskController : Controller
    {
        private readonly DycastDbContext _context;
        private readonly IRiskService _riskService;

        public RiskController(IRiskService riskService)
        {
            _riskService = riskService;
        }


        // GET: api/Risk/?fromDate={0}&toDate={1}
        [HttpGet]
        public async Task<IActionResult> GetRisk(DateTime? fromDate=null, DateTime? toDate=null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var risk = new FeatureCollection();

            if (fromDate != null && toDate != null)
            {
                risk = await _riskService.GetRisk(fromDate, toDate);
            }
            else
            {
                risk = await _riskService.GetRisk();
            }

            if (risk == null)
            {
                return NotFound();
            }

            return Ok(risk);
        }
    }
}