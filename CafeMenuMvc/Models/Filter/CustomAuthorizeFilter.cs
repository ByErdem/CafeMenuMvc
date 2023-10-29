using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CafeMenuMvc.Models.Filter
{
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ITokenService _tokenService;

        public CustomAuthorizeFilter(IRedisCacheService redisCacheService, ITokenService tokenService)
        {
            _redisCacheService = redisCacheService;
            _tokenService = tokenService;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var exists = _redisCacheService.Any();
            if (!exists)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}