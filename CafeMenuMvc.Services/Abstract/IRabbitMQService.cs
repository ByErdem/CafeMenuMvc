using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Abstract
{
    public interface IRabbitMQService
    {
        Task<int> SendMessage(string header, string message);
        Task<string> ReceiveMessage();
    }
}
