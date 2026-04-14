using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Carola.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Message))
                return BadRequest(new { error = "Mesaj boş olamaz." });

            var client = _httpClientFactory.CreateClient("AnthropicClient");

            var systemPrompt = @"Sen Carola adlı bir araç kiralama şirketinin deneyimli ve yardımsever müşteri temsilcisisin.
Adın Carola Asistan. Görevin müşterilere araç kiralama konusunda yardımcı olmak.

Şirket hakkında genel bilgiler:
- Geniş araç filosuna sahibiz: ekonomik, orta segment, lüks, SUV ve minibüs seçenekleri mevcut.
- Araçlarımız benzin, dizel, elektrik ve hibrit yakıt tiplerine sahip.
- Manuel ve otomatik vites seçenekleri sunuyoruz.
- Günlük kiralama fiyatlarımız aracın tipine ve özelliklerine göre değişmektedir.
- Alış ve teslim için farklı lokasyon seçenekleri mevcuttur.
- Rezervasyon tarih bazlı yapılmakta olup araç uygunluğu anlık kontrol edilmektedir.

YAZIM KURALLARI (kesinlikle uy):
- Cevaplarını sade ve anlaşılır Türkçe ile yaz.
- Markdown kullanma: **, ##, *, -, _ gibi işaretler kullanma.
- Başlık, kalın yazı veya liste işareti ekleme.
- Kısa ve öz yaz, gereksiz uzatma.
- Öneri sunacaksan düz cümle olarak yaz (örnek: 'Size 7 kişilik SUV önerebilirim, hem konforlu hem de bagaj alanı geniş.').
- Eğer soru araç kiralama ile alakalı değilse kibarca yönlendir.
- Emin olmadığın konularda 'müşteri hizmetlerimizle iletişime geçebilirsiniz' de.";

            var body = new
            {
                model = "claude-opus-4-6",
                max_tokens = 1024,
                system = systemPrompt,
                messages = new[]
                {
                    new { role = "user", content = request.Message }
                }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("/v1/messages", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, new { error = "Yapay zeka servisine ulaşılamadı." });

                using var doc = JsonDocument.Parse(responseBody);
                var text = doc.RootElement
                    .GetProperty("content")[0]
                    .GetProperty("text")
                    .GetString();

                return Ok(new { reply = text });
            }
            catch
            {
                return StatusCode(500, new { error = "Bir hata oluştu. Lütfen tekrar deneyin." });
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
    }
}
