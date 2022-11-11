using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetAllCities")]
        public IActionResult GetAll()
        {
            var res = _cityService.GetAll();
            return Ok(res);
        }
    }
}
