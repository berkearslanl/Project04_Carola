using Carola.BusinessLayer.Abstract;
using Carola.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ReportsController(
            ICarService carService,
            IReservationService reservationService,
            ICustomerService customerService,
            ILocationService locationService,
            ICategoryService categoryService,
            IBrandService brandService)
        {
            _carService = carService;
            _reservationService = reservationService;
            _customerService = customerService;
            _locationService = locationService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var cars         = await _carService.GetAllCarAsync();
            var reservations = await _reservationService.GetAllReservationAsync();
            var customers    = await _customerService.GetAllCustomerAsync();
            var locations    = await _locationService.GetAllLocationAsync();
            var categories   = await _categoryService.GetAllCategoryAsync();
            var brands       = await _brandService.GetAllBrandAsync();

            var vm = new ReportsViewModel
            {
                TotalCars        = cars.Count,
                AvailableCars    = cars.Count(c => c.IsAvailable),
                RentedCars       = cars.Count(c => !c.IsAvailable),
                TotalReservations = reservations.Count,
                TotalRevenue     = reservations.Sum(r => r.TotalPrice),
                ActiveRevenue    = reservations
                    .Where(r => r.ReservationStatus == "Aktif" || r.ReservationStatus == "Onaylandı")
                    .Sum(r => r.TotalPrice),
                TotalCustomers   = customers.Count,
                TotalLocations   = locations.Count,
                TotalBrands      = brands.Count,
                TotalCategories  = categories.Count,

                CarsByFuelType = cars
                    .GroupBy(c => string.IsNullOrEmpty(c.FuelType) ? "Diğer" : c.FuelType)
                    .ToDictionary(g => g.Key, g => g.Count()),

                CarsByCategory = cars
                    .GroupBy(c => string.IsNullOrEmpty(c.CategoryName) ? "Diğer" : c.CategoryName)
                    .ToDictionary(g => g.Key, g => g.Count()),

                ReservationsByStatus = reservations
                    .GroupBy(r => string.IsNullOrEmpty(r.ReservationStatus) ? "Belirsiz" : r.ReservationStatus)
                    .ToDictionary(g => g.Key, g => g.Count()),
            };

            return View(vm);
        }
    }
}
