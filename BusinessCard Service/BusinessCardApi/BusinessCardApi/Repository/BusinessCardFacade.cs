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

        public async Task<List<BusinessCard>> ProcessFileAsync(IFormFile file)
        {
            if (file == null || (file.ContentType != "text/csv" && file.ContentType != "application/xml" && file.ContentType != "text/xml"))
            {
                throw new InvalidDataException("Please upload a valid CSV or XML file.");
            }

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = await stream.ReadToEndAsync();
                List<BusinessCard> data;

                if (file.ContentType == "text/csv")
                {
                    data = await _fileUploadBusinessCardRepository.GetBusinessCardsFromCsvAsync(fileContent);
                }
                else 
                {
                    data = await _fileUploadBusinessCardRepository.GetBusinessCardsFromXmlAsync(fileContent);
                }

                
                if (data.All(d => _fileUploadBusinessCardRepository.IsValidModel(d)))
                {
                    return data; 
                }
                else
                {
                    throw new InvalidDataException("The data does not match the expected model.");
                }
            }
        }
    }
}
