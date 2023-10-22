using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Services.Abstract;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class ProductPropertyController : Controller
    {
        private readonly IProductPropertyService _productPropertyService;

        public ProductPropertyController(IProductPropertyService productPropertyService)
        {
            _productPropertyService = productPropertyService;
        }

        public async Task<ActionResult> Create(ProductPropertyDto productPropertyDto)
        {
            var result = await _productPropertyService.Create(productPropertyDto);
            return Json(result);
        }

        public async Task<ActionResult> Update(ProductPropertyDto productPropertyDto)
        {
            var result = await _productPropertyService.Update(productPropertyDto);
            return Json(result);
        }

        public async Task<ActionResult> Delete(ProductPropertyDto productPropertyDto)
        {
            var result = await _productPropertyService.Delete(productPropertyDto);
            return Json(result);
        }
    }
}