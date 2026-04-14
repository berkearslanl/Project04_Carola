using Carola.BusinessLayer.Abstract;
using Carola.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ICarService _carService;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public DashboardController(
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

            var vm = new DashboardViewModel
            {
                TotalCars          = cars.Count,
                AvailableCars      = cars.Count(c => c.IsAvailable),
                TotalReservations  = reservations.Count,
                ActiveReservations = reservations.Count(r => r.ReservationStatus == "Aktif" || r.ReservationStatus == "Onaylandı"),
                TotalCustomers     = customers.Count,
                TotalLocations     = locations.Count,
                TotalCategories    = categories.Count,
                TotalBrands        = brands.Count,
                TotalRevenue       = reservations.Sum(r => r.TotalPrice),

                RecentReservations = reservations.OrderByDescending(r => r.ReservationId).Take(5).ToList(),
                RecentCars         = cars.OrderByDescending(c => c.CarId).Take(6).ToList(),
            };

            return View(vm);
        }
    }
}
