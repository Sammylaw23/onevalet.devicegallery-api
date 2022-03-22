using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Wrappers
{

    public interface IResponse
    {
        public bool Succeeded { get; set; }
        public List<string> Messages { get; set; }

    }

    public interface IResponse<T>
    {
        public T Data { get; set; }
    }

}
