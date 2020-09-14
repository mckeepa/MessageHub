using Microsoft.EntityFrameworkCore;

namespace coreWeb.Model
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Contacts { get; set; }
        public PersonDbContext(DbContextOptions<PersonDbContext> options) 
            : base(options)
        { }
    }
}