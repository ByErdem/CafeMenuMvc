using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMenuMvc.Services.Concrete
{
    public class ConfigurationService : IConfigurationService
    {
        public string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
