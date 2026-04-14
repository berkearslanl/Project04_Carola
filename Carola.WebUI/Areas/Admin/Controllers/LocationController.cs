using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.LocationDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IActionResult> LocationList()
        {
            ViewData["Title"] = "Lokasyon Yönetimi";
            ViewData["Desc"] = "Lokasyon Listesi";
            var values = await _locationService.GetAllLocationAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateLocation()
        {
            ViewData["Title"] = "Lokasyon Yönetimi";
            ViewData["Desc"] = "Lokasyon Ekleme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationDto createLocationDto)
        {
            try
            {
                await _locationService.CreateLocationAsync(createLocationDto);
                return RedirectToAction("LocationList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createLocationDto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            ViewData["Title"] = "Lokasyon Yönetimi";
            ViewData["Desc"] = "Lokasyon Güncelleme";
            var value = await _locationService.GetLocationById(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocation(UpdateLocationDto updateLocationDto)
        {
            try
            {
                await _locationService.UpdateLocationAsync(updateLocationDto);
                return RedirectToAction("LocationList", new { area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateLocationDto);
            }
        }

        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return RedirectToAction("LocationList", new { area = "Admin" });
        }
    }
}
