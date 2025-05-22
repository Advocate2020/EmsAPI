using EmployeeManagementSystemDB.Models;

namespace EmployeeManagementSystemDB.Databases.BaseData
{
    /// <summary>
    ///     The roles that users can have are stored here.
    ///     Never modify an existing role.
    ///
    /// This data is pre-populated in the database. <see cref="EmpDatabaseContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)"/>
    /// </summary>
    public static class RoleData
    {
        public static Role ADMIN => new()
        {
            Id = 1,
            Name = "admin",
        };

        public static Role USER => new()
        {
            Id = 2,
            Name = "user",
        };
    }
}