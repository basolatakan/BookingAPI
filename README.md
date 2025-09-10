# BookingAPI

Katmanlı mimariyle geliştirilmiş bir **ASP.NET Core 8 Web API** projesi. Basit bir “rezervasyon” senaryosu üzerinden **müşteri (Customer)**, **oda (Room)** ve **rezervasyon (Reservation)** işlemlerini sağlar. Veri erişimi için **Entity Framework Core** ve **SQL Server** kullanılır.

API, Service, DataAccess ve Core katmanlarıyla bir bağımlılık yönü **(API → Service → DataAccess → Core)** izler. 

Swagger ile dokümantasyon hazır gelir.

## Özellikler:
- ✅ **Katmanlı mimari** (Core / DataAccess / Service / API)
- ✅ **EF Core** ile SQL Server üzerinde CRUD
- ✅ **Swagger/OpenAPI** ile otomatik API dokümantasyonu
- ✅ **DTO & Mapping** ile temiz giriş/çıkış modelleri
- ✅ **Tek tip cevap modeli** (Response / ResponseGeneric\<T\>)
- ✅ **Migrasyonlar** `Booking.DataAccess` projesinde tutulur

## Teknolojiler:
- **.NET**: .NET 8
- **Web**: ASP.NET Core Web API
- **ORM**: Entity Framework Core (SqlServer)
- **Dokümantasyon**: Swashbuckle (Swagger)
- **Veritabanı**: SQL Server (LocalDB veya kurulu bir SQL Server)

## Dizin Yapısı 
```text
BookingAPI.sln

**/Booking.Core**
  /Entities
    Customer.cs
    Room.cs
    Reservation.cs

/Booking.DataAccess
  DatabaseConnection.cs
  /Migrations
    2025xxxxxx_*.cs
    ...

/BookingAPI.Service
  /DTOs
  /Interfaces
  /Mapping
  /Response
    Response.cs
    ResponseGeneric.cs

/Booking.API
  /Controllers
    CustomersController.cs
    RoomsController.cs
    ReservationsController.cs
  /Properties
  Program.cs
  BookingAPI.csproj
  appsettings.json
  appsettings.Development.json
