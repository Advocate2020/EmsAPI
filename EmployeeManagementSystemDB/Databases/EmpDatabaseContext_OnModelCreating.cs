using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemDB.Databases
{
    public partial class EmpDatabaseContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}