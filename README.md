# Kitap Yazar Tablosu - .NET Core N Katmanlı Mimari

Bu proje, Entity Framework Core ile geliştirilmiş basit bir kitap-yazar-içerik kayıt ve takip sistemidir. Amaç, .NET mimarisine uygun olarak CRUD işlemlerini gerçekleştirmektir.

## 🔧 Kullanılan Teknolojiler
- ASP.NET Core Web API
- Entity Framework Core
- Oracle Server
- AutoMapper
- Git & GitHub

## 📁 Katmanlı Mimari Yapısı
- **Entities Layer**: Veri modelleri
- **Data Access Layer**: Repository ve DbContext
- **Business Layer**: Servisler ve iş kuralları
- **Web UI**: Arayüz

## 🧪 Özellikler
- Kitap,yazar ve içerik ekleme, silme, güncelleme, listeleme

## 🚀 Nasıl Çalıştırılır?
1. `appsettings.json` üzerinden veritabanı bağlantısı yapılandırılır.
2. `dotnet ef database update` ile veritabanı oluşturulur.
3. `dotnet run` komutu ile proje başlatılır.
