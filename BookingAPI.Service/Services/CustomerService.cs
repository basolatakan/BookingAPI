using Microsoft.EntityFrameworkCore;
using BookingAPI.Service.Interfaces;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using BookingAPI.Service.Mapping;
using Booking.DataAccess;

namespace BookingAPI.Service.Services
{
    public sealed class CustomerService : ICustomerService
    {
        private readonly DatabaseConnection _db;
        public CustomerService(DatabaseConnection db) => _db = db;

        public async Task<ResponseGeneric<List<CustomerDTO>>> GetAllAsync()
        {
            var list = await _db.Customers
                .AsNoTracking()
                .Select(c => c.ToDto())
                .ToListAsync();

            return ResponseGeneric<List<CustomerDTO>>.Success(list, "Müşteriler listelendi");
        }

        public async Task<ResponseGeneric<CustomerDTO>> GetByIdAsync(int id)
        {
            var entity = await _db.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                return ResponseGeneric<CustomerDTO>.Error("Müşteri bulunamadı");

            return ResponseGeneric<CustomerDTO>.Success(entity.ToDto(), "Müşteri getirildi");
        }

        public async Task<ResponseGeneric<CustomerDTO>> CreateAsync(CustomerDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
                return ResponseGeneric<CustomerDTO>.Error("Ad ve Soyad zorunludur");

            if (string.IsNullOrWhiteSpace(dto.Email))
                return ResponseGeneric<CustomerDTO>.Error("Email zorunludur");

            bool emailInUse = await _db.Customers.AnyAsync(c => c.Email == dto.Email);
            if (emailInUse)
                return ResponseGeneric<CustomerDTO>.Error("Bu email adresi zaten kayıtlı");

            var entity = dto.ToEntity(); //CustomerMapping: DTO -> new Customer
            _db.Customers.Add(entity);
            await _db.SaveChangesAsync();

            return ResponseGeneric<CustomerDTO>.Success(entity.ToDto(), "Müşteri oluşturuldu");
        }

        public async Task<ResponseGeneric<CustomerDTO>> UpdateAsync(int id, CustomerDTO dto)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return ResponseGeneric<CustomerDTO>.Error("Müşteri bulunamadı");

            //Email değişiyorsa benzersizlik kontrolü
            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != entity.Email)
            {
                bool emailInUse = await _db.Customers.AnyAsync(c => c.Email == dto.Email && c.Id != id);
                if (emailInUse)
                    return ResponseGeneric<CustomerDTO>.Error("Bu email başka bir müşteride kayıtlı");
            }

            entity.UpdateFromDto(dto);  //CustomerMapping
            await _db.SaveChangesAsync();

            return ResponseGeneric<CustomerDTO>.Success(entity.ToDto(), "Müşteri güncellendi");
        }

        public async Task<IResponse> DeleteAsync(int id)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                return BookingAPI.Service.Response.Response.Error("Müşteri bulunamadı");

            if (await _db.Reservations.AnyAsync(r => r.CustomerId == id))
                return BookingAPI.Service.Response.Response.Error("Seçilen müşteri bir rezervasyona bağlı olduğu için silinemedi.");
            

            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();

            return BookingAPI.Service.Response.Response.Success("Müşteri silindi");
        }
    }
}
