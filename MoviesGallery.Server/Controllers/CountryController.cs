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
    [Route("api/{v:apiVersion}/country")]
    public class CountryController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public CountryController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/country/10/true 
        // Get : api/1.0/country/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<CountryVM> countries = await _service.ReadCountryAsync(include, noOfRecords);

            if (countries == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (countries.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(countries);
            }
        }

        // Get : api/country/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/country/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            CountryVM country = await _service.ReadCountryAsync(id, include);

            if (country == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);
            }
        }

        // Get : api/country/india/10/true 
        // Get : api/1.0/country/india/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<CountryVM> countries = await _service.ReadCountryAsync(name, include, noOfRecords);

            if (countries == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (countries.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(countries);
            }
        }

        // Get : api/country/india/true
        // Get : api/1.0/country/india/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            CountryVM country = await _service.ReadCountryAsync(name, include);

            if (country == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);
            }
        }

        // Post : api/country/
        // Post : api/country/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateCountryVM createCountryVM)
        {
            string result = await _service.CreateCountryAsync(createCountryVM);

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

        // Put : api/country/
        // Put : api/country/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateCountryVM updateCountryVM)
        {
            string result = await _service.UpdateCountryAsync(updateCountryVM);

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

        // Delete : api/country/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/country/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteCountryAsync(id);

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
