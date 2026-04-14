# 🚗 Carola - Araç Kiralama Sistemi

Carola, kullanıcıların araçları keşfedip rezervasyon yapabildiği; 
yöneticilerin tüm operasyonu tek bir panel üzerinden yönettiği 
yapay zeka destekli bir araç kiralama platformudur.

## 🛠 Teknik Mimari

Proje, sorumlulukların net biçimde ayrıldığı N-Tier mimarisiyle kurgulanmıştır:

| Katman | Sorumluluk |
|--------|------------|
| EntityLayer | Veritabanı entity sınıfları |
| DataAccessLayer | EF Core repository implementasyonları |
| BusinessLayer | İş kuralları, FluentValidation, servis yöneticileri |
| DtoLayer | Katmanlar arası veri transfer nesneleri |
| WebUI | ASP.NET Core 6 MVC — Controller, View, ViewModel |

## ✨ Özellikler

### 🤖 Yapay Zeka Entegrasyonu
- **Ehliyet OCR:** Kullanıcı ehliyetinin fotoğrafını yükler; 
  Claude Vision API görseli analiz ederek ad, soyad, TC, 
  doğum tarihi ve ehliyet numarasını otomatik doldurur
- **AI Sohbet Widgeti:** Anasayfada entegre sohbet arayüzü; 
  kullanıcılar araç ve kiralama süreçleri hakkında 
  Claude API üzerinden anlık yanıt alır

### 📋 Çok Adımlı Rezervasyon
- Ehliyet yükleme → Kişisel bilgiler → Kiralama detayları
- Sağ panelde anlık gün/fiyat hesaplama
- FluentValidation ile sunucu taraflı doğrulama

### 🔍 Araç Listeleme & Filtreleme
- Günlük fiyat aralığı (slider), yakıt tipi, 
  vites tipi ve koltuk sayısına göre filtreleme
- Detay sayfasından direkt rezervasyon başlatma

### 🛡 Admin Paneli
- Araç, müşteri, rezervasyon, kategori ve lokasyon yönetimi
- Rezervasyon durum güncelleme (Beklemede / Onaylandı / İptal)
- Onay durumunda müşteriye otomatik e-posta + kupon kodu gönderimi

### 📧 E-posta Bildirimi
- MailKit / SMTP entegrasyonu
- Rezervasyon onaylandığında müşteriye 
  rezervasyon özeti ve tek kullanımlık kupon kodu iletilir

## 🧰 Kullanılan Teknolojiler

- ASP.NET Core 6 MVC
- Entity Framework Core (Code First)
- MS SQL Server
- FluentValidation
- AutoMapper
- Claude API (Vision + Chat)
- MailKit (SMTP)
- Bootstrap 5

## 📸 Ekran Görüntüleri


| Anasayfa | Araç Listesi | Rezervasyon |
|----------|-------------|-------------|
| <img width="1895" height="909" alt="anasayfa" src="https://github.com/user-attachments/assets/85168c8e-2fe6-4f7d-a1a0-ff81f1c28338" /> | <img width="1920" height="3304" alt="araclistesi" src="https://github.com/user-attachments/assets/c55fd31e-85b2-4e2d-9ab2-68eee3d90f70" /> | <img width="1920" height="3417" alt="rezervasyon" src="https://github.com/user-attachments/assets/bed361a2-4299-4a04-bf46-6c5b1adc67a7" /> |

| Filtreleme | Onay Maili | Admin Paneli |
|-------------|-----------|--------------|
| <img width="1920" height="2973" alt="filtreleme" src="https://github.com/user-attachments/assets/0c3d16f0-008c-480f-93a6-4b101e2bae00" /> | <img width="371" height="760" alt="mail" src="https://github.com/user-attachments/assets/80417f35-a99f-479d-974d-a14475488753" /> | <img width="1920" height="1272" alt="dashboard" src="https://github.com/user-attachments/assets/a38de205-d001-4dd8-a252-3793d53b09d3" /> |

| Rezervasyon Listesi | Raporlar |
|-------------|-----------|
| <img width="1899" height="908" alt="rezervasyonlistesi" src="https://github.com/user-attachments/assets/a9d3d542-b10c-49f7-8f04-ba20079d79f6" /> | <img width="1920" height="1414" alt="raporlar" src="https://github.com/user-attachments/assets/2d48cc04-7db7-45ea-bfc6-f03f8d2c4597" /> |
