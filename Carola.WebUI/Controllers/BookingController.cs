using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
using Carola.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Carola.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICarService _carService;
        private readonly ILocationService _locationService;
        private readonly ICustomerService _customerService;
        private readonly IReservationService _reservationService;
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(ICarService carService, ILocationService locationService, ICustomerService customerService, IReservationService reservationService, IHttpClientFactory httpClientFactory)
        {
            _carService = carService;
            _locationService = locationService;
            _customerService = customerService;
            _reservationService = reservationService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(
    int carId,
    string pickupDate = null,
    string returnDate = null,
    int? pickupLocationId = null,
    int? returnLocationId = null)
        {
            var car = await _carService.GetCarById(carId);
            var locations = await _locationService.GetAllLocationAsync();

            var vm = new BookingViewModel
            {
                Car = car,
                Locations = locations,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                PickupLocationId = pickupLocationId,
                ReturnLocationId = returnLocationId
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(int carId,
                                            string firstName,
                                            string lastName,
                                            string email,
                                            string phone,
                                            string driverLicenseNumber,
                                            DateTime birthDate,
                                            string pickupDate,
                                            string returnDate,
                                            int pickupLocationIdForm,
                                            int returnLocationIdForm,
                                            decimal totalPrice,
                                            string reservationStatus,
                                            string description)
        {
            try
            {
                var customer = new CreateCustomerDto
                {
                    FirstName = firstName?.Trim(),
                    LastName = lastName?.Trim(),
                    Email = email?.Trim(),
                    Phone = phone?.Trim(),
                    DriverLicenseNumber = driverLicenseNumber?.Trim().ToUpperInvariant(),
                    BirthDate = birthDate
                };
                await _customerService.CreateCustomerAsync(customer);

                var allCustomers = await _customerService.GetAllCustomerAsync();
                var newCustomer = allCustomers.FirstOrDefault(x => x.Email == email);
                if (newCustomer == null)
                    throw new Exception("Müşteri kaydı oluşturulamadı!");

                var reservation = new CreateReservationDto
                {
                    CarId = carId,
                    CustomerId = newCustomer.CustomerId,
                    PickupDate = DateTime.Parse(pickupDate),
                    ReturnDate = DateTime.Parse(returnDate),
                    PickupLocationId = pickupLocationIdForm,
                    ReturnLocationId = returnLocationIdForm,
                    TotalPrice = totalPrice,
                    ReservationStatus = reservationStatus,
                    Description = description ?? ""
                };
                await _reservationService.CreateReservationAsync(reservation);
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                try
                {
                    var allCustomer2 = await _customerService.GetAllCustomerAsync();
                    var orphan = allCustomer2.FirstOrDefault(x => x.Email == email);
                    if (orphan != null)
                        await _customerService.DeleteCustomerAsync(orphan.CustomerId);
                }
                catch { }

                var message = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = message;
                Debug.WriteLine("[Confirm] HATA: " + ex.Message);
                Debug.WriteLine("[Confirm] Inner: " + ex.InnerException?.Message);

                var car = await _carService.GetCarById(carId);
                var locations = await _locationService.GetAllLocationAsync();
                var vm = new BookingViewModel
                {
                    Car = car,
                    Locations = locations,
                    PickupDate = pickupDate,
                    ReturnDate = returnDate,
                    PickupLocationId = pickupLocationIdForm,
                    ReturnLocationId = returnLocationIdForm
                };
                return View("Index", vm);
            }


        }
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ScanLicense(IFormFile photo)
        {
            try
            {
                if (photo == null || photo.Length == 0)
                    return BadRequest(new { error = "Dosya yüklenemedi." });

                // Dosya boyutu kontrolü (max 4MB — base64 şişirme payı için)
                if (photo.Length > 4 * 1024 * 1024)
                    return BadRequest(new { error = "Dosya çok büyük. Maks 4MB yükleyin." });

                using var ms = new MemoryStream();
                await photo.CopyToAsync(ms);
                var base64 = Convert.ToBase64String(ms.ToArray());
                var mediaType = photo.ContentType;

                Debug.WriteLine($"[ScanLicense] Dosya: {photo.FileName}, Boyut: {photo.Length}, Tip: {mediaType}");

                var client = _httpClientFactory.CreateClient("AnthropicClient");

                var body = new
                {
                    model = "claude-opus-4-6",
                    max_tokens = 512,
                    messages = new[]
                    {
                    new
                    {
                        role="user",
                        content=new object[]
                        {
                            new
                            {
                                type="image",
                                source = new
                                {
                                    type="base64",
                                    media_type=mediaType,
                                    data=base64
                                }
                            },
                            new
                            {
                                type="text",
                                text = "Bu bir Türk sürücü belgesidir. Türk ehliyetlerinde alanlar numaralandırılmıştır. Görseli dikkatlice incele ve şu alanları oku:\n- 1. alan: Soyad\n- 2. alan: Ad\n- 3. alan: Doğum tarihi (GG.AA.YYYY formatında, JSON'a YYYY-MM-DD olarak yaz)\n- 4d. alan: TC Kimlik Numarası (11 haneli)\n- 5. alan: Ehliyet belge numarası\nOkuyamazsan ilgili alanı boş bırak. YALNIZCA şu JSON formatında yanıt ver, başka hiçbir şey yazma:\n{\"firstName\": \"...\", \"lastName\": \"...\", \"tcNo\": \"...\", \"birthDate\": \"YYYY-MM-DD\", \"licenseNo\": \"...\"}"
                            }
                        }
                    }
                }
                };

                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/v1/messages", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errBody = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("[ScanLicense] Claude API Hata: " + errBody);
                    return StatusCode(500, new { error = "Claude API hatası: " + errBody });
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("[ScanLicense] Claude ham yanıt: " + responseBody);

                // İlk "text" tipindeki bloğu bul (thinking bloklarını atla)
                string? claudeText = null;
                using (var doc = JsonDocument.Parse(responseBody))
                {
                    var contentArr = doc.RootElement.GetProperty("content");
                    foreach (var block in contentArr.EnumerateArray())
                    {
                        if (block.TryGetProperty("type", out var typeEl) &&
                            typeEl.GetString() == "text" &&
                            block.TryGetProperty("text", out var textEl))
                        {
                            claudeText = textEl.GetString(); // string olarak kopyala, doc dispose olunca kaybolmaz
                            break;
                        }
                    }
                } // doc burada dispose olur ama claudeText string olarak elimizde

                Debug.WriteLine("[ScanLicense] Claude metin yanıtı: " + claudeText);

                // Claude bazen JSON etrafına açıklama ekler — regex ile çıkar
                var jsonMatch = System.Text.RegularExpressions.Regex.Match(
                    claudeText ?? "",
                    @"\{[^{}]*\}",
                    System.Text.RegularExpressions.RegexOptions.Singleline);

                if (!jsonMatch.Success)
                {
                    Debug.WriteLine("[ScanLicense] JSON bulunamadı — Claude yanıtı: " + claudeText);
                    return Ok(new { firstName = "", lastName = "", birthDate = "", licenseNo = "" });
                }

                try
                {
                    using var result = JsonDocument.Parse(jsonMatch.Value);
                    var root = result.RootElement;
                    // JsonDocument dispose olmadan önce değerleri string'e çekiyoruz
                    var parsed = new
                    {
                        firstName = root.TryGetProperty("firstName", out var fn) ? fn.GetString() ?? "" : "",
                        lastName = root.TryGetProperty("lastName", out var ln) ? ln.GetString() ?? "" : "",
                        tcNo = root.TryGetProperty("tcNo", out var tc) ? tc.GetString() ?? "" : "",
                        birthDate = root.TryGetProperty("birthDate", out var bd) ? bd.GetString() ?? "" : "",
                        licenseNo = root.TryGetProperty("licenseNo", out var lno) ? lno.GetString() ?? "" : ""
                    };
                    return Ok(parsed);
                }
                catch
                {
                    Debug.WriteLine("[ScanLicense] JSON parse hatası: " + jsonMatch.Value);
                    return Ok(new { firstName = "", lastName = "", birthDate = "", licenseNo = "" });
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ScanLicense] GENEL HATA: " + ex.Message);
                Debug.WriteLine(ex.StackTrace);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
