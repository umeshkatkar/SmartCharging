using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SmartCharging
{
    public partial class DBSmartChargingContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection DbConnection { get; }
        public DBSmartChargingContext()
        {
        }

        public DBSmartChargingContext(DbContextOptions<DBSmartChargingContext> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
            DbConnection = new SqlConnection(this._configuration.GetConnectionString("DevConnection"));
        }

        public virtual DbSet<ChargingConnector> ChargingConnector { get; set; }
        public virtual DbSet<ChargingGroup>  ChargingGroup { get; set; }
        public virtual DbSet<ChargingStation> ChargingStation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
             optionsBuilder.UseSqlServer(DbConnection.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChargingConnector>(entity =>
            {
                entity.HasKey(e => e.ConnectorId);

                entity.Property(e => e.ConnectorIdentifier)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ChargingGroup>(entity =>
            {
                entity.HasKey(e => e.GroupIdentifier);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ChargingStation>(entity =>
            {
                entity.HasKey(e => e.StationIdentifier);

                entity.Property(e => e.StationName)
                    .IsRequired()
                    .HasMaxLength(100);

            });
            modelBuilder.Entity<ChargingStation>()
             .HasOne<ChargingGroup>(s => s.ChargingGroup)
             .WithMany(g => g.ChargingStations)
             .HasForeignKey(s => s.GroupIdentifier);

            modelBuilder.Entity<ChargingConnector>()
             .HasOne<ChargingStation>(s => s.ChargingStation)
             .WithMany(g => g.ChargingConnectors)
             .HasForeignKey(s => s.StationIdentifier);

            //modelBuilder.Entity<ChargingStation>().Ignore(t => t.ChargingGroup);
            //base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
