using Carola.BusinessLayer.Abstract;
using Carola.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    public class CarListController : Controller
    {
        private readonly ICarService _carService;
        private readonly IReservationService _reservationService;
        private readonly ICategoryService _categoryService;
        private readonly ILocationService _locationService;

        public CarListController(
            ICarService carService,
            IReservationService reservationService,
            ICategoryService categoryService,
            ILocationService locationService)
        {
            _carService = carService;
            _reservationService = reservationService;
            _categoryService = categoryService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int? carId              = null,
            int? pickupLocationId   = null,
            int? returnLocationId   = null,
            string pickupDate       = null,
            string returnDate       = null,
            int? categoryId         = null,
            string fuelType         = null,
            string transmissionType = null,
            decimal? minPrice       = null,
            decimal? maxPrice       = null,
            string searchTerm       = null,
            List<int> seatCounts    = null)
        {
            int pageSize = 6;

            // 1) Tüm araçları çek
            var allCars = await _carService.GetAllCarAsync();
            var filtered = allCars.AsEnumerable();

            // 2) Filtreler
            if (carId.HasValue)
                filtered = filtered.Where(c => c.CarId == carId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                filtered = filtered.Where(c =>
                    (c.Brand + " " + c.Model).Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (categoryId.HasValue)
                filtered = filtered.Where(c => c.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(fuelType))
                filtered = filtered.Where(c => c.FuelType == fuelType);

            if (!string.IsNullOrWhiteSpace(transmissionType))
                filtered = filtered.Where(c => c.TransmissionType == transmissionType);

            if (minPrice.HasValue)
                filtered = filtered.Where(c => c.DailyPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                filtered = filtered.Where(c => c.DailyPrice <= maxPrice.Value);

            if (seatCounts != null && seatCounts.Any())
                filtered = filtered.Where(c => seatCounts.Contains(c.SeatCount));

            // 3) Rezervasyonları çek (hem tarih filtresi hem bugün kirada kontrolü için)
            var reservations = await _reservationService.GetAllReservationAsync();

            // Bugün aktif rezervasyonu olan araçlar
            var today = DateTime.Today;
            var currentlyRentedCarIds = reservations
                .Where(r => r.ReservationStatus != "İptal"
                         && r.PickupDate <= today
                         && r.ReturnDate >= today)
                .Select(r => r.CarId)
                .ToHashSet();

            // Tarih filtresi
            if (DateTime.TryParse(pickupDate, out var pickup) &&
                DateTime.TryParse(returnDate, out var returnD) &&
                pickup < returnD)
            {
                var busyCarIds = reservations
                    .Where(r => r.ReservationStatus != "İptal"
                             && r.PickupDate < returnD
                             && r.ReturnDate > pickup)
                    .Select(r => r.CarId)
                    .ToHashSet();

                filtered = filtered.Where(c => !busyCarIds.Contains(c.CarId));
            }


            // 4) Sayfalama
            var filteredList = filtered.ToList();
            int totalCount  = filteredList.Count;
            int totalPages  = (int)Math.Ceiling((double)totalCount / pageSize);
            page            = Math.Clamp(page, 1, Math.Max(1, totalPages));

            var pagedCars = filteredList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 5) ViewModel
            var vm = new CarListViewModel
            {
                Cars            = pagedCars,
                CurrentPage     = page,
                TotalPages      = totalPages,

                CarId             = carId,
                PickupLocationId  = pickupLocationId,
                ReturnLocationId  = returnLocationId,
                PickupDate        = pickupDate,
                ReturnDate        = returnDate,
                CategoryId        = categoryId,
                FuelType          = fuelType,
                TransmissionType  = transmissionType,
                MinPrice          = minPrice,
                MaxPrice          = maxPrice,
                SearchTerm        = searchTerm,
                SeatCounts        = seatCounts ?? new List<int>(),
                CurrentlyRentedCarIds = currentlyRentedCarIds,

                Categories = await _categoryService.GetAllCategoryAsync(),
                Locations  = await _locationService.GetAllLocationAsync(),
            };

            return View(vm);
        }
    }
}
