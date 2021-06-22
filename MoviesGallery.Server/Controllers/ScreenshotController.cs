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
    [Route("api/{v:apiVersion}/screenshot")]
    public class ScreenshotController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public ScreenshotController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/screenshot/10/true 
        // Get : api/1.0/screenshot/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<ScreenshotVM> screenshots = await _service.ReadScreenshotAsync(include, noOfRecords);

            if (screenshots == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (screenshots.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(screenshots);
            }
        }

        // Get : api/screenshot/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/screenshot/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            ScreenshotVM screenshot = await _service.ReadScreenshotAsync(id, include);

            if (screenshot == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(screenshot);
            }
        }

        // Get : api/screenshot/kgf/10/true 
        // Get : api/1.0/screenshot/kgf/10/true 
        [HttpGet("{title}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<ScreenshotVM> screenshots = await _service.ReadScreenshotAsync(title, include, noOfRecords);

            if (screenshots == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (screenshots.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(screenshots);
            }
        }

        // Get : api/screenshot/kgf/true
        // Get : api/1.0/screenshot/kgf/true
        [HttpGet("{title}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] bool include)
        {
            ScreenshotVM screenshot = await _service.ReadScreenshotAsync(title, include);

            if (screenshot == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(screenshot);
            }
        }

        // Post : api/screenshot/
        // Post : api/screenshot/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateScreenshotVM createScreenshotVM)
        {
            string result = await _service.CreateScreenshotAsync(createScreenshotVM);

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

        // Put : api/screenshot/
        // Put : api/screenshot/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateScreenshotVM updateScreenshotVM)
        {
            string result = await _service.UpdateScreenshotAsync(updateScreenshotVM);

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

        // Delete : api/screenshot/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/screenshot/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteScreenshotAsync(id);

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
