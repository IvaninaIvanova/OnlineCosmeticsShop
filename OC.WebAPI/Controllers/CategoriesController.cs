using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OC.Business.DTOs;
using OC.Business.Services;

namespace OC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService categoryService;

        public CategoriesController()
        {
            this.categoryService = new CategoryService();
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<CategoryDto> GetAll()
        {
            return categoryService.GetAll();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get([FromRoute] int id)
        {
            var result = categoryService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto category)
        {
            if (!category.IsValid())  
            {
                return BadRequest();
            }

            if (categoryService.Create(category))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryDto category)
        {
            if (!category.IsValid()) 
            {
                return BadRequest();
            }

            category.Id = id;

            if (categoryService.Update(category))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (categoryService.Delete(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
