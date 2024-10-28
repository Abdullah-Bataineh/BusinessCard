using BusinessCardApi.Model;
using CsvHelper;
using System.Drawing.Imaging;
using System.Globalization;
using ZXing.QrCode;
using ZXing;
using System.Text.Json;
using System.Xml.Serialization;

namespace BusinessCardApi.Service.ExportFileService
{
    public class ExportFileService : IExportFileService
    {
        public string ExportToCsv(BusinessCard businessCard)
        {
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<BusinessCard>();
                csv.NextRecord();
                csv.WriteRecord(businessCard);
                csv.NextRecord(); 

                return writer.ToString();
            }
        }

        public byte[] ExportToQrCode(BusinessCard businessCard)
        {
            var jsonData = JsonSerializer.Serialize(businessCard);
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1,
                    ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L
                }
            };

            using (var bitmap = writer.Write(jsonData))
            {
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        public string ExportToXml(BusinessCard businessCard)
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
