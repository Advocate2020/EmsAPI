using EmployeeManagementSystemBL.DTO_s.Post;
using Microsoft.AspNetCore.Http;

namespace WowBL.Logic.ProfileNS.Interfaces
{
    public interface IUserBL
    {
        Task AddProfile(AddEmployeeForm form, string firebaseId, HttpRequest request, string userEmail);
    }
}