using EmployeeManagementSystemBL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemBL.Logic
{
    public abstract class Queries<T> where T : DbContext
    {
        public abstract required T Context { get; set; }

        public Queries()
        {
        }

        public Queries(IReadOnlyDBContext<T> readOnlyDBContext)
        {
            Context = readOnlyDBContext.DbContext;
        }
    }
}