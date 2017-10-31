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

        public RiskController(DycastDbContext context)
        {
            _context = context;
        }


        // GET: api/Risk/?fromDate={0}&toDate={1}
        [HttpGet]
        public async Task<IActionResult> GetRisk(DateTime? fromDate=null, DateTime? toDate=null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var riskService = new RiskService(_context);
            var risk = new FeatureCollection();


            if (fromDate != null && toDate != null)
            {
                risk = await riskService.GetRisk(fromDate, toDate);
            }
            else
            {
                risk = await riskService.GetRisk();
            }

            if (risk == null)
            {
                return NotFound();
            }

            return Ok(risk);
        }
        }
    }
}