using BusinessCardApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BusinessCardApi.AppDatabaseContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }   
        public DbSet<BusinessCard> BusinessCard { get; set; }
       

    }
}
