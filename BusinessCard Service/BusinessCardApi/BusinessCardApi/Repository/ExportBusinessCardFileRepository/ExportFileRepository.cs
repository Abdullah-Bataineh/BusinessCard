using BusinessCardApi.Model;
using BusinessCardApi.Repository.ExportBusinessCardFileRepository;
using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;

namespace BusinessCardApi.Repository.ExportBusinessCardFileRespository
{
    public class ExportFileRepository : IExportFileRepository
    {
        public string ConvertToCsv(BusinessCard businessCard)
        {
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(new List<BusinessCard> { businessCard });
                return writer.ToString();
            }
        }

        public string ConvertToXml(BusinessCard businessCard)
        {
            var xmlSerializer = new XmlSerializer(typeof(BusinessCard));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, businessCard);
                return stringWriter.ToString();
            }
        }
    }
}
