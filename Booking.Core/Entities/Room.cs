using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Entities
{
    public class Room : BaseEntity
    {
        //Oda ID
        public string RoomNumber { get; set; } //A123 B456 tarzı oda numaraları olabiliceği için string
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
