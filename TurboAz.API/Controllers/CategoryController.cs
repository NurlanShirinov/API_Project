using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurboAz.Core.Models;
using TurboAz.Service.Services.Abstract;

namespace TurboAz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var res = _categoryService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int id)
        {
            var res = _categoryService.GetById(id);
            return Ok(res);
        }

        [HttpPost("Add")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            var res = _categoryService.AddCategory(category);
            return Ok(res);
        }

        [HttpPost("Update")]
        public IActionResult Update(Category category)
        {
            var res = _categoryService.UpdateCategory(category);
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var res = _categoryService.DeleteCategory(id);
            return Ok(res);
        }
    }
}
