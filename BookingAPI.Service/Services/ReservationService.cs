using Microsoft.EntityFrameworkCore;
using BookingAPI.Service.Interfaces;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using BookingAPI.Service.Mapping;
using Booking.DataAccess;
using Booking.Core.Entities;

namespace BookingAPI.Service.Services
{
    public sealed class ReservationService : IReservationService
    {
        private readonly DatabaseConnection _db; 

        public ReservationService(DatabaseConnection db)
        {
            _db = db;
        }

        // LIST
        public async Task<ResponseGeneric<List<ReservationDTO>>> GetAllAsync()
        {
            var data = await _db.Reservations
                                .AsNoTracking()  //Bellek yönetimi için
                                .Select(r => r.ToDto())
                                .ToListAsync();

            return ResponseGeneric<List<ReservationDTO>>.Success(data, "Rezervasyonlar listelendi");
        }

        // GET BY ID
        public async Task<ResponseGeneric<ReservationDTO>> GetByIdAsync(int id)
        {
            var entity = await _db.Reservations
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<ReservationDTO>.Error("Rezervasyon bulunamadı");

            return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Rezervasyon getirildi");
        }

        // CREATE
        public async Task<ResponseGeneric<ReservationDTO>> CreateAsync(ReservationCreateDTO dto)
        {
            if (dto.CustomerId <= 0 || dto.RoomId <= 0)
                return ResponseGeneric<ReservationDTO>.Error("Geçersiz müşteri/oda bilgisi");

            //Tarih aralığı doğrulama
            if (dto.StartDate >= dto.EndDate)
                return ResponseGeneric<ReservationDTO>.Error("Geçersiz tarih aralığı");

            //Müşteri doğrulama
            if (!await _db.Customers.AnyAsync(c => c.Id == dto.CustomerId))
                return ResponseGeneric<ReservationDTO>.Error("Müşteri bulunamadı");
            
            //Oda doğrulama
            if (!await _db.Rooms.AnyAsync(r => r.Id == dto.RoomId))
                return ResponseGeneric<ReservationDTO>.Error("Oda bulunamadı");

            //Tarih çakışma kontrolü (overlap)
            bool overlaps = await _db.Reservations.AnyAsync(x =>
                x.RoomId == dto.RoomId &&
                x.StartDate < dto.EndDate &&
                dto.StartDate < x.EndDate);

            if (overlaps)
                return ResponseGeneric<ReservationDTO>.Error("Seçilen tarihler için oda uygun değil");

            //Kayıt
            var entity = dto.ToEntity();            //ReservationCreateMapping  DTO-->Entity
            _db.Reservations.Add(entity);   
            await _db.SaveChangesAsync();   

            return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Rezervasyon oluşturuldu");
        }

        // UPDATE: sadece tarih
        public async Task<ResponseGeneric<ReservationDTO>> UpdateDatesAsync(int id, DateTime start, DateTime end)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<ReservationDTO>.Error("Rezervasyon bulunamadı");

            if (entity.StartDate == start && entity.EndDate == end)
                return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Tarihlerde bir değişiklik yok");

            if (start >= end)
                return ResponseGeneric<ReservationDTO>.Error("Geçersiz tarih aralığı");

            // Kendi dışındaki aynı odadaki rezervasyonlarla çakışma kontrolü
            bool overlaps = await _db.Reservations.AnyAsync(x =>
                x.RoomId == entity.RoomId &&
                x.Id != id &&
                x.StartDate < end &&
                start < x.EndDate);

            if (overlaps)
                return ResponseGeneric<ReservationDTO>.Error("Yeni tarihler için oda uygun değil");

            entity.StartDate = start;
            entity.EndDate = end;
            await _db.SaveChangesAsync();

            return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Rezervasyon için tarihler güncellendi");
        }

        // UPDATE: sadece müşteri
        public async Task<ResponseGeneric<ReservationDTO>> UpdateCustomerAsync(int id, int customerId)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<ReservationDTO>.Error("Rezervasyon bulunamadı");

            if (entity.CustomerId == customerId)
                return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Müşteri zaten aynı");

            if (!await _db.Customers.AnyAsync(c => c.Id == customerId))
                return ResponseGeneric<ReservationDTO>.Error("Müşteri bulunamadı.Müşterinin sisteme eklendiğinden emin olun.");

            entity.CustomerId = customerId;
            await _db.SaveChangesAsync();

            return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Müşteri güncellendi");
        }

        // UPDATE: sadece oda
        public async Task<ResponseGeneric<ReservationDTO>> UpdateRoomAsync(int id, int roomId)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<ReservationDTO>.Error("Rezervasyon bulunamadı");

            if (entity.RoomId == roomId)
                return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Oda zaten aynı");

            if (!await _db.Rooms.AnyAsync(r => r.Id == roomId))
                return ResponseGeneric<ReservationDTO>.Error("Oda bulunamadı");

            // Yeni odada mevcut tarih aralığı çakışıyor mu kontrolü
            bool overlaps = await _db.Reservations.AnyAsync(x =>
                x.RoomId == roomId &&
                x.Id != id &&
                x.StartDate < entity.EndDate &&
                entity.StartDate < x.EndDate);

            if (overlaps)
                return ResponseGeneric<ReservationDTO>.Error("Yeni oda bu tarihlerde uygun değil");

            entity.RoomId = roomId;
            await _db.SaveChangesAsync();

            return ResponseGeneric<ReservationDTO>.Success(entity.ToDto(), "Oda güncellendi");
        }

        // DELETE
        public async Task<IResponse> DeleteAsync(int id)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return BookingAPI.Service.Response.Response.Error("Rezervasyon bulunamadı");

            _db.Reservations.Remove(entity);
            await _db.SaveChangesAsync();

            return BookingAPI.Service.Response.Response.Success("Rezervasyon silindi");
        }
    }
}
