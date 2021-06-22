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
    [Route("api/{v:apiVersion}/director")]
    public class DirectorController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public DirectorController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/director/10/true 
        // Get : api/1.0/director/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<DirectorVM> directors = await _service.ReadDirectorAsync(include, noOfRecords);

            if (directors == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (directors.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(directors);
            }
        }

        // Get : api/director/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/director/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            DirectorVM director = await _service.ReadDirectorAsync(id, include);

            if (director == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(director);
            }
        }

        // Get : api/director/sagar/10/true 
        // Get : api/1.0/director/sagar/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<DirectorVM> directors = await _service.ReadDirectorAsync(name, include, noOfRecords);

            if (directors == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (directors.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(directors);
            }
        }

        // Get : api/director/sagar/true
        // Get : api/1.0/director/sagar/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            DirectorVM director = await _service.ReadDirectorAsync(name, include);

            if (director == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(director);
            }
        }

        // Post : api/director/
        // Post : api/director/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateDirectorVM createDirectorVM)
        {
            string result = await _service.CreateDirectorAsync(createDirectorVM);

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

        // Put : api/director/
        // Put : api/director/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateDirectorVM updateDirectorVM)
        {
            string result = await _service.UpdateDirectorAsync(updateDirectorVM);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating data into database, please contact the administrator.");
            }
            else if (result.Length == 36)
            {
                return Ok(result);
            }
            else if (result == "NotFound")
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }
        }

        // Delete : api/director/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/director/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteDirectorAsync(id);

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
