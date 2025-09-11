using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Mapping
{
    public static class ReservationCreateMapping
    {
        //CreateDTO --> new Entity (insert)
        public static Reservation ToEntity(this ReservationCreateDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new Reservation
            {
                CustomerId = dto.CustomerId,
                RoomId = dto.RoomId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreateDate = DateTime.UtcNow  
            };
        }
    }
}
