using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Model;
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
    public class DeleteBusinessCardServiceTests
    {
        private readonly Mock<IBusinessCardRepository> _mockRepository;
        private readonly BusinessCardService _service;

        public DeleteBusinessCardServiceTests()
        {
            _mockRepository = new Mock<IBusinessCardRepository>();
            _service = new BusinessCardService(_mockRepository.Object);
        }

        [Fact]
        public async Task DeleteBusinessCard_IdLessThanOrEqualToZero_ThrowsDeleteBusinessCardException()
        {
            int invalidId = -1;
            var exception = await Assert.ThrowsAsync<DeleteBusinessCardException>(() => _service.DeleteBusinessCard(invalidId));
            Assert.Equal("The ID must be greater than 0.", exception.Message);
        }

        [Fact]
        public async Task DeleteBusinessCard_BusinessCardNotFound_ThrowsDeleteBusinessCardException()
        {
            int validId = 1;
            _mockRepository.Setup(repo => repo.GetBusinessCardById(validId)).ReturnsAsync((BusinessCard)null);
            var exception = await Assert.ThrowsAsync<DeleteBusinessCardException>(() => _service.DeleteBusinessCard(validId));
            Assert.Equal($"Business card with ID: {validId} not found.", exception.Message);
        }

    }
}
