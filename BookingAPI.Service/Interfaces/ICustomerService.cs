using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseGeneric<List<CustomerDTO>>> GetAllAsync();
        Task<ResponseGeneric<CustomerDTO>> GetByIdAsync(int id);
        Task<ResponseGeneric<CustomerDTO>> CreateAsync(CustomerDTO dto);
        Task<ResponseGeneric<CustomerDTO>> UpdateAsync(int id, CustomerDTO dto);
        Task<IResponse> DeleteAsync(int id);
    }
}
