using BusinessCardApi.Model;

namespace BusinessCardApi.Service
{
    public interface IBusinessCardService
    {
        public Task CreateBusinessCard(BusinessCard businessCard);
        public Task<IEnumerable<BusinessCard>> GetAllBusinessCard();
        public Task DeleteBusinessCard(int id);
    }
}
