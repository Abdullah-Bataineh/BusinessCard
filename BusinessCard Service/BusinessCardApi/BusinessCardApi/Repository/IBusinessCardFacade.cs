using BusinessCardApi.Model;
using System.Threading.Tasks;

namespace BusinessCardApi.Repository
{
    public interface IBusinessCardFacade
    {
        Task<List<BusinessCard>> ProcessFileAsync(IFormFile file);
    }
}
