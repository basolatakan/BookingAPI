using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Mapping
{
    public static class ReservationMapping
    {
        // Entity --> DTO 
        public static ReservationDTO ToDto(this Reservation reservation)
        {
            ArgumentNullException.ThrowIfNull(reservation);
            return new ReservationDTO
            {
                CustomerId = reservation.CustomerId,
                RoomId = reservation.RoomId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate
            };
        }

        /*  ReservationCreateDTO içinde bu işlemleri daha sağlıklı yapabiliyoruz.
          
        //DTO --> Entity (for create)
        public static Reservation ToEntity(this ReservationDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new Reservation
            {
                Id = dto.Id,
                CustomerId = dto.CustomerId,
                RoomId = dto.RoomId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreateDate = DateTime.UtcNow
            };
        }
        */

        // DTO --> Entity (update)
        public static void UpdateFromDto(this Reservation reservation, ReservationDTO dto) // Mevcut Reservation’ı güncellemek için
        {
            ArgumentNullException.ThrowIfNull(reservation);
            ArgumentNullException.ThrowIfNull(dto);

            reservation.CustomerId = dto.CustomerId;
            reservation.RoomId = dto.RoomId;
            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;
        }

    }
}
