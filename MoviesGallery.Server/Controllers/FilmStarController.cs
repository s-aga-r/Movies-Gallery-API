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
    [Route("api/{v:apiVersion}/film-star")]
    public class FilmStarController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public FilmStarController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/film-star/10/true 
        // Get : api/1.0/film-star/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<FilmStarVM> filmStars = await _service.ReadFilmStarAsync(include, noOfRecords);

            if (filmStars == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (filmStars.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(filmStars);
            }
        }

        // Get : api/film-star/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/film-star/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            FilmStarVM filmStar = await _service.ReadFilmStarAsync(id, include);

            if (filmStar == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(filmStar);
            }
        }

        // Get : api/film-star/sagar/10/true 
        // Get : api/1.0/film-star/sagar/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<FilmStarVM> filmStars = await _service.ReadFilmStarAsync(name, include, noOfRecords);

            if (filmStars == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (filmStars.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(filmStars);
            }
        }

        // Get : api/film-star/sagar/true
        // Get : api/1.0/film-star/sagar/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            FilmStarVM filmStar = await _service.ReadFilmStarAsync(name, include);

            if (filmStar == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(filmStar);
            }
        }

        // Post : api/film-star/
        // Post : api/film-star/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateFilmStarVM createFilmStarVM)
        {
            string result = await _service.CreateFilmStarAsync(createFilmStarVM);

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

        // Put : api/film-star/
        // Put : api/film-star/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateFilmStarVM updateFilmStarVM)
        {
            string result = await _service.UpdateFilmStarAsync(updateFilmStarVM);

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

        // Delete : api/film-star/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/film-star/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteFilmStarAsync(id);

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
