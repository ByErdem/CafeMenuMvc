using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Services.Abstract;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(MCategory categoryDto)
        {
            var result = await _categoryService.Create(categoryDto);
            return Json(result);
        }

        public async Task<ActionResult> Update(MCategory categoryDto)
        {
            var result = await _categoryService.Update(categoryDto);
            return Json(result);
        }

        public async Task<ActionResult> Delete(MCategory categoryDto)
        {
            var result = await _categoryService.Delete(categoryDto);
            return Json(result);
        }

    }
}