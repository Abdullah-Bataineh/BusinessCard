using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Repository.BusinessCardRepository;
using BusinessCardApi.Service.BusinessCardService;
using Microsoft.Extensions.Logging;
using Moq;

namespace BusinessCardUnitTest.BusinessCardServiceTest
{
    public class CreateBusinessCardServiceTests
    {
        private readonly BusinessCardService _service;
        private readonly Mock<IBusinessCardRepository> _mockRepository;

        public CreateBusinessCardServiceTests()
        {
            _mockRepository = new Mock<IBusinessCardRepository>();
            _service = new BusinessCardService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowArgumentNullException_WhenBusinessCardIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateBusinessCard(null));
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogError_WhenNameContainsInvalidCharacters()
        {
            var businessCard = new BusinessCard { Name = "Abdullah Bataineh" };
            await Assert.ThrowsAsync<NameFieldException>(() => _service.CreateBusinessCard(businessCard));
            
        }
        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowGenderFieldException_WhenGenderIsInvalid()
        {
            var businessCard = new BusinessCard { Name = "Abdullah Bataineh", Gender = "Unknown" };
            await Assert.ThrowsAsync<GenderFieldException>(() => _service.CreateBusinessCard(businessCard));
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowDOBFieldException_WhenDOBIsOutOfRange()
        {
            var businessCard = new BusinessCard { Name = "Abdullah", Gender = "Bataineh", DOB = new DateTime(1950, 1, 1) };
            await Assert.ThrowsAsync<DOBFieldException>(() => _service.CreateBusinessCard(businessCard));
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowEmailFieldException_WhenEmailIsInvalid()
        {
            var businessCard = new BusinessCard { Name = "John", Gender = "Male", DOB = new DateTime(1980, 1, 1), Email = "invalid-email" };
            await Assert.ThrowsAsync<EmailFieldException>(() => _service.CreateBusinessCard(businessCard));
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowPhoneFieldException_WhenPhoneIsInvalid()
        {
            var businessCard = new BusinessCard { Name = "Abdullah Bataineh", Gender = "Male", DOB = new DateTime(1980, 1, 1), Email = "test@example.com", Phone = "12345" };
            await Assert.ThrowsAsync<PhoneFieldException>(() => _service.CreateBusinessCard(businessCard));
        }

        [Fact]
        public async Task CreateBusinessCard_ShouldLogErrorAndThrowAddressFieldException_WhenAddressIsNull()
        {
            var businessCard = new BusinessCard { Name = "John", Gender = "Male", DOB = new DateTime(1980, 1, 1), Email = "test@example.com", Phone = "1234567890", Address = null };
            await Assert.ThrowsAsync<AddressFieldException>(() => _service.CreateBusinessCard(businessCard));
        }
    }
}