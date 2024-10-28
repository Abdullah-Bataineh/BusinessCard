using BusinessCardApi.Model;

namespace BusinessCardApi.Service.BusinessCardService
{
    public interface IBusinessCardService
    {
        public Task CreateBusinessCard(BusinessCard businessCard);
        public Task<PaginationResult<BusinessCard>> GetAllBusinessCards(int page, int pageSize, string name = null, string email = null, string phone = null, string address = null, string gender = null, int? year = null, int? month = null, int? day = null);
        public Task DeleteBusinessCard(int id);
        public Task<BusinessCard> GetBusinessCardById(int id);
        public Task UpdateBusinessCard(BusinessCard businessCard);
    }
}
