using BusinessCardApi.Model;

namespace BusinessCardApi.Service.ImportFileService
{
    public interface IImportFileService
    {
        Task<BusinessCard> ImportFromCsv(string csvContent);
        Task<BusinessCard> ImportFromXml(string xmlContent);
        Task<BusinessCard> ImportFromQrCode(IFormFile imagePath);
    }
}
