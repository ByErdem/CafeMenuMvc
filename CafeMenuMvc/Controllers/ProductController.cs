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

        public async Task<ActionResult> Create(MProduct productDto)
        {
            var result = await _productService.Create(productDto);
            return Json(result);
        }

        public async Task<ActionResult> Update(MProduct productDto)
        {
            var result = await _productService.Update(productDto);
            return Json(result);
        }

        public async Task<ActionResult> Delete(MProduct productDto)
        {
            var result = await _productService.Delete(productDto);
            return Json(result);
        }
    }
}