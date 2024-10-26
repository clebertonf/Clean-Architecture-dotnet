using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if(products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetId(int id)
        {
            var product = await _productService.GetById(id);
            if(product is null)
            {
                return BadRequest();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO product)
        {
            if(product is null)
            {
                return BadRequest();
            }

            await _productService.Add(product);
            return Created(string.Empty, product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put([FromBody] ProductDTO product, int id)
        {
            if(product.Id != id)
            {
                return BadRequest();
            }

            await _productService.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if(product is null)
            {
                return BadRequest();
            }

            await _productService.Remove(id);
            return Ok();
        }
    }
}
