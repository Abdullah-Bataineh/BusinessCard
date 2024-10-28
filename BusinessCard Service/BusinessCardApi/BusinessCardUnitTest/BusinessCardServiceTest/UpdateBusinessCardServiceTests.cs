using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Repository.BusinessCardRepository;
using BusinessCardApi.Service.BusinessCardService;
using Moq;


namespace BusinessCardUnitTest.BusinessCardServiceTest
{
    public class UpdateBusinessCardServiceTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepository;
        private readonly BusinessCardService _service;

        public UpdateBusinessCardServiceTests()
        {
            _mockRepository = new Mock<IBusinessCardRepository>();
            _service = new BusinessCardService(_mockRepository.Object);
        }

        [Fact]
        public async Task UpdateBusinessCard_NullBusinessCard_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<NullReferenceException>(() => _service.UpdateBusinessCard(null));

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidId_ThrowsUpdateBusinessCardException()
        {
            var businessCard = new BusinessCard { Id = -1 };
            var exception = await Assert.ThrowsAsync<UpdateBusinessCardException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("The ID must be greater than 0.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_CardNotFound_ThrowsUpdateBusinessCardException()
        {
            var businessCard = new BusinessCard { Id = 1 };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync((BusinessCard)null);
            var exception = await Assert.ThrowsAsync<UpdateBusinessCardException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("Business card with ID: 1 not found.", exception.Message);

        }


        [Fact]
        public async Task UpdateBusinessCard_InvalidName_ThrowsNameFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah"
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<NameFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("The name contains invalid characters.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidGender_ThrowsGenderFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah",
                Gender = "Unknown"
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<GenderFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("Gender must be either 'Male' or 'Female'.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidDOB_ThrowsDOBFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah",
                Gender = "Male",
                DOB = new DateTime(1950, 1, 1)
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<DOBFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("Date of Birth must be after 1960 and before the current year.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidEmail_ThrowsEmailFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah",
                Gender = "Male",
                DOB = new DateTime(1990, 1, 1),
                Email = "invalid-email"
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<EmailFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("The email address is not valid.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidPhone_ThrowsPhoneFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah",
                Gender = "Male",
                DOB = new DateTime(1990, 1, 1),
                Email = "john@example.com",
                Phone = "12345"
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<PhoneFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("Phone number must be exactly 10 digits and contain only numbers.", exception.Message);

        }

        [Fact]
        public async Task UpdateBusinessCard_InvalidAddress_ThrowsAddressFieldException()
        {
            var businessCard = new BusinessCard
            {
                Id = 1,
                Name = "Abdullah",
                Gender = "Male",
                DOB = new DateTime(1990, 1, 1),
                Email = "john@example.com",
                Phone = "1234567890",
                Address = ""
            };
            _mockRepository.Setup(repo => repo.GetBusinessCardById(businessCard.Id))
                           .ReturnsAsync(new BusinessCard { Id = 1 });

            var exception = await Assert.ThrowsAsync<AddressFieldException>(
                () => _service.UpdateBusinessCard(businessCard));

            Assert.Equal("Address cannot be null or empty.", exception.Message);

        }
    }
}
