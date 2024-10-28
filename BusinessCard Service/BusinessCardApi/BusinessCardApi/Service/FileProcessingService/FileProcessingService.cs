using BusinessCardApi.Enum;
using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Exceptions.FileProcessingExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service.ExportFileService;
using BusinessCardApi.Service.ImportFileService;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BusinessCardApi.Service.FileProcessingService
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly IExportFileService _exportFileService;
        private readonly IImportFileService _importFileService;
        private readonly ILogger<FileProcessingService> _logger;
        private readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public FileProcessingService(IExportFileService exportFileService=null,IImportFileService importFileService=null, ILogger<FileProcessingService> logger=null)
        {
            _exportFileService = exportFileService;
            _importFileService = importFileService;
            _logger = logger;
        }

        public async Task<(byte[] fileData, string contentType, string fileName)> Export(BusinessCard businessCard, string format)
        {
            if (businessCard == null)
            {
                _logger?.LogError("BusinessCard must not be null.");
                throw new ExportFileException("BusinessCard must not be null.");
            }

            if (!IsValidJson(businessCard))
            {
                _logger?.LogError("BusinessCard must contain valid JSON data.");
                throw new ExportFileException("BusinessCard must contain valid JSON data.");
            }

            if (string.IsNullOrEmpty(format))
            {
                _logger?.LogError("Format must not be empty.");
                throw new ExportFileException("Format must not be empty.");
            }
            if (!businessCard.Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                _logger?.LogError("The name contains invalid characters.");
                throw new NameFieldException("The name contains invalid characters.");
            }

            if (!(businessCard.Gender.ToLower() == Gender.Male.ToString().ToLower() || businessCard.Gender.ToLower() == Gender.Female.ToString().ToLower()))
            {
                _logger?.LogError("Gender must be either 'Male' or 'Female'.");
                throw new GenderFieldException("Gender must be either 'Male' or 'Female'.");
            }

            if (!(businessCard.DOB.Year > 1959 && businessCard.DOB.Year < DateTime.Now.Year))
            {
                _logger?.LogError("Date of Birth must be after 1960 and before the current year.");
                throw new DOBFieldException("Date of Birth must be after 1960 and before the current year.");
            }

            if (!Regex.IsMatch(businessCard.Email, emailPattern))
            {
                _logger?.LogError("The email address is not valid.");
                throw new EmailFieldException("The email address is not valid.");
            }

            if (!(businessCard.Phone.Length >= 10 && businessCard.Phone.All(char.IsDigit)))
            {
                _logger?.LogError("Phone number must be exactly 10 digits and contain only numbers.");
                throw new PhoneFieldException("Phone number must be exactly 10 digits and contain only numbers.");
            }

            if (string.IsNullOrWhiteSpace(businessCard.Address))
            {
                _logger?.LogError("Address cannot be null or empty.");
                throw new AddressFieldException("Address cannot be null or empty.");
            }

            if (format.Equals(FileType.csv.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var csvData = _exportFileService?.ExportToCsv(businessCard);
                var fileName = "business_card.csv";
                return (Encoding.UTF8.GetBytes(csvData), "text/csv", fileName);
            }
            else if (format.Equals(FileType.xml.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var xmlData = _exportFileService?.ExportToXml(businessCard);
                var fileName = "business_card.xml";
                return (Encoding.UTF8.GetBytes(xmlData), "application/xml", fileName);
            }
            else if (format.Equals(FileType.qrcode.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var qrCodeBytes = _exportFileService?.ExportToQrCode(businessCard);
                var fileName = "business_card.png";
                return (qrCodeBytes, "image/png", fileName);
            }
            _logger?.LogError("Unsupported format. Use 'csv', 'xml', or QrCode '.png' '.jpeg'.");
            throw new ExportFileException("Unsupported format. Use 'csv', 'xml', or QrCode '.png' '.jpeg'.");
        }

        public async Task<BusinessCard> Import(IFormFile file)
        {
            if (file == null)
            {
                _logger?.LogError("Uploaded file is null.");
                throw new ImportFileException("The file cannot be null.");
            }

            if (!IsValidContentType(file.ContentType))
            {
                _logger?.LogError("Invalid file type uploaded: {ContentType}", file.ContentType);
                throw new ImportFileException("Please upload a valid CSV, XML, or image file.");
            }

            if (file.ContentType.StartsWith("image/"))
            {
                var businessCard = await _importFileService?.ImportFromQrCode(file);

                if (businessCard == null)
                {
                    _logger?.LogError("Failed to decode QR code; business card is null.");
                    throw new ImportFileException("Failed to decode QR code; no data found.");
                }

                if (!IsValidJson(businessCard))
                {
                    _logger?.LogError("Decoded QR code data does not match the expected model.");
                    throw new ImportFileException("Decoded QR code data does not match the expected model.");
                }

                return businessCard;
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = await stream.ReadToEndAsync();
                BusinessCard businessCard = null;

                if (file.ContentType == "text/csv")
                {
                    businessCard = await _importFileService?.ImportFromCsv(fileContent);
                }
                else if (file.ContentType == "application/xml" || file.ContentType == "text/xml")
                {
                    businessCard = await _importFileService?.ImportFromXml(fileContent);
                }
                else
                {
                    _logger?.LogError("Unsupported file type: {ContentType}", file.ContentType);
                    throw new ImportFileException("Unsupported file type. Please upload a valid CSV or XML file.");
                }

                if (!IsValidBusinessCard(businessCard))
                {
                    _logger?.LogError("Processed business card is null after import.");
                    throw new ImportFileException("The imported data resulted in a null or invalid business card.");
                }

                return businessCard;
            }
        }

        private bool IsValidContentType(string contentType)
        {
            return contentType == "text/csv" ||
                   contentType == "application/xml" ||
                   contentType == "text/xml" ||
                   contentType.StartsWith("image/");
        }

        private bool IsValidJson(BusinessCard businessCard)
        {
            var jsonString = JsonSerializer.Serialize(businessCard);
            return !string.IsNullOrEmpty(jsonString) && jsonString.StartsWith("{") && jsonString.EndsWith("}");
        }

        private bool IsValidBusinessCard(BusinessCard businessCard)
        {
            return businessCard != null && IsValidJson(businessCard);
        }
    }
}
