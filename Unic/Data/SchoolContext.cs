using Microsoft.EntityFrameworkCore;
using Unic.Models;

namespace Unic.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
    }
}
