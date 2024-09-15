using Microsoft.EntityFrameworkCore;
using SkepERP.Models;

namespace SkepERP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<PersonalRelation> PersonalRelations { get; set; }
    }
}
