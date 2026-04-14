using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.ReservationDtos;
using FluentValidation;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;
using System.Threading.Tasks;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;
        private readonly IConfiguration _config;

        public ReservationController(
            IReservationService reservationService,
            ICustomerService customerService,
            ICarService carService,
            IConfiguration config)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _carService = carService;
            _config = config;
        }

        public async Task<IActionResult> ReservationList()
        {
            ViewData["Title"] = "Rezervasyon Yönetimi";
            ViewData["Desc"] = "Rezervasyon Listesi";
            var values = await _reservationService.GetAllReservationAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateReservation()
        {
            ViewData["Title"] = "Rezervasyon Yönetimi";
            ViewData["Desc"] = "Rezervasyon Ekleme";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
        {
            ViewData["Title"] = "Rezervasyon Yönetimi";
            ViewData["Desc"] = "Rezervasyon Ekleme";
            try
            {
                await _reservationService.CreateReservationAsync(createReservationDto);
                return RedirectToAction("ReservationList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(createReservationDto);
            }

        }
        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            ViewData["Title"] = "Rezervasyon Yönetimi";
            ViewData["Desc"] = "Rezervasyon Güncelleme";
            var value = await _reservationService.GetReservationById(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            ViewData["Title"] = "Rezervasyon Yönetimi";
            ViewData["Desc"] = "Rezervasyon Güncelleme";

            if (decimal.TryParse(
                    Request.Form["TotalPrice"],
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal parsedPrice))
            {
                updateReservationDto.TotalPrice = parsedPrice;
            }

            try
            {
                await _reservationService.UpdateReservationAsync(updateReservationDto);
                return RedirectToAction("ReservationList", new { Area = "Admin" });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(updateReservationDto);
            }
        }
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("ReservationList", new { Area = "Admin" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int reservationId, string status)
        {
            await _reservationService.UpdateReservationStatusAsync(reservationId, status);

            if (status == "Onaylandı")
            {
                var couponCode = "CRL-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();


                var reservation = await _reservationService.GetReservationById(reservationId);
                var customer    = await _customerService.GetCustomerById(reservation.CustomerId);
                var car         = await _carService.GetCarById(reservation.CarId);
                var mail        = _config.GetSection("MailSettings");

                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(mail["SenderName"], mail["SenderEmail"]));
                message.To.Add(new MailboxAddress(customer.FirstName + " " + customer.LastName, customer.Email));
                message.Subject = $"Rezervasyonunuz Onaylandı - #{reservationId}";
            
                message.Body = new TextPart("html")
                {
                    Text = $@"
<div style='font-family:sans-serif;max-width:600px;margin:auto;'>
    <div style='background:#0d6efd;padding:30px;text-align:center;border-radius:12px 12px 0 0;'>
        <h1 style='color:#fff;margin:0;'>Carola Araç Kiralama</h1>
    </div>
    <div style='background:#f8f9fb;padding:30px;border-radius:0 0 12px 12px;'>
        <h2 style='color:#1a1a2e;'>Merhaba {customer.FirstName},</h2>
        <p style='color:#555;'>Rezervasyonunuz onaylanmıştır. Detaylar aşağıdadır:</p>
        <div style='background:#fff;border-radius:10px;padding:20px;margin:20px 0;'>
            <div style='font-size:12px;font-weight:700;color:#888;text-transform:uppercase;letter-spacing:1px;margin-bottom:10px;'>Rezervasyon Bilgileri</div>
            <table style='width:100%;font-size:14px;color:#333;'>
                <tr><td style='padding:8px 0;color:#888;'>Rezervasyon No</td><td><strong>#{reservationId}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Alış Tarihi</td><td><strong>{reservation.PickupDate:dd.MM.yyyy}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>İade Tarihi</td><td><strong>{reservation.ReturnDate:dd.MM.yyyy}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Toplam Tutar</td><td><strong style='color:#0d6efd;'>{reservation.TotalPrice.ToString("N2")} ₺</strong></td></tr>
            </table>
            <hr style='border:none;border-top:1px solid #eee;margin:16px 0;'/>
            <div style='font-size:12px;font-weight:700;color:#888;text-transform:uppercase;letter-spacing:1px;margin-bottom:10px;'>Araç Bilgileri</div>
            <table style='width:100%;font-size:14px;color:#333;'>
                <tr><td style='padding:8px 0;color:#888;'>Araç</td><td><strong>{car.Brand} {car.Model}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Plaka</td><td><strong>{car.PlateNumber}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Yakıt Tipi</td><td><strong>{car.FuelType}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Vites</td><td><strong>{car.TransmissionType}</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Koltuk Sayısı</td><td><strong>{car.SeatCount} Kişilik</strong></td></tr>
                <tr><td style='padding:8px 0;color:#888;'>Model Yılı</td><td><strong>{car.ModelYear}</strong></td></tr>
            </table>
        </div>

        <div style='text-align:center;margin:24px 0;'>
            <img src='https://i.ibb.co/hRvphQp9/kupon.png' alt='Kupon' style='width:100%;border-radius:12px;' />
            <div style='background:#fff;border:2px dashed #0d6efd;border-radius:10px;padding:16px;margin-top:12px;'>
                <div style='font-size:13px;color:#888;margin-bottom:8px;'>
                    Sonraki kiralamanızda kullanabileceğiniz indirim kodunuz:
                </div>
                <div style='font-size:24px;font-weight:800;color:#0d6efd;letter-spacing:3px;'>
                    {couponCode}
                </div>
                <div style='font-size:12px;color:#aaa;margin-top:6px;'>%30 indirim • Tek kullanımlık</div>
            </div>
        </div>

        <p style='color:#555;'>İyi yolculuklar dileriz!</p>
        <p style='color:#aaa;font-size:12px;margin-top:30px;'>Bu mail otomatik olarak gönderilmiştir.</p>
    </div>
</div>"

                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(mail["Host"], int.Parse(mail["Port"]!), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(mail["SenderEmail"], mail["Password"]);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }

            return RedirectToAction("ReservationList");
        }

    }
}
