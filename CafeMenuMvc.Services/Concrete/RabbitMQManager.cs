using CafeMenuMvc.Services.Abstract;
using RabbitMQ.Client;
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
        private readonly string _connectionString;

        public RabbitMQManager(IConfigurationService configurationService)
        {

            _configurationService = configurationService;
            _connectionString = _configurationService.GetSetting("rabbitMQConnectionString");
        }


        public async Task<string> ReceiveMessage()
        {
            await Task.Run(() => { });
            return null;
            //throw new NotImplementedException();
        }

        public async Task<int> SendMessage(string header, string message)
        {
            try
            {
                await Task.Run(() =>
                {
                    var factory = new ConnectionFactory();
                    factory.Uri = new Uri(_connectionString);
                    using (var conn = factory.CreateConnection())
                    {
                        var channel = conn.CreateModel();
                        channel.QueueDeclare(header, true, false, false);
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(String.Empty, header, null, body);
                    }
                });

                return 1;

            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
