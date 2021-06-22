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
    [Route("api/{v:apiVersion}/language")]
    public class LanguageController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public LanguageController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/language/10/true 
        // Get : api/1.0/language/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<LanguageVM> languages = await _service.ReadLanguageAsync(include, noOfRecords);

            if (languages == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (languages.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(languages);
            }
        }

        // Get : api/language/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/language/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            LanguageVM language = await _service.ReadLanguageAsync(id, include);

            if (language == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(language);
            }
        }

        // Get : api/language/hindi/10/true 
        // Get : api/1.0/language/hindi/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<LanguageVM> languages = await _service.ReadLanguageAsync(name, include, noOfRecords);

            if (languages == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (languages.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(languages);
            }
        }

        // Get : api/language/hindi/true
        // Get : api/1.0/language/hindi/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            LanguageVM language = await _service.ReadLanguageAsync(name, include);

            if (language == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(language);
            }
        }

        // Post : api/language/
        // Post : api/language/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateLanguageVM createLanguageVM)
        {
            string result = await _service.CreateLanguageAsync(createLanguageVM);

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

        // Put : api/language/
        // Put : api/language/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateLanguageVM updateLanguageVM)
        {
            string result = await _service.UpdateLanguageAsync(updateLanguageVM);

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

        // Delete : api/language/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/language/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteLanguageAsync(id);

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
