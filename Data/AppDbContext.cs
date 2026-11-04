using LandData.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LandData.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<RegionRule> RegionRules => Set<RegionRule>();
        public DbSet<DocumentLink> DocumentLinks => Set<DocumentLink>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // База лежит в папке пользователя, а не рядом с программой:
            // так она не попадёт в Git и не потеряется при пересборке.
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "LandData");
            Directory.CreateDirectory(folder);

            options.UseSqlite($"Data Source={Path.Combine(folder, "landdata.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Код региона уникален
            modelBuilder.Entity<Region>()
                .HasIndex(r => r.Code)
                .IsUnique();

            // Категорию храним словом ("Moratorium"), а не числом 5 — так база читаема
            modelBuilder.Entity<RegionRule>()
                .Property(r => r.Category)
                .HasConversion<string>();
        }
    }
}