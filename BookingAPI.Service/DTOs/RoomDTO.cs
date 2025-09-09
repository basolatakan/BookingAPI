using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Service.DTOs
{
    public sealed class RoomDTO
    {
        public int Id { get; init; }
        public string RoomNumber { get; init; }
        public int Capacity { get; init; }
        public bool IsAvailable { get; init; }
    }
}
