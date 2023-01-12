using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PoriectSD.Models;

namespace PoriectSD.Database
{
    public partial class SDDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SDDbContext()
        {
        }

        public SDDbContext(DbContextOptions<SDDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Energy> Energies { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=host.docker.internal;Database=SD;Username=postgres;Password=calin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.MaxConsumption).HasColumnName("max_consumption");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Energy>(entity =>
            {
                entity.ToTable("Energy");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.DeviceId).HasColumnName("deviceId");

                entity.Property(e => e.EnergyConsumption).HasColumnName("energy_consumption");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
