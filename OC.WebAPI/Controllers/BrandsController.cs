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
    public class BrandsController : ControllerBase
    {
        private readonly BrandService brandService;

        public BrandsController()
        {
            this.brandService = new BrandService();
        }

        [HttpGet]  // novo celiq metod
        public IEnumerable<BrandDto> GetAll()
        {
            return brandService.GetAll();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public ActionResult<BrandDto> Get([FromRoute] int id)
        {
            var result = brandService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/Brands
        [HttpPost]
        public IActionResult Create([FromBody] BrandDto brandDto)
        {
            if (!brandDto.IsValid()) 
            {
                return BadRequest();
            }

            if (brandService.Create(brandDto))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandDto brandDto)
        {

            if (!brandDto.IsValid()) 
            {
                return BadRequest();
            }
            brandDto.Id = id;

            if (brandService.Update(brandDto))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (brandService.Delete(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
