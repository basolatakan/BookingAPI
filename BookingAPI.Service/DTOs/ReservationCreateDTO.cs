using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.DTOs
{
    public sealed class ReservationCreateDTO  //kayıt oluşturma için
    {
        [Required]
        public int CustomerId { get; init; }

        [Required] 
        public int RoomId { get; init; }

        [Required]
        public DateTime StartDate { get; init; }
        
        [Required]
        public DateTime EndDate { get; init; }
    }
}
