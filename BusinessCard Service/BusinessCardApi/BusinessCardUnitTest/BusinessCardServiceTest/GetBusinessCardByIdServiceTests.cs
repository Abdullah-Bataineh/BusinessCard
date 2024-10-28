using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Repository.BusinessCardRepository;
using BusinessCardApi.Service.BusinessCardService;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardUnitTest.BusinessCardServiceTest
{
    public class GetBusinessCardByIdServiceTests
    {
        private readonly BusinessCardService _service;
        private readonly Mock<IBusinessCardRepository> _mockRepository;

        public GetBusinessCardByIdServiceTests()
        {
            _mockRepository = new Mock<IBusinessCardRepository>();
            _service = new BusinessCardService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetBusinessCardById_WhenIdIsLessThanZero_ShouldThrowGetBusinessCardByIdException()
        {
            var invalidId = -1;
            var exception = await Assert.ThrowsAsync<GetBusinessCardByIdException>(() => _service.GetBusinessCardById(invalidId));
            Assert.Equal("Business card ID must be greater than zero.", exception.Message);
        }
    }
}
