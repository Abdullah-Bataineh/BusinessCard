using BusinessCardApi.Model;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Xml.Serialization;

namespace BusinessCardApi.Repository
{
    public class FileUploadBusinessCardRepository : IFileUploadBusinessCardRepository
    {
        public async Task<List<BusinessCard>> GetBusinessCardsFromCsvAsync(string csvContent)
        {
            var lines = csvContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var businessCards = new List<BusinessCard>();
            var headers = lines[0].Split(',');
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var values = line.Split(',');
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
                businessCards.Add(businessCard);
            }

            return await Task.FromResult(businessCards);
        }

        public async Task<List<BusinessCard>> GetBusinessCardsFromXmlAsync(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(List<BusinessCard>), new XmlRootAttribute("BusinessCards"));

            using (var reader = new StringReader(xmlContent))
            {
                var businessCards = (List<BusinessCard>)serializer.Deserialize(reader);
                foreach (var businessCard in businessCards)
                {
                    foreach (var prop in typeof(BusinessCard).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            var value= (string)prop.GetValue(businessCard);
                            if (string.IsNullOrEmpty(value))
                            {
                                prop.SetValue(businessCard, null);
                            }
                        }
                    }
                }
                return await Task.FromResult(businessCards);
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
