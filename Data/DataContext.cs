using Microsoft.EntityFrameworkCore;
using SkepERP.Models;

namespace SkepERP.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; }

        public DbSet<Phone> Phone { get; set; }

        public DbSet<PersonalRelation> PersonalRelations { get; set; }

        public DbSet<ErrorLog> ErrorLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalRelation>()
                .HasOne(pr => pr.Person)
                .WithMany(p => p.PersonalRelations)
                .HasForeignKey(pr => pr.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PersonalRelation>()
                .HasOne(pr => pr.RelatedPerson)
                .WithMany()
                .HasForeignKey(pr => pr.RelatedPersonId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
