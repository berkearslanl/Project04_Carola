using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.WhyUsDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhyUsController : Controller
    {
        private readonly IWhyUsService _WhyUsService;

        public WhyUsController(IWhyUsService WhyUsService)
        {
            _WhyUsService = WhyUsService;
        }
        public async Task<IActionResult> WhyUsList()
        {
            ViewData["Title"] = "Neden Biz Yönetimi";
            ViewData["Desc"] = "Neden Biz Listesi";

            var values = await _WhyUsService.GetAllWhyUsAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateWhyUs()
        {
            ViewData["Title"] = "Neden Biz Yönetimi";
            ViewData["Desc"] = "Neden Biz Ekleme";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateWhyUs(CreateWhyUsDto createWhyUsDto)
        {
            try
            {
                await _WhyUsService.CreateWhyUsAsync(createWhyUsDto);
                return RedirectToAction("WhyUsList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createWhyUsDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateWhyUs(int id)
        {
            ViewData["Title"] = "Neden Biz Yönetimi";
            ViewData["Desc"] = "Neden Biz Güncelleme";
            var value = await _WhyUsService.GetWhyUsById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWhyUs(UpdateWhyUsDto updateWhyUsDto)
        {
            try
            {
                await _WhyUsService.UpdateWhyUsAsync(updateWhyUsDto);
                return RedirectToAction("WhyUsList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateWhyUsDto);
            }
        }
        public async Task<IActionResult> DeleteWhyUs(int id)
        {
            await _WhyUsService.DeleteWhyUsAsync(id);
            return RedirectToAction("WhyUsList", new { area = "Admin" });
        }
    }
}
