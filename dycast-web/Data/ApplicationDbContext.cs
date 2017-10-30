using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dycast_web.Models;

namespace dycast_web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public virtual DbSet<DeadBirds> DeadBirds { get; set; }
        public virtual DbSet<DeadBirdsProjected> DeadBirdsProjected { get; set; }
        public virtual DbSet<DeadBirdsUnprojected> DeadBirdsUnprojected { get; set; }
        public virtual DbSet<EffectsPolys> EffectsPolys { get; set; }
        public virtual DbSet<EffectsPolysProjected> EffectsPolysProjected { get; set; }
        public virtual DbSet<EffectsPolysUnprojected> EffectsPolysUnprojected { get; set; }
        public virtual DbSet<Risk> Risk { get; set; }
        public virtual DbSet<RiskTableList> RiskTableList { get; set; }
        public virtual DbSet<SpatialRefSys> SpatialRefSys { get; set; }
        public virtual DbSet<TmpClusterPerPointSelection> TmpClusterPerPointSelection { get; set; }
        public virtual DbSet<TmpDailyCaseSelection> TmpDailyCaseSelection { get; set; }

        // Unable to generate entity type for table 'public.dist_margs'. Please see the warning messages.

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<DeadBirds>(entity =>
            {
                entity.HasKey(e => e.BirdId);

                entity.ToTable("dead_birds");

                entity.Property(e => e.BirdId)
                    .HasColumnName("bird_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<DeadBirdsProjected>(entity =>
            {
                entity.HasKey(e => e.BirdId);

                entity.ToTable("dead_birds_projected");

                entity.HasIndex(e => e.Location)
                    .HasName("dead_birds_projected_locationsidx")
                    .ForNpgsqlHasMethod("gist");

                entity.Property(e => e.BirdId)
                    .HasColumnName("bird_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<DeadBirdsUnprojected>(entity =>
            {
                entity.HasKey(e => e.BirdId);

                entity.ToTable("dead_birds_unprojected");

                entity.HasIndex(e => e.Location)
                    .HasName("dead_birds_unprojected_locationsidx")
                    .ForNpgsqlHasMethod("gist");

                entity.Property(e => e.BirdId)
                    .HasColumnName("bird_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<EffectsPolys>(entity =>
            {
                entity.HasKey(e => e.TileId);

                entity.ToTable("effects_polys");

                entity.Property(e => e.TileId)
                    .HasColumnName("tile_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.County).HasColumnName("county");
            });

            modelBuilder.Entity<EffectsPolysProjected>(entity =>
            {
                entity.HasKey(e => e.TileId);

                entity.ToTable("effects_polys_projected");

                entity.Property(e => e.TileId)
                    .HasColumnName("tile_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.TheGeom).HasColumnName("the_geom");
            });

            modelBuilder.Entity<EffectsPolysUnprojected>(entity =>
            {
                entity.HasKey(e => e.TileId);

                entity.ToTable("effects_polys_unprojected");

                entity.Property(e => e.TileId)
                    .HasColumnName("tile_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.County).HasColumnName("county");

                entity.Property(e => e.TheGeom).HasColumnName("the_geom");
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

                entity.Property(e => e.Nmcm).HasColumnName("nmcm");

                entity.Property(e => e.NumBirds).HasColumnName("num_birds");
            });

            modelBuilder.Entity<RiskTableList>(entity =>
            {
                entity.HasKey(e => e.TableId);

                entity.ToTable("risk_table_list");

                entity.Property(e => e.TableId)
                    .HasColumnName("table_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateGenerated)
                    .HasColumnName("date_generated")
                    .HasColumnType("date");

                entity.Property(e => e.MonteCarloId).HasColumnName("monte_carlo_id");

                entity.Property(e => e.Tablename).HasColumnName("tablename");
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

            modelBuilder.Entity<TmpClusterPerPointSelection>(entity =>
            {
                entity.HasKey(e => e.BirdId);

                entity.ToTable("tmp_cluster_per_point_selection");

                entity.HasIndex(e => e.Location)
                    .HasName("tmp_cluster_per_point_selection_locationsidx")
                    .ForNpgsqlHasMethod("gist");

                entity.Property(e => e.BirdId)
                    .HasColumnName("bird_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TmpDailyCaseSelection>(entity =>
            {
                entity.HasKey(e => e.BirdId);

                entity.ToTable("tmp_daily_case_selection");

                entity.HasIndex(e => e.Location)
                    .HasName("tmp_daily_case_selection_locationsidx")
                    .ForNpgsqlHasMethod("gist");

                entity.Property(e => e.BirdId)
                    .HasColumnName("bird_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.ReportDate)
                    .HasColumnName("report_date")
                    .HasColumnType("date");
            });
        }
    }
}
