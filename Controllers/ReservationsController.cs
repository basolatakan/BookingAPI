using BookingAPI.Service.DTOs;
using BookingAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ApiControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            ArgumentNullException.ThrowIfNull(service);
            _service = service;
        }

        // GET /api/reservations  (liste)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return From(await _service.GetAllAsync());
        }

        // GET /api/reservations/{id}  (tek kayıt)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return From(await _service.GetByIdAsync(id));
        }

        // POST /api/reservations  (oluştur) 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationCreateDTO dto)
        {
            var response = await _service.CreateAsync(dto);
            if (!response.IsSuccess) 
                return BadRequest(response);

            var created = response.Data;
            if (created == null) 
                return BadRequest("Beklenmedik hata: Data null döndü.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, response); //201 Created + Location
        }

        // PUT /api/reservations/{id}/dates?start=...&end=...  (sadece tarih güncelle)
        [HttpPut("{id:int}/dates")]
        public async Task<IActionResult> UpdateDates(int id, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return From(await _service.UpdateDatesAsync(id, start, end));
        }

        // PUT /api/reservations/{id}/customer?customerId=...  (sadece müşteri güncelle)
        [HttpPut("{id:int}/customer")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromQuery] int customerId)
        {
            return From(await _service.UpdateCustomerAsync(id, customerId));
        }

        // PUT /api/reservations/{id}/room?roomId=...  (sadece oda güncelle)
        [HttpPut("{id:int}/room")]
        public async Task<IActionResult> UpdateRoom(int id, [FromQuery] int roomId)
        {
            return From(await _service.UpdateRoomAsync(id, roomId));
        }

        // DELETE /api/reservations/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return From(await _service.DeleteAsync(id));
        }
    }
}
