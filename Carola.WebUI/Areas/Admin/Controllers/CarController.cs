using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;

        public CarController(ICarService carService, ICategoryService categoryService)
        {
            _carService = carService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CarList()
        {
            ViewData["Title"] = "Araç Yönetimi";
            ViewData["Desc"] = "Araç Listesi";
            var values = await _carService.GetAllCarAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            ViewData["Title"] = "Araç Yönetimi";
            ViewData["Desc"] = "Araç Ekleme";
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            try
            {
                await _carService.CreateCarAsync(createCarDto);
                return RedirectToAction("CarList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
                return View(createCarDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            ViewData["Title"] = "Araç Yönetimi";
            ViewData["Desc"] = "Araç Güncelleme";
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            var value = await _carService.GetCarById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
        {
            try
            {
                await _carService.UpdateCarAsync(updateCarDto);
                return RedirectToAction("CarList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
                return View(updateCarDto);
            }
        }

        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction("CarList", new { area = "Admin" });
        }
    }
}
