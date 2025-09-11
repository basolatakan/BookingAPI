using Microsoft.EntityFrameworkCore;
using BookingAPI.Service.Interfaces;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using BookingAPI.Service.Mapping;
using Booking.DataAccess;

namespace BookingAPI.Service.Services
{
    public sealed class RoomService : IRoomService
    {
        private readonly DatabaseConnection _db;
        public RoomService(DatabaseConnection db) => _db = db;

        public async Task<ResponseGeneric<List<RoomDTO>>> GetAllAsync()
        {
            var list = await _db.Rooms
                .AsNoTracking()
                .Select(r => r.ToDto())
                .ToListAsync();

            return ResponseGeneric<List<RoomDTO>>.Success(list, "Odalar listelendi");
        }

        public async Task<ResponseGeneric<RoomDTO>> GetByIdAsync(int id)
        {
            var entity = await _db.Rooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<RoomDTO>.Error("Oda bulunamadı");

            return ResponseGeneric<RoomDTO>.Success(entity.ToDto(), "Oda getirildi");
        }

        public async Task<ResponseGeneric<RoomDTO>> CreateAsync(RoomDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoomNumber))
                return ResponseGeneric<RoomDTO>.Error("Oda numarası zorunludur");

            if (dto.Capacity <= 0)
                return ResponseGeneric<RoomDTO>.Error("Kapasite 0'dan büyük olmalıdır");

            //Oda numarası benzersizliği
            bool numberInUse = await _db.Rooms.AnyAsync(r => r.RoomNumber == dto.RoomNumber);
            if (numberInUse)
                return ResponseGeneric<RoomDTO>.Error("Bu oda numarası zaten kullanılıyor");

            var entity = dto.ToEntity(); //RoomMapping: DTO -> new Room
            _db.Rooms.Add(entity);
            await _db.SaveChangesAsync();

            return ResponseGeneric<RoomDTO>.Success(entity.ToDto(), "Oda oluşturuldu");
        }

        public async Task<ResponseGeneric<RoomDTO>> UpdateAsync(int id, RoomDTO dto)
        {
            var entity = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return ResponseGeneric<RoomDTO>.Error("Oda bulunamadı");

            //Oda numarası değişiyorsa benzersizlik kontrolü
            if (!string.IsNullOrWhiteSpace(dto.RoomNumber) && dto.RoomNumber != entity.RoomNumber)
            {
                bool numberInUse = await _db.Rooms.AnyAsync(r => r.RoomNumber == dto.RoomNumber && r.Id != id);
                if (numberInUse)
                    return ResponseGeneric<RoomDTO>.Error("Bu oda numarası başka bir odada kayıtlı");
            }

            if (dto.Capacity <= 0)
                return ResponseGeneric<RoomDTO>.Error("Kapasite 0'dan büyük olmalıdır");

            entity.UpdateFromDto(dto); //RoomMapping
            await _db.SaveChangesAsync();

            return ResponseGeneric<RoomDTO>.Success(entity.ToDto(), "Oda güncellendi");
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var entity = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return BookingAPI.Service.Response.Response.Error("Oda bulunamadı");

            bool hasFutureReservations = await _db.Reservations
                .AnyAsync(res => res.RoomId == id && res.EndDate > DateTime.UtcNow);
            if (hasFutureReservations)
                return BookingAPI.Service.Response.Response.Error("Bu odaya bağlı aktif rezervasyonlar varken silemezsiniz");

            _db.Rooms.Remove(entity);
            await _db.SaveChangesAsync();

            return BookingAPI.Service.Response.Response.Success("Oda silindi");
        }
    }
}
