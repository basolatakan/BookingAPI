using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.DTOs
{
    public sealed class RoomDTO
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "Oda numarası zorunludur.")]
        [StringLength(10, ErrorMessage = "Oda numarası en fazla 50 karakter olabilir.")]
        public string RoomNumber { get; init; }
        [Required(ErrorMessage = "Kapasite zorunludur.")]
        [Range(1,20,ErrorMessage = "Kapasite 1 ile 20 arasında olmalıdır..")]
        public int Capacity { get; init; }
        [Required(ErrorMessage = "Müsaitlik durumu belirtilmelidir.")]
        public bool IsAvailable { get; init; }
    }
}
