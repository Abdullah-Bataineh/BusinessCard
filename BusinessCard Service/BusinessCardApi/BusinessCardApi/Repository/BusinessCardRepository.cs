using BusinessCardApi.AppDatabaseContext;
using BusinessCardApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardApi.Repository
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly AppDbContext _context;
        public BusinessCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateBusinessCard(BusinessCard businessCard)
        {
            await _context.BusinessCards.AddAsync(businessCard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusinessCard(int id)
        {
            var _BusinessCard=await _context.BusinessCards.FindAsync(id);
            if (_BusinessCard != null) { 
            _context.BusinessCards.Remove(_BusinessCard); 
              await  _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BusinessCard>> GetAllBusinessCard()
        {
           return await _context.BusinessCards.ToListAsync();
        }
    }
}
