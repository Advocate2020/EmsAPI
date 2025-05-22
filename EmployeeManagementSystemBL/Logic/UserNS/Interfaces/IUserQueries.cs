using EmployeeManagementSystemDB.Databases;
using EmployeeManagementSystemDB.Models;

namespace WowBL.Logic.ProfileNS.Interfaces
{
    public interface IUserQueries
    {
        public IUserQueries New(EmpDatabaseContext tContext);

        IQueryable<User> GetUserByFirebaseId(string firebaseId);

        IQueryable<User> GetUserById(int userId);
    }
}