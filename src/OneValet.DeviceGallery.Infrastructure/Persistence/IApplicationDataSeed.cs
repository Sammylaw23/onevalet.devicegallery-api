using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.Infrastructure.Contexts;

namespace OneValet.DeviceGallery.Infrastructure.Persistence
{
    public interface IApplicationDataSeed
    {
        Task DefaultDevicesAsync(DeviceDbContext context);
        Task DefaultDeviceTypesAsync(DeviceDbContext context);
        Task DefaultUsersAsync();
        //Task DefaultUsersAsync(IUserService userService, RepositoryProvider repositoryProvider);
    }
}