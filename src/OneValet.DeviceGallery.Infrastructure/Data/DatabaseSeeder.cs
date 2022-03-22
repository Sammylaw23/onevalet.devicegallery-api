using OneValet.DeviceGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        public static List<DeviceType> SeedDeviceTypes()
        {
            return new List<DeviceType>
            {
                new DeviceType {
                     DeviceTypeId = 1,
                    Name = "Phone",
                    Description = "A pocket-sized device"
                },
                 new DeviceType {
                     DeviceTypeId = 2,
                    Name = "Tablet",
                    Description = "A palm-sized device"
                },
                  new DeviceType {
                        DeviceTypeId= 3,
                    Name = "PC",
                    Description = "A personal computer"
                },
            };
        }

        public static List<Device> SeedDevices()
        {
            return new List<Device>
            {
                  new Device {
                    Id = 1,
                    Name = "Nokia 7 Plus",
                    TemperatureC =  49,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = true,
                    DeviceTypeId = 1
                  },
                  new Device {
                      Id=2,
                    Name = "iPad 11",
                    TemperatureC =  67,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId = 2
                  },
                  new Device {
                      Id=3,
                    Name = "HP Elitebook",
                    TemperatureC =  72,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId = 3
                  },
                  new Device {
                      Id=4,
                    Name = "Samsung Tablet",
                    TemperatureC =  31,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId = 2
                  },
                  new Device {
                      Id=5,
                    Name = "DELL 205",
                    TemperatureC =  55,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId =3
                  },
                  new Device {
                      Id=6,
                    Name = "Tecno Spark 6",
                    TemperatureC =  84,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId = 1
                  },
                  new Device {
                      Id=7,
                    Name = "iPhone 13 Pro Max",
                    TemperatureC =  50,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = true,
                    DeviceTypeId = 1
                  },
                  new Device {
                      Id = 8,
                    Name = "Nokia 3310",
                    TemperatureC =  37,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    IsOnline = false,
                    DeviceTypeId = 1
                  }
            };
        }

        public static DeviceUser SeedUser()
        {
            return new DeviceUser
            {
                Id = 1,
                FirstName = "One",
                LastName = "Valet",
                UserName = "onevalet",
                Email = "onevalet@gmail.com",
                Password = "sapass123$",
                EmailConfirmed = true
            };
        }
    }
}
