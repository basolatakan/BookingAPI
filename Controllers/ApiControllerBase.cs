using BookingAPI.Service.Interfaces;
using BookingAPI.Service.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        // ResponseGeneric<T> için
        protected static IActionResult From<T>(ResponseGeneric<T> r)
        {
            if (r.IsSuccess)
            {
                return new OkObjectResult(r);   // Başarılıysa 200
            }
            else
            {
                return new BadRequestObjectResult(r);   // Başarısızsa 400
            }
        }

        // IResponse için (Data taşımayanlar)
        protected static IActionResult From(IResponse r)
        {
            if (r.IsSuccess)
            {
                return new OkObjectResult(r);   // Başarılıysa 200
            }
            else
            {
                return new BadRequestObjectResult(r);   // Başarısızsa 400
            }
        }
    }
}
