using Microsoft.EntityFrameworkCore;

namespace BusinessCardApi.AppDbContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<DbContext> options):base(options) { }
    }

}
