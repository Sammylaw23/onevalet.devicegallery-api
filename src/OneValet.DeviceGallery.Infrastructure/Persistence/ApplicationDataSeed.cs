using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.Application.Services;
using OneValet.DeviceGallery.Domain.Entities;
using OneValet.DeviceGallery.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Infrastructure.Persistence
{
    public class ApplicationDataSeed /*: IApplicationDataSeed*/
    {
        //private readonly IUserService _userService;
        //private readonly IRepositoryProvider _repositoryProvider;

        //public ApplicationDataSeed(IUserService userService, IRepositoryProvider repositoryProvider)
        //{
        //    _userService = userService;
        //    _repositoryProvider = repositoryProvider;
        //}


        //public static void SeedData(IHost app)
        //{
        //    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        //    using (var scope = scopedFactory.CreateScope())
        //    {
        //        var service = scope.ServiceProvider.GetService<ApplicationDataSeed>();
        //        service.d();
        //    }

        //}

        public static async Task DefaultUsersAsync(IUserService _userService, IRepositoryProvider _repositoryProvider)
        {
            var defaultUser = new UserRequest
            {
                //Id =
                FirstName = "One",
                LastName = "Valet",
                UserName = "onevalet",
                Email = "onevalet@gmail.com",
                Password = "sapass123$",
                ConfirmPassword = "sapass123$"
            };

            var userWithThisEmail = await _repositoryProvider.UserRepository.GetUserByEmailAsync(defaultUser.Email);
            if (userWithThisEmail == null)
            {
                await _userService.AddUserAsync(defaultUser);
            }
        }

        public static async Task DefaultDeviceTypesAsync(DeviceDbContext context)
        {
            //Phone, tablet, PC
            var deviceTypes = new List<DeviceType>
            {
                new DeviceType {
                    Name = "Phone",
                    Description = "A pocket-sized device"
                },
                 new DeviceType {
                    Name = "Tablet",
                    Description = "A palm-sized device"
                },
                  new DeviceType {
                    Name = "PC",
                    Description = "A personal computer"
                },
            };

            foreach (var deviceType in deviceTypes)
            {
                if (await context.DeviceTypes.AllAsync(x => x.Name != deviceType.Name))
                {
                    context.DeviceTypes.Add(deviceType);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task DefaultDevicesAsync(DeviceDbContext context)
        {
            var devices = new List<Device>
            {
                  new Device {
                    Name = "Nokia 7 Plus",
                    TemperatureC =  49,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = true
                  },
                  new Device {
                    Name = "iPad 11",
                    TemperatureC =  67,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  },
                  new Device {
                    Name = "HP Elitebook",
                    TemperatureC =  72,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  },
                  new Device {
                    Name = "Samsung Tablet",
                    TemperatureC =  31,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  },
                  new Device {
                    Name = "DELL 205",
                    TemperatureC =  55,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  },
                  new Device {
                    Name = "Tecno Spark 6",
                    TemperatureC =  84,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  },
                  new Device {
                    Name = "iPhone 13 Pro Max",
                    TemperatureC =  50,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = true
                  },
                  new Device {
                    Name = "Nokia 3310",
                    TemperatureC =  37,
                    IconBase64String = "FLKLihJHggJJKklKOhjGJkjKLkLJKjhjHHkhhjgJKJLklkh",
                    Online = false
                  }


                  //,new Device {
                  //  Name = "Nokia 7 Plus",
                  //  TemperatureC =  49,
                  //  IconBase64String = "iVBORw0KGgoAAAANSUhEUgAAAY0AAALQCAYAAABsXyxlAAAoxUlEQVR42u3de5Sdd13v8c/3+9szSdokM703e2ZPoU2BGhRUikqpBaX1Wp" +
                  //  "CLyhEFj+ClrnM86joC9YgXUFF0HZHlfYFAjx5EDlSgIAIKSFtUvBSktECStrP3TJo2tM2kmTSZ/ft9zx/z7GQnnWsymdl78n6tNSvNZGaSPvvZz/v5Pb/nYupNJilJK" +
                  //  "tVHR2o0Gk8ppTw5InZIukLSiKQLJJ0vaWNEDJrZYPUzAGDVRcSMpLaZHYmIfZL2SmpJutvdvxAROyPiS3v27JmeY7uXJUWv/r/12obVq39T7nxibGysUUp5tpldExFXRcSlk" +
                  //  "s6a40ViTQXQextZe/xmtpRyxN2bZna7mf3D4ODgP+7atWt39w5yFY5CNOaPhToLaGxs7LyIeEFE/EBEPNvdz64WdCcOnQUZ1f+D9WgEAZzhg46uX6NrG+VmdjQoEXEkIm5LKb3ny" +
                  //  "JEjN+/du/f++Xakz/RoHLdA6vX6Dnf/8Yj4ATPbZmbKOatruOYnRAIA+jUm0RWVZGZyd5VS9pnZeyPibRMTE5+da8f6TIyGVQshS9LIyMgzJP1PSS9098FqRJFPCAsArOeIdIKQUko" +
                  //  "qpYSkD7r77zabzU93/qz6ujU7Hr8WG2PvLJx6vf4EM3udpJe7e61rVEEoAJzpAenEQ+7+V2b2q+Pj41/qiseaHLJa7Q1zTVJb0kCj0fiZiHitmZ1PLABgTlmSp5SslHJA0v929zc1m83p" +
                  //  "ru3puoxGZx6iNBqNb5D0FklXVbFoV9UkFgAwfzxSSkk55/9w959ptVq3dm9bV+sfklbh7/BquBWjo6P/PSLeFRGXllLaOnZeMsEAgEW2oxGRzWxE0o+cc845ef/+/Z+utq+dU3T7fqSRJOV6" +
                  //  "vX62u/+Ju/9wu90+Wk3WAwBYtiLJUkoWER8qpfzY5OTkA1qlw1WnMxo1Se2xsbHL2+32X6eUnp5z5lAUAJy6kJRTSrVSyk4z+8FWq/XvqxGO07Xxrklqj46OPjsi3iPp4ohoV58HAKyMtpnVJB0o" +
                  //  "pfyXPXv2fOh0h+N0HCKqSWrX6/XvNLP3Szq3uuaCYKBf9uBiFUbiwEpwSdnMNprZ9w8PD39lamrq89X29rRMjq90NGqS2tu2bfted3+fpLMiooj5C/S+zgVTbhUdu2Gms3jQ4+Eomr2q/CWbN2/edeD" +
                  //  "AgTtOVzhWcmPeOSR1rZn9jaQNVTB4w6Hng2FmbmYWEQ9r9o6kbTM728w6b0hGHehlptmzq8LdXzg0NHT31NTUf56OcKzUBj1VI4xvioj3Egz0UzBSSu7unzGzF9ZqtcsHBga+ZtOmTZfXarXvcvePu7urB" +
                  //  "+82Csy1PY8IK6X8Rb1e/y4duw5uReu0EsHI27Zte6K7367ZSW+Cgb4Ihru7mf1Zs9n8ac1zW4ZGo/HGiHhtKYX1Gn0zcpa0392f02w279AK3nbkVKPhkuLiiy8+O6V0q6SncZYU+kQ2s2Rmt7VarWdXn6ud" +
                  //  "8MbqXJhaRkZG3i/p+dVJHczRoS/Wb0m72+32VdWt1ldkxHwqe02dO9VGSult7k4w0E+quW69oSsYbR1/y+pcrefm7r9mZqUrJEAvSxGR3f3SWq32F1rBx0r4KX5vu9Fo/EJK6QeqC/cIBvpBSPKIuP/RRx/" +
                  //  "9Zy38kJsiKZrN5uclfbkqDdFAX4Qj59xOKX17o9H4Ta3QnThONhqd24NcWUr5jXa7zZAdfRUNM1NE7H3kkUemFolA52lrbUkTXZ8D+iIc7Xa7HRGvGR0d/Q6twMT4yUTDJKler59tZu+UNND9eaBfmNlQ" +
                  //  "o9HYuIz1/hzWdfTbal6NqqOU8tZGo3G+jn/s7KpEo3MF4q+6+xXVPAajDPQTj9mHzY9FxBXVOu0LrO82MjIyVkq5onpGPdFAv63vJaU0Wkr5XZ3iBavL/cYkKY+NjT1L0s/n2QdiEAz0o+LuHhE3" +
                  //  "6tjFez7H+6NzcdRrzGyTjk2OA/0k5ZzbZvaKRqNxvU5hfmO532SSfPPmze+RNFoNczhvHX279+XuO4aGhg5MTU3d1jVs79yJuUjKjUbjFZLeUI1O2ElCP7NSyjO2bNny5wcOHJg53dFIksrY2N" +
                  //  "iPu/tPVQ9R4mwp9PUbKCKKmX3n1q1b68PDw1/Zv3//vioe0Wg0xrZs2fJLkt40e10fO0jo7/Vds7dTP7+UMv3oo4/+o07i4U22nK/bvn370KFDh76o2au+GWVgvSgpJc85Hzaz/zCzByUNlV" +
                  //  "KellIayjlz11usF6HZswcPpJS+dnx8vKVlPi52qSONmqRy1llnvdrMrufOtViHI44sacDMRiU9WdIlkjZWn3eCgXU02igppU2llI0HDhz4kJZ5wepS3gguKer1+gWSvijpXDGXgfW7F1Z" +
                  //  "OWPeJBdblaEPS4YGBgafdd999O5cz2ljKht8khbvfkFI6T7Oz7gQD63UvLHV9EAys69FGu93+eS3zug1byp9fdtllw9PT03e5+4Wcpw4A62K0ITM76O47ms1mc6mjjcVGDElSTE9Pv" +
                  //  "yyldFE1l0EwAKD/RxvZ3TeXUl61nNGGL/ZDn//859ci4iers6UAAOuDl1IiIn704osv3qwlPqEyLfJn5cCBA9eaWee4F2dMAcD6GW0UMxtOKd251MfDLjqh7e6vSCmFeNwlAKy" +
                  //  "/4YZ7SPrR6reLbudtgc/Htm3bLjKzuyUN6xTvjIg10f1AIRNnvQF4/DZCko7UarWvGx8f/7IWecLffBuRJMlqtdr3mNmwuElbvymavROxmZlXjzXtXMCTWTwAugYIOaW0o" +
                  //  "ZTyokW6sOAfFklRSvm+aujCJHgfBaMTCkkPmtm/SvpMRDSriCRxqBFAVzhKKYqI66vf5+VGwySVbdu2XSTp6pxz54In9EEw3N0l3R0RL5e0o9VqXTkxMfEsSVeY2XeY2",
                  //  Online = true
                  //}
            };
            foreach (var device in devices)
            {
                if (await context.Devices.AllAsync(x => x.Name != device.Name))
                {
                    context.Devices.Add(device);
                }
            }
            await context.SaveChangesAsync();
        }

    }
}
