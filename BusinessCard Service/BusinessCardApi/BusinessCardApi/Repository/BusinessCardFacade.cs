using BusinessCardApi.Model;

namespace BusinessCardApi.Repository
{
    public class BusinessCardFacade : IBusinessCardFacade
    {
        private readonly IFileUploadBusinessCardRepository _fileUploadBusinessCardRepository;

        public BusinessCardFacade(IFileUploadBusinessCardRepository fileUploadBusinessCardRepository)
        {
            _fileUploadBusinessCardRepository = fileUploadBusinessCardRepository;
        }

        public async Task<BusinessCard> ProcessFileAsync(IFormFile file)
        {
            if (file == null || (file.ContentType != "text/csv" && file.ContentType != "application/xml" && file.ContentType != "text/xml"))
            {
                throw new InvalidDataException("Please upload a valid CSV or XML file.");
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = await stream.ReadToEndAsync();
                BusinessCard businessCard;

                if (file.ContentType == "text/csv")
                {
                    // Process CSV and get a single BusinessCard
                    businessCard = await _fileUploadBusinessCardRepository.GetBusinessCardsFromCsvAsync(fileContent);
                }
                else
                {
                    // Process XML and get a single BusinessCard
                    businessCard = await _fileUploadBusinessCardRepository.GetBusinessCardsFromXmlAsync(fileContent);
                }

                // Validate the processed BusinessCard
                if (_fileUploadBusinessCardRepository.IsValidModel(businessCard))
                {
                    return businessCard;
                }
                else
                {
                    throw new InvalidDataException("The data does not match the expected model.");
                }
            }
        }
    }
}
