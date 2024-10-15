using BusinessCardApi.Model;
using System.Threading.Tasks;

namespace BusinessCardApi.Repository
{
    public interface IBusinessCardFacade
    {
        Task<BusinessCard> ProcessFileAsync(IFormFile file);
    }
}
