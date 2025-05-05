using EmployeeManagementSystemDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemDB.Databases
{
    public partial class EmpDatabaseContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}