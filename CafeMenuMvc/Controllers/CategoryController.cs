﻿using CafeMenuMvc.Entity.Concrete;
using CafeMenuMvc.Entity.Dtos;
using CafeMenuMvc.Models.Attributes;
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

        [CustomAuthorize]
        public async Task<ActionResult> Index()
        {
            var list = await _categoryService.GetAll();
            return View(list);
        }

        [CustomAuthorize]
        public async Task<ActionResult> Create(CategoryDto categoryDto)
        {
            var result = await _categoryService.Create(categoryDto);
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> Update(CategoryDto categoryDto)
        {
            var result = await _categoryService.Update(categoryDto);
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> Delete(MCategory categoryDto)
        {
            var result = await _categoryService.Delete(categoryDto);
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> HardDelete(MCategory categoryDto)
        {
            var result = await _categoryService.HardDelete(categoryDto);
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> GetMainCategories()
        {
            var result = await _categoryService.GetMainCategories();
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> GetSubCategories(int categoryId)
        {
            var result = await _categoryService.GetSubCategories(categoryId);
            return Json(result);
        }

        [CustomAuthorize]

        public async Task<ActionResult> GetAllByParentID(MCategory categoryDto)
        {
            var result = await _categoryService.GetAllByParentID(categoryDto);
            return Json(result);
        }

    }
}