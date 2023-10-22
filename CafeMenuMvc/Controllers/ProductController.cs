using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> SignIn(MProduct productDto)
        {
            var result = await _productService.Create(productDto);
            return Json(result);
        }
    }
}