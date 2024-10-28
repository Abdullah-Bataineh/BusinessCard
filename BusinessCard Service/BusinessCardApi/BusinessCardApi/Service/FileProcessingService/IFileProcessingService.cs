using BusinessCardApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardApi.Service.FileProcessingService
{
    public interface IFileProcessingService
    {
         Task<(byte[] fileData, string contentType, string fileName)> Export(BusinessCard businessCard, string format);
        Task<BusinessCard> Import(IFormFile file);
    }
}
