using BusinessCardApi.Exceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service.FileProcessingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileProcessingController : ControllerBase
    {
        private readonly IFileProcessingService _fileProcessingService;

        public FileProcessingController(IFileProcessingService fileProcessingService) {
        _fileProcessingService = fileProcessingService;
        }

        [HttpPost("Export")]
        public async Task<IActionResult> Export([FromBody] BusinessCard businessCard, [FromQuery] string format) 
        {
            try
            {
                var (fileData, contentType, fileName) = await _fileProcessingService.Export(businessCard, format);
                return File(fileData, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Import")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var data = await _fileProcessingService.Import(file);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
