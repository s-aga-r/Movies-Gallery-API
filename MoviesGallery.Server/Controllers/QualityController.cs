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
    [Route("api/{v:apiVersion}/quality")]
    public class QualityController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public QualityController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/quality/10/true 
        // Get : api/1.0/quality/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<QualityVM> qualities = await _service.ReadQualityAsync(include, noOfRecords);

            if (qualities == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (qualities.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(qualities);
            }
        }

        // Get : api/quality/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/quality/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            QualityVM quality = await _service.ReadQualityAsync(id, include);

            if (quality == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(quality);
            }
        }

        // Get : api/quality/720pHD/10/true 
        // Get : api/1.0/quality/720pHD/10/true 
        [HttpGet("{name}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<QualityVM> qualities = await _service.ReadQualityAsync(name, include, noOfRecords);

            if (qualities == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (qualities.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(qualities);
            }
        }

        // Get : api/quality/720pHD/true
        // Get : api/1.0/quality/720pHD/true
        [HttpGet("{name}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string name, [FromRoute] bool include)
        {
            QualityVM qualityVM = await _service.ReadQualityAsync(name, include);

            if (qualityVM == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(qualityVM);
            }
        }

        // Post : api/quality/
        // Post : api/quality/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateQualityVM createQualityVM)
        {
            string result = await _service.CreateQualityAsync(createQualityVM);

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

        // Put : api/quality/
        // Put : api/quality/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateQualityVM updateQualityVM)
        {
            string result = await _service.UpdateQualityAsync(updateQualityVM);

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

        // Delete : api/quality/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/quality/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteQualityAsync(id);

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
