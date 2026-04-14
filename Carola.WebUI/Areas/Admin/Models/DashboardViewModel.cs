using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;

namespace Carola.WebUI.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int TotalReservations { get; set; }
        public int ActiveReservations { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalLocations { get; set; }
        public int TotalCategories { get; set; }
        public int TotalBrands { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<ResultReservationDto> RecentReservations { get; set; } = new();
        public List<ResultCarDto> RecentCars { get; set; } = new();
    }
}
