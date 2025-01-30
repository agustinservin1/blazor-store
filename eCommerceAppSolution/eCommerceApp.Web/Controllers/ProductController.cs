﻿using eCommerceApp.Application.Models.ProductDTO;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);    
        }
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.AddAsync(product);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update (UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productService.UpdateAsync(product);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Succes ? Ok(result) : BadRequest(result);
        }

    }
}
