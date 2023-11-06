using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class RabbitMQManager : IRabbitMQService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IHttpService _httpService;
        private readonly string _connectionString;


        public RabbitMQManager(IConfigurationService configurationService, IHttpService httpService)
        {

            _configurationService = configurationService;
            _connectionString = _configurationService.GetSetting("rabbitMQConnectionString");
            _httpService = httpService;
        }


        public async Task<string> ReceiveMessage()
        {
            await Task.Run(() => { });
            return null;
            //throw new NotImplementedException();
        }

        public async Task<int> SendMessage(string header, string message)
        {
            var dto = new RabbitDto
            {
                Header = header,
                Message = message
            };
            var result = await _httpService.SendHttpPostRequestAsync("http://localhost:5131/api/controller/SendMessage", JsonConvert.SerializeObject(dto));
            return result;
        }
    }
}
