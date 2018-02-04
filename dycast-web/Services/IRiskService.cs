using GeoJSON.Net.Feature;
using System;
using System.Threading.Tasks;

namespace dycast_web.Services
{
    public interface IRiskService
    {
        Task<FeatureCollection> GetRisk();
        Task<FeatureCollection> GetRisk(DateTime? fromDate, DateTime? toDate);
    }
}
