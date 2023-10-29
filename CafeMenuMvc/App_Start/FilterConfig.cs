using CafeMenuMvc.Models.Filter;
using CafeMenuMvc.Services.Abstract;
using System.Web;
using System.Web.Mvc;

namespace CafeMenuMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IRedisCacheService redisCacheService, ITokenService tokenService)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeFilter(redisCacheService,tokenService));
        }
    }
}
