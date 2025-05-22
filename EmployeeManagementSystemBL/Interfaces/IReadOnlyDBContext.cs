using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemBL.Interfaces
{
    public interface IReadOnlyDBContext<T> where T : DbContext
    {
        T DbContext { get; set; }
    }
}