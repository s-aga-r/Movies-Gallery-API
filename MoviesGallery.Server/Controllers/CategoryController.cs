using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesGallery.Server.Services;
using MoviesGallery.ViewModels;
using MoviesGallery.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesGallery.Server.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/category")]
    public class CategoryController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public CategoryController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/category/10/true 
        // Get : api/1.0/category/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<CategoryVM> categories = await _service.ReadCategoryAsync(include, noOfRecords);

            if (categories == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (categories.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(categories);
            }
        }

        // Get : api/category/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/category/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            CategoryVM category = await _service.ReadCategoryAsync(id, include);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        // Get : api/category/action/10/true 
        // Get : api/1.0/category/action/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<CategoryVM> categories = await _service.ReadCategoryAsync(name, include, noOfRecords);

            if (categories == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (categories.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(categories);
            }
        }

        // Get : api/category/action/true
        // Get : api/1.0/category/action/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            CategoryVM category = await _service.ReadCategoryAsync(name, include);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        // Post : api/category/
        // Post : api/category/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateCategoryVM createCategoryVM)
        {
            string result = await _service.CreateCategoryAsync(createCategoryVM);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while inserting data into database, please contact the administrator.");
            }
            else if (result.Length == 36)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // Put : api/category/
        // Put : api/category/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateCategoryVM updateCategoryVM)
        {
            string result = await _service.UpdateCategoryAsync(updateCategoryVM);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data into database, please contact the administrator.");
            }
            else if (result.Length == 36)
            {
                return Ok(result);
            }
            else if(result == "NotFound")
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }
        }

        // Delete : api/category/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/category/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteCategoryAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
