using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entities
{
    public class Reservation : BaseEntity
    {
        //Rezervasyon ID
        public int CustomerId { get; set; } //Müşteri FK (Sadece ilk misafirin id'sini tutar.Fatura sahibi gibi düşünmek lazım.)
        public int RoomId { get; set; } //Oda FK
        public DateTime StartDate { get; set; } //Giriş
        public DateTime EndDate { get; set; } //Çıkış

        public Customer Customer { get; set; } // navigation
        public Room Room { get; set; }         // navigation

    }
}
