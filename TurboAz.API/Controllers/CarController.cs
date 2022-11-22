using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Core.Models;
using TurboAz.Core.RequestsModels;
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
        public async Task<IActionResult> GetAll()
        {
            var res = await _carService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetallPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingModel model)
        {
            var res = await _carService.GetAllPaging(model);
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int id)
        {
            var res = await _carService.GetById(id);
            return Ok(res);
        }


        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            var res = await _carService.AddCar(car);
            return Ok(res);
        }

        [HttpDelete("DeleteCar")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var res = await _carService.DeleteCar(id);
            return Ok(res);
        }

        [HttpPut("UpdateCar")]
        public async Task<IActionResult> Update([FromBody] Car car)
        {
            var res = await _carService.UpdateCar(car);
            return Ok(res);
        }
    }
}
