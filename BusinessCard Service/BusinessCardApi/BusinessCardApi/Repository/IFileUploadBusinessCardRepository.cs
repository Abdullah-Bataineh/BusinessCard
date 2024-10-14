using BusinessCardApi.Model;

namespace BusinessCardApi.Repository
{
    public interface IFileUploadBusinessCardRepository
    {
        Task<List<BusinessCard>> GetBusinessCardsFromCsvAsync(string csvContent);
        Task<List<BusinessCard>> GetBusinessCardsFromXmlAsync(string xmlContent);
        bool IsValidModel(BusinessCard businessCard);
    }
}
