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
            await _context.BusinessCard.AddAsync(businessCard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusinessCard(int id)
        {
            var businessCard = await _context.BusinessCard.FindAsync(id);
            if (businessCard != null)
            {
                _context.BusinessCard.Remove(businessCard);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Business card with ID: {id} not found.");
            }
        }

        public async Task<IEnumerable<BusinessCard>> GetAllBusinessCard()
        {
           return await _context.BusinessCard.ToListAsync();
        }
    }
}
