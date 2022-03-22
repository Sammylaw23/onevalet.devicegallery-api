using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OneValet.DeviceGallery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Infrastructure.Persistence;

namespace OneValet.DeviceGallery.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<DeviceDbContext>(options =>
                    options.UseInMemoryDatabase("OneValetDeviceGalleryDB"));
            }
            else
            {
                string appConnectionString = configuration.GetConnectionString("ApplicationDbConnectionString");
                services.AddDbContext<DeviceDbContext>(options => options.UseSqlServer(appConnectionString,
                    x => x.MigrationsAssembly(typeof(DeviceDbContext).Assembly.FullName)));
            }


            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<DeviceDbContext>());

            services.AddScoped<IRepositoryProvider, RepositoryProvider>();






        }
    }
}
