using EmployeeManagementSystemBL.Extentions;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace WowBL.Logic.FirebaseNS
{
    public class FirebaseHandler : IFirebaseHandler
    {
        /// <summary>
        ///     Get the firebase id of the signed in user.
        /// </summary>
        public async Task<string> GetFirebaseIdAsync(HttpRequest request)
        {
            var token = await GetTokenAsync(request);

            return token.Uid;
        }

        /// <summary>
        /// Get the email stored inside the firebase token.
        /// </summary>

        public async Task<string> GetUserEmail(string userId)
        {
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(userId);

            return userRecord.Email;
        }

        /// <summary>
        ///     Set the claims that will be stored inside a user's JWT token.
        ///     Please note that setting the user claims overrides all previous custom claims.
        /// </summary>
        public async Task SetUserClaims(HttpRequest request, UserCustomClaims userClaims)
        {
            var token = await GetTokenAsync(request);

            var claims = new Dictionary<string, object>
            {
                // Add all roles that the user belongs to the the claims.
                { ClaimTypes.Role, userClaims.Role },

                // Add the user's database user id as a claim.
                { CustomClaimType.UserId, userClaims.UserId }
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(token.Uid, claims);
        }

        /// <summary>
        ///     Set the claims that will be stored inside a user's JWT token.
        ///     Please note that setting the user claims overrides all previous custom claims.
        /// </summary>
        public async Task SetAminUserClaims(string firebaseUserId, UserCustomClaims userClaims)
        {
            var claims = new Dictionary<string, object>
            {
                // Add all roles that the user belongs to the the claims.
                { ClaimTypes.Role, userClaims.Role },
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(firebaseUserId, claims);
        }

        /// <summary>
        ///     Set the claims that will be stored inside a user's JWT token.
        ///     Please note that setting the user claims overrides all previous custom claims.
        /// </summary>
        public async Task SetUserClaims(string firebaseId, UserCustomClaims userClaims)
        {
            var claims = new Dictionary<string, object>
            {
                // Add all roles that the user belongs to the the claims.
                { ClaimTypes.Role, userClaims.Role },

                // Add the user's database user id as a claim.
                { CustomClaimType.UserId, userClaims.UserId }
            };

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(firebaseId, claims);
        }

        /// <summary>
        ///     Returns the UserNumber stored in the JSON Web Token (JWT).
        ///     An exception will be thrown if the UserNumber could not be found.
        /// </summary>
        /// <exception cref="ClientError"></exception>
        public int GetUserId(ClaimsPrincipal user)
        {
            var claims = GetClaims(user);

            var userNumberClaim = claims?
                .Where(c => c.Type == CustomClaimType.UserId)
                .FirstOrDefault();

            if (userNumberClaim is null)
            {
                throw new Exception($"{nameof(CustomClaimType.UserId)} not found in token.");
            }

            if (int.TryParse(userNumberClaim.Value, out var userId))
            {
                return userId;
            }

            throw new UserClientError("User Id not found.");
        }

        private async Task<FirebaseToken> GetTokenAsync(HttpRequest request)
        {
            const string bearer = "Bearer ";

            // Get the token from the Authorization header.
            var value = request.Headers.Authorization.ToString();

            if (value is null)
            {
                throw new UserClientError("Bearer token was null.");
            }

            if (value.Length < bearer.Length)
            {
                throw new UserClientError("Invalid token.");
            }

            // Remove the "bearer " text.
            var idToken = value["Bearer ".Length..].Trim();

            var token = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

            return token;
        }

        private IEnumerable<Claim>? GetClaims(IPrincipal user)
        {
            var identity = (ClaimsIdentity?)user.Identity;

            return identity?.Claims;
        }
    }
}