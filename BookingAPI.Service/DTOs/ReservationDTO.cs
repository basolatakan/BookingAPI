using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.DTOs
{
    public sealed class ReservationDTO //kaydı okuma için, o yüzden validasyona gerek duymasım.
    {
        public int Id { get; init; }
        public int CustomerId { get; init; }
        public int RoomId { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
