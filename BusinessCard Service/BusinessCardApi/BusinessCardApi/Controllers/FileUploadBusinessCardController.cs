using BusinessCardApi.Exceptions;
using BusinessCardApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadBusinessCardController : ControllerBase
    {
        private readonly IBusinessCardFacade _businessCardFacade;

        public FileUploadBusinessCardController(IBusinessCardFacade businessCardFacade)
        {
            _businessCardFacade = businessCardFacade;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var data = await _businessCardFacade.ProcessFileAsync(file);
                return Ok(data);
            }
            catch (FileUploadBusinessCardException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) { 
            return StatusCode(500,ex.Message);
            }
        }
    }
}
