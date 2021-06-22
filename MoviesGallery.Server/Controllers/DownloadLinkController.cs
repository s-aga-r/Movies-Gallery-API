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
    [Route("api/{v:apiVersion}/download-link")]
    public class DownloadLinkController : ControllerBase
    {
        // Private Field(s)
        private readonly IMoviesGalleryService _service;

        // Public Constructor
        public DownloadLinkController(IMoviesGalleryService service)
        {
            _service = service;
        }

        // Get : api/download-link/10/true 
        // Get : api/1.0/download-link/10/true 
        [HttpGet("{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<DownloadLinkVM> downloadLinks = await _service.ReadDownloadLinkAsync(include, noOfRecords);

            if (downloadLinks == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (downloadLinks.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(downloadLinks);
            }
        }

        // Get : api/download-link/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        // Get : api/1.0/download-link/f77ecd66-f531-4a7a-adfb-c16e8b34afa5/true
        [HttpGet("{id:guid}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, [FromRoute] bool include)
        {
            DownloadLinkVM downloadLink = await _service.ReadDownloadLinkAsync(id, include);

            if (downloadLink == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(downloadLink);
            }
        }

        // Get : api/download-link/kgf/10/true 
        // Get : api/1.0/download-link/kgf/10/true 
        [HttpGet("{title}/{noOfRecords:int}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] int noOfRecords, [FromRoute] bool include)
        {
            List<DownloadLinkVM> downloadLinks = await _service.ReadDownloadLinkAsync(title, include, noOfRecords);

            if (downloadLinks == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while fetching data from database, please contact the administrator.");
            }
            else if (downloadLinks.Count() < 1)
            {
                return NoContent();
            }
            else
            {
                return Ok(downloadLinks);
            }
        }

        // Get : api/download-link/kgf/true
        // Get : api/1.0/download-link/kgf/true
        [HttpGet("{title}/{include:bool}")]
        public async Task<IActionResult> Get([FromRoute] string title, [FromRoute] bool include)
        {
            DownloadLinkVM downloadLink = await _service.ReadDownloadLinkAsync(title, include);

            if (downloadLink == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(downloadLink);
            }
        }

        // Post : api/download-link/
        // Post : api/download-link/1.0/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateDownloadLinkVM createDownloadLinkVM)
        {
            string result = await _service.CreateDownloadLinkAsync(createDownloadLinkVM);

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

        // Put : api/download-link/
        // Put : api/download-link/1.0/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateDownloadLinkVM updateDownloadLinkVM)
        {
            string result = await _service.UpdateDownloadLinkAsync(updateDownloadLinkVM);

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

        // Delete : api/download-link/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        // Delete : api/1.0/download-link/f77ecd66-f531-4a7a-adfb-c16e8b34afa5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            string result = await _service.DeleteDownloadLinkAsync(id);

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
