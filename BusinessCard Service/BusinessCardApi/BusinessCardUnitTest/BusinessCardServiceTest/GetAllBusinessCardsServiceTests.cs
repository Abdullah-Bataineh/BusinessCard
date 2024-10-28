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
    public class GetAllBusinessCardsServiceTests
    {
        private readonly BusinessCardService _service;
        private readonly Mock<IBusinessCardRepository> _mockRepository;

        public GetAllBusinessCardsServiceTests()
        {
            _mockRepository = new Mock<IBusinessCardRepository>();
            _service = new BusinessCardService(_mockRepository.Object);
        }

        [Theory]
        [InlineData(0, 10, "Page number must be greater than 0.")]
        [InlineData(-1, 10, "Page number must be greater than 0.")]
        public async Task GetAllBusinessCards_InvalidPage_ThrowsExceptionAndLogsError(int page, int pageSize, string expectedMessage)
        {
            var exception = await Assert.ThrowsAsync<GetAllBusinessCardException>(() => _service.GetAllBusinessCards(page, pageSize));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(1, 0, "Page size must be greater than 0.")]
        [InlineData(1, -5, "Page size must be greater than 0.")]
        public async Task GetAllBusinessCards_InvalidPageSize_ThrowsExceptionAndLogsError(int page, int pageSize, string expectedMessage)
        {
            var exception = await Assert.ThrowsAsync<GetAllBusinessCardException>(() =>_service.GetAllBusinessCards(page, pageSize));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
