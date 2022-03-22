using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TemperatureC { get; set; }
        public string IconBase64String { get; set; }
        public bool IsOnline { get; set; }
        public int DeviceTypeId { get; set; } //FK
        //public DeviceType DeviceType { get; set; }
        //public string Usage { get; set; }
        //public List<Device> RelatedDevices { get; set; } list of Ids






    }



}
