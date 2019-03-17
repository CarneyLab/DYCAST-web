using Microsoft.EntityFrameworkCore;
using dycast_web.Models.Entities;

namespace dycast_web.Data
{
    public class DycastDbContext : DbContext
    {

        public virtual DbSet<Cases> Cases { get; set; }
        public virtual DbSet<DistributionMargins> DistributionMargins { get; set; }
        public virtual DbSet<Risk> Risk { get; set; }
        public virtual DbSet<SpatialRefSys> SpatialRefSys { get; set; }


        public DycastDbContext(DbContextOptions<DycastDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<Cases>(entity =>
            {
                entity.ToTable("cases");

                entity.HasIndex(e => e.Location)
                    .HasName("idx_cases_location")
                    .ForNpgsqlHasMethod("gist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<DistributionMargins>(entity =>
            {
                entity.HasKey(e => new { e.NumberOfCases, e.CloseInSpaceAndTime, e.CloseSpace, e.CloseTime });

                entity.ToTable("distribution_margins");

                entity.HasIndex(e => e.CloseInSpaceAndTime)
                    .HasName("ix_distribution_margins_close_in_space_and_time");

                entity.HasIndex(e => e.CloseSpace)
                    .HasName("ix_distribution_margins_close_space");

                entity.HasIndex(e => e.CloseTime)
                    .HasName("ix_distribution_margins_close_time");

                entity.HasIndex(e => e.NumberOfCases)
                    .HasName("ix_distribution_margins_number_of_cases");

                entity.Property(e => e.NumberOfCases).HasColumnName("number_of_cases");

                entity.Property(e => e.CloseInSpaceAndTime).HasColumnName("close_in_space_and_time");

                entity.Property(e => e.CloseSpace).HasColumnName("close_space");

                entity.Property(e => e.CloseTime).HasColumnName("close_time");

                entity.Property(e => e.CumulativeProbability).HasColumnName("cumulative_probability");

                entity.Property(e => e.Probability).HasColumnName("probability");
            });

            modelBuilder.Entity<Risk>(entity =>
            {
                entity.HasKey(e => new { e.RiskDate, e.Lat, e.Long });

                entity.ToTable("risk");

                entity.Property(e => e.RiskDate)
                    .HasColumnName("risk_date")
                    .HasColumnType("date");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.ClosePairs).HasColumnName("close_pairs");

                entity.Property(e => e.CloseSpace).HasColumnName("close_space");

                entity.Property(e => e.CloseTime).HasColumnName("close_time");

                entity.Property(e => e.CumulativeProbability).HasColumnName("cumulative_probability");

                entity.Property(e => e.NumberOfCases).HasColumnName("number_of_cases");
            });

            modelBuilder.Entity<SpatialRefSys>(entity =>
            {
                entity.HasKey(e => e.Srid);

                entity.ToTable("spatial_ref_sys");

                entity.Property(e => e.Srid)
                    .HasColumnName("srid")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthName).HasColumnName("auth_name");

                entity.Property(e => e.AuthSrid).HasColumnName("auth_srid");

                entity.Property(e => e.Proj4text).HasColumnName("proj4text");

                entity.Property(e => e.Srtext).HasColumnName("srtext");
            });
        }
    }
}
