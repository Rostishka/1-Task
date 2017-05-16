using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TEST;");
        }
    }
}
