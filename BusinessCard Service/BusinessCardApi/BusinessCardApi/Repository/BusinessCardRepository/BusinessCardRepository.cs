using BusinessCardApi.AppDatabaseContext;
using BusinessCardApi.Enum;
using BusinessCardApi.Exceptions;
using BusinessCardApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BusinessCardApi.Repository.BusinessCardRepository
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
            _context.BusinessCard.Remove(businessCard);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginationResult<BusinessCard>> GetAllBusinessCards(int page, int pageSize, string name = null, string email = null, string phone = null, string address = null, string gender = null, int? year = null, int? month = null, int? day = null)
        {
            var query = _context.BusinessCard.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(b => b.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(b => b.Phone.Contains(phone));
            }

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(b => b.Address.Contains(address));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(b => b.Gender == gender);
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.DOB.Year == year.Value);

                if (month.HasValue)
                {
                    query = query.Where(b => b.DOB.Month == month.Value);

                    if (day.HasValue)
                    {
                        query = query.Where(b => b.DOB.Day == day.Value);
                    }
                }
            }

            var totalRecords = await query.CountAsync();

            var businessCards = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<BusinessCard>
            {
                Data = businessCards,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords
            };
        }

        public async Task<BusinessCard> GetBusinessCardById(int id)
        {
            var businessCard = await _context.BusinessCard.FindAsync(id);
            return businessCard;
        }

        public async Task UpdateBusinessCard(BusinessCard businessCard)
        {
            var existingCard = await _context.BusinessCard.FindAsync(businessCard.Id);
            existingCard.Name = businessCard.Name;
            existingCard.Gender = businessCard.Gender;
            existingCard.DOB = businessCard.DOB;
            existingCard.Email = businessCard.Email;
            existingCard.Phone = businessCard.Phone;
            existingCard.Photo = businessCard.Photo;
            existingCard.Address = businessCard.Address;

            await _context.SaveChangesAsync();

        }

    }
}
