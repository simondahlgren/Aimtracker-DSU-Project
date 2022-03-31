
using DSUgrupp2.Data.Dto;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DSUgrupp2.Data
{
    public class AppDbContext : DbContext
    {
        
        /// <summary>
        /// Sets classes that can be saved to the DB.
        /// </summary>
        public DbSet<ShootingSessionDto>? ShootingSessions { get; set; }
        public DbSet<AthleteDto>? AthleteDatas { get; set; }
        public DbSet<StatSeasonsDto> StatSeasonsDatas { get; set; }
        public DbSet<StatShootingStandingDto> StatShootingStandingsDatas { get; set; }
        public DbSet<StatShootingProneDto> StatShootingProneDtosDatas { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
