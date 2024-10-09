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
        private readonly IBusinessCardService _businessCardService;
        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }
        [HttpGet]
        public async Task<IEnumerable<BusinessCard>> GetAllBusinessCards() {
        return await _businessCardService.GetAllBusinessCard();
        }
        [HttpPost]
        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            await _businessCardService.CreateBusinessCard(businessCard);
        }
        [HttpDelete]
        [Route("/{id}")]
        public async Task DeleteBusinessCard(int id)
        {
            await _businessCardService.DeleteBusinessCard(id);
        }

    }
}
