using BusinessCardApi.Model;

namespace BusinessCardApi.Service.ExportFileService
{
    public interface IExportFileService
    {
        public string ExportToCsv(BusinessCard businessCard);
        public string ExportToXml(BusinessCard businessCard);
        public byte[] ExportToQrCode(BusinessCard businessCard);
    }
}
