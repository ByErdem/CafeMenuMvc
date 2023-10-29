using AutoMapper;
using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.Attributes;
using CafeMenuMvc.Services.Abstract;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;


        public LoginController(IUserService userService)
        { 
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        public async Task<ActionResult> SignIn(UserLoginDto user)
        {
            ResponseDto<UserParameter> result = null;

            try
            {
                result = await _userService.SignIn(user);
            }
            catch (System.Exception ex)
            {

            }


            return Json(result);
        }

        public async Task<ActionResult> Register(UserRegisterDto user)
        {
            var result = await _userService.Register(user);
            return Json(result);
        }
    }
}