using BusinessCardApi.Exceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Repository;

namespace BusinessCardApi.Service
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly ILogger<BusinessCardService> _logger;

        private readonly IBusinessCardRepository _businessCardRepository;
        public BusinessCardService(IBusinessCardRepository businessCardRepository, ILogger<BusinessCardService> logger)
        {
            _businessCardRepository = businessCardRepository;
            _logger = logger;
        }

        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            try
            {
                await _businessCardRepository.CreateBusinessCard(businessCard);
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while creating the business card for {businessCard.Name}.");
                throw new CreateBusinessCardException("Failed to create the business card. Please check the input model."+ ex.Message);
            }
        }

        public async Task DeleteBusinessCard(int id)
        {
            try
            {
                await _businessCardRepository.DeleteBusinessCard(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the business card with ID: {id}.");
                throw new DeleteBusinessCardException($"Failed to delete the business card with ID: {id}"+ ex.Message);
            }
        }

        public async Task<IEnumerable<BusinessCard>> GetAllBusinessCard()
        {
            try
            {
               return await _businessCardRepository.GetAllBusinessCard();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all business cards.");
                throw new GetAllBusinessCardException("Failed to retrieve business BusinessCards: " + ex.Message);
            }
        }
    }
}
