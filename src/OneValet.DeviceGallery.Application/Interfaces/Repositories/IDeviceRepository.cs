using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Application.ResourceParameters;
using OneValet.DeviceGallery.Application.Wrappers;
using OneValet.DeviceGallery.Domain.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Interfaces.Repositories
{
    public interface IDeviceRepository
    {
        Task CreateDeviceAsync(Domain.Entities.Device device);
        Task AddMultipleDevicesAsync(IEnumerable<Domain.Entities.Device> devices);
        Task<Domain.Entities.Device> GetDeviceByIdAsync(int id);
        Task<PagedList<Domain.Entities.Device>> GetAllDeviceAsync(DeviceParameters deviceParameters);
        Task<IEnumerable<Domain.Entities.Device>> GetAllDeviceAsync();
        Task<IEnumerable<Domain.Entities.Device>> GetAllDeviceAsync(DevicesResourceParameters devicesResourceParameters);
        void UpdateDevice(Domain.Entities.Device device);
        void DeleteDevice(Domain.Entities.Device device);
        Task<bool> DeviceTypeExistAsync(int deviceTypeId);
    }
}
