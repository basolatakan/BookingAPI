using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Interfaces
{
    public interface IReservationService
    {
        //Listeleme
        Task<ResponseGeneric<List<ReservationDTO>>> GetAllAsync();

        //Tek kayıt
        Task<ResponseGeneric<ReservationDTO>> GetByIdAsync(int id);

        //Oluşturma (Create)
        Task<ResponseGeneric<ReservationDTO>> CreateAsync(ReservationCreateDTO dto);

        //Güncelleme (sadece tarihleri değiştirmek için)
        Task<ResponseGeneric<ReservationDTO>> UpdateDatesAsync(int id, DateTime start, DateTime end);

        //Güncelleme (sadece müşteri no değiştirmek için)
        Task<ResponseGeneric<ReservationDTO>> UpdateCustomerAsync(int id, int customerId);

        //Güncelleme (sadece oda no değiştirmek için)
        Task<ResponseGeneric<ReservationDTO>> UpdateRoomAsync(int id, int roomId);

        //Silme/iptal
        Task<IResponse> DeleteAsync(int id);
    }
}
