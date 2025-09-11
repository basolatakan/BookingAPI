using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.DTOs
{
    public sealed class CustomerDTO
    {
        public int Id { get; init; }
        [Required(ErrorMessage = "İsim zorunludur.")]
        [StringLength(50, ErrorMessage = "İsim en fazla 50 karakter olabilir.")]
        public string FirstName { get; init; }
        [Required(ErrorMessage = "Soyisim zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyisim en fazla 50 karakter olabilir.")]
        public string LastName { get; init; }
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir.")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Email zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        [StringLength(100, ErrorMessage = "Email en fazla 100 karakter olabilir.")]
        public string Phone { get; init; }
        public DateTime CreateDate { get; set; }
    }
}
