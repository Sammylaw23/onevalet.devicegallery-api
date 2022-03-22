using OneValet.DeviceGallery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Infrastructure.Data;

namespace OneValet.DeviceGallery.Infrastructure.Contexts
{
    public class DeviceDbContext : DbContext, IApplicationDbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options)
        {
        }

        public DbSet<DeviceUser> DeviceUsers { get; set; }
        public DbSet<Domain.Entities.Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DeviceUsers table
            var deviceUser = modelBuilder.Entity<DeviceUser>().ToTable("DeviceUsers");
            deviceUser.HasKey(u => u.Id);
            deviceUser.Property(x => x.Id).UseIdentityColumn();
            deviceUser.Property(x => x.FirstName).HasMaxLength(55);
            deviceUser.Property(x => x.LastName).HasMaxLength(55);
            deviceUser.Property(x => x.UserName).HasMaxLength(55);
            deviceUser.Property(x => x.Email).HasMaxLength(55);
            deviceUser.Property(x => x.Password).HasMaxLength(55);
            
            deviceUser.HasData(DatabaseSeeder.SeedUser());

            //Devices table
            var device = modelBuilder.Entity<Device>().ToTable("Devices");
            device.HasKey(x => x.Id);
            device.Property(x => x.Id).UseIdentityColumn();
            device.Property(x => x.DeviceTypeId).ValueGeneratedNever();
            device.Property(x => x.IconBase64String).HasMaxLength(255);
            device.Property(x => x.Name).HasMaxLength(60);
            device.HasIndex(x => x.Name).IsUnique();

            device.HasData(DatabaseSeeder.SeedDevices());

            //DeviceTypes table
            var deviceType = modelBuilder.Entity<DeviceType>().ToTable("DeviceTypes");
            deviceType.HasKey(x => x.DeviceTypeId);
            deviceType.Property(x => x.DeviceTypeId).UseIdentityColumn();
            deviceType.Property(x => x.Name).HasMaxLength(60);
            deviceType.Property(x => x.Description).HasMaxLength(255);
       
            deviceType.HasData(DatabaseSeeder.SeedDeviceTypes());
        }

    }
}
