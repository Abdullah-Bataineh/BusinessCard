using BusinessCardApi.Model;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Drawing;
using ZXing;
using Newtonsoft.Json;
using System.Xml.Serialization;
using BusinessCardApi.Exceptions.FileProcessingExceptions;

namespace BusinessCardApi.Service.ImportFileService
{
    public class ImportFileService : IImportFileService
    {
        public async Task<BusinessCard> ImportFromCsv(string csvContent)
        {
            using (var reader = new StringReader(csvContent))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true, 
                IgnoreBlankLines = true, 
            }))
            {
                var records = csv.GetRecords<BusinessCard>();
                var firstRecord = await Task.FromResult(records.FirstOrDefault());
                return firstRecord;
            }
        }

        public async Task<BusinessCard> ImportFromQrCode(IFormFile imageStream)
        {
            using (var stream = new MemoryStream())
            {
                await imageStream.CopyToAsync(stream);
                stream.Position = 0;
                using (var image = Image.FromStream(stream))
                {
                    var barcodeReader = new BarcodeReader();
                    var result = barcodeReader.Decode((Bitmap)image);
                    try
                    {
                        return JsonConvert.DeserializeObject<BusinessCard>(result.Text);
                    }
                    catch (JsonException ex)
                    {
                        throw new ImportFileException("Failed to decode QR code; data format is incorrect.");
                    }
                }
            }
        }

        public async Task<BusinessCard> ImportFromXml(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(BusinessCard));

            using (var reader = new StringReader(xmlContent))
            {
                var businessCard = (BusinessCard)serializer.Deserialize(reader);
                foreach (var prop in typeof(BusinessCard).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var value = (string)prop.GetValue(businessCard);
                        if (string.IsNullOrEmpty(value))
                        {
                            prop.SetValue(businessCard, null);
                        }
                    }
                    else if (prop.PropertyType == typeof(DateOnly))
                    {
                        var dateValue = (string)prop.GetValue(businessCard);
                        if (DateOnly.TryParse(dateValue, out var date))
                        {
                            prop.SetValue(businessCard, date);
                        }
                        else
                        {
                            prop.SetValue(businessCard, default(DateOnly));
                        }
                    }
                }

                return await Task.FromResult(businessCard);
            }
        }
    }
}
