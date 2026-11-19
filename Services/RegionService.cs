using LandData.Data;
using LandData.Models;
using Microsoft.EntityFrameworkCore;

namespace LandData.Services
{
    public class RegionService
    {
        public List<Region> GetAll()
        {
            using var db = new AppDbContext();
            return db.Regions
                .Include(r => r.Rules)
                    .ThenInclude(rule => rule.Links)
                .OrderBy(r => r.Code)
                .ToList();
        }
    }
}