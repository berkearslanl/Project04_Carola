using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.LocationDtos;

namespace Carola.WebUI.Models
{
    public class BookingViewModel
    {
        // Seçilen araç
        public GetCarByIdDto Car { get; set; }

        // Lokasyon listesi (dropdown için)
        public List<ResultLocationDto> Locations { get; set; } = new();

        // Tarih ve lokasyon bilgileri (query string'den gelir)
        public string PickupDate { get; set; }
        public string ReturnDate { get; set; }
        public int? PickupLocationId { get; set; }
        public int? ReturnLocationId { get; set; }
    }
}
