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
    [Route("api/{v:apiVersion}/slide-show")]
    public class SlideShowController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public SlideShowController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/slide-show/10/true 
        // Get : api/1.0/slide-show/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<SlideShowVM> slideShows = await _service.ReadSlideShowAsync(include, noOfRecords);

            if (slideShows == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (slideShows.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(slideShows);
            }
        }

        // Get : api/slide-show/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/slide-show/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            SlideShowVM slideShow = await _service.ReadSlideShowAsync(id, include);

            if (slideShow == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(slideShow);
            }
        }

        // Get : api/slide-show/slide1/10/true 
        // Get : api/1.0/slide-show/slide1/10/true 
        [HttpGet("{title}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<SlideShowVM> slideShows = await _service.ReadSlideShowAsync(title, include, noOfRecords);

            if (slideShows == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (slideShows.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(slideShows);
            }
        }

        // Get : api/slide-show/slide1/true
        // Get : api/1.0/slide-show/slide1/true
        [HttpGet("{title}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] bool include)
        {
            SlideShowVM slideShow = await _service.ReadSlideShowAsync(title, include);

            if (slideShow == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(slideShow);
            }
        }

        // Post : api/slide-show/
        // Post : api/slide-show/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateSlideShowVM createSlideShowVM)
        {
            string result = await _service.CreateSlideShowAsync(createSlideShowVM);

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

        // Put : api/slide-show/
        // Put : api/slide-show/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateSlideShowVM updateSlideShowVM)
        {
            string result = await _service.UpdateSlideShowAsync(updateSlideShowVM);

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

        // Delete : api/slide-show/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/slide-show/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteSlideShowAsync(id);

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
