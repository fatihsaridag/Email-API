using Microsoft.EntityFrameworkCore;

namespace EmailApp.Model
{
    public class EmailAppContext : DbContext
    {
        public EmailAppContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }



    }
}
