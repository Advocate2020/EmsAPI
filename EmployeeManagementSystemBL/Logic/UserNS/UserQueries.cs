using EmployeeManagementSystemBL.Interfaces;
using EmployeeManagementSystemDB.Databases;
using EmployeeManagementSystemDB.Models;
using WowBL.Logic.ProfileNS.Interfaces;

namespace EmployeeManagementSystemBL.Logic.ProfileNS
{
    public class UserQueries : Queries<EmpDatabaseContext>, IUserQueries
    {
        public override required EmpDatabaseContext Context { get; set; }

        public UserQueries()
        {
        }

        public UserQueries(IReadOnlyDBContext<EmpDatabaseContext> readOnlyDBContext)
        {
            Context = readOnlyDBContext.DbContext;
        }

        public IUserQueries New(EmpDatabaseContext tContext)
        {
            return new UserQueries() { Context = tContext };
        }

        public IQueryable<User> GetUserById(int userId)
        {
            return Context.Users
                .Where(u => u.Id == userId);
        }

        public IQueryable<User> GetUserByFirebaseId(string firebaseId)
        {
            return Context.Users
                .Where(u => u.FirebaseId == firebaseId);
        }
    }
}