using Bogus;
using Company.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Data
{
    public class EmployeesCatalogueDbContext : DbContext
    {
        public EmployeesCatalogueDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }


    }
}
