namespace Carola.WebUI.Areas.Admin.Models
{
    public class ReportsViewModel
    {
        // Araç
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int RentedCars { get; set; }

        // Yakıt tipi dağılımı → Chart.js için label + veri
        public Dictionary<string, int> CarsByFuelType { get; set; } = new();

        // Kategori dağılımı
        public Dictionary<string, int> CarsByCategory { get; set; } = new();

        // Rezervasyon
        public int TotalReservations { get; set; }
        public Dictionary<string, int> ReservationsByStatus { get; set; } = new();

        // Gelir
        public decimal TotalRevenue { get; set; }
        public decimal ActiveRevenue { get; set; }

        // Diğer
        public int TotalCustomers { get; set; }
        public int TotalLocations { get; set; }
        public int TotalBrands { get; set; }
        public int TotalCategories { get; set; }
    }
}
