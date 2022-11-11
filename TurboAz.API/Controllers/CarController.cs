using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var res = _carService.GetAll();
            return Ok(res);
        }
    }
}
