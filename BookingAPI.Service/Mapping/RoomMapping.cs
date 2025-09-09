using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Mapping
{
    public static class RoomMapping
    {
        // Entity --> DTO
        public static RoomDTO ToDto(this Room room) //DB'deki kaydı dışarıya çıkartmak için DTO'ya çeviriyor
        {
            ArgumentNullException.ThrowIfNull(room);

            return new RoomDTO
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                IsAvailable = room.IsAvailable
            };
        }

        //DTO --> Entity(create için)
        public static Room ToEntity(this RoomDTO dto)  //Yeni kayıt
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new Room
            {
                RoomNumber = dto.RoomNumber,
                Capacity = dto.Capacity,
                IsAvailable = dto.IsAvailable
            };
        }

        // DTO --> Entity (Update için)
        public static void UpdateFromDto(this Room room, RoomDTO dto)
        {
            ArgumentNullException.ThrowIfNull(room);
            ArgumentNullException.ThrowIfNull(dto);

            room.RoomNumber = dto.RoomNumber;
            room.Capacity = dto.Capacity;
            room.IsAvailable = dto.IsAvailable;
        }
    }
}
