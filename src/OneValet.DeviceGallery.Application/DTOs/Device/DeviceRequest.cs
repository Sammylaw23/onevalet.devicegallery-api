using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.DTOs.Device
{
    public class DeviceRequest
    {
        public string? Name { get; set; }
        public double TemperatureC { get; set; }
        public string? IconBase64String { get; set; }
        public bool IsOnline { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
