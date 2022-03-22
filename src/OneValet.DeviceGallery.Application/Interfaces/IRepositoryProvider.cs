using OneValet.DeviceGallery.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Interfaces
{
    public interface IRepositoryProvider
    {
        public IDeviceRepository DeviceRepository { get; }
        public IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}
