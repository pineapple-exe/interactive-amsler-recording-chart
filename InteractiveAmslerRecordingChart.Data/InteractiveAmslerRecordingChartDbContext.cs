using Microsoft.EntityFrameworkCore;
using InteractiveAmslerRecordingChart.Domain.Entities;
using InteractiveAmslerRecordingChart.Data.Configurations;

namespace InteractiveAmslerRecordingChart.Data
{
    public class InteractiveAmslerRecordingChartDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        private readonly string _connectionString = "Server=localhost;Database=InitialAmslerRecord;Trusted_Connection=True;";

        public InteractiveAmslerRecordingChartDbContext()
        {

        }

        public InteractiveAmslerRecordingChartDbContext(DbContextOptions<InteractiveAmslerRecordingChartDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SessionsConfiguration());
            modelBuilder.ApplyConfiguration(new CoordinatesConfiguration());
        }
    }
}
