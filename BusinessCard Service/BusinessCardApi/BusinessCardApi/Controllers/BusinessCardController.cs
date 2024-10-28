using BusinessCardApi.Exceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service.BusinessCardService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController : ControllerBase
    {
        private readonly IBusinessCardService _businessCardService;
        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBusinessCards(int page, int pageSize, string name = null, string email = null, string phone = null, string address = null, string gender = null, int? year = null, int? month = null, int? day = null)
        {
            try
            {
                var result = await _businessCardService.GetAllBusinessCards(page, pageSize, name, email, phone, address, gender, year, month, day);

                return Ok(new
                {
                    data = result.Data,
                    recordsTotal = result.RecordsTotal,
                    recordsFiltered = result.RecordsFiltered
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
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
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
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
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessCardById(int id)
        {
            try
            {
                var businesscard = await _businessCardService.GetBusinessCardById(id);
                return Ok(businesscard);
            }

            catch (Exception ex) 
            {
                return StatusCode(500, new { ex.Message }); 
            }
        }
        [HttpPut]
        public async Task<IActionResult> UbdateBusinessCard(BusinessCard businessCard)
        {
            try
            {
                await _businessCardService.UpdateBusinessCard(businessCard);
                return Ok(new { message = $"Update Business Card successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

    }
}
