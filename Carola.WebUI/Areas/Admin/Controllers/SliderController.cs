using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.SliderDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> SliderList()
        {
            ViewData["Title"] = "Slider Yönetimi";
            ViewData["Desc"] = "Slider Listesi";
            var values = await _sliderService.GetAllSliderAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            ViewData["Title"] = "Slider Yönetimi";
            ViewData["Desc"] = "Slider Ekleme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {
            try
            {
                await _sliderService.CreateSliderAsync(createSliderDto);
                return RedirectToAction("SliderList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createSliderDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            ViewData["Title"] = "Slider Yönetimi";
            ViewData["Desc"] = "Slider Güncelleme";
            var value = await _sliderService.GetSliderById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            try
            {
                await _sliderService.UpdateSliderAsync(updateSliderDto);
                return RedirectToAction("SliderList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateSliderDto);
            }
        }

        public async Task<IActionResult> DeleteSlider(int id)
        {
            await _sliderService.DeleteSliderAsync(id);
            return RedirectToAction("SliderList", new { Area = "Admin" });
        }
    }
}
