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
    [Route("api/{v:apiVersion}/movie")]
    public class MovieController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public MovieController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/movie/10/true 
        // Get : api/1.0/movie/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<MovieVM> movies = await _service.ReadMovieAsync(include, noOfRecords);

            if (movies == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (movies.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(movies);
            }
        }

        // Get : api/movie/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/movie/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            MovieVM movie = await _service.ReadMovieAsync(id, include);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        // Get : api/movie/kgf/10/true 
        // Get : api/1.0/movie/kgf/10/true 
        [HttpGet("{title}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<MovieVM> movies = await _service.ReadMovieAsync(title, include, noOfRecords);

            if (movies == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (movies.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(movies);
            }
        }

        // Get : api/movie/kgf/true
        // Get : api/1.0/movie/kgf/true
        [HttpGet("{title}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] bool include)
        {
            MovieVM movie = await _service.ReadMovieAsync(title, include);

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }
        }

        // Post : api/movie/
        // Post : api/movie/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateMovieVM createMovieVM)
        {
            string result = await _service.CreateMovieAsync(createMovieVM);

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

        // Put : api/movie/
        // Put : api/movie/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateMovieVM updateMovieVM)
        {
            string result = await _service.UpdateMovieAsync(updateMovieVM);

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

        // Delete : api/movie/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/movie/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteMovieAsync(id);

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
