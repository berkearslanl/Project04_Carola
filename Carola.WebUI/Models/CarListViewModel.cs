using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.DtoLayer.Dtos.LocationDtos;

namespace Carola.WebUI.Models
{
    public class CarListViewModel
    {
        public List<ResultCarDto> Cars { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        //bu gün araç rezervasyonu dolu mu?
        public HashSet<int> CurrentlyRentedCarIds { get; set; } = new();

        // Filtre değerleri
        public int? CarId { get; set; }
        public int? PickupLocationId { get; set; }
        public int? ReturnLocationId { get; set; }
        public string PickupDate { get; set; }
        public string ReturnDate { get; set; }
        public int? CategoryId { get; set; }
        public string FuelType { get; set; }
        public string TransmissionType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchTerm { get; set; }
        public List<int> SeatCounts { get; set; } = new();

        // Sidebar dropdown verileri
        public List<ResultCategoryDto> Categories { get; set; } = new();
        public List<ResultLocationDto> Locations { get; set; } = new();
    }
}
