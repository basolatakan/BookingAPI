using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.Mapping
{
    public static class CustomerMapping
    {
        // Entity --> DTO
        public static CustomerDTO ToDto(this Customer customer) //DB’den Customer okudun, dışarı döneceksin.
        {
            ArgumentNullException.ThrowIfNull(customer);

            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }

        //DTO --> new Entity(create için)
        public static Customer ToEntity(this CustomerDTO dto)  //API’den gelen DTO --> yeni entity
        {
            ArgumentNullException.ThrowIfNull(dto);

            return new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone
            };
        }

        // DTO --> Entity (Update için)
        public static void UpdateFromDto(this Customer customer, CustomerDTO dto) //DB’den bulunan entity --> DTO ile güncelle
        {
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentNullException.ThrowIfNull(dto);

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
        }
    }
}
