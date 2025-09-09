using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Interfaces
{
    public interface IRoomService
    {
        Task<ResponseGeneric<List<RoomDTO>>> GetAllAsync();
        Task<ResponseGeneric<RoomDTO>> GetByIdAsync(int id);
        Task<ResponseGeneric<RoomDTO>> CreateAsync(RoomDTO dto);
        Task<ResponseGeneric<RoomDTO>> UpdateAsync(int id, RoomDTO dto);
        Task<IResponse> DeleteAsync(int id);
    }
}
