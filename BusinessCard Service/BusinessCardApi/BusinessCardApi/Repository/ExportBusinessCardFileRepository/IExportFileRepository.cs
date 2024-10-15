using BusinessCardApi.Model;

namespace BusinessCardApi.Repository.ExportBusinessCardFileRepository
{
    public interface IExportFileRepository
    {
        public string ConvertToCsv(BusinessCard businessCard);
        public string ConvertToXml(BusinessCard businessCard);
    }
}
