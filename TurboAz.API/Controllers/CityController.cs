using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Core.Models;
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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var res = _cityService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery]int id)
        {
            var res = _cityService.GetById(id);
            return Ok(res);
        }

        [HttpDelete("DeleteCity")]
        public IActionResult Delete([FromQuery]int id)
        {
            var res = _cityService.Delete(id);
            return Ok(res);
        }

        [HttpPost("AddCity")]
        public IActionResult Add([FromBody]City city)
        {
            var res = _cityService.AddCity(city);
            return Ok(res);
        }

        [HttpPut("UpdateCity")]
        public IActionResult Update([FromBody]City city)
        {
            var res = _cityService.Update(city);
            return Ok(res);
        }
    }
}
