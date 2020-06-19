using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OC.Business.DTOs;
using OC.Business.Services;

namespace OC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController()
        {
            this.productService = new ProductService();
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<ProductDto> GetAll()
        {
            return productService.GetAll();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get([FromRoute] int id)
        {
            var result = productService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/Movies
        [HttpPost]
        public IActionResult Create([FromBody] ProductDto product)
        {
            if (!product.IsValid())
            {
                return BadRequest();
            }

            if (productService.Create(product))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductDto product)
        {
            if (!product.IsValid())
            {
                return BadRequest();
            }

            product.Id = id;

            if (productService.Update(product))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (productService.Delete(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
