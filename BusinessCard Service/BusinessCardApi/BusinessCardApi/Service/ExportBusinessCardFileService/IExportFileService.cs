using BusinessCardApi.Model;

namespace BusinessCardApi.Service.ExportBusinessCardFileService
{
    public interface IExportFileService
    {
        public string ConvertToCsv(BusinessCard businessCard);
        public string ConvertToXml(BusinessCard businessCard);
    }
}
