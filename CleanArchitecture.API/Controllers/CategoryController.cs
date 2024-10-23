using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.API.Controllers
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

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if(categories is null)
            {
                return NotFound(new {
                    error = true,
                    content = "Categories not found" 
                });
            }

            return Ok(new
            {
                error = false,
                content = categories
            });
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category is null)
            {
                return NotFound(new {
                    error = true,
                    content = "Categorie not found." 
                });
            }

            return Ok(new
            {
                error = false,
                content = category
            });
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if(categoryDTO is null)
            {
                return BadRequest(new
                {
                    error = true,
                    content = "Invalid data."
                });
            }

            await _categoryService.Add(categoryDTO);
            return Created(string.Empty, categoryDTO);
        }

        [HttpPut("")]
        public void Update([FromBody] CategoryDTO categoryDTO)
        {
            return;
        }
    }
}
