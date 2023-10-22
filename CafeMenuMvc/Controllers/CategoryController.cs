﻿using CafeMenuMvc.Entity.Concrete;
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

        public async Task<ActionResult> Index()
        {
            var list = await _categoryService.GetAll();
            return View(list);
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

        public async Task<ActionResult> HardDelete(MCategory categoryDto)
        {
            var result = await _categoryService.HardDelete(categoryDto);
            return Json(result);
        }

        public async Task<ActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Json(result);
        }

        public async Task<ActionResult> GetAllByParentID(MCategory categoryDto)
        {
            var result = await _categoryService.GetAllByParentID(categoryDto);
            return Json(result);
        }

    }
}