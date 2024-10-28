using BusinessCardApi.Exceptions.FileProcessingExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Service.FileProcessingService;
using BusinessCardApi.Service.ImportFileService;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BusinessCardUnitTest.FileProcesssingServiceTest
{
    public class ImportFileServiceTests
    {
        private readonly FileProcessingService _service;
        private readonly Mock<IImportFileService> _mockImportFileService;

        public ImportFileServiceTests()
        {
            _mockImportFileService = new Mock<IImportFileService>();
            _service = new FileProcessingService(null,_mockImportFileService.Object);
        }

        [Fact]
        public async Task Import_NullFile_ThrowsImportFileException()
        {
            IFormFile file = null;
            var exception = await Assert.ThrowsAsync<ImportFileException>(() => _service.Import(file));
            Assert.Equal("The file cannot be null.", exception.Message);
        }

        [Fact]
        public async Task Import_InvalidContentType_ThrowsImportFileException()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.ContentType).Returns("application/pdf");
            fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());
            var exception = await Assert.ThrowsAsync<ImportFileException>(() => _service.Import(fileMock.Object));
            Assert.Equal("Please upload a valid CSV, XML, or image file.", exception.Message);
        }

        [Fact]
        public async Task Import_ImageFile_SuccessfullyDecodesQrCode()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.ContentType).Returns("image/png");
            fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());
            var expectedBusinessCard = new BusinessCard { Name = "Abdullah" };
            _mockImportFileService.Setup(s => s.ImportFromQrCode(fileMock.Object)).ReturnsAsync(expectedBusinessCard);
            var result = await _service.Import(fileMock.Object);
            Assert.Equal(expectedBusinessCard, result);
        }
    }
}
