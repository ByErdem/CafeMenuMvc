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
            var result = await _productPropertyService.AddProperty(productPropertyDto);
            return Json(result);
        }

        public async Task<ActionResult> Update(ProductPropertyDto productPropertyDto)
        {
            var result = await _productPropertyService.UpdateProperty(productPropertyDto);
            return Json(result);
        }

        public async Task<ActionResult> Delete(ProductPropertyDto productPropertyDto)
        {
            var result = await _productPropertyService.DeleteProperty(productPropertyDto);
            return Json(result);
        }
    }
}