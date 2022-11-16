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
        public async Task<IActionResult> GetAll()
        {
            var res = await _categoryService.GetAll();
            return Ok(res);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var res = await _categoryService.GetById(id);
            return Ok(res);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var res = await _categoryService.AddCategory(category);
            return Ok(res);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            var res = await _categoryService.UpdateCategory(category);
            return Ok(res);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var res = await _categoryService.DeleteCategory(id);
            return Ok(res);
        }
    }
}
