using Booking.Core.Entities;
using BookingAPI.Service.DTOs;
using BookingAPI.Service.Interfaces;
using BookingAPI.Service.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => /api/customers
    public class CustomersController : ApiControllerBase
    {
        private readonly ICustomerService _service;
        public CustomersController(ICustomerService service)
        {
            ArgumentNullException.ThrowIfNull(service);
            _service = service;
        }

        // 1) GET /api/customers  (liste)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return From(await _service.GetAllAsync());
        }

        // 2) GET /api/customers/{id}  (tek kayıt)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return From(await _service.GetByIdAsync(id));
        }

        // 3) POST /api/customers  (oluştur)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
        {
            var response = await _service.CreateAsync(dto);

            if (!response.IsSuccess) 
            {
                return BadRequest(response);
            }

            var customer = response.Data;

            if (response.Data == null)
                return BadRequest("Beklenmedik hata: Data null döndü.");

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, response);    //Başarılı kayıt sonrası 201 dönecek ve yeni kaynağın URL'ni verecek
        }

        // 4) PUT /api/customers/{id}  (güncelle)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDTO dto)
        {
            return From(await _service.UpdateAsync(id, dto));
        }

        // 5) DELETE /api/customers/{id}  (sil)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return From(await _service.DeleteAsync(id));
        }
    }
}
