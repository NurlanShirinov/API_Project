using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Core.Models;
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

        [HttpGet("GetById")]
        public IActionResult GetByID(int id)
        {
            var res = _carService.GetById(id);
            return Ok(res);
        }


        [HttpPost("AddCar")]
        public IActionResult AddCar(Car car)
        {
            var res = _carService.AddCar(car);
            return Ok(res);
        }

        [HttpDelete("DeleteCar")]
        public IActionResult Delete(int id)
        {
            var res = _carService.DeleteCar(id);
            return Ok(res);
        }

        [HttpPut("UpdateCar")]
        public IActionResult Update(Car car)
        {
            var res = _carService.UpdateCar(car);
            return Ok(res);
        }


    }
}
