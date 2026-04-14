using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CategoryDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            ViewData["Title"] = "Kategori Yönetimi";
            ViewData["Desc"] = "Kategori Listesi";
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewData["Title"] = "Kategori Yönetimi";
            ViewData["Desc"] = "Kategori Ekleme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {
                await _categoryService.CreateCategoryAsync(createCategoryDto);
                return RedirectToAction("CategoryList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                return View(createCategoryDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            ViewData["Title"] = "Kategori Yönetimi";
            ViewData["Desc"] = "Kategori Güncelleme";
            var value = await _categoryService.GetCategoryById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(updateCategoryDto);
                return RedirectToAction("CategoryList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateCategoryDto);
            }
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList", new { area = "Admin" });
        }
    }
}
