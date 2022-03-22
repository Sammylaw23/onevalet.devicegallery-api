using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Repositories;
using OneValet.DeviceGallery.Infrastructure.Contexts;
using OneValet.DeviceGallery.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Infrastructure.Persistence
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly IApplicationDbContext _dbContext;
        private IDeviceRepository? _deviceRepository;
        private IUserRepository? _userRepository;

        public RepositoryProvider(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IDeviceRepository DeviceRepository => _deviceRepository ??= new DeviceRepository(_dbContext);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
    }
}
