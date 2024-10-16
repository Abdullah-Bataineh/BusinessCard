using BusinessCardApi.Exceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController : ControllerBase
    {
        private readonly ILogger<BusinessCardController> _logger;

        private readonly IBusinessCardService _businessCardService;
        public BusinessCardController(IBusinessCardService businessCardService, ILogger<BusinessCardController> logger)
        {
            _businessCardService = businessCardService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessCards() 
        {
            try
            {
                var businessCards = await _businessCardService.GetAllBusinessCard();
                return Ok(businessCards);
            }
            catch (GetAllBusinessCardException ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusinessCard(BusinessCard businessCard)
        {
            try
            {

                await _businessCardService.CreateBusinessCard(businessCard);
                return Ok(new { message = "Business card created successfully." });
            }
            catch (CreateBusinessCardException ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessCard(int id)
        {
            try
            {
                await _businessCardService.DeleteBusinessCard(id);
                return Ok(new { message = $"Business card with ID: {id} deleted successfully." });
            }
            catch (DeleteBusinessCardException ex)
            {
                _logger.LogError(ex, $"Error deleting business card with ID: {id}");
                return StatusCode(500, new { error = ex.Message });
            }
            
        }

    }
}
