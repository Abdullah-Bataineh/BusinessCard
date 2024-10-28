using BusinessCardApi.Enum;
using BusinessCardApi.Exceptions;
using BusinessCardApi.Exceptions.BusinessCardExceptions;
using BusinessCardApi.Model;
using BusinessCardApi.Repository.BusinessCardRepository;
using System.Text.RegularExpressions;

namespace BusinessCardApi.Service.BusinessCardService
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly ILogger<BusinessCardService> _logger;

        private readonly IBusinessCardRepository _businessCardRepository;
        private readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public BusinessCardService(IBusinessCardRepository businessCardRepository, ILogger<BusinessCardService> logger=null)
        {
            _businessCardRepository = businessCardRepository;
            _logger = logger;
        }

        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            if (businessCard == null)
            {
                _logger?.LogError("Business card is null.");
                throw new ArgumentNullException(nameof(businessCard));
            }
            ValidateBusinessCardFields(businessCard);
            await _businessCardRepository.CreateBusinessCard(businessCard);
            _logger.LogInformation("Business card created successfully.");
        }

        public async Task DeleteBusinessCard(int id)
        {
            if (id <= 0)
            {
                _logger?.LogError("The ID must be greater than 0.");

                throw new DeleteBusinessCardException("The ID must be greater than 0.");
            }
            var businessCard = await _businessCardRepository.GetBusinessCardById(id);
            if (businessCard == null)
            {
                _logger?.LogError($"Business card with ID: {id} not found.");
                throw new DeleteBusinessCardException($"Business card with ID: {id} not found.");
            }
            await _businessCardRepository.DeleteBusinessCard(id);
        }

        public async Task<PaginationResult<BusinessCard>> GetAllBusinessCards(int page, int pageSize, string name = null, string email = null, string phone = null, string address = null, string gender = null, int? year = null, int? month = null, int? day = null)
        {
            if (page <= 0)
            {
                _logger?.LogError("Page number must be greater than 0.");
                throw new GetAllBusinessCardException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                _logger?.LogError("Page size must be greater than 0.");
                throw new GetAllBusinessCardException("Page size must be greater than 0.");
            }

            return await _businessCardRepository.GetAllBusinessCards(page, pageSize, name, email, phone, address, gender, year, month, day);

        }

        public async Task<BusinessCard> GetBusinessCardById(int id)
        {
            if (id < 0)
            {
                _logger?.LogError("Business card ID must be greater than zero.");
                throw new GetBusinessCardByIdException("Business card ID must be greater than zero.");
            }

            var businessCard = await _businessCardRepository.GetBusinessCardById(id);

            if (businessCard == null)
            {
                _logger?.LogError($"Business card with ID: {id} not found.");
                throw new GetBusinessCardByIdException($"Business card with ID: {id} not found.");
            }

            return businessCard;

        }

        public async Task UpdateBusinessCard(BusinessCard businessCard)
        {
            if (businessCard.Id <= 0)
            {

                _logger?.LogError("The ID must be greater than 0.");
                throw new UpdateBusinessCardException("The ID must be greater than 0.");
            }
            if (businessCard == null)
            {
                _logger?.LogError("Business card is null.");
                throw new NullReferenceException(nameof(businessCard));
            }
            var _businessCard = await _businessCardRepository.GetBusinessCardById(businessCard.Id);
            if (_businessCard == null)
            {
                _logger?.LogError($"Business card with ID: {businessCard.Id} not found.");
                throw new UpdateBusinessCardException($"Business card with ID: {businessCard.Id} not found.");
            }

            ValidateBusinessCardFields(businessCard);
            await _businessCardRepository.UpdateBusinessCard(businessCard);
            _logger?.LogInformation("Business card created successfully.");
        }
        private void ValidateBusinessCardFields(BusinessCard businessCard)
        {
           

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
        }
    }
}
