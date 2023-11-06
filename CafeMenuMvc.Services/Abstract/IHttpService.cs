using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface IHttpService
    {
        Task<int> SendHttpPostRequestAsync(string apiUrl, string data);
    }
}
