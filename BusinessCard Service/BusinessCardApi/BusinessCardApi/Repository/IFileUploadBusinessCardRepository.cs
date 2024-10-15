using BusinessCardApi.Model;

namespace BusinessCardApi.Repository
{
    public interface IFileUploadBusinessCardRepository
    {
        Task<BusinessCard> GetBusinessCardsFromCsvAsync(string csvContent);
        Task<BusinessCard> GetBusinessCardsFromXmlAsync(string xmlContent);
        bool IsValidModel(BusinessCard businessCard);
    }
}
