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
        public async Task<IActionResult> GetAll()
        {
            var res = await _cityService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var res = await _cityService.GetById(id);
            return Ok(res);
        }

        [HttpDelete("DeleteCity")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var res = await _cityService.Delete(id);
            return Ok(res);
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> Add([FromBody] City city)
        {
            var res = await _cityService.AddCity(city);
            return Ok(res);
        }

        [HttpPut("UpdateCity")]
        public async Task<IActionResult> Update([FromBody] City city)
        {
            var res = await _cityService.Update(city);
            return Ok(res);
        }
    }
}
