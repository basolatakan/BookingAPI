using BookingAPI.Service.DTOs;
using BookingAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ApiControllerBase
    {
        private readonly IRoomService _service;

        public RoomsController(IRoomService service)
        {
            ArgumentNullException.ThrowIfNull(service);
            _service = service;
        }

        // GET /api/rooms  (liste)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return From(await _service.GetAllAsync());
        }

        // GET /api/rooms/{id}  (tek kayıt)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return From(await _service.GetByIdAsync(id));
        }

        // POST /api/rooms  (oluştur)  
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomDTO dto)
        {
            var response = await _service.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
                
            var room = response.Data;

            if (room == null) 
                return BadRequest("Beklenmedik hata: Data null döndü.");

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, response);    //201 Created + Location header
        }

        // PUT /api/rooms/{id}  (güncelle)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoomDTO dto)
        {
            return From(await _service.UpdateAsync(id, dto));
        }

        // DELETE /api/rooms/{id}  (sil)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return From(await _service.DeleteAsync(id));
        }
        

        [HttpGet("availability")]
        public async Task<IActionResult> GetAvailableRooms(DateTime start, DateTime end, int capacity) 
        {
            return From(await _service.GetAvailableRoomsAsync(start,end,capacity));
        }
    }
}
