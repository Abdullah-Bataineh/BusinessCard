using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Exceptions.FileProcessingExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service.ExportFileService;
using BusinessCardApi.Service.FileProcessingService;
using Moq;
using System.Text;

namespace BusinessCardUnitTest.FileProcesssingServiceTest
{
    public class ExportFileServiceTests
    {
        private readonly FileProcessingService _service;
        private readonly Mock<IExportFileService> _mockExportFileService;

        public ExportFileServiceTests()
        {
            _mockExportFileService = new Mock<IExportFileService>();
            _service = new FileProcessingService(_mockExportFileService.Object);
        }

        [Fact]
        public async Task Export_NullBusinessCard_ThrowsExportFileException()
        {
            BusinessCard businessCard = null;
            var exception = await Assert.ThrowsAsync<ExportFileException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("BusinessCard must not be null.", exception.Message);
           
        }

        [Fact]
        public async Task Export_EmptyFormat_ThrowsExportFileException()
        {
            var businessCard = new BusinessCard {  };

            var exception = await Assert.ThrowsAsync<ExportFileException>(() => _service.Export(businessCard, ""));
            Assert.Equal("Format must not be empty.", exception.Message);
        }

        [Fact]
        public async Task Export_InvalidNameField_ThrowsNameFieldException()
        {
            var businessCard = new BusinessCard { Name = "John123" };
            var exception = await Assert.ThrowsAsync<NameFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("The name contains invalid characters.", exception.Message);
        }

        [Fact]
        public async Task Export_InvalidGenderField_ThrowsGenderFieldException()
        {
            var businessCard = new BusinessCard { Id=1,Name="Abdullah",Gender = "Unknown" };
            var exception = await Assert.ThrowsAsync<GenderFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("Gender must be either 'Male' or 'Female'.", exception.Message);
        }

        [Fact]
        public async Task Export_InvalidDOBField_ThrowsDOBFieldException()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male",DOB = new DateTime(1950, 1, 1) };
            var exception = await Assert.ThrowsAsync<DOBFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("Date of Birth must be after 1960 and before the current year.", exception.Message);
        }

        [Fact]
        public async Task Export_InvalidEmailField_ThrowsEmailFieldException()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "invalid-email"};
            var exception = await Assert.ThrowsAsync<EmailFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("The email address is not valid.", exception.Message);
        }

        [Fact]
        public async Task Export_InvalidPhoneField_ThrowsPhoneFieldException()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "12345" };
            var exception = await Assert.ThrowsAsync<PhoneFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("Phone number must be exactly 10 digits and contain only numbers.", exception.Message);
        }

        [Fact]
        public async Task Export_EmptyAddressField_ThrowsAddressFieldException()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "0795072792", Address = "" };
            var exception = await Assert.ThrowsAsync<AddressFieldException>(() => _service.Export(businessCard, "csv"));
            Assert.Equal("Address cannot be null or empty.", exception.Message);
        }

        [Fact]
        public async Task Export_ValidCsvFormat_ReturnsCsvData()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "0795072791", Address = "Amman" };
            string csvData = "Name,Gender,Email,Phone,Address,DOB\nAbdullah,Male,abd@gmail.com,0795072792,Amman,1970-01-01";
            _mockExportFileService.Setup(service => service.ExportToCsv(businessCard)).Returns(csvData);
            var result = await _service.Export(businessCard, "csv");
            Assert.Equal("text/csv", result.contentType);
            Assert.Equal("business_card.csv", result.fileName);
            Assert.Equal(Encoding.UTF8.GetBytes(csvData), result.fileData);
        }

        [Fact]
        public async Task Export_ValidXmlFormat_ReturnsXmlData()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "0795072791", Address = "Amman" };
            string xmlData = "Name,Gender,Email,Phone,Address,DOB\nAbdullah,Male,abd@gmail.com,0795072792,Amman,1970-01-01";
            _mockExportFileService.Setup(service => service.ExportToXml(businessCard)).Returns(xmlData);
            var result = await _service.Export(businessCard, "xml");
            Assert.Equal("application/xml", result.contentType);
            Assert.Equal("business_card.xml", result.fileName);
            Assert.Equal(Encoding.UTF8.GetBytes(xmlData), result.fileData);
        }

        [Fact]
        public async Task Export_ValidQrCodeFormat_ReturnsQrCodeData()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "0795072791" ,Address="Amman"};
            byte[] qrCodeBytes = { 1, 2, 3, 4 };
            _mockExportFileService.Setup(service => service.ExportToQrCode(businessCard)).Returns(qrCodeBytes);
            var result = await _service.Export(businessCard, "qrcode");
            Assert.Equal("image/png", result.contentType);
            Assert.Equal("business_card.png", result.fileName);
            Assert.Equal(qrCodeBytes, result.fileData);
        }

        [Fact]
        public async Task Export_UnsupportedFormat_ThrowsExportFileException()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "Abdullah", Gender = "Male", DOB = new DateTime(1970, 1, 1), Email = "abd@gmail.com", Phone = "0795072791", Address = "Amman" };
            var exception = await Assert.ThrowsAsync<ExportFileException>(() => _service.Export(businessCard, "pdf"));
            Assert.Equal("Unsupported format. Use 'csv', 'xml', or QrCode '.png' '.jpeg'.", exception.Message);
        }
    }
}
