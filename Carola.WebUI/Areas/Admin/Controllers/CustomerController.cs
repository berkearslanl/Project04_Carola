using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CustomerDtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> CustomerList()
        {
            ViewData["Title"] = "Müşteri Yönetimi";
            ViewData["Desc"] = "Müşteri Listesi";
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            ViewData["Title"] = "Müşteri Yönetimi";
            ViewData["Desc"] = "Müşteri Ekleme";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            ViewData["Title"] = "Müşteri Yönetimi";
            ViewData["Desc"] = "Müşteri Ekleme";
            try
            {
                await _customerService.CreateCustomerAsync(createCustomerDto);
                return RedirectToAction("CustomerList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createCustomerDto);
            }
        }
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList", new { Area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            ViewData["Title"] = "Müşteri Yönetimi";
            ViewData["Desc"] = "Müşteri Güncelleme";
            var value = await _customerService.GetCustomerById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            ViewData["Title"] = "Müşteri Yönetimi";
            ViewData["Desc"] = "Müşteri Güncelleme";
            try
            {
                await _customerService.UpdateCustomerAsync(updateCustomerDto);
                return RedirectToAction("CustomerList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateCustomerDto);
            }
        }
    }
}
