using eCommerceApp.Application.Models.CategoryDto;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await categoryService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await categoryService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategory category)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var result = await categoryService.AddAsync(category);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Succes ? Ok(result) : BadRequest(result);
        }

    }
}
