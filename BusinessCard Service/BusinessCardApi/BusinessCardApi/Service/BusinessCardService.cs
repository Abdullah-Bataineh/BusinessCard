using BusinessCardApi.Model;
using BusinessCardApi.Repository;

namespace BusinessCardApi.Service
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly IBusinessCardRepository _businessCardRepository;
        public BusinessCardService(IBusinessCardRepository businessCardRepository)
        {
            _businessCardRepository = businessCardRepository;
        }

        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            try
            {
                await _businessCardRepository.CreateBusinessCard(businessCard);
            }
            catch (Exception ex) {
                throw new Exception("Please Check Filed Model", ex);
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
            throw new NotImplementedException("Not Found id BusinessCard with delete");
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
                throw new NotImplementedException("Not Found BusinessCard");
            }
        }
    }
}
