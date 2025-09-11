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
        [Required(ErrorMessage = "Müşteri ID zorunludur.")]
        public int CustomerId { get; init; }

        [Required(ErrorMessage = "Oda ID zorunludur.")]
        public int RoomId { get; init; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; init; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; init; }
    }
}
