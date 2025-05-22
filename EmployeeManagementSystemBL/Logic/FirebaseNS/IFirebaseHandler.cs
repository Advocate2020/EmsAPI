using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WowBL.Logic.FirebaseNS
{
    public interface IFirebaseHandler
    {
        Task<string> GetFirebaseIdAsync(HttpRequest request);
        Task<string> GetUserEmail(string userId);
        int GetUserId(ClaimsPrincipal user);
        Task SetAminUserClaims(string firebaseUserId, UserCustomClaims userClaims);
        Task SetUserClaims(HttpRequest request, UserCustomClaims userClaims);
        Task SetUserClaims(string firebaseId, UserCustomClaims userClaims);
    }
}