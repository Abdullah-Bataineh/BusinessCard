using BusinessCardApi.Model;
using BusinessCardApi.Repository.ExportBusinessCardFileRepository;

namespace BusinessCardApi.Service.ExportBusinessCardFileService
{
    public class ExportFileService : IExportFileService
    {
        private readonly IExportFileRepository _exportFileRepository;
        public ExportFileService(IExportFileRepository exportFileRepository)
        {
            _exportFileRepository = exportFileRepository;
        }
        public string ConvertToCsv(BusinessCard businessCard)
        {
            return _exportFileRepository.ConvertToCsv(businessCard);
        }

        public string ConvertToXml(BusinessCard businessCard)
        {
            return _exportFileRepository.ConvertToXml(businessCard);
        }
    }
}
