using BusinessCardApi.Model;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Xml.Serialization;

namespace BusinessCardApi.Repository
{
    public class FileUploadBusinessCardRepository : IFileUploadBusinessCardRepository
    {

        public async Task<BusinessCard> GetBusinessCardsFromCsvAsync(string csvContent)
        {
            var lines = csvContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            if (lines.Length < 2) // Ensure there's at least a header and one data row
            {
                return null; // Or throw an exception based on your preference
            }

            var headers = lines[0].Split(',');
            var values = lines[1].Split(','); // Read the first data line
            var businessCard = new BusinessCard();

            for (int j = 0; j < headers.Length; j++)
            {
                var propertyInfo = typeof(BusinessCard).GetProperty(headers[j].Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(businessCard, string.IsNullOrEmpty(values[j]) ? null : values[j]);
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        propertyInfo.SetValue(businessCard, DateTime.TryParse(values[j], out var date) ? date : default(DateTime));
                    }
                }
            }

            return await Task.FromResult(businessCard);
        }

        public async Task<BusinessCard> GetBusinessCardsFromXmlAsync(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(BusinessCard));

            using (var reader = new StringReader(xmlContent))
            {
                var businessCard = (BusinessCard)serializer.Deserialize(reader);

                // Post-process to replace empty strings with null
                foreach (var prop in typeof(BusinessCard).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var value = (string)prop.GetValue(businessCard);
                        if (string.IsNullOrEmpty(value))
                        {
                            prop.SetValue(businessCard, null);
                        }
                    }
                    else if (prop.PropertyType == typeof(DateOnly))
                    {
                        // You may need to handle DateOnly parsing here if the XML stores the date as a string
                        var dateValue = (string)prop.GetValue(businessCard);
                        if (DateOnly.TryParse(dateValue, out var date))
                        {
                            prop.SetValue(businessCard, date);
                        }
                        else
                        {
                            // Handle the case where the date format is incorrect
                            prop.SetValue(businessCard, default(DateOnly)); // or log an error
                        }
                    }
                }

                return await Task.FromResult(businessCard);
            }
        }

        public bool IsValidModel(BusinessCard businessCard)
        {
            return !string.IsNullOrWhiteSpace(businessCard.Name) &&
                 !string.IsNullOrWhiteSpace(businessCard.Email) &&
                 businessCard.DOB != default;
        }
    }
}
