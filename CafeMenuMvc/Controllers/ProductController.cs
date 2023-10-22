using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUserService _userService;

        public ProductController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SignIn(UserLoginDto user)
        {
            var result = await _userService.SignIn(user);
            return Json(result);
        }
    }
}